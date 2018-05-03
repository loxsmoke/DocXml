using System;
using System.Collections.Generic;
using System.Text;

namespace DocXmlUnitTests.TestData
{
    /// <inheritdoc/>
    public class ClassForInheritdoc : BaseClassForInheritdoc
    {
        /// <inheritdoc cref="BaseClassForInheritdoc.Method"/>
        public override void Method()
        {
        }

        /// <inheritdoc/>
        public override int Property { get; set; }
    }
}
