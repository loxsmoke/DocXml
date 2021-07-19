using System;
using System.Collections.Generic;
using System.Numerics;
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
        /// Integer value of the enum if enum value fits in signed 32-bit integer.
        /// If value is too big (uint, long or ulong) then returned value is 0.
        /// </summary>
        /// <remarks>
        /// Use <see cref="BigValue"/> to get the value regardless of integer type.
        /// </remarks>
        public int Value { get; set; }

        /// <summary>
        /// True if enum value is too big to fit in int Value property. Use BigValue property instead.
        /// </summary>
        public bool IsBigValue { get; set; }

        /// <summary>
        /// The value of the enum. This field can handle any enum size.
        /// </summary>
        public BigInteger BigValue { get; set; }

        /// <summary>
        /// Debugging-friendly text.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{(Name??"")}={(IsBigValue ? BigValue.ToString() : Value.ToString())}" + (Summary != null ? $" {Summary}" : "");
        }
    }
}
