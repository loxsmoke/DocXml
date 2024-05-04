using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS1591
namespace DocXmlUnitTests
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
        public void GetMemberComments_GetOnly()
        {
            var comments = Reader.GetMemberComments(MyClass_ItemProperty_GetOnly);
            Assert.AreEqual("Property description", comments.Summary);
        }

        [TestMethod]
        public void GetMemberComments_GetSet()
        {
            var comments = Reader.GetMemberComments(MyClass_ItemProperty_GetSet);
            Assert.AreEqual("ItemGetSetProperty description", comments.Summary);
        }

        [TestMethod]
        public void GetMemberComments_GetSetAndCommon()
        {
            var comments = Reader.GetMemberComments(MyClass_GetSetProperty);
            Assert.AreEqual("GetSetProperty comment", comments.Summary);
        }
    }
}
