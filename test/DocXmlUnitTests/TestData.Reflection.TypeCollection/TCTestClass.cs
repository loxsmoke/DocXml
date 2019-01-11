using System;
using System.Collections.Generic;

namespace DocXmlUnitTests.TestData.Reflection
{
    /// <summary>
    /// TestEnum1
    /// </summary>
    public enum ReflectionTestEnum1
    {
        One1,
        Two1
    }

    /// <summary>
    /// TestEnum2
    /// </summary>
    public enum ReflectionTestEnum2
    {
        One2,
        Two2
    }

    /// <summary>
    /// TCTestClass
    /// </summary>
    public class TCTestClass
    {
        /// <summary>
        /// StringProperty
        /// </summary>
        public string StringProperty { get; set; }

        /// <summary>
        /// ClassProperty
        /// </summary>
        public TCTestPropertyClass ClassProperty { get; set; }

        /// <summary>
        /// ClassListProperty
        /// </summary>
        public List<TCTestListPropertyClass> ClassListProperty { get; set; }
    }
}
