using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class MemberInfoUnitTests : BaseTestClass
    {
        [TestInitialize]
        public new void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void GetMemberComment_NullMemberInfo_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Reader.GetMemberComment(null));
        }

        [TestMethod]
        public void GetMemberComments_NullMemberInfo_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Reader.GetMemberComments(null));
        }

        [TestMethod]
        public void GetMemberComment_NoMember()
        {
            var memberInfo = typeof(MemberInfoUnitTests).GetMember(nameof(GetMemberComment_NoMember)).First();
            var commentText = Reader.GetMemberComment(memberInfo);
            
            Assert.IsTrue(string.IsNullOrEmpty(commentText));
        }

        [TestMethod]
        public void GetMemberComments_NoMember()
        {
            var memberInfo = typeof(MemberInfoUnitTests).GetMember(nameof(GetMemberComment_NoMember)).First();
            var comment = Reader.GetMemberComments(memberInfo);
            
            AssertEmpty(comment);
        }

        [DataTestMethod]
        [DataRow(nameof(MyClass.stringField), "String field description")]
        [DataRow(nameof(MyClass.noCommentField), "")]
        [DataRow(nameof(MyClass.PI), "Const field description")]
        [DataRow(nameof(MyClass.ValProperty), "Value property description")]
        [DataRow(nameof(MyClass.ImportantEnum), "Enum property description")]
        [DataRow(nameof(MyClass.eventField), "Event field description")]
        [DataRow(nameof(MyClass.genericTypeField), "Generic field description")]
        public void GetMemberComment(string memberName, string expectedComment)
        {
            var memberInfo = typeof(MyClass).GetMember(memberName).First();

            var comment = Reader.GetMemberComment(memberInfo);

            Assert.AreEqual(expectedComment, comment);
        }
    }
}
