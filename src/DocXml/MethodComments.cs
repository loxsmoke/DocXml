using System;
using System.Collections.Generic;

namespace LoxSmoke.DocXml
{
    /// <summary>
    /// Method, operator and constructor comments
    /// </summary>
    public class MethodComments : CommonComments
    {
        /// <summary>
        /// "param" comments of the method. Each item in the list is the tuple where 
        /// "Name" is the parameter in XML file and 
        /// "Text" is the body of the comment.
        /// </summary>
        public List<(string Name, string Text)> Parameters { get; set; } = new();

        /// <summary>
        /// "returns" comment of the method.
        /// </summary>
        public string Returns { get; set; }

        /// <summary>
        /// "response" comments of the method. The list contains tuples where 
        /// "Code" is the response code
        /// "Text" is the body of the comment.
        /// </summary>
        public List<(string Code, string Text)> Responses { get; set; } = new();

        /// <summary>
        /// "typeparam" comments of the method. Each item in the list is the tuple where
        /// "Name" of the parameter in XML file and 
        /// "Text" is the body of the comment.
        /// </summary>
        public List<(string Name, string Text)> TypeParameters { get; set; } = new();

        /// <summary>
        /// "exception" comments of the method or property. Each item in the list is the tuple where
        /// "Cref" is the exception type
        /// "Text" is the description of the exception
        /// </summary>
        public List<(string Cref, string Text)> Exceptions { get; set; } = new();
    }
}
