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
