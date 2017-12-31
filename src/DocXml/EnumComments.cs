using System;
using System.Collections.Generic;
using System.Text;

namespace LoxSmoke.DocXml
{
    /// <summary>
    /// Enum type comments
    /// </summary>
    public class EnumComments
    {
        /// <summary>
        /// "summary" comment of Enum
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// "summary" comments of enum values. List contains tuples where 
        /// Item1 is the enum value name and Item2 is the summary comment.
        /// If none of values have any summary comments then this list is empty.
        /// If at least one value has summary comment then this list contains 
        /// all enum values with empty comments for values without comments.
        /// </summary>
        public List<Tuple<string,string>> ValueComments { get; set; } = new List<Tuple<string, string>>();
    }
}
