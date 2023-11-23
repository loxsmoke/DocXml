using System.Collections.Generic;

namespace LoxSmoke.DocXml
{
    /// <summary>
    /// Class, Struct or  delegate comments
    /// </summary>
    public class TypeComments : CommonComments
    {
        /// <summary>
        /// This list contains descriptions of delegate type parameters. 
        /// For non-delegate types this list is empty.
        /// For delegate types this list contains tuples where 
        /// Name is the "name" attribute of "param"
        /// Text is the body of the comment
        /// </summary>
        public List<(string Name, string Text)> Parameters { get; set; } = new List<(string Name, string Text)>();
        /// <summary>
        /// This list contains description of generic type parameter
        /// Name is the "name" attribute of "typeparam"
        /// Text is the body of the comment
        /// </summary>
        public List<(string Name, string Text)> TypeParameters { get; set; } = new List<(string Name, string Text)>();
    }
}
