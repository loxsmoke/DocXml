using System;
using System.Collections.Generic;
using System.Text;

namespace DocXmlUnitTests
{
    /// <summary>
    /// BaseClass description
    /// </summary>
    class BaseClass
    {
        /// <summary>
        /// TemplateMethod3 description
        /// </summary>
        /// <typeparam name="X">Type parameter</typeparam>
        /// <typeparam name="Y">Type parameter</typeparam>
        /// <param name="parameter1">Parameter description</param>
        /// <param name="parameter2">Parameter description</param>
        /// <returns>Return value description</returns>
        public List<X> TemplateMethod3<X, Y>(List<X> parameter1, List<Y> parameter2)
        {
            return null;
        }
    }

    /// <summary>
    /// MySubClass description
    /// </summary>
    class MySubClass : BaseClass
    {
        /// <summary>
        /// Constructor comment
        /// </summary>
        public MySubClass()
        {
        }

        /// <summary>Method summary</summary>
        /// <remarks>Method remarks</remarks>
        /// <example>Method example</example>
        public void MethodWithComments()
        {
        }

        /// <summary>
        /// MethodWithInParam description
        /// </summary>
        /// <param name="paramIn"></param>
        /// <returns></returns>
        public int MethodWithInParam(in int paramIn)
        {
            return paramIn;
        }

        /// <summary>
        /// Summary line 1
        /// Summary line 2
        /// Summary line 3
        /// </summary>
        public void MultilineSummary()
        { }
    }
}
