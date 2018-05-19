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
        public void Constructor_Comments()
        {
            var constructors = MyClass_Type.GetConstructors();
            Assert.AreEqual(2, constructors.Length);
            foreach (var constr in constructors)
            {
                var mm = Reader.GetMethodComments(constr);
                if (constr.GetParameters().Length == 0)
                {
                    Assert.AreEqual(mm.Parameters.Count, constr.GetParameters().Length);
                    Assert.AreEqual("Constructor with no parameters", mm.Summary);
                }
                else if (constr.GetParameters().Length == 1)
                {
                    Assert.AreEqual(mm.Parameters.Count, constr.GetParameters().Length);
                    AssertParam(mm, 0, "one", "Parameter one");
                    Assert.AreEqual("Constructor with one parameter", mm.Summary);
                }
            }
        }

        [TestMethod]
        public void MemberFunction_Comments()
        {
            var mm = Reader.GetMethodComments(MyClass_MemberFunction);
            Assert.AreEqual("Member function description", mm.Summary);
            Assert.AreEqual(0, mm.Parameters.Count);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual("200", mm.Responses.First().Item1);
            Assert.AreEqual("OK", mm.Responses.First().Item2);
        }

        [TestMethod]
        public void MemberFunction_Comments_Empty()
        {
            var mm = Reader.GetMethodComments(MyNoCommentClass_Method);
            Assert.IsNull(mm.Summary);
            Assert.AreEqual(0, mm.Parameters.Count);
            Assert.IsNull(mm.Returns);
        }

        [TestMethod]
        public void MemberFunction_Overload_1_Params_Comments()
        {
            var mm = Reader.GetMethodComments(MyClass_MemberFunction2_string);

            Assert.AreEqual("Member function description. 2", mm.Summary);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "one", "Parameter one");
            AssertParam(mm, 1, "two", "Parameter two");
            Assert.AreEqual("Return value description", mm.Returns);
        }

        [TestMethod]
        public void MemberFunction_Overload_2_Params_Comments()
        {
            var mm = Reader.GetMethodComments(MyClass_MemberFunction2_int);

            Assert.AreEqual("Member function description. Overload 2", mm.Summary);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "one", "Parameter one");
            AssertParam(mm, 1, "two", "Parameter two");
            Assert.AreEqual("Return value description", mm.Returns);
        }

        [TestMethod]
        public void MemberFunctio_ArrayParams_Comments()
        {
            var mm = Reader.GetMethodComments(MyClass_MemberFunctionWithArray);

            Assert.AreEqual("MemberFunctionWithArray description", mm.Summary);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "array1", "Parameter array1");
            AssertParam(mm, 1, "array2", "Parameter array2");
            Assert.AreEqual("Return value description", mm.Returns);
        }


        [TestMethod]
        public void StaticOperator_Comments()
        {
            var mm = Reader.GetMethodComments(MyClass_PlusOperator);
            Assert.AreEqual("Operator description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "param1", "Parameter param1");
            AssertParam(mm, 1, "param2", "Parameter param2");
        }

        [TestMethod]
        public void IndexerProperty_Comments()
        {
            var mm = Reader.GetMethodComments(MyClass_ItemProperty);
            Assert.AreEqual("Property description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(1, mm.Parameters.Count);
            AssertParam(mm, 0, "parameter", "Parameter description");
        }

        [TestMethod]
        public void ExplicitOperator_Comments()
        {
            var mm = Reader.GetMethodComments(MyClass_explicitOperator);
            Assert.AreEqual("Operator description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(1, mm.Parameters.Count);
            AssertParam(mm, 0,  "parameter", "Parameter description");
        }


        [TestMethod]
        public void TemplateMethod_Comments()
        {
            var mm = Reader.GetMethodComments(MyClass_TemplateMethod);
            Assert.AreEqual("TemplateMethod description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns.Trim());
            Assert.AreEqual(0, mm.Parameters.Count);
            Assert.AreEqual(1, mm.TypeParameters.Count);
            AssertTypeParam(mm, 0, "T", "Type parameter");
        }

        [TestMethod]
        public void TemplateMethod_WithGenericParameter_Comments()
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
        public void TemplateMethod_With_2_GenericParameters_Comments()
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
        public void TemplateMethod_In_Base_Class_Comments()
        {
            var mm = Reader.GetMethodComments(MySubClass_TemplateMethod3);
            Assert.AreEqual("TemplateMethod3 description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "parameter1", "Parameter description");
            AssertParam(mm, 1, "parameter2", "Parameter description");
        }

        [TestMethod]
        public void MemberSummary_Comment()
        {
            var constructors = typeof(MySubClass).GetConstructors();
            Assert.AreEqual(1, constructors.Length);
            Assert.AreEqual("Constructor comment", Reader.GetMemberComment(constructors.First()));
        }

        [TestMethod]
        public void MemberComments()
        {
            var comments = Reader.GetMemberComments(MySubClass_MethodWithComments);
            Assert.AreEqual("Method summary", comments.Summary);
            Assert.AreEqual("Method example", comments.Example);
            Assert.AreEqual("Method remarks", comments.Remarks);
        }

        [TestMethod]
        public void MemberFunction_InParamter()
        {
            var comments = Reader.GetMethodComments(MySubClass_MethodWithInParam);
            Assert.AreEqual("MethodWithInParam description", comments.Summary);
        }

        [TestMethod]
        public void MemberFunction_MultiLineSummary()
        {
            var comments = Reader.GetMethodComments(MySubClass_MultilineSummary);
            Assert.AreEqual("Summary line 1\r\nSummary line 2\r\nSummary line 3", comments.Summary);
        }

        [TestMethod]
        public void MemberFunction_Inheritdoc_Constructor()
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
        public void MemberFunction_Inheritdoc_VirtualOverride()
        {
            var comments =
                Reader.GetMethodComments(typeof(ClassForInheritdoc)
                    .GetMethod(nameof(ClassForInheritdoc.Method)));
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("Method for Inheritdoc", comments.Summary);
        }

        [TestMethod]
        public void MemberFunction_Inheritdoc_InterfaceImplementation()
        {
            var comments =
                Reader.GetMethodComments(typeof(ClassForInheritdoc)
                    .GetMethod(nameof(ClassForInheritdoc.InterfaceMethod)));
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("Interface method", comments.Summary);
        }

        [TestMethod]
        public void MemberFunction_Inheritdoc_Cref()
        {
            var comments =
                Reader.GetMethodComments(typeof(ClassForInheritdocCref)
                    .GetMethod(nameof(ClassForInheritdocCref.Method)));
            Assert.IsNotNull(comments.Inheritdoc);
            Assert.AreEqual("M:DocXmlUnitTests.TestData.BaseClassForInheritdoc.Method", comments.Inheritdoc.Cref);
            Assert.AreEqual("Method for Inheritdoc", comments.Summary);
        }

        [TestMethod]
        public void MemberFunction_Inheritdoc_Cref_OtherAssembly()
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
        public void Property_Inheritdoc()
        {
            var comments =
                Reader.GetMemberComments(typeof(ClassForInheritdoc).GetProperty(nameof(ClassForInheritdoc.Property)));
            Assert.IsNotNull(comments.Inheritdoc);
        }
    }
}
