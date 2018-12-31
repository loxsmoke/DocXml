using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.XPath;
using DocXmlOtherLibForUnitTests;
using DocXmlUnitTests.TestData;
using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

#pragma warning disable CS1591
namespace LoxSmoke.DocXmlUnitTests
{
    [TestClass]
    public class PropertyInfoUnitTests : BaseTestClass
    {
        public PropertyInfo MyClass_ItemProperty_GetOnly;
        public PropertyInfo MyClass_ItemProperty_GetSet;
        public PropertyInfo MyClass_GetSetProperty;

        [TestInitialize]
        public new void Setup()
        {
            base.Setup();
            MyClass_ItemProperty_GetOnly = MyClass_Type.GetProperties().FirstOrDefault(
                mt => mt.Name == "Item" && mt.GetMethod != null && mt.SetMethod == null);
            MyClass_ItemProperty_GetSet = MyClass_Type.GetProperties().FirstOrDefault(
                mt => mt.Name == "Item" && mt.GetMethod != null && mt.SetMethod != null);
            MyClass_GetSetProperty = MyClass_Type.GetProperties().FirstOrDefault(
                mt => mt.Name == nameof(MyClass.GetSetProperty));

        }

        [TestMethod]
        public void Property_Item_GetOnly()
        {
            var comments = Reader.GetMemberComments(MyClass_ItemProperty_GetOnly);
            Assert.AreEqual("Property description", comments.Summary);
        }

        [TestMethod]
        public void Property_Item_GetSet()
        {
            var comments = Reader.GetMemberComments(MyClass_ItemProperty_GetSet);
            Assert.AreEqual("ItemGetSetProperty description", comments.Summary);
        }

        [TestMethod]
        public void Property_GetSetAndCommon()
        {
            var comments = Reader.GetMemberComments(MyClass_GetSetProperty);
            Assert.AreEqual("GetSetProperty comment", comments.Summary);
        }
    }
}
