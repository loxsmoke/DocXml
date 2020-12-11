using DocXmlOtherLibForUnitTests.TestData;
using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class EnumCommentsUnitTests : BaseTestClass
    {
        public Type TestEnum2_Type;
        public Type TestEnumUInt8_Type;
        public Type TestEnumUInt64_Type;
        public Type TestEnumInt64_Type;
        public Type TestEnumWithNegativeValues_Type;
        public Type TestEnumWithValueComments_Type;
        public Type TestEnumWNoComments_Type;

        [TestInitialize]
        public new void Setup()
        {
            base.Setup();
            TestEnum2_Type = typeof(TestEnum2);
            TestEnumUInt8_Type = typeof(TestEnumUInt8);
            TestEnumUInt64_Type = typeof(TestEnumUInt64);
            TestEnumInt64_Type = typeof(TestEnumInt64);
            TestEnumWithNegativeValues_Type = typeof(TestEnumWithNegativeValues);
            TestEnumWithValueComments_Type = typeof(TestEnumWithValueComments);
            TestEnumWNoComments_Type = typeof(MyNoCommentClass.TestEnumNoComments);
        }

        void AssertEnumComment(int expectedValue, string expectedName, string expectedSummary, EnumValueComment comment)
        {
            Assert.AreEqual(expectedValue, comment.Value);
            Assert.AreEqual(expectedName, comment.Name);
            Assert.AreEqual(expectedSummary, comment.Summary);
        }

        [TestMethod]
        public void NotEnumType_Comment()
        {
            Assert.ThrowsException<ArgumentException>(() => Reader.GetEnumComments(MyClass_Type));
        }

        [TestMethod]
        public void EnumType_Comment()
        {
            var mm = Reader.GetEnumComments(TestEnum2_Type);
            Assert.AreEqual("Enum 2 type description", mm.Summary);
            Assert.AreEqual(0, mm.ValueComments.Count);
        }

        [TestMethod]
        public void EnumType_NoComment()
        {
            var mm = Reader.GetEnumComments(TestEnumWNoComments_Type);
            Assert.IsNull(mm.Summary);
            Assert.AreEqual(0, mm.ValueComments.Count);
        }

        [TestMethod]
        public void EnumType_Values_NoComments()
        {
            var mm = Reader.GetEnumComments(TestEnum2_Type, true);
            Assert.AreEqual("Enum 2 type description", mm.Summary);
            Assert.AreEqual(2, mm.ValueComments.Count);
            AssertEnumComment(0, "Value21", "", mm.ValueComments[0]);
            AssertEnumComment(1, "Value22", "", mm.ValueComments[1]);
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
        public void EnumType_WithValues_Comments()
        {
            var mm = Reader.GetEnumComments(TestEnumWithValueComments_Type);
            Assert.AreEqual("Enum type description", mm.Summary);
            Assert.AreEqual(2, mm.ValueComments.Count);
            AssertEnumComment(10, "Value1", "Enum value one", mm.ValueComments[0]);
            AssertEnumComment(20, "Value2", "Enum value two", mm.ValueComments[1]);
        }

        [TestMethod]
        public void EnumType_WithValue_Comments_OtherAssembly()
        {
            var mm = new DocXmlReader().GetEnumComments(typeof(OtherEnum));
            Assert.AreEqual("Other enum", mm.Summary);
            Assert.AreEqual(1, mm.ValueComments.Count);
            AssertEnumComment(1, "Value1", "Enum value one", mm.ValueComments[0]);
        }

        [TestMethod]
        public void EnumType_WithValues_UInt8()
        {
            var mm = Reader.GetEnumComments(TestEnumUInt8_Type);
            Assert.AreEqual(2, mm.ValueComments.Count);
            AssertEnumComment(0, "Value1", "Enum value one", mm.ValueComments[0]);
            AssertEnumComment(0, "Value2", "Enum value two", mm.ValueComments[1]);
            Assert.AreEqual((byte)10, mm.ValueComments[0].ValueObject);
            Assert.AreEqual((byte)20, mm.ValueComments[1].ValueObject);
        }

        [TestMethod]
        public void EnumType_WithValues_UInt64()
        {
            var mm = Reader.GetEnumComments(TestEnumUInt64_Type);
            Assert.AreEqual(2, mm.ValueComments.Count);
            AssertEnumComment(0, "Value1", "Enum value one", mm.ValueComments[0]);
            AssertEnumComment(0, "Value2", "Enum value two", mm.ValueComments[1]);
            Assert.AreEqual(10UL, mm.ValueComments[0].ValueObject);
            Assert.AreEqual(20UL, mm.ValueComments[1].ValueObject);
        }

        [TestMethod]
        public void EnumType_WithValues_Int64()
        {
            var mm = Reader.GetEnumComments(TestEnumInt64_Type);
            Assert.AreEqual(2, mm.ValueComments.Count);
            AssertEnumComment(0, "Value1", "Enum value one", mm.ValueComments[0]);
            AssertEnumComment(0, "Value2", "Enum value two", mm.ValueComments[1]);
            Assert.AreEqual(10L, mm.ValueComments[0].ValueObject);
            Assert.AreEqual(20L, mm.ValueComments[1].ValueObject);
        }

        [TestMethod]
        public void EnumType_WithNegativeValues()
        {
            var mm = Reader.GetEnumComments(TestEnumWithNegativeValues_Type);
            Assert.AreEqual(2, mm.ValueComments.Count);
            AssertEnumComment(-20, "Value1", "Enum value one", mm.ValueComments[0]);
            AssertEnumComment(-10, "Value2", "Enum value two", mm.ValueComments[1]);
        }
    }
}
