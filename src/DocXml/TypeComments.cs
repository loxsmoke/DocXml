using System;
using System.Collections.Generic;
using System.Text;

namespace LoxSmoke.DocXml
{
    /// <summary>
    /// Class, Struct and delegate comments
    /// </summary>
    public class TypeComments : CommonComments
    {
        /// <summary>
        /// This list contains descriptions of delegate type parameters. 
        /// For non-delegate types this list is empty.
        /// For delegate types this list contains tuples where 
        /// Item1 is the "param" item "name" attribute and
        /// Item2 is the body of the comment
        /// </summary>
        public List<Tuple<string, string>> Parameters { get; set; } = new List<Tuple<string, string>>();

        /// <summary>
        /// Inheritdoc tag for the type. Null if missing in comments.
        /// </summary>
        public InheritdocTag Inheritdoc { get; set; }
    }
}
