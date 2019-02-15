using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DocXml.Reflection;

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
        }

        [DataTestMethod]
        [DataRow(typeof(DateTime), false)]
        [DataRow(typeof(DateTime?), true)]
        [DataRow(typeof(string), false)]
        public void IsNullable(Type type, bool expectedNullable)
        {
            Assert.AreEqual(expectedNullable, type.IsNullable());
        }
    }
}
