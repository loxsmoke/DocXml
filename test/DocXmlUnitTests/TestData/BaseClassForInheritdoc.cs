using System;
using System.Collections.Generic;
using System.Text;

namespace DocXmlUnitTests.TestData
{
    /// <summary>
    /// Base Inheritdoc
    /// </summary>
    public class BaseClassForInheritdoc
    {
        /// <summary>
        /// Constructor1
        /// </summary>
        protected BaseClassForInheritdoc()
        {
        }

        /// <summary>
        /// Constructor2
        /// </summary>
        /// <param name="x"></param>
        protected BaseClassForInheritdoc(int x)
        {
        }

        /// <summary>
        /// Method for Inheritdoc
        /// </summary>
        public virtual void Method()
        {
        }
        /// <summary>
        /// Property for Inheritdoc
        /// </summary>
        public virtual int Property { get; set; }
    }
}
