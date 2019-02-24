using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using DocXml.Reflection;
using System.Reflection;
using DocXmlUnitTests.TestData.Reflection;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class ReflectionExtensionsUnitTests
    {
        [DataTestMethod]        
        [DataRow(typeof(DateTime), "DateTime")]
        [DataRow(typeof(double), "double")]
        [DataRow(typeof(float), "float")]
        [DataRow(typeof(decimal), "decimal")]
        [DataRow(typeof(sbyte), "sbyte")]
        [DataRow(typeof(byte), "byte")]
        [DataRow(typeof(char), "char")]
        [DataRow(typeof(short), "short")]
        [DataRow(typeof(ushort), "ushort")]
        [DataRow(typeof(int), "int")]
        [DataRow(typeof(uint), "uint")]
        [DataRow(typeof(long), "long")]
        [DataRow(typeof(ulong), "ulong")]
        [DataRow(typeof(bool), "bool")]
        [DataRow(typeof(void), "void")]
        [DataRow(typeof(string), "string")]

        [DataRow(typeof(bool?), "bool?")]
        [DataRow(typeof(List<bool?>), "List<bool?>")]
        [DataRow(typeof(List<>), "List<T>")]
        [DataRow(typeof(bool[]), "bool[]")]
        [DataRow(typeof(bool[,]), "bool[,]")]
        [DataRow(typeof(bool[,,]), "bool[,,]")]
        public void ToNameString(Type type, string expectedText)
        {
            var text = type.ToNameString();
            Assert.AreEqual(expectedText, text);

            var text2 = type.ToNameString((_, queue) => null);
            Assert.AreEqual(expectedText, text2);
        }

        [DataTestMethod]
        [DataRow(typeof(DateTime), false)]
        [DataRow(typeof(DateTime?), true)]
        [DataRow(typeof(string), false)]
        public void IsNullable(Type type, bool expectedNullable)
        {
            Assert.AreEqual(expectedNullable, type.IsNullable());
        }

        [DataTestMethod]
        [DataRow(typeof(MethodsReflectionClass), nameof(MethodsReflectionClass.PublicMethod), "()")]
        [DataRow(typeof(MethodsReflectionClass), nameof(MethodsReflectionClass.GetTuple1), "()")]
        [DataRow(typeof(MethodsReflectionClass), nameof(MethodsReflectionClass.GetTuple2), "((string Three, string Four) tupleParam)")]
        [DataRow(typeof(MethodsReflectionClass), nameof(MethodsReflectionClass.GetTuple3), "((string, string) unnamedTupleParam)")]
        [DataRow(typeof(MethodsReflectionClass), nameof(MethodsReflectionClass.GetTuple4), "((string, string) unnamedTupleParam, (string Three, string Four) tupleParam)")]
        [DataRow(typeof(MethodsReflectionClass), nameof(MethodsReflectionClass.GetTuple5), "((string A1, string A2, string A3, string A4, string A5, string A6, string A7) tupleParam)")]
        [DataRow(typeof(MethodsReflectionClass), nameof(MethodsReflectionClass.GetTuple6), "((string A1, string A2, string A3, string A4, string A5, string A6, (string A8, string A9)) tupleParam)")]
        [DataRow(typeof(MethodsReflectionClass), nameof(MethodsReflectionClass.GetTuple7), "((string A1, (string A2, string A3), (string, string) A4, (string A5, string A6) A7) tupleParam)")]
        public void ToParametersString(Type type, string methodName, string expectedText)
        {
            var methodInfo = type.GetMethod(methodName);
            var text = methodInfo.ToParametersString();
            Assert.AreEqual(expectedText, text);
        }

        [DataTestMethod]
        [DataRow(typeof(MethodsReflectionClass), nameof(MethodsReflectionClass.PublicMethod), "void")]
        [DataRow(typeof(MethodsReflectionClass), nameof(MethodsReflectionClass.GetTuple1), "(string One, string Two)")]
        public void ToTypeNameString_MethodReturn(Type type, string methodName, string expectedText)
        {
            var methodInfo = type.GetMethod(methodName);
            var text = methodInfo.ToTypeNameString();
            Assert.AreEqual(expectedText, text);
        }

        [DataTestMethod]
        [DataRow(typeof(TCTestPropertyClass), nameof(TCTestPropertyClass.DoubleListProperty), "List<double>")]
        [DataRow(typeof(TCTestPropertyClass), nameof(TCTestPropertyClass.TupleProperty), "(int One, int Two)")]
        public void ToTypeNameString_Property(Type type, string propName, string expectedText)
        {
            var propInfo = type.GetProperty(propName);
            var text = propInfo.ToTypeNameString();
            Assert.AreEqual(expectedText, text);
        }

        [DataTestMethod]
        [DataRow(typeof(FieldsReflectionClass), nameof(FieldsReflectionClass.PublicField), "int")]
        [DataRow(typeof(FieldsReflectionClass), nameof(FieldsReflectionClass.TupleField), "(int One, int Two)")]
        public void ToTypeNameString_Field(Type type, string fieldName, string expectedText)
        {
            var fieldInfo = type.GetField(fieldName);
            var text = fieldInfo.ToTypeNameString();
            Assert.AreEqual(expectedText, text);
        }
    }
}
