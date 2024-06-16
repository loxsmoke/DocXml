using DocXmlOtherLibForUnitTests.TestData;
using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class EnumCommentsUnitTests : BaseTestClass
    {
        [TestInitialize]
        public new void Setup()
        {
            base.Setup();
        }

        void AssertEnumComment(int expectedValue, string expectedName, string expectedSummary, EnumValueComment comment)
        {
            Assert.AreEqual(expectedValue, comment.Value);
            Assert.AreEqual(expectedValue, comment.BigValue);
            Assert.AreEqual(expectedName, comment.Name);
            Assert.AreEqual(expectedSummary, comment.Summary);
        }

        void AssertBigEnumComment(BigInteger expectedValue, string expectedName, string expectedSummary, EnumValueComment comment)
        {
            Assert.IsTrue(comment.IsBigValue);
            Assert.AreEqual(expectedValue, comment.BigValue);
            Assert.AreEqual(0, comment.Value);
            Assert.AreEqual(expectedValue, comment.BigValue);
            Assert.AreEqual(expectedName, comment.Name);
            Assert.AreEqual(expectedSummary, comment.Summary);
        }

        [TestMethod]
        public void GetEnumComments_NotEnumType()
        {
            Assert.ThrowsException<ArgumentException>(() => Reader.GetEnumComments(typeof(MyClass)));
        }

        [TestMethod]
        public void GetEnumComments()
        {
            var comments = Reader.GetEnumComments(typeof(TestEnum2));

            Assert.AreEqual("Enum 2 type description", comments.Summary);
            Assert.AreEqual(0, comments.ValueComments.Count);
        }

        [TestMethod]
        public void GetEnumComments_NoComment()
        {
            var comments = Reader.GetEnumComments(typeof(MyNoCommentClass.TestEnumNoComments));

            Assert.IsNull(comments.Summary);
            Assert.AreEqual(0, comments.ValueComments.Count);
        }

        [TestMethod]
        public void GetEnumComments_Values_NoComments()
        {
            var comments = Reader.GetEnumComments(typeof(TestEnum2), true);

            Assert.AreEqual("Enum 2 type description", comments.Summary);
            Assert.AreEqual(2, comments.ValueComments.Count);
            AssertEnumComment(0, "Value21", string.Empty, comments.ValueComments[0]);
            AssertEnumComment(1, "Value22", string.Empty, comments.ValueComments[1]);
        }

        [TestMethod]
        public void EnumValueComments_ToString_Null()
        {
            var enumComment = new EnumValueComment();
            Assert.AreEqual("=0", enumComment.ToString());
        }

        [TestMethod]
        public void EnumValueComments_ToString()
        {
            var enumComment = new EnumValueComment()
            {
                Value = 5,
                Name = "name",
                Summary = "summary"
            };
            Assert.AreEqual("name=5 summary", enumComment.ToString());
        }

        [TestMethod]
        public void GetEnumComments_WithValues_Comments()
        {
            var comments = Reader.GetEnumComments(typeof(TestEnumWithValueComments));

            Assert.AreEqual("Enum type description", comments.Summary);
            Assert.AreEqual(2, comments.ValueComments.Count);
            AssertEnumComment(10, "Value1", "Enum value one", comments.ValueComments[0]);
            AssertEnumComment(20, "Value2", "Enum value two", comments.ValueComments[1]);
        }

        [TestMethod]
        public void GetEnumComments_WithValue_Comments_OtherAssembly()
        {
            var comments = new DocXmlReader().GetEnumComments(typeof(OtherEnum));

            Assert.AreEqual("Other enum", comments.Summary);
            Assert.AreEqual(1, comments.ValueComments.Count);
            AssertEnumComment(1, "Value1", "Enum value one", comments.ValueComments[0]);
        }

        [TestMethod]
        public void GetEnumComments_WithValues_UInt8()
        {
            var comments = Reader.GetEnumComments(typeof(TestEnumUInt8));

            Assert.AreEqual(2, comments.ValueComments.Count);
            AssertEnumComment(10, "Value1", "Enum value one", comments.ValueComments[0]);
            AssertEnumComment(20, "Value2", "Enum value two", comments.ValueComments[1]);
        }

        [TestMethod]
        public void GetEnumComments_WithValues_UInt64()
        {
            var comments = Reader.GetEnumComments(typeof(TestEnumUInt64));

            Assert.AreEqual(2, comments.ValueComments.Count);
            AssertBigEnumComment(new BigInteger(10L), "Value1", "Enum value one", comments.ValueComments[0]);
            AssertBigEnumComment(new BigInteger(20L), "Value2", "Enum value two", comments.ValueComments[1]);
        }

        [TestMethod]
        public void GetEnumComments_WithValues_Int64()
        {
            var comments = Reader.GetEnumComments(typeof(TestEnumInt64));

            Assert.AreEqual(2, comments.ValueComments.Count);
            AssertBigEnumComment(new BigInteger(10L), "Value1", "Enum value one", comments.ValueComments[0]);
            AssertBigEnumComment(new BigInteger(20L), "Value2", "Enum value two", comments.ValueComments[1]);
        }

        [TestMethod]
        public void GetEnumComments_WithNegativeValues()
        {
            var comments = Reader.GetEnumComments(typeof(TestEnumWithNegativeValues));

            Assert.AreEqual(2, comments.ValueComments.Count);
            AssertEnumComment(-20, "Value1", "Enum value one", comments.ValueComments[0]);
            AssertEnumComment(-10, "Value2", "Enum value two", comments.ValueComments[1]);
        }
    }
}
