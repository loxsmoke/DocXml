using System;
using System.Collections.Generic;
using System.Text;
using LoxSmoke.DocXml;

namespace LoxSmoke.DocXml
{
    /// <summary>
    /// Comment of one enum value
    /// </summary>
    public class EnumValueComment : CommonComments
    {
        /// <summary>
        /// The name of the enum value
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Integer value of the enum
        /// </summary>
        public int Value { get; set; }
    }
}
