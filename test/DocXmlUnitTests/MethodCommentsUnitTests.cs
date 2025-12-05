using System;
using System.IO;
using System.Linq;
using System.Reflection;
using DocXmlOtherLibForUnitTests;
using DocXmlUnitTests.TestData;
using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class MethodCommentsUnitTests : BaseTestClass
    {
        [TestInitialize]
        public new void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void GetMethodComments_NullInfo()
        {
            MethodBase methodBase = null;

            Assert.ThrowsException<ArgumentNullException>(() => Reader.GetMethodComments(methodBase));
        }

        [TestMethod]
        public void GetMethodComments_Constructor_NoParams()
        {
            var constructor = typeof(MyClass).GetConstructor(Array.Empty<Type>());

            var comments = Reader.GetMethodComments(constructor);

            Assert.AreEqual(0, comments.Parameters.Count);
            Assert.AreEqual("Constructor with no parameters", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_Constructor_OneParam()
        {
            var constructor = typeof(MyClass).GetConstructor(new[] { typeof(int) });

            var comments = Reader.GetMethodComments(constructor);

            AssertParam(comments, 0, "one", "Parameter one");
            Assert.AreEqual("Constructor with one parameter", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments()
        {
            var method = typeof(MyClass).GetMethod(nameof(MyClass.MemberFunction));

            var comments = Reader.GetMethodComments(method);

            Assert.AreEqual("Member function description", comments.Summary);
            Assert.AreEqual(0, comments.Parameters.Count);
            Assert.AreEqual("Return value description", comments.Returns);
            Assert.AreEqual("200", comments.Responses.First().Code);
            Assert.AreEqual("OK", comments.Responses.First().Text);
            Assert.AreEqual(
                "<summary>" + Environment.NewLine +
                "            Member function description" + Environment.NewLine +
                "            </summary>" + Environment.NewLine +
                "            <returns>Return value description</returns>" + Environment.NewLine +
                "            <response code=\"200\">OK</response>" + Environment.NewLine +
                "            <exception cref=\"T:System.NullReferenceException\">Null exception</exception>" + Environment.NewLine +
                "            <exception cref=\"T:System.NotImplementedException\">NI exception</exception>",
                comments.FullCommentText.Trim('\r', '\n', ' '));
            Assert.AreEqual(2, comments.Exceptions.Count);
            Assert.AreEqual("T:System.NullReferenceException", comments.Exceptions[0].Cref);
            Assert.AreEqual("Null exception", comments.Exceptions[0].Text);
            Assert.AreEqual("T:System.NotImplementedException", comments.Exceptions[1].Cref);
            Assert.AreEqual("NI exception", comments.Exceptions[1].Text);
        }

        [TestMethod]
        public void GetMethodComments_Empty()
        {
            var method = typeof(MyNoCommentClass).GetMethod(nameof(MyNoCommentClass.Method));

            var comments = Reader.GetMethodComments(method);

            Assert.IsNull(comments.Summary);
            Assert.AreEqual(0, comments.Parameters.Count);
            Assert.IsNull(comments.Returns);
        }

        [TestMethod]
        public void GetMethodComments_1_Param()
        {
            var method = typeof(MyClass).GetMethod(
                    nameof(MyClass.MemberFunction2),
                     new[] { typeof(string), typeof(int).MakeByRefType() });

            var comments = Reader.GetMethodComments(method);

            Assert.AreEqual("Member function description. 2", comments.Summary);
            Assert.AreEqual(2, comments.Parameters.Count);
            AssertParam(comments, 0, "one", "Parameter one");
            AssertParam(comments, 1, "two", "Parameter two");
            Assert.AreEqual("Return value description", comments.Returns);
        }

        [TestMethod]
        public void GetMethodComments_2_Params()
        {
            var method = typeof(MyClass).GetMethod(
                    nameof(MyClass.MemberFunction2),
                    new[] { typeof(int), typeof(int).MakeByRefType() });

            var comments = Reader.GetMethodComments(method);

            Assert.AreEqual("Member function description. Overload 2", comments.Summary);
            Assert.AreEqual(2, comments.Parameters.Count);
            AssertParam(comments, 0, "one", "Parameter one");
            AssertParam(comments, 1, "two", "Parameter two");
            Assert.AreEqual("Return value description", comments.Returns);
        }

        [TestMethod]
        public void GetMethodComments_ArrayParams()
        {
            var method = typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithArray));

            var comments = Reader.GetMethodComments(method);

            Assert.AreEqual("MemberFunctionWithArray description", comments.Summary);
            Assert.AreEqual(2, comments.Parameters.Count);
            AssertParam(comments, 0, "array1", "Parameter array1");
            AssertParam(comments, 1, "array2", "Parameter array2");
            Assert.AreEqual("Return value description", comments.Returns);
        }


        [TestMethod]
        public void GetMethodComments_StaticOperator()
        {
            var method = typeof(MyClass).GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "op_Addition");

            var comments = Reader.GetMethodComments(method);

            Assert.AreEqual("Operator description", comments.Summary);
            Assert.AreEqual("Return value description", comments.Returns);
            Assert.AreEqual(2, comments.Parameters.Count);
            AssertParam(comments, 0, "param1", "Parameter param1");
            AssertParam(comments, 1, "param2", "Parameter param2");
        }

        [TestMethod]
        public void GetMethodComments_IndexerProperty()
        {
            var itemProperty = typeof(MyClass).GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "get_Item");

            var comments = Reader.GetMethodComments(itemProperty);

            Assert.AreEqual("Property description", comments.Summary);
            Assert.AreEqual("Return value description", comments.Returns);
            Assert.AreEqual(1, comments.Parameters.Count);
            AssertParam(comments, 0, "parameter", "Parameter description");
        }

        [TestMethod]
        public void GetMethodComments_ExplicitOperator()
        {
            var explicitOperator = typeof(MyClass).GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "op_Explicit");

            var comments = Reader.GetMethodComments(explicitOperator);

            Assert.AreEqual("Operator description", comments.Summary);
            Assert.AreEqual("Return value description", comments.Returns);
            Assert.AreEqual(1, comments.Parameters.Count);
            AssertParam(comments, 0,  "parameter", "Parameter description");
        }


        [TestMethod]
        public void GetMethodComments_TemplateMethod()
        {
            var method = typeof(MyClass).GetMethod(nameof(MyClass.TemplateMethod));

            var comments = Reader.GetMethodComments(method);

            Assert.AreEqual("TemplateMethod description", comments.Summary);
            Assert.AreEqual("Return value description", comments.Returns.Trim());
            Assert.AreEqual(0, comments.Parameters.Count);
            Assert.AreEqual(1, comments.TypeParameters.Count);
            AssertTypeParam(comments, 0, "T", "Type parameter");
        }

        [TestMethod]
        public void GetMethodComments_TemplateMethod_GenericParameter()
        {
            var method = typeof(MyClass).GetMethod(nameof(MyClass.TemplateMethod2));

            var comments = Reader.GetMethodComments(method);

            Assert.AreEqual("TemplateMethod2 description", comments.Summary);
            Assert.AreEqual("Return value description", comments.Returns);
            Assert.AreEqual(1, comments.Parameters.Count);
            AssertParam(comments, 0, "parameter", "Parameter description");
            Assert.AreEqual(comments.TypeParameters.Count, 1);
            AssertTypeParam(comments, 0, "T", "Type parameter");
        }

        [TestMethod]
        public void GetMethodComments_TemplateMethod_2_GenericParameters()
        {
            var method = typeof(MyClass).GetMethod(nameof(MyClass.TemplateMethod3));

            var comments = Reader.GetMethodComments(method);

            Assert.AreEqual("TemplateMethod3 description", comments.Summary);
            Assert.AreEqual("Return value description", comments.Returns);
            Assert.AreEqual(2, comments.Parameters.Count, 2);
            AssertParam(comments, 0, "parameter1", "Parameter description");
            AssertParam(comments, 1, "parameter2", "Parameter description");
            Assert.AreEqual(2, comments.TypeParameters.Count);
            AssertTypeParam(comments, 0, "X", "Type parameter");
            AssertTypeParam(comments, 1, "Y", "Type parameter");
        }

        [TestMethod]
        public void GetMethodComments_TemplateMethod_BaseClass()
        {
            var method = typeof(MySubClass).GetMethod(nameof(MySubClass.TemplateMethod3));

            var comments = Reader.GetMethodComments(method);

            Assert.AreEqual("TemplateMethod3 description", comments.Summary);
            Assert.AreEqual("Return value description", comments.Returns);
            Assert.AreEqual(2, comments.Parameters.Count);
            AssertParam(comments, 0, "parameter1", "Parameter description");
            AssertParam(comments, 1, "parameter2", "Parameter description");
        }

        [TestMethod]
        public void GetMethodComment()
        {
            var constructors = typeof(MySubClass).GetConstructors();

            var comment = Reader.GetMemberComment(constructors.First());

            Assert.AreEqual(1, constructors.Length);
            Assert.AreEqual("Constructor comment", comment);
        }

        [TestMethod]
        public void GetMethodComments_SubClass()
        {
            var method = typeof(MySubClass).GetMethod(nameof(MySubClass.MethodWithComments));
            
            var comments = Reader.GetMemberComments(method);
            
            Assert.AreEqual("Method summary", comments.Summary);
            Assert.AreEqual("Method example", comments.Example);
            Assert.AreEqual("Method remarks", comments.Remarks);
            Assert.AreEqual(
                "<summary>Method summary</summary>" + Environment.NewLine +
                "            <remarks>Method remarks</remarks>" + Environment.NewLine +
                "            <example>Method example</example>",
                comments.FullCommentText.Trim('\r', '\n', ' '));
         }

        [DataTestMethod]
        [DataRow(typeof(MySubClass), nameof(MySubClass.MethodWithInParam), "MethodWithInParam description")]
        [DataRow(typeof(MySubClass), nameof(MySubClass.MultilineSummary), "Summary line 1\r\nSummary line 2\r\nSummary line 3")]
        [DataRow(typeof(MyClass), nameof(MyClass.MemberFunctionWithGenericArray), "MemberFunctionWithGenericTypeArray description")]
        [DataRow(typeof(MyClass), nameof(MyClass.MemberFunctionWithGenericMultiDimArray), "MemberFunctionWithGenericTypeMultiDimArray description")]
        [DataRow(typeof(MyClass), nameof(MyClass.MemberFunctionWithGenericJaggedArray), "MemberFunctionWithGenericTypeJaggedArray description")]
        [DataRow(typeof(MyClass), nameof(MyClass.MemberFunctionWithGenericOutArray), "MemberFunctionWithGenericTypeOutArray description")]
        [DataRow(typeof(MyClass), nameof(MyClass.MemberFunctionWithReadOnlyStringCollection), "MemberFunctionWithReadOnlyStringCollection description")]
        public void GetMethodComments_InParamter(Type type, string methodName, string expectedSummary)
        {
            var method = type.GetMethod(methodName);
            
            var comments = Reader.GetMethodComments(method);
            
            Assert.AreEqual(expectedSummary.ReplaceLineEndings(), comments.Summary.ReplaceLineEndings());
        }

        [TestMethod]
        public void GetMethodComments_Inheritdoc_Constructor()
        {
            var method = typeof(ClassForInheritdoc)
                    .GetConstructor(new Type[] { typeof(int) });
            
            var comments = Reader.GetMethodComments(method);
            
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("Constructor2", comments.Summary);
            Assert.IsNotNull(comments.Parameters);
            Assert.AreEqual(1, comments.Parameters.Count);
            Assert.AreEqual("x", comments.Parameters[0].Name);
        }

        [TestMethod]
        public void GetMethodComments_Inheritdoc_VirtualOverride()
        {
            var method = typeof(ClassForInheritdoc)
                    .GetMethod(nameof(ClassForInheritdoc.Method));
            
            var comments = Reader.GetMethodComments(method);
            
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("Method for Inheritdoc", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_Inheritdoc_InterfaceImplementation()
        {
            var method = typeof(ClassForInheritdoc)
                    .GetMethod(nameof(ClassForInheritdoc.InterfaceMethod));

            var comments = Reader.GetMethodComments(method);

            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("Interface method", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_Inheritdoc_Cref()
        {
            var method = typeof(ClassForInheritdocCref)
                    .GetMethod(nameof(ClassForInheritdocCref.Method));

            var comments = Reader.GetMethodComments(method);

            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("M:DocXmlUnitTests.TestData.BaseClassForInheritdoc.Method", comments.Inheritdoc.Cref);
            Assert.AreEqual("Method for Inheritdoc", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_Inheritdoc_Cref_OtherAssembly()
        {
            var docReader = new DocXmlReader(
                new Assembly[] { typeof(MyClass).Assembly, typeof(OtherClass).Assembly},
                (a) => Path.GetFileNameWithoutExtension(a.Location) + ".xml");

            var method = typeof(ClassForInheritdocCref)
                    .GetMethod(nameof(ClassForInheritdocCref.OtherLibMethod));

            var comments = docReader.GetMethodComments(method);

            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("OtherLibMethod summary", comments.Summary);
        }

        [TestMethod]
        public void GetMemberComments_Property_Inheritdoc()
        {
            var property = typeof(ClassForInheritdoc).GetProperty(nameof(ClassForInheritdoc.Property));

            var comments =Reader.GetMemberComments(property);
            
            Assert.IsNotNull(comments.Inheritdoc);
        }
    }
}
