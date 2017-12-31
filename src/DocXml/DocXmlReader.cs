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
        /// Summary, Description, Paramters (if present), Responses (if present), Returns
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public MethodComments GetMethodComments(MethodBase methodInfo)
        {
            var methodNode = GetXmlMemberNode(methodInfo.MethodId());
            var comments = new MethodComments();
            if (methodNode != null)
            {
                comments.Summary = GetSummaryComment(methodNode);
                comments.Description = GetRemarksComment(methodNode);
                comments.Parameters = GetParametersComments(methodNode);
                comments.Returns = GetReturnsComment(methodNode);
                comments.Responses = GetResponsesComments(methodNode);
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
                comments.Summary = GetSummaryComment(typeNode);
            }
            return comments;
        }

        /// <summary>
        /// Returns Summary comment for specified class member.
        /// </summary>
        /// <param name="mmemberInfo"></param>
        /// <returns></returns>
        public string GetMemberComment(MemberInfo memberInfo)
        {
            return GetSummaryComment(GetXmlMemberNode(memberInfo.MemberId()));
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

            var comments = new EnumComments()
            {
                Summary = GetTypeComments(enumType).Summary
            };

            bool valueCommentsExist = false;
            foreach (var enumName in enumType.GetEnumNames())
            {
                var typeNode = GetXmlMemberNode(enumType.EnumValueId(enumName));
                valueCommentsExist |= (typeNode != null);
                comments.ValueComments.Add(new Tuple<string, string>(enumName, GetSummaryComment(typeNode)));
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
        private const string ParamXPath = "param";
        private const string ResponsesXPath = "response";
        private const string ReturnsXPath = "returns";

        //  XML attribute names
        private const string NameAttribute = "name";
        private const string CodeAttribute = "code";
        #endregion

        #region XML helper functions
        private XPathNavigator GetXmlMemberNode(string name)
        {
            return navigator.SelectSingleNode(string.Format(MemberXPath, name));
        }

        private List<Tuple<string, string>> GetParametersComments(XPathNavigator rootNode)
        {
            var list = new List<Tuple<string, string>>();
            if (rootNode == null) return list;
            var paramNodes = rootNode.Select(ParamXPath);
            while (paramNodes.MoveNext())
            {
                var name = paramNodes.Current.GetAttribute(NameAttribute, "");
                list.Add(new Tuple<string, string>(name, paramNodes.Current.InnerXml ?? ""));
            }
            return list;
        }

        private string GetSummaryComment(XPathNavigator rootNode)
        {
            return rootNode?.SelectSingleNode(SummaryXPath)?.InnerXml ?? "";
        }
        private string GetRemarksComment(XPathNavigator rootNode)
        {
            return rootNode?.SelectSingleNode(RemarksXPath)?.InnerXml ?? "";
        }

        private string GetReturnsComment(XPathNavigator methodNode)
        {
            var responseNodes = methodNode?.Select(ReturnsXPath);
            if (responseNodes?.MoveNext() == true) return responseNodes.Current.InnerXml ?? "";
            return "";
        }

        private List<Tuple<string, string>> GetResponsesComments(XPathNavigator methodNode)
        {
            var list = new List<Tuple<string, string>>();
            var responseNodes = methodNode?.Select(ResponsesXPath);
            if (responseNodes != null)
            {
                while (responseNodes.MoveNext())
                {
                    var code = responseNodes.Current.GetAttribute(CodeAttribute, "");
                    list.Add(new Tuple<string, string>(code, responseNodes.Current.InnerXml ?? ""));
                }
            }
            return list;
        }
        #endregion
    }
}
