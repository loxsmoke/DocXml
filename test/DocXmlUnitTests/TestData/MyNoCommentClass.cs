using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1591

namespace LoxSmoke.DocXmlUnitTests
{
    public class MyNoCommentClass
    {
        public void Method()
        { }

        public enum TestEnumNoComments
        {
            Value1,
            Value2
        }

        public string field;
    }
}
