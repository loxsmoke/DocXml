using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#pragma warning disable CS1591

namespace LoxSmoke.DocXmlUnitTests
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
            var info = typeof(MyClass2).GetMember(nameof(MyClass2.stringField)).First();
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
            var info = typeof(MyClass2).GetMember(nameof(MyClass2.ValProperty)).First();
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
            var info = typeof(MyClass2).GetMember(nameof(MyClass2.ValProperty)).First();
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
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.EnumValueId(typeof(TestEnum), null));
        }
        [TestMethod]
        public void XmlDocId_EnumValueIdId_NonEnum()
        {
            Assert.ThrowsException<ArgumentException>(() => XmlDocId.EnumValueId(typeof(MyClass2), "notnull"));
        }
    }
}
