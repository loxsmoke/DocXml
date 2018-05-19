using System;
using System.Collections.Generic;
using System.Text;
using DocXmlOtherLibForUnitTests;

namespace DocXmlUnitTests.TestData
{
    /// <inheritdoc cref="BaseClassForInheritdoc"/>
    public class ClassForInheritdocCref : BaseClassForInheritdoc
    {
        /// <inheritdoc cref="BaseClassForInheritdoc.Method"/>
        public override void Method()
        {
        }

        /// <inheritdoc cref="BaseClassForInheritdoc.Property"/>
        public override int Property { get; set; }

        /// <inheritdoc cref="OtherClass.OtherLibMethod"/>
        public void OtherLibMethod()
        {
        }
    }
}
