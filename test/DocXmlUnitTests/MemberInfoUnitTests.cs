using System;
using System.Linq;
using System.Reflection;
using System.Xml.XPath;
using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS1591

namespace LoxSmoke.DocXmlUnitTests
{
    [TestClass]
    public class MemberInfoUnitTests : BaseTestClass
    {
        public MemberInfo MyClass_stringField;
        public MemberInfo MyClass_PI;
        public MemberInfo MyClass_ValProperty;
        public MemberInfo MyClass_ImportantEnum;
        public MemberInfo MyClass_eventField;
        public MemberInfo MyClass_genericTypeField;

        public MemberInfo MyNoCommentClass_field;

        [TestInitialize]
        public new void Setup()
        {
            base.Setup();
            MyClass_stringField = MyClass_Type.GetMember(nameof(MyClass.stringField)).First();
            MyClass_PI = MyClass_Type.GetMember(nameof(MyClass.PI)).First();
            MyClass_ValProperty = MyClass_Type.GetMember(nameof(MyClass.ValProperty)).First();
            MyClass_ImportantEnum = MyClass_Type.GetMember(nameof(MyClass.ImportantEnum)).First();
            MyClass_eventField = MyClass_Type.GetMember(nameof(MyClass.eventField)).First();
            MyClass_genericTypeField = MyClass_Type.GetMember(nameof(MyClass.genericTypeField)).First();

            MyNoCommentClass_field = typeof(MyNoCommentClass).GetMember(nameof(MyNoCommentClass.field)).First();
        }

        [TestMethod]
        public void SimpleField_Summary()
        {
            var mm = Reader.GetMemberComment(MyClass_stringField);
            Assert.AreEqual("String field description", mm);
        }

        [TestMethod]
        public void SimpleField_NoComment()
        {
            var mm = Reader.GetMemberComments(MyNoCommentClass_field);
            Assert.IsNull(mm.Summary);
        }

        [TestMethod]
        public void ConstField_Summary()
        {
            var mm = Reader.GetMemberComment(MyClass_PI);
            Assert.AreEqual("Const field description", mm);
        }

        [TestMethod]
        public void ValueProperty_Summary()
        {
            var mm = Reader.GetMemberComment(MyClass_ValProperty);
            Assert.AreEqual("Value property description", mm);
        }

        [TestMethod]
        public void EnumProperty_Summary()
        {
            var mm = Reader.GetMemberComment(MyClass_ImportantEnum);
            Assert.AreEqual("Enum property description", mm);
        }

        [TestMethod]
        public void EventField_Summary()
        {
            var mm = Reader.GetMemberComment(MyClass_eventField);
            Assert.AreEqual("Event field description", mm);
        }

        [TestMethod]
        public void GenericField_Summary()
        {
            var mm = Reader.GetMemberComment(MyClass_genericTypeField);
            Assert.AreEqual("Generic field description", mm);
        }

    }
}
