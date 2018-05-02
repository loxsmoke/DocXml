using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LoxSmoke.DocXml;
using LoxSmoke.DocXmlUnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocXmlUnitTests
{
    [TestClass]
    public class DocXmlReaderUnitTests
    {
        [TestMethod]
        public void DocXmlReader_AutoName_ClassComment()
        {
            var doc = new DocXmlReader((a) =>Path.GetFileNameWithoutExtension(a.Location) + ".xml");
            var mm = doc.GetTypeComments(typeof(MyClass));
            Assert.AreEqual("This is MyClass", mm.Summary);
        }
        [TestMethod]
        public void DocXmlReader_AutoName_UnknownAssembly_ClassComment()
        {
            var doc = new DocXmlReader((a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");
            var mm = doc.GetTypeComments(typeof(FileInfo));
            Assert.IsNull(mm.Summary);
            Assert.IsNull(mm.Remarks);
            Assert.IsNull(mm.Example);
        }
    }
}
