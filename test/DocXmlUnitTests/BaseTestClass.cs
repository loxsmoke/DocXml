using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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
            Assert.AreEqual(name, comments.Parameters[paramIndex].Name);
            Assert.AreEqual(text, comments.Parameters[paramIndex].Text);
        }
        public void AssertParam(TypeComments comments, int paramIndex, string name, string text)
        {
            Assert.AreEqual(name, comments.Parameters[paramIndex].Name);
            Assert.AreEqual(text, comments.Parameters[paramIndex].Text);
        }

        public void AssertTypeParam(MethodComments comments, int paramIndex, string name, string text)
        {
            Assert.AreEqual(name, comments.TypeParameters[paramIndex].Name);
            Assert.AreEqual(text, comments.TypeParameters[paramIndex].Text);
        }
    }
}
