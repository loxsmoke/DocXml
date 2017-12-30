using System;
using System.Collections.Generic;
using System.Text;

namespace LoxSmoke.DocXmlUnitTests
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
    /// MyClass3 description
    /// </summary>
    class MyClass3 : BaseClass
    {
    }
}
