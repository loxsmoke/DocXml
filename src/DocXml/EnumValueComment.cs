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
        /// Integer value of the enum, if the enum is the default (int32) base type; otherwise, returns 0.
        /// </summary>
        /// <remarks>
        /// Use <see cref="ValueObject"/> to get the base integer value regardless of integer type.
        /// </remarks>
        public int Value { get; set; }

        /// <summary>
        /// Integer value of the enum, whether signed or unsigned, or 8, 16, 32, or 64 bits in length.
        /// </summary>
        public object ValueObject { get; set; }

        /// <summary>
        /// Debugging-friendly text.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{(Name??"")}={ValueObject??Value}" + (Summary != null ? $" {Summary}" : "");
        }
    }
}
