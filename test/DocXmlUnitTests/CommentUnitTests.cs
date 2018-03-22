using System;
using System.Linq;
using System.Reflection;
using System.Xml.XPath;
using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS1591

namespace LoxSmoke.DocXmlUnitTests
{
    [TestClass]
    public class CommentUnitTests
    {
        public DocXmlReader Reader { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Reader = new DocXmlReader("DocXmlUnitTests.xml");
        }

        [TestMethod]
        public void Class_Comment()
        {
            var mm = Reader.GetTypeComments(typeof(MyClass2));
            Assert.AreEqual("This is MyClass2", mm.Summary);
        }


        [TestMethod]
        public void Class_Comment_Empty()
        {
            var mm = Reader.GetTypeComments(typeof(MyNoCommentClass));
            Assert.IsNull(mm.Summary);
        }

        [TestMethod]
        public void Reader_From_XPathDocument()
        {
            var m = new DocXmlReader(new XPathDocument("DocXmlUnitTests.xml"));
            var mm = m.GetTypeComments(typeof(MyClass2));
            Assert.AreEqual("This is MyClass2", mm.Summary);
        }

        [TestMethod]
        public void NestedClass_Comment()
        {
            var mm = Reader.GetTypeComments(typeof(MyClass2.Nested));
            Assert.AreEqual("Nested class", mm.Summary);
        }

        [TestMethod]
        public void NotEnumType_Comment()
        {
            Assert.ThrowsException<ArgumentException>(() => Reader.GetEnumComments(typeof(MyClass2)));
        }

        [TestMethod]
        public void EnumType_Comment()
        {
            var mm = Reader.GetEnumComments(typeof(TestEnum2));
            Assert.AreEqual("Enum 2 type description", mm.Summary);
            Assert.AreEqual(mm.ValueComments.Count, 0);
        }

        [TestMethod]
        public void EnumType_Values_NoComments()
        {
            var mm = Reader.GetEnumComments(typeof(TestEnum2), true);
            Assert.AreEqual("Enum 2 type description", mm.Summary);
            Assert.AreEqual(mm.ValueComments.Count, 2);
            AssertEnumComment(0, "Value21", "", mm.ValueComments[0]);
            AssertEnumComment(1, "Value22", "", mm.ValueComments[1]);
        }

        [TestMethod]
        public void EnumValueComments_ToString_Null()
        {
            var enumComment = new EnumValueComment();
            Assert.AreEqual("=0", enumComment.ToString());
        }

        [TestMethod]
        public void EnumValueComments_ToString()
        {
            var enumComment = new EnumValueComment()
            {
                Value = 5,
                 Name = "name",
                 Summary = "summary"
            };
            Assert.AreEqual("name=5 summary", enumComment.ToString());
        }

        void AssertEnumComment(int expectedValue, string expectedName, string expectedSummary, EnumValueComment comment)
        {
            Assert.AreEqual(expectedValue, comment.Value);
            Assert.AreEqual(expectedName, comment.Name);
            Assert.AreEqual(expectedSummary, comment.Summary);
        }

        [TestMethod]
        public void EnumType_WithValues_Comments()
        {
            var mm = Reader.GetEnumComments(typeof(TestEnum));
            Assert.AreEqual("Enum type description", mm.Summary);
            Assert.AreEqual(2, mm.ValueComments.Count);
            AssertEnumComment(10, "Value1", "Enum value one", mm.ValueComments[0]);
            AssertEnumComment(20, "Value2", "Enum value two", mm.ValueComments[1]);
        }

