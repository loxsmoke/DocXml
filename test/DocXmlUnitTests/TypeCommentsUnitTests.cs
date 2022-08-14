using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using DocXmlOtherLibForUnitTests;
using DocXmlUnitTests.TestData;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class TypeCommentsUnitTests : BaseTestClass
    {

        [TestInitialize]
        public new void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void GetTypeComments()
        {
            var mm = Reader.GetTypeComments(MyClass_Type);
            Assert.AreEqual("This is MyClass", mm.Summary);
        }

        [TestMethod]
        public void GetTypeComments_OtherAssembly()
        {
            var mm = MultiAssemblyReader.GetTypeComments(typeof(OtherClass));
            Assert.AreEqual("Other class", mm.Summary);
        }

        [TestMethod]
        public void GetTypeComments_Empty()
        {
            var mm = Reader.GetTypeComments(typeof(MyNoCommentClass));
            Assert.IsNull(mm.Summary);
        }

        [TestMethod]
        public void GetTypeComments_XPathDocument()
        {
            var m = new DocXmlReader(new XPathDocument("DocXmlUnitTests.xml"));
            var mm = m.GetTypeComments(MyClass_Type);
            Assert.AreEqual("This is MyClass", mm.Summary);
        }

        [TestMethod]
        public void GetTypeComments_NestedClass()
        {
            var mm = Reader.GetTypeComments(typeof(MyClass.Nested));
            Assert.AreEqual("Nested class", mm.Summary);
        }

        [TestMethod]
        public void GetTypeComments_DelegateType()
        {
            var mm = Reader.GetTypeComments(MyClass_Type.GetNestedType(nameof(MyClass.DelegateType)));
            Assert.AreEqual("Delegate type description", mm.Summary);
            Assert.AreEqual(1, mm.Parameters.Count);
            AssertParam(mm, 0, "parameter", "Parameter description");
        }

        [TestMethod]
        public void GetTypeComments_Inheritdoc()
        {
            var comments = Reader.GetTypeComments(typeof(ClassForInheritdoc));
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual(string.Empty, comments.Inheritdoc.Cref);
            Assert.AreEqual("Base Inheritdoc", comments.Summary);
        }

        [TestMethod]
        public void GetTypeComments_InterfaceInheritdoc()
        {
            var comments = Reader.GetTypeComments(typeof(InterfaceImplInheritdoc));
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual(string.Empty, comments.Inheritdoc.Cref);
            Assert.AreEqual("Interface summary", comments.Summary);
        }
    }
}
