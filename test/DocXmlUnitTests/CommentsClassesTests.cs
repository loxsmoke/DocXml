using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    /// <summary>
    /// Test comments classes to make sure that they are initialized as expected.
    /// Other tests rely on this initialization behavior.
    /// </summary>
    [TestClass]
    public class CommentsClassesTests : BaseTestClass
    {

        [TestMethod]
        public void CommonComments_IsEmpty()
        {
            var comments = new CommonComments();

            base.AssertEmpty(comments);
        }

        [TestMethod]
        public void MethodComments_IsEmpty()
        {
            var comments = new MethodComments();

            base.AssertEmpty(comments);
        }

        [TestMethod]
        public void TypeComments_IsEmpty()
        {
            var comments = new TypeComments();
            
            base.AssertEmpty(comments);
        }

        [TestMethod]
        public void EnumComments_IsEmpty()
        {
            var comments = new EnumComments();
            base.AssertEmpty(comments);
        }
    }
}