        [TestMethod]
        public void SimpleField_Summary()
        {
            var mm = Reader.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.stringField)).First());
            Assert.AreEqual("String field description", mm);
        }

        [TestMethod]
        public void ConstField_Summary()
        {
            var mm = Reader.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.PI)).First());
            Assert.AreEqual("Const field description", mm);
        }

        [TestMethod]
        public void ValueProperty_Summary()
        {
            var mm = Reader.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.ValProperty)).First());
            Assert.AreEqual("Value property description", mm);
        }

        [TestMethod]
        public void EnumProperty_Summary()
        {
            var mm = Reader.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.ImportantEnum)).First());
            Assert.AreEqual("Enum property description", mm);
        }

        [TestMethod]
        public void EventField_Summary()
        {
            var mm = Reader.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.eventField)).First());
            Assert.AreEqual("Event field description", mm);
        }

        [TestMethod]
        public void GenericField_Summary()
        {
            var mm = Reader.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.genericTypeField)).First());
            Assert.AreEqual("Generic field description", mm);
        }

        void AssertParam(MethodComments comments, int paramIndex, string name, string text)
        {
            Assert.AreEqual(name, comments.Parameters[paramIndex].Item1);
            Assert.AreEqual(text, comments.Parameters[paramIndex].Item2);
        }
        void AssertParam(TypeComments comments, int paramIndex, string name, string text)
        {
            Assert.AreEqual(name, comments.Parameters[paramIndex].Item1);
            Assert.AreEqual(text, comments.Parameters[paramIndex].Item2);
        }

        [TestMethod]
        public void Constructor_Comments()
        {
            var constructors = typeof(MyClass2).GetConstructors();
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
            var mm = Reader.GetMethodComments(typeof(MyClass2).GetMethod(nameof(MyClass2.MemberFunction)));
            Assert.AreEqual("Member function description", mm.Summary);
            Assert.AreEqual(0, mm.Parameters.Count);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual("200", mm.Responses.First().Item1);
            Assert.AreEqual("OK", mm.Responses.First().Item2);
        }

        [TestMethod]
        public void MemberFunction_Comments_Empty()
        {
            var mm = Reader.GetMethodComments(typeof(MyNoCommentClass).GetMethod(nameof(MyNoCommentClass.Method)));
            Assert.IsNull(mm.Summary);
            Assert.AreEqual(0, mm.Parameters.Count);
            Assert.IsNull(mm.Returns);
        }

        [TestMethod]
        public void MemberFunction_Overload_2_Params_Comments()
        {
            var mm = Reader.GetMethodComments(
                typeof(MyClass2).GetMethod(
                    nameof(MyClass2.MemberFunction2),
                     new [] { typeof(string), typeof(int).MakeByRefType()}));

            Assert.AreEqual("Member function description. 2", mm.Summary);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "one", "Parameter one");
            AssertParam(mm, 1, "two", "Parameter two");
            Assert.AreEqual("Return value description", mm.Returns);

            mm = Reader.GetMethodComments(
                typeof(MyClass2).GetMethod(
                    nameof(MyClass2.MemberFunction2),
                    new[] { typeof(int), typeof(int).MakeByRefType() }));

            Assert.AreEqual("Member function description. Overload 2", mm.Summary);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "one", "Parameter one");
            AssertParam(mm, 1, "two", "Parameter two");
            Assert.AreEqual("Return value description", mm.Returns);
        }

        [TestMethod]
        public void MemberFunctio_ArrayParams_Comments()
        {
            var mm = Reader.GetMethodComments(
                typeof(MyClass2).GetMethod(
                    nameof(MyClass2.MemberFunctionWithArray)));

            Assert.AreEqual("MemberFunctionWithArray description", mm.Summary);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "array1", "Parameter array1");
            AssertParam(mm, 1, "array2", "Parameter array2");
            Assert.AreEqual("Return value description", mm.Returns);
        }

        [TestMethod]
        public void DelegateType_Comments()
        {
            var mm = Reader.GetTypeComments(typeof(MyClass2)
                .GetNestedType(nameof(MyClass2.DelegateType)));
            Assert.AreEqual("Delegate type description", mm.Summary);
            Assert.AreEqual(1, mm.Parameters.Count);
            AssertParam(mm, 0, "parameter", "Parameter description");
        }

        [TestMethod]
        public void StaticOperator_Comments()
        {
            var minfo = typeof(MyClass2).GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "op_Addition");
            var mm = Reader.GetMethodComments(minfo);
            Assert.AreEqual("Operator description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "param1", "Parameter param1");
            AssertParam(mm, 1, "param2", "Parameter param2");
        }

        [TestMethod]
        public void IndexerProperty_Comments()
        {
            var minfo = typeof(MyClass2).GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "get_Item");
            var mm = Reader.GetMethodComments(minfo);
            Assert.AreEqual("Property description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(1, mm.Parameters.Count);
            AssertParam(mm, 0, "parameter", "Parameter description");
        }

        [TestMethod]
        public void ExplicitOperator_Comments()
        {
            var minfo = typeof(MyClass2).GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "op_Explicit");
            var mm = Reader.GetMethodComments(minfo);
            Assert.AreEqual("Operator description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(1, mm.Parameters.Count);
            AssertParam(mm, 0,  "parameter", "Parameter description");
        }

        void AssertTypeParam(MethodComments comments, int paramIndex, string name, string text)
        {
            Assert.AreEqual(name, comments.TypeParameters[paramIndex].Item1);
            Assert.AreEqual(text, comments.TypeParameters[paramIndex].Item2);
        }

        [TestMethod]
        public void TemplateMethod_Comments()
        {
            var minfo = typeof(MyClass2).GetMethod(nameof(MyClass2.TemplateMethod));
            var mm = Reader.GetMethodComments(minfo);
            Assert.AreEqual("TemplateMethod description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns.Trim());
            Assert.AreEqual(0, mm.Parameters.Count);
            Assert.AreEqual(1, mm.TypeParameters.Count);
            AssertTypeParam(mm, 0, "T", "Type parameter");
        }

        [TestMethod]
        public void TemplateMethod_WithGenericParameter_Comments()
        {
            var minfo = typeof(MyClass2).GetMethods().FirstOrDefault(mt => mt.Name == "TemplateMethod2");
            var mm = Reader.GetMethodComments(minfo);
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
            var minfo = typeof(MyClass2).GetMethods().FirstOrDefault(mt => mt.Name == "TemplateMethod3");
            var mm = Reader.GetMethodComments(minfo);
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
            var minfo = typeof(MyClass3).GetMethods().FirstOrDefault(mt => mt.Name == "TemplateMethod3");
            var mm = Reader.GetMethodComments(minfo);
            Assert.AreEqual("TemplateMethod3 description", mm.Summary);
            Assert.AreEqual("Return value description", mm.Returns);
            Assert.AreEqual(2, mm.Parameters.Count);
            AssertParam(mm, 0, "parameter1", "Parameter description");
            AssertParam(mm, 1, "parameter2", "Parameter description");
        }

        [TestMethod]
        public void MemberSummary_Comment()
        {
            var constructors = typeof(MyClass3).GetConstructors();
            Assert.AreEqual(1, constructors.Length);
            Assert.AreEqual("Constructor comment", Reader.GetMemberComment(constructors.First()));
        }

        [TestMethod]
        public void MemberComments()
        {
            var info = typeof(MyClass3).GetMethod(nameof(MyClass3.MethodWithComments));
            var comments = Reader.GetMemberComments(info);
            Assert.AreEqual("Method summary", comments.Summary);
            Assert.AreEqual("Method example", comments.Example);
            Assert.AreEqual("Method remarks", comments.Remarks);
        }

        [TestMethod]
        public void MemberFunction_InParamter()
        {
            var info = typeof(MyClass3).GetMethod(nameof(MyClass3.MethodWithInParam));
            var comments = Reader.GetMethodComments(info);
            Assert.AreEqual("MethodWithInParam description", comments.Summary);
        }

        [TestMethod]
        public void MemberFunction_MultiLineSummary()
        {
            var info = typeof(MyClass3).GetMethod(nameof(MyClass3.MultilineSummary));
            var comments = Reader.GetMethodComments(info);
            Assert.AreEqual("Summary line 1\r\nSummary line 2\r\nSummary line 3", comments.Summary);
        }
    }
}
