using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocXmlUnitTests.TestData
{
    /// <summary>
    /// My record description
    /// </summary>
    /// <param name="First">First field</param>
    /// <param name="Second">Second field</param>
    /// <param name="Value">Value field</param>
    public record struct MyRecordStruct(string First, string Second, int Value);
}
