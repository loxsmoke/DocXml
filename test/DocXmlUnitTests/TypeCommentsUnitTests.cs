using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

        [DataTestMethod]
        [DataRow(typeof(MyClass), "This is MyClass")]
        [DataRow(typeof(MyNoCommentClass), null)]
        [DataRow(typeof(MyClass.Nested), "Nested class")]
        [DataRow(typeof(MyClass.Nested.DoubleNested), "Double nested class")]
        [DataRow(typeof(MyClass.Nested.DoubleNested.TripleNested), "Triple nested class")]
        [DataRow(typeof(MyClass.Nested<>), "Nested generic class")]
        [DataRow(typeof(MyClass.Nested<>.DoubleNested<>), "Double nested generic class")]
        public void GetTypeComments(Type type, string expectedSummaryComment)
        {
            var mm = Reader.GetTypeComments(type);
            Assert.AreEqual(expectedSummaryComment, mm.Summary);
        }

        [TestMethod]
        public void GetTypeComments_OtherAssembly()
        {
            var mm = MultiAssemblyReader.GetTypeComments(typeof(OtherClass));
            Assert.AreEqual("Other class", mm.Summary);
        }


        [TestMethod]
        public void GetTypeComments_XPathDocument()
        {
            var m = new DocXmlReader(new XPathDocument("DocXmlUnitTests.xml"));
            var mm = m.GetTypeComments(typeof(MyClass));
            Assert.AreEqual("This is MyClass", mm.Summary);
        }


        [TestMethod]
        public void GetTypeComments_DelegateType()
        {
            var mm = Reader.GetTypeComments(MyClass_Type.GetNestedType(nameof(MyClass.DelegateType)));
            Assert.AreEqual("Delegate type description", mm.Summary);
            Assert.AreEqual(1, mm.Parameters.Count);
            AssertParam(mm, 0, "parameter", "Parameter description");
        }

        [DataTestMethod]
        [DataRow(typeof(ClassForInheritdoc), "Base Inheritdoc")]
        [DataRow(typeof(InterfaceImplInheritdoc), "Interface summary")]
        public void GetTypeComments_Inheritdoc(Type type, string expectedComment)
        {
            var comments = Reader.GetTypeComments(type);
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual(string.Empty, comments.Inheritdoc.Cref);
            Assert.AreEqual(expectedComment, comments.Summary);
        }

        [TestMethod]
        public void GetTypeComments_TypeParam()
        {
            var mm = Reader.GetTypeComments(typeof(ClassWithTypeParams<int, string>));
            Assert.AreEqual(2, mm.TypeParameters.Count);
            Assert.AreEqual("T1", mm.TypeParameters[0].Name);
            Assert.AreEqual("Type param1", mm.TypeParameters[0].Text);
            Assert.AreEqual("T2", mm.TypeParameters[1].Name);
            Assert.AreEqual("Type param2", mm.TypeParameters[1].Text);
        }
    }
}
