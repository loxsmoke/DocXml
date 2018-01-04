using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace LoxSmoke.DocXml
{
    public class DocXmlReader
    {
        private readonly XPathDocument document;
        private readonly XPathNavigator navigator;

        /// <summary>
        /// Open specified XML documentation file
        /// </summary>
        /// <param name="fileName">The name of the XML documentation file.</param>
        public DocXmlReader(string fileName)
        {
            document = new XPathDocument(fileName);
            navigator = document.CreateNavigator();
        }

        /// <summary>
        /// Create reader for specified xpath document
        /// </summary>
        /// <param name="xPathDocument">XML documentation</param>
        public DocXmlReader(XPathDocument xPathDocument)
        {
            document = xPathDocument ?? throw new ArgumentException(nameof(xPathDocument));
            navigator = document.CreateNavigator();
        }


        #region Public methods
        /// <summary>
        /// Returns the following comments for class method:
        /// Summary, Remarks, Paramters (if present), Responses (if present), Returns
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public MethodComments GetMethodComments(MethodBase methodInfo)
        {
            var methodNode = GetXmlMemberNode(methodInfo.MethodId());
            var comments = new MethodComments();
            if (methodNode != null)
            {
                GetCommonComments(comments, methodNode);
                comments.Parameters = GetParametersComments(methodNode);
                comments.TypeParameters = GetNamedComments(methodNode, TypeParamXPath, NameAttribute);
                comments.Returns = GetReturnsComment(methodNode);
                comments.Responses = GetNamedComments(methodNode, ResponsesXPath, CodeAttribute);
            }
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
            var typeNode = GetXmlMemberNode(type.TypeId());
            if (typeNode != null)
            {
                if (type.IsSubclassOf(typeof(Delegate)))
                {
                    comments.Parameters = GetParametersComments(typeNode);
                }
                GetCommonComments(comments, typeNode);
            }
            return comments;
        }

        /// <summary>
        /// Returns Summary comment for specified class member.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public string GetMemberComment(MemberInfo memberInfo)
        {
            return GetSummaryComment(GetXmlMemberNode(memberInfo.MemberId()));
        }

        /// <summary>
        /// Returns comments for specified class member.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public CommonComments GetMemberComments(MemberInfo memberInfo)
        {
            var comments = new CommonComments();
            var node = GetXmlMemberNode(memberInfo.MemberId());
            if (node != null) GetCommonComments(comments, node);
            return comments;
        }

        /// <summary>
        /// Get enum type description and comments for enum values if comment exists for 
        /// at least one enum value. 
        /// </summary>
        /// <param name="enumType">For non-enum types ArgumentException would be throws</param>
        /// <returns>EnumComment</returns>
        public EnumComments GetEnumComments(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException(nameof(enumType));

            var comments = new EnumComments();
            var typeNode = GetXmlMemberNode(enumType.TypeId());
            if (typeNode != null)
            {
                GetCommonComments(comments, typeNode);
            };

            bool valueCommentsExist = false;
            foreach (var enumName in enumType.GetEnumNames())
            {
                var valueNode = GetXmlMemberNode(enumType.EnumValueId(enumName));
                valueCommentsExist |= (valueNode != null);
                comments.ValueComments.Add(new Tuple<string, string>(enumName, GetSummaryComment(valueNode)));
            }
            if (!valueCommentsExist) comments.ValueComments.Clear();
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

        //  XML attribute names
        private const string NameAttribute = "name";
        private const string CodeAttribute = "code";
        #endregion

        #region XML helper functions

        private void GetCommonComments(CommonComments comments, XPathNavigator rootNode)
        {
            comments.Summary = GetSummaryComment(rootNode);
            comments.Remarks = GetRemarksComment(rootNode);
            comments.Example = GetExampleComment(rootNode);
        }

        private XPathNavigator GetXmlMemberNode(string name)
        {
            return navigator.SelectSingleNode(string.Format(MemberXPath, name));
        }

        private string GetSummaryComment(XPathNavigator rootNode)
        {
            return rootNode?.SelectSingleNode(SummaryXPath)?.InnerXml ?? "";
        }
        private string GetRemarksComment(XPathNavigator rootNode)
        {
            return rootNode?.SelectSingleNode(RemarksXPath)?.InnerXml ?? "";
        }

        private string GetExampleComment(XPathNavigator rootNode)
        {
            return rootNode?.SelectSingleNode(ExampleXPath)?.InnerXml ?? "";
        }

        private string GetReturnsComment(XPathNavigator methodNode)
        {
            var responseNodes = methodNode?.Select(ReturnsXPath);
            if (responseNodes?.MoveNext() == true) return responseNodes.Current.InnerXml ?? "";
            return "";
        }

        private List<Tuple<string, string>> GetParametersComments(XPathNavigator rootNode)
        {
            return GetNamedComments(rootNode, ParamXPath, NameAttribute);
        }

        private List<Tuple<string, string>> GetNamedComments(XPathNavigator rootNode, string path, string attributeName)
        {
            var list = new List<Tuple<string, string>>();
            var childNodes = rootNode?.Select(path);
            if (childNodes == null) return list;

            while (childNodes.MoveNext())
            {
                var code = childNodes.Current.GetAttribute(attributeName, "");
                list.Add(new Tuple<string, string>(code, childNodes.Current.InnerXml ?? ""));
            }
            return list;
        }

        #endregion
    }
}
