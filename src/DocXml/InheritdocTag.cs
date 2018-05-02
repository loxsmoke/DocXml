using System;
using System.Collections.Generic;
using System.Text;

namespace LoxSmoke.DocXml
{
    /// <summary>
    /// Inheritdoc tag with optional cref attribute.
    /// </summary>
    public class InheritdocTag
    {
        /// <summary>
        /// Cref attribute value. This value is optional.
        /// </summary>
        public string Cref { get; set; }
    }
}
