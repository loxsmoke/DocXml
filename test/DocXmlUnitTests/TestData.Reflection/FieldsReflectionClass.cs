using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0169
#pragma warning disable CS0649

namespace DocXmlUnitTests.TestData.Reflection
{
    /// <summary>
    /// Test data class with fields
    /// </summary>
    public class FieldsReflectionClass
    {
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

        /// <summary>
        /// TupleField
        /// </summary>
        public (int One, int Two) TupleField;

    }
}
