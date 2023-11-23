using System;
using System.Collections.Generic;
using System.Text;

namespace DocXmlUnitTests.TestData
{
    /// <summary>
    /// ClassWithTypeParams
    /// </summary>
    /// <typeparam name="T1">Type param1</typeparam>
    /// <typeparam name="T2">Type param2</typeparam>
    public class ClassWithTypeParams<T1, T2>
    {
        /// <summary>
        /// Nothing
        /// </summary>
        public T1 Type1 { get; set; }
        /// <summary>
        /// Nothing
        /// </summary>
        public T2 Type2 { get; set; }
    }
}
