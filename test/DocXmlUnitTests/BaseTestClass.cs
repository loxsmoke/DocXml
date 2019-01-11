using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    public class BaseTestClass
    {
        public DocXmlReader Reader { get; set; }
        public DocXmlReader MultiAssemblyReader { get; set; }
        public Type MyClass_Type;

        public void Setup()
        {
            Reader = new DocXmlReader("DocXmlUnitTests.xml");
            MultiAssemblyReader = new DocXmlReader((a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");
            MyClass_Type = typeof(MyClass);
        }

        public void AssertParam(MethodComments comments, int paramIndex, string name, string text)
        {
            Assert.AreEqual(name, comments.Parameters[paramIndex].Item1);
            Assert.AreEqual(text, comments.Parameters[paramIndex].Item2);
        }
        public void AssertParam(TypeComments comments, int paramIndex, string name, string text)
        {
            Assert.AreEqual(name, comments.Parameters[paramIndex].Item1);
            Assert.AreEqual(text, comments.Parameters[paramIndex].Item2);
        }

        public void AssertTypeParam(MethodComments comments, int paramIndex, string name, string text)
        {
            Assert.AreEqual(name, comments.TypeParameters[paramIndex].Item1);
            Assert.AreEqual(text, comments.TypeParameters[paramIndex].Item2);
        }

    }
}
