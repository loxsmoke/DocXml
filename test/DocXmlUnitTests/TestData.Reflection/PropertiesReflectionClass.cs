using System;
using System.Collections.Generic;
using System.Text;

namespace DocXmlUnitTests.TestData.Reflection
{
    /// <summary>
    /// Test data class with properties
    /// </summary>
    public class PropertiesReflectionClass
    {
        /// <summary>
        /// PublicGetProp
        /// </summary>
        public int PublicGetProp { get; }

        /// <summary>
        /// PublicSetProp
        /// </summary>
        public int PublicSetProp { set { } }

        /// <summary>
        /// PublicGetSetProp
        /// </summary>
        public int PublicGetSetProp { get; set; }

        /// <summary>
        /// ProtectedGetSetProp
        /// </summary>
        protected int ProtectedGetSetProp { get; set; }

        /// <summary>
        /// PrivateGetSetProp
        /// </summary>
        private int PrivateGetSetProp { get; set; }

        /// <summary>
        /// InternalGetSetProp
        /// </summary>
        internal int InternalGetSetProp { get; set; }

        /// <summary>
        /// PublicStaticGetSetProp
        /// </summary>
        public static int PublicStaticGetSetProp { get; set; }

        /// <summary>
        /// PublicMethod
        /// </summary>
        public void PublicMethod()
        {
        }

        /// <summary>
        /// ProtectedMethod
        /// </summary>
        protected void ProtectedMethod()
        {
        }

        /// <summary>
        /// PrivateMethod
        /// </summary>
        private void PrivateMethod()
        {
        }

        /// <summary>
        /// InternalMethod
        /// </summary>
        internal void InternalMethod()
        {
        }

        /// <summary>
        /// PublicStaticMethod
        /// </summary>
        public static void PublicStaticMethod()
        {
        }

        /// <summary>
        /// PublicField
        /// </summary>
        public int PublicField;

        /// <summary>
        /// ProtectedField
        /// </summary>
        protected int ProtectedField;

        /// <summary>
        /// PrivateField
        /// </summary>
        private int PrivateField;

        /// <summary>
        /// InternalField
        /// </summary>
        internal int InternalField;

        /// <summary>
        /// PublicStaticField
        /// </summary>
        public static int PublicStaticField;
    }
}
