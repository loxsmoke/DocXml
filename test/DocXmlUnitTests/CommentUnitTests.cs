using System;
using System.Linq;
using System.Reflection;
using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS1591

namespace LoxSmoke.DocXmlUnitTests
{
    [TestClass]
    public class CommentUnitTests
    {
        static DocXmlReader GetReader()
        {
            return new DocXmlReader("DocXmlUnitTests.xml");
        }

        [TestMethod]
        public void Class_Comment()
        {
            var m = GetReader(); 
            var mm = m.GetTypeComments(typeof(MyClass2));
            Assert.AreEqual(mm.Summary.Trim(), "This is MyClass2");
        }

        [TestMethod]
        public void NestedClass_Comment()
        {
            var m = GetReader();
            var mm = m.GetTypeComments(typeof(MyClass2.Nested));
            Assert.AreEqual(mm.Summary.Trim(), "Nested class");
        }

        [TestMethod]
        public void EnumType_WithValues_Comments()
        {
            var m = GetReader();
            var mm = m.GetEnumComments(typeof(TestEnum));
            Assert.AreEqual(mm.Summary.Trim(), "Enum type description");
            Assert.AreEqual(mm.ValueComments.Count, 2);
            Assert.AreEqual(mm.ValueComments[0].Item1.Trim(), "Value1");
            Assert.AreEqual(mm.ValueComments[0].Item2.Trim(), "Enum value one");
            Assert.AreEqual(mm.ValueComments[1].Item1.Trim(), "Value2");
            Assert.AreEqual(mm.ValueComments[1].Item2.Trim(), "Enum value two");
        }

