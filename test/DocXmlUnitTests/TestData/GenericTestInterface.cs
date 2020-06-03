using System;
using System.Collections.Generic;
using System.Text;

namespace DocXmlUnitTests.TestData
{
    /// <summary>
    /// GenericTestInterface-summmary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface GenericTestInterface<T>
        where T : class, GenericTestInterface<T>, new()
    {
    }
}
