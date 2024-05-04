using System;
using System.Collections.Generic;
using System.Reflection;
using DocXmlUnitTests.TestData.Reflection;
using LoxSmoke.DocXml.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class TypeCollectionUnitTests
    {
        [TestMethod]
        public void UnwrapType_SimpleValue()
        {
            var tc = new TypeCollection();
            tc.Settings = ReflectionSettings.Default;
            tc.Settings.AssemblyFilter = (c) => true;
            tc.UnwrapType(null, typeof(string));
            tc.UnwrapType(null, typeof(string));
            Assert.AreEqual(1, tc.ReferencedTypes.Count);
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(string)));
        }

        [TestMethod]
        public void UnwrapType_GenericType()
        {
            var tc = new TypeCollection();
            tc.Settings = ReflectionSettings.Default;
            tc.Settings.AssemblyFilter = (c) => true;
            tc.UnwrapType(null, typeof(List<Tuple<string, List<double[]>>>));
            Assert.AreEqual(4, tc.ReferencedTypes.Count);
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(string)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(double)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(List<>)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(Tuple<,>)));
        }

        [TestMethod]
        public void ForReferencedTypes_Type()
        {
            var settings = ReflectionSettings.Default;
            settings.AssemblyFilter = (a) => a.FullName.Contains("DocXml");

            var tc = TypeCollection.ForReferencedTypes(typeof(TCTestClass), settings);

            Assert.AreEqual(6, tc.ReferencedTypes.Count);
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(ReflectionTestEnum2)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestPropertyClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestListPropertyClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(ReflectionTestEnum1)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestParameterClass)));
            Assert.IsFalse(tc.ReferencedTypes.ContainsKey(typeof(TCTestParameterClass).MakeByRefType()));
        }

        [TestMethod]
        public void ForReferencedTypes_Assembly()
        {
            var settings = ReflectionSettings.Default;
            settings.AssemblyFilter = (a) => a.FullName.Contains("DocXml");

            var tc = TypeCollection.ForReferencedTypes(Assembly.GetExecutingAssembly(), settings);

            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(ReflectionTestEnum2)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestPropertyClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestListPropertyClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(ReflectionTestEnum1)));
        }

        [TestMethod]
        public void ForReferencedTypes_AssemblyEnum()
        {
            var settings = ReflectionSettings.Default;
            settings.AssemblyFilter = (a) => a.FullName.Contains("DocXml");
            var list = new List<Assembly>() {Assembly.GetExecutingAssembly()};
            var tc = TypeCollection.ForReferencedTypes(list, settings);

            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(ReflectionTestEnum2)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestPropertyClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(TCTestListPropertyClass)));
            Assert.IsTrue(tc.ReferencedTypes.ContainsKey(typeof(ReflectionTestEnum1)));
        }

        [TestMethod]
        public void GetReferencedTypes_DefaultSettings()
        {
            var tc = new TypeCollection();
            
            tc.GetReferencedTypes(typeof(TCTestClass));
            Assert.IsTrue(tc.ReferencedTypes.Count > 3);
        }
    }
}
