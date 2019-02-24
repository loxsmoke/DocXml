using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BindingFlags = System.Reflection.BindingFlags;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class XmlDocIdUnitTests
    {
        [TestMethod]
        public void XmlDocId_MethodId_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.MethodId(null));
        }

        [TestMethod]
        public void XmlDocId_MemberId_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.MemberId(null));
        }

        [TestMethod]
        public void XmlDocId_PropertyId_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.PropertyId(null));
        }

        [TestMethod]
        public void XmlDocId_PropertyId_NonProperty()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.stringField)).First();
            Assert.ThrowsException<ArgumentException>(() => XmlDocId.PropertyId(info));
        }

        [TestMethod]
        public void XmlDocId_FieldId_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.FieldId(null));
        }

        [TestMethod]
        public void XmlDocId_FieldId_NonField()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.ValProperty)).First();
            Assert.ThrowsException<ArgumentException>(() => XmlDocId.FieldId(info));
        }

        [TestMethod]
        public void XmlDocId_EventId_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.EventId(null));
        }

        [TestMethod]
        public void XmlDocId_EventId_NonEvent()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.ValProperty)).First();
            Assert.ThrowsException<ArgumentException>(() => XmlDocId.EventId(info));
        }

        [TestMethod]
        public void XmlDocId_EnumValueIdId_TypeNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.EnumValueId(null, ""));
        }
        [TestMethod]
        public void XmlDocId_EnumValueIdId_ValueNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.EnumValueId(typeof(TestEnumWithValueComments), null));
        }
        [TestMethod]
        public void XmlDocId_EnumValueIdId_NonEnum()
        {
            Assert.ThrowsException<ArgumentException>(() => XmlDocId.EnumValueId(typeof(MyClass), "notnull"));
        }

        [TestMethod]
        public void XmlDocId_MemberId_Constructor()
        {
            var info = typeof(MyClass).GetConstructors().First();
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.#ctor", id);
        }

        [TestMethod]
        public void XmlDocId_MemberId_Method()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.MemberFunction)).First();
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.MemberFunction", id);
        }

        [TestMethod]
        public void XmlDocId_MemberId_Property()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.GetSetProperty)).First();
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.MyClass.GetSetProperty", id);
        }

        [TestMethod]
        public void XmlDocId_MemberId_Field()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.stringField)).First();
            var id = info.MemberId();
            Assert.AreEqual("F:DocXmlUnitTests.MyClass.stringField", id);
        }

        [TestMethod]
        public void XmlDocId_MemberId_NestedType()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.NestedClass)).First();
            var id = info.MemberId();
            Assert.AreEqual("T:DocXmlUnitTests.MyClass.NestedClass", id);
        }
        [TestMethod]
        public void XmlDocId_MemberId_Event()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.eventField)).First();
            var id = info.MemberId();
            Assert.AreEqual("E:DocXmlUnitTests.MyClass.eventField", id);
        }
        [TestMethod]
        public void XmlDocId_MemberId_Unsupported()
        {
            var info = typeof(MyClass);
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                XmlDocId.MemberId(info);
            });
        }
    }
}
