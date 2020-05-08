using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DocXmlOtherLibForUnitTests;
using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS1591

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

        [TestMethod]
        public void DocXmlReader_AutoName_OtherAssembly_ClassComment()
        {
            var doc = new DocXmlReader((a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");
            var mm = doc.GetTypeComments(typeof(OtherClass));
            Assert.IsNotNull(mm.Summary);
            Assert.IsNotNull(mm.Remarks);
            Assert.IsNotNull(mm.Example);
        }

        [TestMethod]
        public void DocXmlReader_AutoName_PreservesWhitespace()
        {
            var doc = new DocXmlReader((a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");
            var summary = doc.GetMemberComment(typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithParaTagsInSummary)));
            Assert.AreEqual(
@"<para>
First paragraph.
</para>
<para>
Second paragraph.
</para>",
                summary);
        }
    }
}
