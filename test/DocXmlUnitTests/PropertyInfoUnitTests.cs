using System.Linq;
using DocXmlUnitTests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS1591
namespace DocXmlUnitTests
{
    [TestClass]
    public class PropertyInfoUnitTests : BaseTestClass
    {
        [TestInitialize]
        public new void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void GetMemberComments_GetOnly()
        {
            var itemGetProperty = typeof(MyClass).GetProperties().FirstOrDefault(
                mt => mt.Name == "Item" && mt.GetMethod != null && mt.SetMethod == null);

            var comments = Reader.GetMemberComments(itemGetProperty);

            Assert.AreEqual("Property description", comments.Summary);
        }

        [TestMethod]
        public void GetMemberComments_GetSet()
        {
            var itemGetSetProperty = typeof(MyClass).GetProperties().FirstOrDefault(
                mt => mt.Name == "Item" && mt.GetMethod != null && mt.SetMethod != null);

            var comments = Reader.GetMemberComments(itemGetSetProperty);

            Assert.AreEqual("ItemGetSetProperty description", comments.Summary);
        }

        [TestMethod]
        public void GetMemberComments_GetSetAndCommon()
        {
            var getSetProperty = typeof(MyClass).GetProperties().FirstOrDefault(
                mt => mt.Name == nameof(MyClass.GetSetProperty));

            var comments = Reader.GetMemberComments(getSetProperty);
            Assert.AreEqual("GetSetProperty comment", comments.Summary);
        }

        [TestMethod]
        public void GetMemberComments_Record()
        {
            var firstProp = typeof(MyRecord).GetProperties().FirstOrDefault(mt => mt.Name == nameof(MyRecord.First));
            var comments = Reader.GetMemberComments(firstProp);
            Assert.AreEqual("First field", comments.Summary);
        }
    }
}
