using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace LoxSmoke.DocXml
{
    public class DocXmlReader
    {
        protected readonly XPathNavigator navigator;
        protected readonly Dictionary<Assembly, XPathNavigator> assemblyNavigators;
        protected readonly Func<Assembly, string> assemblyXmlPathFunction;

        /// <summary>
        /// True if comments from XML should have unnecessary leading spaces removed.
        /// By default XML comments are indented for human readability but it adds
        /// unnecessary leading spaces that are not present in source code.
        /// For example here is compiler genearated XML documentation with '-' 
        /// showing spaces for readability. 
        /// ----<summary>
        /// ----Text
        /// ----</summary>
        /// With UnIndentText set to true returned summary text would be "Text"
        /// With UnIndentText set to false returned summary text would be "\n----Text\n----" 
        /// </summary>
        public bool UnIndentText { get; set; } = true;

        /// <summary>
        /// Open specified XML documentation file
        /// </summary>
        /// <param name="fileName">The name of the XML documentation file.</param>
        /// <param name="unindentText">True if extra leading spaces should be removed from comments</param>
        public DocXmlReader(string fileName, bool unindentText = true)
        {
            var document = new XPathDocument(fileName);
            navigator = document.CreateNavigator();
            UnIndentText = unindentText;
        }

        /// <summary>
        /// Create reader for specified xpath document
        /// </summary>
        /// <param name="xPathDocument">XML documentation</param>
        /// <param name="unindentText">True if extra leading spaces should be removed from comments</param>
        public DocXmlReader(XPathDocument xPathDocument, bool unindentText = true)
        {
            var document = xPathDocument ?? throw new ArgumentException(nameof(xPathDocument));
            navigator = document.CreateNavigator();
            UnIndentText = unindentText;
        }

        /// <summary>
        /// Open XML documentation files based on assemblies of types. Comment file names 
        /// are generated based on assembly location.
        /// </summary>
        /// <param name="assemblyXmlPathFunction">Function that returns path to the assembly XML comment file.
        /// If function is null then comments file is assumed to have the same file name as assembly.
        /// If function returns null or if comments file does not exist then all comments for types from that 
        /// assembly would remain empty. </param>
        /// <param name="unindentText">True if extra leading spaces should be removed from comments</param>
        public DocXmlReader(Func<Assembly, string> assemblyXmlPathFunction = null, bool unindentText = true)
        {
            assemblyNavigators = new Dictionary<Assembly, XPathNavigator>();
            UnIndentText = unindentText;
            this.assemblyXmlPathFunction = assemblyXmlPathFunction;
        }

        /// <summary>
        /// Open XML documentation files based on assemblies of types. Comment file names 
        /// are generated based on assembly location.
        /// </summary>
        /// <param name="assemblies">The list of assemblies for XML documentation</param>
        /// <param name="assemblyXmlPathFunction">Function that returns path to the assembly XML comment file.
        /// If function is null then comments file is assumed to have the same file name as assembly.
        /// If function returns null or if comments file does not exist then all comments for types from that 
        /// assembly would remain empty. </param>
        /// <param name="unindentText">True if extra leading spaces should be removed from comments</param>
        public DocXmlReader(IEnumerable<Assembly> assemblies,
            Func<Assembly, string> assemblyXmlPathFunction = null, bool unindentText = true)
        {
            assemblyNavigators = new Dictionary<Assembly, XPathNavigator>();
            UnIndentText = unindentText;
            this.assemblyXmlPathFunction = assemblyXmlPathFunction;
            foreach (var assembly in assemblies)
            {
                GetNavigatorForAssembly(assembly);
            }
        }

        #region Public methods
        /// <summary>
        /// Returns the following comments for class method:
        /// Summary, Remarks, Parameters (if present), Responses (if present), Returns
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public MethodComments GetMethodComments(MethodBase methodInfo)
        {
            var comments = new MethodComments();
            var methodNode = GetXmlMemberNode(methodInfo.MethodId(), methodInfo?.ReflectedType);
            return GetComments(methodInfo, comments, methodNode);
        }

        protected MethodComments GetComments(MethodBase methodInfo, MethodComments comments, XPathNavigator node)
        {
            if (node == null) return comments;

            GetCommonComments(comments, node);
            comments.Parameters = GetParametersComments(node);
            comments.TypeParameters = GetNamedComments(node, TypeParamXPath, NameAttribute);
            comments.Returns = GetReturnsComment(node);
            comments.Responses = GetNamedComments(node, ResponsesXPath, CodeAttribute);
            comments = ResolveInheritdocComments(comments, methodInfo);
            return comments;
        }

        /// <summary>
        /// Return Summary comments for specified type.
        /// For Delegate types Paramters field may be returned as well.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>TypeComment</returns>
        public TypeComments GetTypeComments(Type type)
        {
            var comments = new TypeComments();
            var node = GetXmlMemberNode(type.TypeId(), type);
            return GetComments(type, comments, node);
        }

        protected TypeComments GetComments(Type type, TypeComments comments, XPathNavigator node)
        {
            if (node == null) return comments;
            if (type.IsSubclassOf(typeof(Delegate)))
            {
                comments.Parameters = GetParametersComments(node);
            }
            GetCommonComments(comments, node);
            comments = ResolveInheritdocComments(comments, type);
            return comments;
        }

        /// <summary>
        /// Returns Summary comment for specified class member.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public string GetMemberComment(MemberInfo memberInfo)
        {
            return GetSummaryComment(GetXmlMemberNode(memberInfo.MemberId(), memberInfo?.ReflectedType));
        }

        /// <summary>
        /// Returns comments for specified class member.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public CommonComments GetMemberComments(MemberInfo memberInfo)
        {
            var comments = new CommonComments();
            var node = GetXmlMemberNode(memberInfo.MemberId(), memberInfo?.ReflectedType);
            return GetComments(memberInfo, comments, node);
        }

        protected CommonComments GetComments(MemberInfo memberInfo, CommonComments comments, XPathNavigator node)
        {
            if (node == null) return comments;

            GetCommonComments(comments, node);
            comments = ResolveInheritdocComments(comments, memberInfo);
            return comments;
        }

        /// <summary>
        /// Get enum type description and comments for enum values. If <paramref name="fillValues"/>
        /// is false and no comments exist for any value then ValueComments list is empty.
        /// </summary>
        /// <param name="enumType">For non-enum types ArgumentException would be throws</param>
        /// <param name="fillValues">True if ValueComments list should be filled even if 
        /// non of the enum values have any summary comments</param>
        /// <returns>EnumComment</returns>
        public EnumComments GetEnumComments(Type enumType, bool fillValues = false)
        {
            if (!enumType.IsEnum) throw new ArgumentException(nameof(enumType));

            var comments = new EnumComments();
            var typeNode = GetXmlMemberNode(enumType.TypeId(), enumType?.ReflectedType);
            if (typeNode != null)
            {
                GetCommonComments(comments, typeNode);
            };

            bool valueCommentsExist = false;
            foreach (var enumName in enumType.GetEnumNames())
            {
                var valueNode = GetXmlMemberNode(enumType.EnumValueId(enumName), enumType?.ReflectedType);
                valueCommentsExist |= (valueNode != null);
                var valueComment = new EnumValueComment()
                {
                    Name = enumName,
                    Value = (int) Enum.Parse(enumType, enumName)
                };
                comments.ValueComments.Add(valueComment);
                GetCommonComments(valueComment, valueNode);
            }
            if (!valueCommentsExist && !fillValues) comments.ValueComments.Clear();
            return comments;
        }
        #endregion

        #region XML items and atribute names
        // XPath strings and XML attribute names
        private const string MemberXPath = "/doc/members/member[@name='{0}']";
        private const string SummaryXPath = "summary";
        private const string RemarksXPath = "remarks";
        private const string ExampleXPath = "example";
        private const string ParamXPath = "param";
        private const string TypeParamXPath = "typeparam";
        private const string ResponsesXPath = "response";
        private const string ReturnsXPath = "returns";
        private const string InheritdocXPath = "inheritdoc";

        //  XML attribute names
        private const string NameAttribute = "name";
        private const string CodeAttribute = "code";
        private const string CrefAttribute = "cref";
        #endregion

        #region XML helper functions

        private void GetCommonComments(CommonComments comments, XPathNavigator rootNode)
        {
            comments.Summary = GetSummaryComment(rootNode);
            comments.Remarks = GetRemarksComment(rootNode);
            comments.Example = GetExampleComment(rootNode);
            comments.Inheritdoc = GetInheritdocTag(rootNode);
        }

        private XPathNavigator GetXmlMemberNode(string name, Type typeForAssembly, bool searchAllCurrentFiles = false)
        {
            if (navigator != null)
            {
                return navigator.SelectSingleNode(string.Format(MemberXPath, name));
            }

            var node = GetXmlMemberNodeFromDictionary(name, typeForAssembly);
            if (node != null ||
                !searchAllCurrentFiles ||
                assemblyNavigators.Count <= 1 && typeForAssembly != null) return node;
            foreach (var docNavigator in assemblyNavigators.Values)
            {
                node = docNavigator.SelectSingleNode(string.Format(MemberXPath, name));
                if (node != null) break;
            }
            return node;
        }

        private XPathNavigator GetXmlMemberNodeFromDictionary(string name, Type typeForAssembly)
        {
            var typeNavigator = GetNavigatorForAssembly(typeForAssembly?.Assembly);
            return typeNavigator?.SelectSingleNode(string.Format(MemberXPath, name));
        }

        private XPathNavigator GetNavigatorForAssembly(Assembly assembly)
        {
            if (assembly == null) return null;
            if (assemblyNavigators.TryGetValue(assembly, out var typeNavigator))
            {
                return typeNavigator;
            }

            var commentFileName = assemblyXmlPathFunction == null
                ? Path.ChangeExtension(assembly.Location, ".xml")
                : assemblyXmlPathFunction(assembly);
            if (commentFileName == null || !File.Exists(commentFileName))
            {
                assemblyNavigators.Add(assembly, null);
                return null;
            }
            var document = new XPathDocument(commentFileName);
            var docNavigator = document.CreateNavigator();
            assemblyNavigators.Add(assembly, docNavigator);
            return docNavigator;
        }

        private string GetXmlText(XPathNavigator node)
        {
            var innerText = node?.InnerXml ?? "";
            if (!UnIndentText || string.IsNullOrEmpty(innerText)) return innerText;

            var outerText = node?.OuterXml ?? "";
            var indentText = FindIndent(outerText);
            if (string.IsNullOrEmpty(indentText)) return innerText;
            return innerText.Replace(indentText, indentText[0].ToString()).Trim('\r', '\n');
        }

        private string FindIndent(string outerText)
        {
            if (string.IsNullOrEmpty(outerText)) return "";
            var end = outerText.LastIndexOf("</");
            if (end < 0) return "";
            var start = end - 1;
            for (; start >= 0 && outerText[start] != '\r' && outerText[start] != '\n'; start--) ;
            if (start < 0 || end <= start) return "";
            return outerText.Substring(start, end - start);
        }


        private string GetSummaryComment(XPathNavigator rootNode)
        {
            return GetXmlText(rootNode?.SelectSingleNode(SummaryXPath));
        }
        private string GetRemarksComment(XPathNavigator rootNode)
        {
            return GetXmlText(rootNode?.SelectSingleNode(RemarksXPath));
        }

        private string GetExampleComment(XPathNavigator rootNode)
        {
            return GetXmlText(rootNode?.SelectSingleNode(ExampleXPath));
        }

        private string GetReturnsComment(XPathNavigator methodNode)
        {
            var responseNodes = methodNode?.Select(ReturnsXPath);
            if (responseNodes?.MoveNext() == true)
                return GetXmlText(responseNodes.Current);
            return "";
        }

        private List<(string Name, string Text)> GetParametersComments(XPathNavigator rootNode)
        {
            return GetNamedComments(rootNode, ParamXPath, NameAttribute);
        }

        private List<(string Name, string Text)> GetNamedComments(XPathNavigator rootNode, string path, string attributeName)
        {
            var list = new List<(string Name, string Text)>();
            var childNodes = rootNode?.Select(path);
            if (childNodes == null) return list;

            while (childNodes.MoveNext())
            {
                var code = childNodes.Current.GetAttribute(attributeName, "");
                list.Add((code, GetXmlText(childNodes.Current)));
            }
            return list;
        }

        private InheritdocTag GetInheritdocTag(XPathNavigator rootNode)
        {
            if (rootNode == null) return null;
            var inheritdoc = GetNamedComments(rootNode, InheritdocXPath, CrefAttribute);
            if (inheritdoc.Count == 0) return null;
            return new InheritdocTag() {Cref = inheritdoc.First().Item1};
        }

        private bool NeedsResolving(CommonComments comments)
        {
            return comments?.Inheritdoc != null &&
                   string.IsNullOrEmpty(comments.Summary) &&
                   string.IsNullOrEmpty(comments.Remarks) &&
                   string.IsNullOrEmpty(comments.Example);
        }

        private bool GetCrefComments(string cref, Type typeForAssembly, CommonComments comments, 
            Action<XPathNavigator> getCommentAction) 
        {
            if (string.IsNullOrEmpty(cref)) return false;
            var typeNode = GetXmlMemberNode(cref, typeForAssembly, true);
            var inheritdoc = comments.Inheritdoc;
            comments.Inheritdoc = null;
            getCommentAction(typeNode);
            comments.Inheritdoc = inheritdoc;
            return true;
        }

        private MethodComments ResolveInheritdocComments(MethodComments comments, MethodBase methodInfo)
        {
            if (!NeedsResolving(comments) ||
                comments?.Parameters?.Count > 0 ||
                !string.IsNullOrEmpty(comments?.Returns) ||
                comments?.Responses?.Count > 0 ||
                comments?.TypeParameters?.Count > 0) return comments;

            // If an explicit cref attribute is specified, the documentation from 
            // the specified namespace/type/member is inherited.
            if (GetCrefComments(comments.Inheritdoc.Cref, methodInfo.ReflectedType, comments, 
                (node) => GetComments(methodInfo, comments, node))) return comments;

            // For constructors:
            // - Search backwards up the type inheritance chain for a constructor 
            //   with a matching signature.
            // - If a match is found, its documentation is inherited.
            if (methodInfo.IsConstructor)
            {
                var baseClass = methodInfo.ReflectedType.BaseType;
                var constructorSignature = methodInfo.GetParameters()
                    .Select(p => p.ParameterType).ToArray();
                while (baseClass != null)
                {
                    var baseConstructor = baseClass.GetConstructor(
                        BindingFlags.Public| BindingFlags.NonPublic| BindingFlags.Instance,
                        null, CallingConventions.Any, constructorSignature, null);
                    if (baseConstructor != null)
                    {
                        var newComments = GetMethodComments(baseConstructor);
                        if (newComments == null) return comments;
                        newComments.Inheritdoc = comments.Inheritdoc;
                        return newComments;
                    }
                    baseClass = baseClass.BaseType;
                }
                return comments;
            }

            // For virtual members and interface implementations:
            // - If the member is an override, documentation is inherited from the 
            //   member it overrides.
            // - If the member is part of an interface, documentation is inherited 
            //   from the interface member being implemented.
            var interfaceDeclaration = methodInfo.ReflectedType
                .GetTypeInfo()
                .ImplementedInterfaces
                .Select(ii => methodInfo.ReflectedType.GetInterfaceMap(ii))
                .SelectMany(map => Enumerable.Range(0, map.TargetMethods.Length)
                    .Where(n => map.TargetMethods[n] == methodInfo)
                    .Select(n => map.InterfaceMethods[n]))
                .FirstOrDefault();
            if (interfaceDeclaration != null)
            {
                var newComments = GetMethodComments(interfaceDeclaration);
                if (newComments == null) return comments;
                newComments.Inheritdoc = comments.Inheritdoc;
                return newComments;
            }

            if (methodInfo.IsVirtual)
            {
                var baseMethod = (methodInfo as MethodInfo)?.GetBaseDefinition();
                if (baseMethod != null)
                {
                    var newComments = GetMethodComments(baseMethod);
                    if (newComments == null) return comments;
                    newComments.Inheritdoc = comments.Inheritdoc;
                    return newComments;
                }
                return comments;
            }


            return comments;
        }

        private TypeComments ResolveInheritdocComments(TypeComments comments, Type type)
        {
            if (!NeedsResolving(comments) ||
                comments?.Parameters?.Count > 0) return comments;

            // If an explicit cref attribute is specified, the documentation from
            // the specified namespace/type/member is inherited. 
            if (GetCrefComments(comments.Inheritdoc.Cref, type, comments,
                (node) => GetComments(type, comments, node))) return comments;

            // For types and interfaces:
            // - Inherit documentation from all base classes working backwards up 
            //   the inheritance chain.
            // - Inherit documentation from all interface implementations (if any) 
            //   working through them in the order listed in the reflection information file (usually alphabetically).
            if (type.BaseType != null && type.BaseType != typeof(object))
            {
                var newComments = GetTypeComments(type.BaseType);
                if (newComments != null)
                {
                    newComments.Parameters = comments.Parameters;
                    newComments.Inheritdoc = comments.Inheritdoc;
                    return newComments;
                }
            }

            var interfaces = type.GetInterfaces();
            if (interfaces?.Length > 0)
            {
                foreach (var intf in interfaces.OrderBy(i => i.FullName))
                {
                    var newComments = GetTypeComments(intf);
                    if (newComments != null)
                    {
                        newComments.Parameters = comments.Parameters;
                        newComments.Inheritdoc = comments.Inheritdoc;
                        return newComments;
                    }
                }
            }

            return comments;
        }

        private CommonComments ResolveInheritdocComments(CommonComments comments, MemberInfo memberInfo)
        {
            if (!NeedsResolving(comments)) return comments;
            // If an explicit cref attribute is specified, the documentation from 
            // the specified namespace/type/member is inherited. 
            GetCrefComments(comments.Inheritdoc.Cref, memberInfo.DeclaringType, comments,
                (node) => GetComments(memberInfo, comments, node));
            return comments;
        }
        #endregion
    }
}
