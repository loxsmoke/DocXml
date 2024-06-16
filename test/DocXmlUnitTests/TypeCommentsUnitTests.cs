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
            var comments = Reader.GetTypeComments(type);
            Assert.AreEqual(expectedSummaryComment, comments.Summary);
        }

        [TestMethod]
        public void GetTypeComments_OtherAssembly()
        {
            var comments = MultiAssemblyReader.GetTypeComments(typeof(OtherClass));
            Assert.AreEqual("Other class", comments.Summary);
        }


        [TestMethod]
        public void GetTypeComments_XPathDocument()
        {
            var reader = new DocXmlReader(new XPathDocument("DocXmlUnitTests.xml"));
            
            var comments = reader.GetTypeComments(typeof(MyClass));
            
            Assert.AreEqual("This is MyClass", comments.Summary);
        }


        [TestMethod]
        public void GetTypeComments_DelegateType()
        {
            var comments = Reader.GetTypeComments(typeof(MyClass).GetNestedType(nameof(MyClass.DelegateType)));

            Assert.AreEqual("Delegate type description", comments.Summary);
            Assert.AreEqual(1, comments.Parameters.Count);
            AssertParam(comments, 0, "parameter", "Parameter description");
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
            var comments = Reader.GetTypeComments(typeof(ClassWithTypeParams<int, string>));

            Assert.AreEqual(2, comments.TypeParameters.Count);
            Assert.AreEqual("T1", comments.TypeParameters[0].Name);
            Assert.AreEqual("Type param1", comments.TypeParameters[0].Text);
            Assert.AreEqual("T2", comments.TypeParameters[1].Name);
            Assert.AreEqual("Type param2", comments.TypeParameters[1].Text);
        }
    }
}
