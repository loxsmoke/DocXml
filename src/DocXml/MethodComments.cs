using System;
using System.Collections.Generic;
using System.Text;

namespace LoxSmoke.DocXml
{
    /// <summary>
    /// Method, operator and constructor comments
    /// </summary>
    public class MethodComments : CommonComments
    {
        /// <summary>
        /// "param" comments of the method. Each item in the list is the tuple
        /// where Item1 is the "name" of the parameter in XML file and 
        /// Item2 is the body of the comment.
        /// </summary>
        public List<Tuple<string,string>> Parameters { get; set; } = new List<Tuple<string,string>>();

        /// <summary>
        /// "returns" comment of the method.
        /// </summary>
        public string Returns { get; set; }

        /// <summary>
        /// "response" comments of the method. The list contains tuples where 
        /// Item1 is the "code" of the response and
        /// Item1 is the body of the comment.
        /// </summary>
        public List<Tuple<string, string>> Responses { get; set; } = new List<Tuple<string, string>>();

        /// <summary>
        /// "typeparam" comments of the method. Each item in the list is the tuple
        /// where Item1 is the "name" of the parameter in XML file and 
        /// Item2 is the body of the comment.
        /// </summary>
        public List<Tuple<string, string>> TypeParameters { get; set; } = new List<Tuple<string, string>>();

        /// <summary>
        /// Inheritdoc tag for the method. Null if missing in comments.
        /// </summary>
        public InheritdocTag Inheritdoc { get; set; }
    }
}
