using System;
using System.Collections.Generic;

namespace DocXmlUnitTests.TestData.Reflection
{
    /// <summary>
    /// TCTestPropertyClass
    /// </summary>
    public class TCTestPropertyClass
    {
        /// <summary>
        /// Enum1Property
        /// </summary>
        public ReflectionTestEnum1 Enum1Property { get; set; }

        /// <summary>
        /// Enum2Property
        /// </summary>
        public ReflectionTestEnum2? Enum2Property { get; set; }

        /// <summary>
        /// DateTimeProperty1
        /// </summary>
        public DateTime DateTimeProperty1 { get; set; }

        /// <summary>
        /// DateTimeProperty2
        /// </summary>
        public DateTime? DateTimeProperty2 { get; set; }

        /// <summary>
        /// BoolProperty1
        /// </summary>
        public bool BoolProperty1 { get; set; }

        /// <summary>
        /// BoolProperty2
        /// </summary>
        public bool? BoolProperty2 { get; set; }

        /// <summary>
        /// IntListProperty
        /// </summary>
        public List<int> IntListProperty { get; set; }

        /// <summary>
        /// DoubleListProperty
        /// </summary>
        public List<double> DoubleListProperty { get; set; }

        /// <summary>
        /// GetBoolProperty
        /// </summary>
        public bool GetBoolProperty => true;

        /// <summary>
        /// IntFunction
        /// </summary>
        /// <param name="param1"></param>
        /// <returns></returns>
        public int IntFunction(int param1) => 1;

        /// <summary>
        /// TupleProperty
        /// </summary>
        public (int One, int Two) TupleProperty { get; set; }
    }
}