        [TestMethod]
        public void SimpleField_Summary()
        {
            var m = GetReader();
            var mm = m.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.stringField)).First());
            Assert.AreEqual(mm.Trim(), "String field description");
        }

        [TestMethod]
        public void ConstField_Summary()
        {
            var m = GetReader();
            var mm = m.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.PI)).First());
            Assert.AreEqual(mm.Trim(), "Const field description");
        }

        [TestMethod]
        public void ValueProperty_Summary()
        {
            var m = GetReader();
            var mm = m.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.ValProperty)).First());
            Assert.AreEqual(mm.Trim(), "Value property description");
        }

        [TestMethod]
        public void EnumProperty_Summary()
        {
            var m = GetReader();
            var mm = m.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.ImportantEnum)).First());
            Assert.AreEqual(mm.Trim(), "Enum property description");
        }

        [TestMethod]
        public void EventField_Summary()
        {
            var m = GetReader();
            var mm = m.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.eventField)).First());
            Assert.AreEqual(mm.Trim(), "Event field description");
        }

        [TestMethod]
        public void GenericField_Summary()
        {
            var m = GetReader();
            var mm = m.GetMemberComment(typeof(MyClass2).GetMember(nameof(MyClass2.genericTypeField)).First());
            Assert.AreEqual(mm.Trim(), "Generic field description");
        }

        [TestMethod]
        public void Constructor_Comments()
        {
            var m = GetReader();
            var constructors = typeof(MyClass2).GetConstructors();
            Assert.AreEqual(constructors.Length, 2);
            foreach (var constr in constructors)
            {
                var mm = m.GetMethodComments(constr);
                if (constr.GetParameters().Length == 0)
                {
                    Assert.AreEqual(mm.Parameters.Count, constr.GetParameters().Length);
                    Assert.AreEqual(mm.Summary.Trim(), "Constructor with no parameters");
                }
                else if (constr.GetParameters().Length == 1)
                {
                    Assert.AreEqual(mm.Parameters.Count, constr.GetParameters().Length);
                    Assert.AreEqual(mm.Parameters.First().Item1, "one");
                    Assert.AreEqual(mm.Parameters.First().Item2, "Parameter one");
                    Assert.AreEqual(mm.Summary.Trim(), "Constructor with one parameter");
                }
            }
        }

        [TestMethod]
        public void MemberFunction_Comments()
        {
            var m = GetReader();
            var mm = m.GetMethodComments(typeof(MyClass2).GetMethod(nameof(MyClass2.MemberFunction)));
            Assert.AreEqual(mm.Summary.Trim(), "Member function description");
            Assert.AreEqual(mm.Parameters.Count, 0);
            Assert.AreEqual(mm.Returns.Trim(), "Return value description");
            Assert.AreEqual(mm.Responses.First().Item1.Trim(), "200");
            Assert.AreEqual(mm.Responses.First().Item2.Trim(), "OK");
        }

        [TestMethod]
        public void MemberFunction_Overload_2_Params_Comments()
        {
            var m = GetReader();
            var mm = m.GetMethodComments(
                typeof(MyClass2).GetMethod(
                    nameof(MyClass2.MemberFunction2),
                     new [] { typeof(string), typeof(int).MakeByRefType()}));

            Assert.AreEqual(mm.Summary.Trim(), "Member function description. 2");
            Assert.AreEqual(mm.Parameters.Count, 2);
            Assert.AreEqual(mm.Parameters[0].Item1, "one");
            Assert.AreEqual(mm.Parameters[0].Item2, "Parameter one");
            Assert.AreEqual(mm.Parameters[1].Item1, "two");
            Assert.AreEqual(mm.Parameters[1].Item2, "Parameter two");
            Assert.AreEqual(mm.Returns.Trim(), "Return value description");

            mm = m.GetMethodComments(
                typeof(MyClass2).GetMethod(
                    nameof(MyClass2.MemberFunction2),
                    new[] { typeof(int), typeof(int).MakeByRefType() }));

            Assert.AreEqual(mm.Summary.Trim(), "Member function description. Overload 2");
            Assert.AreEqual(mm.Parameters.Count, 2);
            Assert.AreEqual(mm.Parameters[0].Item1, "one");
            Assert.AreEqual(mm.Parameters[0].Item2, "Parameter one");
            Assert.AreEqual(mm.Parameters[1].Item1, "two");
            Assert.AreEqual(mm.Parameters[1].Item2, "Parameter two");
            Assert.AreEqual(mm.Returns.Trim(), "Return value description");
        }

        [TestMethod]
        public void MemberFunctio_ArrayParams_Comments()
        {
            var m = GetReader();
            var mm = m.GetMethodComments(
                typeof(MyClass2).GetMethod(
                    nameof(MyClass2.MemberFunctionWithArray)));

            Assert.AreEqual(mm.Summary.Trim(), "MemberFunctionWithArray description");
            Assert.AreEqual(mm.Parameters.Count, 2);
            Assert.AreEqual(mm.Parameters[0].Item1, "array1");
            Assert.AreEqual(mm.Parameters[0].Item2, "Parameter array1");
            Assert.AreEqual(mm.Parameters[1].Item1, "array2");
            Assert.AreEqual(mm.Parameters[1].Item2, "Parameter array2");
            Assert.AreEqual(mm.Returns.Trim(), "Return value description");
        }

        [TestMethod]
        public void DelegateType_Comments()
        {
            var m = GetReader();
            var mm = m.GetTypeComments(typeof(MyClass2)
                .GetNestedType(nameof(MyClass2.DelegateType)));
            Assert.AreEqual(mm.Summary.Trim(), "Delegate type description");
            Assert.AreEqual(mm.Parameters.Count, 1);
            Assert.AreEqual(mm.Parameters[0].Item1, "parameter");
            Assert.AreEqual(mm.Parameters[0].Item2, "Parameter description");
        }

        [TestMethod]
        public void StaticOperator_Comments()
        {
            var m = GetReader();
            var minfo = typeof(MyClass2).GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "op_Addition");
            var mm = m.GetMethodComments(minfo);
            Assert.AreEqual(mm.Summary.Trim(), "Operator description");
            Assert.AreEqual(mm.Returns.Trim(), "Return value description");
            Assert.AreEqual(mm.Parameters.Count, 2);
            Assert.AreEqual(mm.Parameters[0].Item1, "param1");
            Assert.AreEqual(mm.Parameters[0].Item2, "Parameter param1");
            Assert.AreEqual(mm.Parameters[1].Item1, "param2");
            Assert.AreEqual(mm.Parameters[1].Item2, "Parameter param2");
        }

        [TestMethod]
        public void IndexerProperty_Comments()
        {
            var m = GetReader();
            var minfo = typeof(MyClass2).GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "get_Item");
            var mm = m.GetMethodComments(minfo);
            Assert.AreEqual(mm.Summary.Trim(), "Property description");
            Assert.AreEqual(mm.Returns.Trim(), "Return value description");
            Assert.AreEqual(mm.Parameters.Count, 1);
            Assert.AreEqual(mm.Parameters[0].Item1, "parameter");
            Assert.AreEqual(mm.Parameters[0].Item2, "Parameter description");
        }

        [TestMethod]
        public void ExplicitOperator_Comments()
        {
            var m = GetReader();
            var minfo = typeof(MyClass2).GetMethods().FirstOrDefault(
                mt => mt.IsSpecialName && mt.Name == "op_Explicit");
            var mm = m.GetMethodComments(minfo);
            Assert.AreEqual(mm.Summary.Trim(), "Operator description");
            Assert.AreEqual(mm.Returns.Trim(), "Return value description");
            Assert.AreEqual(mm.Parameters.Count, 1);
            Assert.AreEqual(mm.Parameters[0].Item1, "parameter");
            Assert.AreEqual(mm.Parameters[0].Item2, "Parameter description");
        }

        [TestMethod]
        public void TemplateMethod_Comments()
        {
            var m = GetReader();
            var minfo = typeof(MyClass2).GetMethod("TemplateMethod");
            var mm = m.GetMethodComments(minfo);
            Assert.AreEqual(mm.Summary.Trim(), "TemplateMethod description");
            Assert.AreEqual(mm.Returns.Trim(), "Return value description");
            Assert.AreEqual(mm.Parameters.Count,0);
        }

        [TestMethod]
        public void TemplateMethod_WithGenericParameter_Comments()
        {
            var m = GetReader();
            var minfo = typeof(MyClass2).GetMethods().FirstOrDefault(mt => mt.Name == "TemplateMethod2");
            var mm = m.GetMethodComments(minfo);
            Assert.AreEqual(mm.Summary.Trim(), "TemplateMethod2 description");
            Assert.AreEqual(mm.Returns.Trim(), "Return value description");
            Assert.AreEqual(mm.Parameters.Count, 1);
            Assert.AreEqual(mm.Parameters[0].Item1, "parameter");
            Assert.AreEqual(mm.Parameters[0].Item2, "Parameter description");
        }

        [TestMethod]
        public void TemplateMethod_With_2_GenericParameters_Comments()
        {
            var m = GetReader();
            var minfo = typeof(MyClass2).GetMethods().FirstOrDefault(mt => mt.Name == "TemplateMethod3");
            var mm = m.GetMethodComments(minfo);
            Assert.AreEqual(mm.Summary.Trim(), "TemplateMethod3 description");
            Assert.AreEqual(mm.Returns.Trim(), "Return value description");
            Assert.AreEqual(mm.Parameters.Count, 2);
            Assert.AreEqual(mm.Parameters[0].Item1, "parameter1");
            Assert.AreEqual(mm.Parameters[0].Item2, "Parameter description");
            Assert.AreEqual(mm.Parameters[1].Item1, "parameter2");
            Assert.AreEqual(mm.Parameters[1].Item2, "Parameter description");
        }

        [TestMethod]
        public void TemplateMethod_In_Base_Class_Comments()
        {
            var m = GetReader();
            var minfo = typeof(MyClass3).GetMethods().FirstOrDefault(mt => mt.Name == "TemplateMethod3");
            var mm = m.GetMethodComments(minfo);
            Assert.AreEqual(mm.Summary.Trim(), "TemplateMethod3 description");
            Assert.AreEqual(mm.Returns.Trim(), "Return value description");
            Assert.AreEqual(mm.Parameters.Count, 2);
            Assert.AreEqual(mm.Parameters[0].Item1, "parameter1");
            Assert.AreEqual(mm.Parameters[0].Item2, "Parameter description");
            Assert.AreEqual(mm.Parameters[1].Item1, "parameter2");
            Assert.AreEqual(mm.Parameters[1].Item2, "Parameter description");
        }
    }
}
