using System;
using System.Collections.Generic;

#pragma warning disable CS1591

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

        /// <summary>
        /// ClassMethod
        /// </summary>
        /// <param name="parameter">TCTestParameterClass parameter</param>
        public void ClassMethod(TCTestParameterClass parameter) { }

        /// <summary>
        /// RefClassMethod
        /// </summary>
        /// <param name="parameter">TCTestParameterClass ref parameter</param>
        public void RefClassMethod(ref TCTestParameterClass parameter) { }
    }
}
