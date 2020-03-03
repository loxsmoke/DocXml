using DocXmlOtherLibForUnitTests.TestData;
using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class EnumCommentsUnitTests : BaseTestClass
    {
        public Type TestEnum2_Type;
        public Type TestEnumWithValueComments_Type;
        public Type TestEnumWNoComments_Type;

        [TestInitialize]
        public new void Setup()
        {
            base.Setup();
            TestEnum2_Type = typeof(TestEnum2);
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
            var docReader = new DocXmlReader(
                new[] { typeof(MyClass).Assembly, typeof(OtherEnum).Assembly },
                (a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");

            var mm = docReader.GetEnumComments(typeof(OtherEnum));

            Assert.AreEqual("Other enum", mm.Summary);
            Assert.AreEqual(1, mm.ValueComments.Count);
            AssertEnumComment(1, "Value1", "Enum value one", mm.ValueComments[0]);
        }
    }
}
