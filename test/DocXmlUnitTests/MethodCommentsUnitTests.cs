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
        public MethodInfo MyClass_MemberFunction;
        public MethodInfo MyClass_MemberFunction2_string;
        public MethodInfo MyClass_MemberFunction2_int;
        public MethodInfo MyClass_MemberFunctionWithArray;
        public MethodInfo MyClass_TemplateMethod;
        public MethodInfo MyClass_TemplateMethod2;
        public MethodInfo MyClass_TemplateMethod3;
        public MethodInfo MyClass_PlusOperator;
        public MethodInfo MyClass_ItemProperty;
        public MethodInfo MyClass_explicitOperator;

        public MethodInfo MyNoCommentClass_Method;

        public MethodInfo MySubClass_TemplateMethod3;
        public MethodInfo MySubClass_MethodWithComments;
        public MethodInfo MySubClass_MethodWithInParam;
        public MethodInfo MySubClass_MultilineSummary;

        [TestInitialize]
        public new void Setup()
        {
            base.Setup();
            MyClass_MemberFunction = MyClass_Type.GetMethod(nameof(MyClass.MemberFunction));
            MyClass_MemberFunction2_string = MyClass_Type.GetMethod(
                    nameof(MyClass.MemberFunction2),
                     new[] { typeof(string), typeof(int).MakeByRefType() });
            MyClass_MemberFunction2_int = MyClass_Type.GetMethod(
                    nameof(MyClass.MemberFunction2),
                    new[] { typeof(int), typeof(int).MakeByRefType() });
            MyClass_MemberFunctionWithArray = MyClass_Type.GetMethod(nameof(MyClass.MemberFunctionWithArray));
            MyClass_TemplateMethod = MyClass_Type.GetMethod(nameof(MyClass.TemplateMethod));
            MyClass_TemplateMethod2 = MyClass_Type.GetMethod(nameof(MyClass.TemplateMethod2));
            MyClass_TemplateMethod3 = MyClass_Type.GetMethod(nameof(MyClass.TemplateMethod3));
            MyClass_PlusOperator = MyClass_Type.GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "op_Addition");
            MyClass_ItemProperty = MyClass_Type.GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "get_Item");
            MyClass_explicitOperator = MyClass_Type.GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "op_Explicit");

            MyNoCommentClass_Method = typeof(MyNoCommentClass).GetMethod(nameof(MyNoCommentClass.Method));

            MySubClass_TemplateMethod3 = typeof(MySubClass).GetMethod(nameof(MySubClass.TemplateMethod3));
            MySubClass_MethodWithComments = typeof(MySubClass).GetMethod(nameof(MySubClass.MethodWithComments));
            MySubClass_MethodWithInParam = typeof(MySubClass).GetMethod(nameof(MySubClass.MethodWithInParam));
            MySubClass_MultilineSummary = typeof(MySubClass).GetMethod(nameof(MySubClass.MultilineSummary));
        }

        [TestMethod]
        public void GetMethodComments_Constructor_NoParams()
        {
            var constr = MyClass_Type.GetConstructor(Array.Empty<Type>());
            var mm = Reader.GetMethodComments(constr);
            Assert.AreEqual(0, mm.Parameters.Count);
            Assert.AreEqual("Constructor with no parameters", mm.Summary);
        }

        [TestMethod]
        public void GetMethodComments_Constructor_OneParam()
        {
            var constr = MyClass_Type.GetConstructor(new[] { typeof(int) });
            var mm = Reader.GetMethodComments(constr);
            AssertParam(mm, 0, "one", "Parameter one");
            Assert.AreEqual("Constructor with one parameter", mm.Summary);
        }

        [TestMethod]
        public void GetMethodComments()
        {
            var comments = Reader.GetMethodComments(MyClass_MemberFunction);
            Assert.AreEqual("Member function description", comments.Summary);
            Assert.AreEqual(0, comments.Parameters.Count);
            Assert.AreEqual("Return value description", comments.Returns);
            Assert.AreEqual("200", comments.Responses.First().Item1);
            Assert.AreEqual("OK", comments.Responses.First().Item2);
            Assert.AreEqual(
                "<summary>" + Environment.NewLine +
                "            Member function description" + Environment.NewLine +
                "            </summary>" + Environment.NewLine +
                "            <returns>Return value description</returns>" + Environment.NewLine +
                "            <response code=\"200\">OK</response>",
                comments.FullCommentText.Trim('\r', '\n', ' '));
        }

        [TestMethod]
        public void GetMethodComments_Empty()
        {
            var mm = Reader.GetMethodComments(MyNoCommentClass_Method);
            Assert.IsNull(mm.Summary);
            Assert.AreEqual(0, mm.Parameters.Count);
            Assert.IsNull(mm.Returns);
        }

        [TestMethod]
        public void GetMethodComments_1_Param()
        {
            var mm = Reader.GetMethodComments(MyClass_MemberFunction2_string);

            Assert.AreEqual("Member function description. 2", mm.Summary);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "one", "Parameter one");
            AssertParam(mm, 1, "two", "Parameter two");
            Assert.AreEqual("Return value description", mm.Returns);
        }

        [TestMethod]
        public void GetMethodComments_2_Params()
        {
            var mm = Reader.GetMethodComments(MyClass_MemberFunction2_int);

            Assert.AreEqual("Member function description. Overload 2", mm.Summary);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "one", "Parameter one");
            AssertParam(mm, 1, "two", "Parameter two");
            Assert.AreEqual("Return value description", mm.Returns);
        }

        [TestMethod]
        public void GetMethodComments_ArrayParams()
        {
            var mm = Reader.GetMethodComments(MyClass_MemberFunctionWithArray);

            Assert.AreEqual("MemberFunctionWithArray description", mm.Summary);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "array1", "Parameter array1");
            AssertParam(mm, 1, "array2", "Parameter array2");
            Assert.AreEqual("Return value description", mm.Returns);
        }


        [TestMethod]
        public void GetMethodComments_StaticOperator()
        {
            var mm = Reader.GetMethodComments(MyClass_PlusOperator);
            Assert.AreEqual("Operator description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "param1", "Parameter param1");
            AssertParam(mm, 1, "param2", "Parameter param2");
        }

        [TestMethod]
        public void GetMethodComments_IndexerProperty()
        {
            var mm = Reader.GetMethodComments(MyClass_ItemProperty);
            Assert.AreEqual("Property description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(1, mm.Parameters.Count);
            AssertParam(mm, 0, "parameter", "Parameter description");
        }

        [TestMethod]
        public void GetMethodComments_ExplicitOperator()
        {
            var mm = Reader.GetMethodComments(MyClass_explicitOperator);
            Assert.AreEqual("Operator description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(1, mm.Parameters.Count);
            AssertParam(mm, 0,  "parameter", "Parameter description");
        }


        [TestMethod]
        public void GetMethodComments_TemplateMethod()
        {
            var mm = Reader.GetMethodComments(MyClass_TemplateMethod);
            Assert.AreEqual("TemplateMethod description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns.Trim());
            Assert.AreEqual(0, mm.Parameters.Count);
            Assert.AreEqual(1, mm.TypeParameters.Count);
            AssertTypeParam(mm, 0, "T", "Type parameter");
        }

        [TestMethod]
        public void GetMethodComments_TemplateMethod_GenericParameter()
        {
            var mm = Reader.GetMethodComments(MyClass_TemplateMethod2);
            Assert.AreEqual("TemplateMethod2 description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(1, mm.Parameters.Count);
            AssertParam(mm, 0, "parameter", "Parameter description");
            Assert.AreEqual(mm.TypeParameters.Count, 1);
            AssertTypeParam(mm, 0, "T", "Type parameter");
        }

        [TestMethod]
        public void GetMethodComments_TemplateMethod_2_GenericParameters()
        {
            var mm = Reader.GetMethodComments(MyClass_TemplateMethod3);
            Assert.AreEqual("TemplateMethod3 description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(2, mm.Parameters.Count, 2);
            AssertParam(mm, 0, "parameter1", "Parameter description");
            AssertParam(mm, 1, "parameter2", "Parameter description");
            Assert.AreEqual(2, mm.TypeParameters.Count);
            AssertTypeParam(mm, 0, "X", "Type parameter");
            AssertTypeParam(mm, 1, "Y", "Type parameter");
        }

        [TestMethod]
        public void GetMethodComments_TemplateMethod_BaseClass()
        {
            var mm = Reader.GetMethodComments(MySubClass_TemplateMethod3);
            Assert.AreEqual("TemplateMethod3 description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "parameter1", "Parameter description");
            AssertParam(mm, 1, "parameter2", "Parameter description");
        }

        [TestMethod]
        public void GetMethodComment()
        {
            var constructors = typeof(MySubClass).GetConstructors();
            Assert.AreEqual(1, constructors.Length);
            Assert.AreEqual("Constructor comment", Reader.GetMemberComment(constructors.First()));
        }

        [TestMethod]
        public void GetMethodComments_SubClass()
        {
            var comments = Reader.GetMemberComments(MySubClass_MethodWithComments);
            Assert.AreEqual("Method summary", comments.Summary);
            Assert.AreEqual("Method example", comments.Example);
            Assert.AreEqual("Method remarks", comments.Remarks);
            Assert.AreEqual(
                "<summary>Method summary</summary>" + Environment.NewLine +
                "            <remarks>Method remarks</remarks>" + Environment.NewLine +
                "            <example>Method example</example>",
                comments.FullCommentText.Trim('\r', '\n', ' '));
         }

        [TestMethod]
        public void GetMethodComments_InParamter()
        {
            var comments = Reader.GetMethodComments(MySubClass_MethodWithInParam);
            Assert.AreEqual("MethodWithInParam description", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_MultiLineSummary()
        {
            var comments = Reader.GetMethodComments(MySubClass_MultilineSummary);
            Assert.AreEqual("Summary line 1\r\nSummary line 2\r\nSummary line 3", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_GenericTypeArray()
        {
            var comments = Reader.GetMethodComments(typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithGenericArray)));
            Assert.AreEqual("MemberFunctionWithGenericTypeArray description", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_GenericTypeMultiDimArray()
        {
            var comments = Reader.GetMethodComments(typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithGenericMultiDimArray)));
            Assert.AreEqual("MemberFunctionWithGenericTypeMultiDimArray description", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_GenericTypeJaggedArray()
        {
            var comments = Reader.GetMethodComments(typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithGenericJaggedArray)));
            Assert.AreEqual("MemberFunctionWithGenericTypeJaggedArray description", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_GenericTypeOutArray()
        {
            var comments = Reader.GetMethodComments(typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithGenericOutArray)));
            Assert.AreEqual("MemberFunctionWithGenericTypeOutArray description", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_ReadOnlyStringCollection()
        {
            var comments = Reader.GetMethodComments(typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithReadOnlyStringCollection)));
            Assert.AreEqual("MemberFunctionWithReadOnlyStringCollection description", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_Inheritdoc_Constructor()
        {
            var comments =
                Reader.GetMethodComments(typeof(ClassForInheritdoc)
                    .GetConstructor(new Type[] { typeof(int) }));
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("Constructor2", comments.Summary);
            Assert.IsNotNull(comments.Parameters);
            Assert.AreEqual(1, comments.Parameters.Count);
            Assert.AreEqual("x", comments.Parameters[0].Item1);
        }

        [TestMethod]
        public void GetMethodComments_Inheritdoc_VirtualOverride()
        {
            var comments =
                Reader.GetMethodComments(typeof(ClassForInheritdoc)
                    .GetMethod(nameof(ClassForInheritdoc.Method)));
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("Method for Inheritdoc", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_Inheritdoc_InterfaceImplementation()
        {
            var comments =
                Reader.GetMethodComments(typeof(ClassForInheritdoc)
                    .GetMethod(nameof(ClassForInheritdoc.InterfaceMethod)));
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("Interface method", comments.Summary);
        }

        [TestMethod]
        public void GetMethodComments_Inheritdoc_Cref()
        {
            var comments =
                Reader.GetMethodComments(typeof(ClassForInheritdocCref)
                    .GetMethod(nameof(ClassForInheritdocCref.Method)));
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

            var comments =
                docReader.GetMethodComments(typeof(ClassForInheritdocCref)
                    .GetMethod(nameof(ClassForInheritdocCref.OtherLibMethod)));
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("OtherLibMethod summary", comments.Summary);
        }

        [TestMethod]
        public void GetMemberComments_Property_Inheritdoc()
        {
            var comments =
                Reader.GetMemberComments(typeof(ClassForInheritdoc).GetProperty(nameof(ClassForInheritdoc.Property)));
            Assert.IsNotNull(comments.Inheritdoc);
        }
    }
}
