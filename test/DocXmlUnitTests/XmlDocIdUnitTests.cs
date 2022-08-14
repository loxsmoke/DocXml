using DocXmlUnitTests.TestData;
using LoxSmoke.DocXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BindingFlags = System.Reflection.BindingFlags;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class XmlDocIdUnitTests
    {
        [TestMethod]
        public void MethodId_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.MethodId(null));
        }

        [TestMethod]
        public void MemberId_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.MemberId(null));
        }

        [TestMethod]
        public void PropertyId_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.PropertyId(null));
        }

        [TestMethod]
        public void PropertyId_NonProperty()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.stringField)).First();
            Assert.ThrowsException<ArgumentException>(() => XmlDocId.PropertyId(info));
        }

        [TestMethod]
        public void FieldId_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.FieldId(null));
        }

        [TestMethod]
        public void FieldId_NonField()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.ValProperty)).First();
            Assert.ThrowsException<ArgumentException>(() => XmlDocId.FieldId(info));
        }

        [TestMethod]
        public void EventId_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.EventId(null));
        }

        [TestMethod]
        public void EventId_NonEvent()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.ValProperty)).First();
            Assert.ThrowsException<ArgumentException>(() => XmlDocId.EventId(info));
        }

        [TestMethod]
        public void EnumValueIdId_TypeNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.EnumValueId(null, string.Empty));
        }
        [TestMethod]
        public void EnumValueIdId_ValueNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => XmlDocId.EnumValueId(typeof(TestEnumWithValueComments), null));
        }
        [TestMethod]
        public void EnumValueIdId_NonEnum()
        {
            Assert.ThrowsException<ArgumentException>(() => XmlDocId.EnumValueId(typeof(MyClass), "notnull"));
        }

        [TestMethod]
        public void MemberId_Constructor()
        {
            var info = typeof(MyClass).GetConstructor(Array.Empty<Type>());
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.#ctor", id);
        }

        [TestMethod]
        public void MemberId_ConstructorWithParam()
        {
            var info = typeof(MyClass).GetConstructor(new[] { typeof(int) });
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.#ctor(System.Int32)", id);
        }

        [TestMethod]
        public void MemberId_ConstructorWithGenericParam()
        {
            var info = typeof(MyClass).GetConstructor(new[] { typeof(List<int>) });
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.#ctor(System.Collections.Generic.List{System.Int32})", id);
        }

        [TestMethod]
        public void MemberId_ConstructorWithGenericArrayParam()
        {
            var info = typeof(MyClass).GetConstructor(new[] { typeof(List<int>[]) });
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.#ctor(System.Collections.Generic.List{System.Int32}[])", id);
        }

        [TestMethod]
        public void MemberId_ConstructorWithGenericArrayInParam()
        {
            var info = typeof(MyClass).GetConstructor(new[] { typeof(List<int>[]).MakeByRefType() });
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.#ctor(System.Collections.Generic.List{System.Int32}[]@)", id);
        }

        [TestMethod]
        public void MemberId_Method()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.MemberFunction)).First();
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.MemberFunction", id);
        }

        [TestMethod]
        public void MemberId_MethodWithRefParam()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.MemberFunction2), new[] { typeof(int), typeof(int).MakeByRefType() });
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.MemberFunction2(System.Int32,System.Int32@)", id);
        }

        [TestMethod]
        public void MemberId_MethodWithArrayParam()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithArray));
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.MemberFunctionWithArray(System.Int16[],System.Int32[0:,0:])", id);
        }

        [TestMethod]
        public void MemberId_MethodWithGenericArrayParam()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithGenericArray));
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.MemberFunctionWithGenericArray(System.Collections.Generic.List{System.Int32}[])", id);
        }

        [TestMethod]
        public void MemberId_MethodWithGenericMultiDimArrayParam()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithGenericMultiDimArray));
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.MemberFunctionWithGenericMultiDimArray(System.Collections.Generic.List{System.Int32}[0:,0:])", id);
        }

        [TestMethod]
        public void MemberId_MethodWithGenericJaggedArrayParam()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithGenericJaggedArray));
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.MemberFunctionWithGenericJaggedArray(System.Collections.Generic.List{System.Int32}[][])", id);
        }

        [TestMethod]
        public void MemberId_MethodWithGenericJaggedArrayOutParam()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.MemberFunctionWithGenericOutArray));
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.MemberFunctionWithGenericOutArray(System.Collections.Generic.List{System.Single}[]@)", id);
        }

        [TestMethod]
        public void MemberId_TemplateMethod()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.TemplateMethod));
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.TemplateMethod``1", id);
        }

        [TestMethod]
        public void MemberId_TemplateMethodWithGenericParam()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.TemplateMethod2));
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.TemplateMethod2``1(System.Collections.Generic.List{``0})", id);
        }

        [TestMethod]
        public void MemberId_TemplateMethodWithTwoTemplateTypes()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.TemplateMethod3));
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.TemplateMethod3``2(System.Collections.Generic.List{``0},System.Collections.Generic.List{``1})", id);
        }

        [TestMethod]
        public void MemberId_TemplateMethodWithGenericJaggedArrayInParam()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.TemplateMethod4));
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.TemplateMethod4``1(System.Collections.Generic.List{``0}[][][]@)", id);
        }

        [TestMethod]
        public void MemberId_TemplateMethodWithGenericParamsArray()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.TemplateMethod5));
            var id   = info.MemberId();

            Assert.AreEqual("M:DocXmlUnitTests.MyClass.TemplateMethod5``1(``0[])",id);
        }

        [TestMethod]
        public void MemberId_TemplateMethodWithGenericOutParam()
        {
            var info = typeof(MyClass).GetMethod(nameof(MyClass.TemplateMethod6));
            var id   = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.MyClass.TemplateMethod6``1(System.Object,``0@)", id);
        }

        [TestMethod]
        public void MemberId_Property()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.GetSetProperty)).First();
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.MyClass.GetSetProperty", id);
        }

        [TestMethod]
        public void MemberId_ItemSetOnlyProperty()
        {
            var info = typeof(MyClass.NestedClass).GetMember(nameof(MyClass.NestedClass.Item)).First();
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.MyClass.NestedClass.Item", id);
        }

        [TestMethod]
        public void MemberId_Indexer()
        {
            var info = typeof(MyClass).GetProperty("Item", new[] { typeof(string) });
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.MyClass.Item(System.String)", id);
        }

        [TestMethod]
        public void MemberId_GetOnlyIndexer()
        {
            var info = typeof(MyClass.ClassWithGetOnlyIndexer).GetProperty("Item");
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.MyClass.ClassWithGetOnlyIndexer.Item(System.Int32,System.String)", id);
        }

        [TestMethod]
        public void MemberId_SetOnlyIndexer()
        {
            var info = typeof(MyClass.ClassWithSetOnlyIndexer).GetProperty("Item");
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.MyClass.ClassWithSetOnlyIndexer.Item(System.Int32,System.String)", id);
        }

        [TestMethod]
        public void MemberId_IndexerWithTwoParams()
        {
            var info = typeof(MyClass).GetProperty("Item", new[] { typeof(int), typeof(string) });
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.MyClass.Item(System.Int32,System.String)", id);
        }

        [TestMethod]
        public void MemberId_IndexerWithInParam()
        {
            var info = typeof(MyClass).GetProperty("Item", new[] { typeof(int).MakeByRefType() });
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.MyClass.Item(System.Int32@)", id);
        }

        [TestMethod]
        public void MemberId_IndexerWithGenericParam()
        {
            var info = typeof(MyClass).GetProperty("Item", new[] { typeof(List<int>) });
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.MyClass.Item(System.Collections.Generic.List{System.Int32})", id);
        }

        [TestMethod]
        public void MemberId_IndexerWithGenericArrayParam()
        {
            var info = typeof(MyClass).GetProperty("Item", new[] { typeof(List<int>[]) });
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.MyClass.Item(System.Collections.Generic.List{System.Int32}[])", id);
        }

        [TestMethod]
        public void MemberId_IndexerWithGenericMultiDimArrayInParam()
        {
            var info = typeof(MyClass).GetProperty("Item", new[] { typeof(List<int>[,,]).MakeByRefType() });
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.MyClass.Item(System.Collections.Generic.List{System.Int32}[0:,0:,0:]@)", id);
        }

        [TestMethod]
        public void MemberId_Field()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.stringField)).First();
            var id = info.MemberId();
            Assert.AreEqual("F:DocXmlUnitTests.MyClass.stringField", id);
        }

        [TestMethod]
        public void MemberId_NestedType()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.NestedClass)).First();
            var id = info.MemberId();
            Assert.AreEqual("T:DocXmlUnitTests.MyClass.NestedClass", id);
        }

        [TestMethod]
        public void MemberId_Event()
        {
            var info = typeof(MyClass).GetMember(nameof(MyClass.eventField)).First();
            var id = info.MemberId();
            Assert.AreEqual("E:DocXmlUnitTests.MyClass.eventField", id);
        }

        [TestMethod]
        public void MemberId_Unsupported()
        {
            var info = typeof(MyClass);
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                XmlDocId.MemberId(info);
            });
        }

        [TestMethod]
        public void TypeId_TemplateClass()
        {
            var info = typeof(MyTemplateClass<,>);
            var id = info.TypeId();
            Assert.AreEqual("T:DocXmlUnitTests.TestData.MyTemplateClass`2", id);
        }

        [TestMethod]
        public void MemberId_TemplateClassCtor()
        {
            var info = typeof(MyTemplateClass<,>).GetConstructor(Type.EmptyTypes);
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.TestData.MyTemplateClass`2.#ctor", id);
        }

        [TestMethod]
        public void MemberId_TemplateClassMethodUsingClassTypeParams()
        {
            var info = typeof(MyTemplateClass<,>).GetMethod("Foo");
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.TestData.MyTemplateClass`2.Foo(`0,System.Collections.Generic.List{`1})", id);
        }

        [TestMethod]
        public void MemberId_TemplateClassMethodUsingOnlyOwnTypeParams()
        {
            var info = typeof(MyTemplateClass<,>).GetMethod("Bar");
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.TestData.MyTemplateClass`2.Bar``2(``0,System.Collections.Generic.List{``1})", id);
        }

        [TestMethod]
        public void MemberId_TemplateClassMethodMixingTypeParamsFromClassAndMethod()
        {
            var info = typeof(MyTemplateClass<,>).GetMethod("Qux");
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.TestData.MyTemplateClass`2.Qux``2(`0,System.Collections.Generic.List{`1},``0,System.Collections.Generic.List{``1})", id);
        }

        [TestMethod]
        public void MemberId_TemplateClassIndexerUsingClassTypeParams()
        {
            var info = typeof(MyTemplateClass<,>).GetProperty("Item");
            var id = info.MemberId();
            Assert.AreEqual("P:DocXmlUnitTests.TestData.MyTemplateClass`2.Item(`0,System.Collections.Generic.List{`1})", id);
        }

        [TestMethod]
        public void MemberId_NestedTemplateClassMethodMixingTypeParamsFromParentClassNestedClassAndMethod()
        {
            var info = typeof(MyTemplateClass<,>.MyNestedTemplateClass<,>).GetMethod("Baz");
            var id = info.MemberId();
            Assert.AreEqual("M:DocXmlUnitTests.TestData.MyTemplateClass`2.MyNestedTemplateClass`2.Baz``2(`0,System.Collections.Generic.List{`1},`2,System.Collections.Generic.List{`3},``0,System.Collections.Generic.List{``1})", id);
        }

        [TestMethod]
        public void GenericInterface()
        {
            var id = typeof(GenericTestInterface<>).TypeId();
            Assert.AreEqual("T:DocXmlUnitTests.TestData.GenericTestInterface`1", id);
        }
    }
}
