using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    public class BaseTestClass
    {
        public DocXmlReader Reader { get; set; }
        public DocXmlReader MultiAssemblyReader { get; set; }

        public void Setup()
        {
            Reader = new DocXmlReader("DocXmlUnitTests.xml");
            MultiAssemblyReader = new DocXmlReader((a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");
        }

        public void AssertParam(MethodComments comments, int paramIndex, string name, string text)
        {
            Assert.AreEqual(name, comments.Parameters[paramIndex].Name);
            Assert.AreEqual(text, comments.Parameters[paramIndex].Text);
        }
        public void AssertParam(TypeComments comments, int paramIndex, string name, string text)
        {
            Assert.AreEqual(name, comments.Parameters[paramIndex].Name);
            Assert.AreEqual(text, comments.Parameters[paramIndex].Text);
        }

        public void AssertTypeParam(MethodComments comments, int paramIndex, string name, string text)
        {
            Assert.AreEqual(name, comments.TypeParameters[paramIndex].Name);
            Assert.AreEqual(text, comments.TypeParameters[paramIndex].Text);
        }

        #region Empty comment checks
        public void AssertEmpty(CommonComments comments)
        { 
            // Assert that all fields have default values
            Assert.IsNull(comments.Summary);
            Assert.IsNull(comments.Remarks);
            Assert.IsNull(comments.Example);
            Assert.IsNull(comments.Inheritdoc);
            Assert.IsNull(comments.FullCommentText);
            Assert.IsNotNull(comments.SeeAlso);
            Assert.AreEqual(0, comments.SeeAlso.Count);
        }

        public void AssertEmpty(EnumComments comments)
        {
            AssertEmpty((CommonComments)comments);

            Assert.IsNotNull(comments.ValueComments);
            Assert.AreEqual(0, comments.ValueComments.Count);
        }

        public void AssertEmpty(TypeComments comments)
        {
            AssertEmpty((CommonComments)comments);

            Assert.IsNotNull(comments.Parameters);
            Assert.AreEqual(0, comments.Parameters.Count);
            Assert.IsNotNull(comments.TypeParameters);
            Assert.AreEqual(0, comments.TypeParameters.Count);
        }

        public void AssertEmpty(MethodComments comments)
        {
            AssertEmpty((CommonComments)comments);

            Assert.IsNotNull(comments.Parameters);
            Assert.AreEqual(0, comments.Parameters.Count);
            Assert.IsNull(comments.Returns);
            Assert.IsNotNull(comments.Responses);
            Assert.AreEqual(0, comments.Responses.Count);
            Assert.IsNotNull(comments.TypeParameters);
            Assert.AreEqual(0, comments.TypeParameters.Count);
            Assert.IsNotNull(comments.Exceptions);
            Assert.AreEqual(0, comments.Exceptions.Count);
        }
        #endregion
    }
}
