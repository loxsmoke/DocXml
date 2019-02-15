using System;
using System.Collections.Generic;
using System.Text;

namespace DocXmlUnitTests.TestData
{
    /// <inheritdoc/>
    public class ClassForInheritdoc : BaseClassForInheritdoc, InterfaceForInheritdoc
    {
        /// <inheritdoc/>
        public ClassForInheritdoc(int i) : base(i)
        {
        }

        /// <inheritdoc/>
        public override void Method()
        {
        }

        /// <inheritdoc/>
        public override int Property { get; set; }

        /// <inheritdoc/>
        public void InterfaceMethod()
        {
        }
    }
}
