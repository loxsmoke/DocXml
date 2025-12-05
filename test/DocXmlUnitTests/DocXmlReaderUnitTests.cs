using DocXmlOtherLibForUnitTests;
using DocXmlUnitTests.TestData;
using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml.XPath;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class DocXmlReaderUnitTests : BaseTestClass
    {
        [TestMethod]
        public void Constructor_NullPathProvider_Throws()
        {
            XPathDocument xPathDocument = null!;
            Assert.ThrowsException<ArgumentException>(() => new DocXmlReader(xPathDocument));
        }

        [TestMethod]
        public void GetTypeComments()
        {
            var doc = new DocXmlReader((a) =>Path.GetFileNameWithoutExtension(a.Location) + ".xml");
            var mm = doc.GetTypeComments(typeof(MyClass));
            Assert.AreEqual("This is MyClass", mm.Summary);
        }

        [TestMethod]
        public void GetRecordComments()
        {
            var doc = new DocXmlReader((a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");
            var mm = doc.GetTypeComments(typeof(MyRecord));
            Assert.AreEqual("My record description", mm.Summary);
        }

        [TestMethod]
        public void GetTypeComments_UnknownAssembly()
        {
            var doc = new DocXmlReader((a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");
            var comments = doc.GetTypeComments(typeof(FileInfo));

            base.AssertEmpty(comments);
        }

        [TestMethod]
        public void GetTypeComments_OtherAssembly()
        {
            var doc = new DocXmlReader((a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");
            var mm = doc.GetTypeComments(typeof(OtherClass));
            Assert.IsNotNull(mm.Summary);
            Assert.IsNotNull(mm.Remarks);
            Assert.IsNotNull(mm.Example);
        }

        [TestMethod]
        public void GetMemberComment_PreservesWhitespace()
        {
            var doc = new DocXmlReader((a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");
            var summary = doc.GetMemberComment(typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithParaTagsInSummary)));
            Assert.AreEqual(
@"<para>
First paragraph.
</para>
<para>
Second paragraph.
</para>".ReplaceLineEndings(),
                summary.ReplaceLineEndings());
        }

        [TestMethod]
        public void GetTypeComments_FullCommentText()
        {
            var doc = new DocXmlReader((a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");
            var mm = doc.GetTypeComments(typeof(OtherClass));
            Assert.IsNotNull(mm.Summary);
            Assert.IsNotNull(mm.Remarks);
            Assert.IsNotNull(mm.Example);
            Assert.IsNotNull(mm.FullCommentText);
        }
    }
}
