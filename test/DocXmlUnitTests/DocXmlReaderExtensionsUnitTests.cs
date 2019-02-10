using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DocXmlOtherLibForUnitTests;
using DocXmlUnitTests.TestData.Reflection;
using LoxSmoke.DocXml.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LoxSmoke.DocXml.Reflection.DocXmlReaderExtensions;

#pragma warning disable CS1591

namespace DocXmlUnitTests
{
    [TestClass]
    public class DocXmlReaderExtensionsUnitTests : BaseTestClass
    {
        [TestInitialize]
        public new void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void DocXmlReaderExtensions_PropertyComments()
        {
            var tc = TypeCollection.ForReferencedTypes(typeof(PropertiesReflectionClass));
            var props = Reader.Comments(tc.ReferencedTypes[typeof(PropertiesReflectionClass)].Properties)
                .ToDictionary(d => d.Info.Name, d => d.Comments);
            Assert.AreEqual(7, props.Count);
            Assert.AreEqual("PublicGetProp", props["PublicGetProp"].Summary);
            Assert.AreEqual("PublicSetProp", props["PublicSetProp"].Summary);
            Assert.AreEqual("PublicGetSetProp", props["PublicGetSetProp"].Summary);
            Assert.AreEqual("ProtectedGetSetProp", props["ProtectedGetSetProp"].Summary);
            Assert.AreEqual("PrivateGetSetProp", props["PrivateGetSetProp"].Summary);
            Assert.AreEqual("InternalGetSetProp", props["InternalGetSetProp"].Summary);
            Assert.AreEqual("PublicStaticGetSetProp", props["PublicStaticGetSetProp"].Summary);
        }

        [TestMethod]
        public void DocXmlReaderExtensions_MethodComments()
        {
            var settings = ReflectionSettings.Default;
            settings.MethodFlags |= BindingFlags.NonPublic;

            var tc = TypeCollection.ForReferencedTypes(typeof(PropertiesReflectionClass), settings);
            var methods = Reader.Comments(tc.ReferencedTypes[typeof(PropertiesReflectionClass)].Methods)
                .ToDictionary(d => d.Info.Name, d => d.Comments);
            Assert.AreEqual(5, methods.Count);
            Assert.AreEqual("PublicMethod", methods["PublicMethod"].Summary);
            Assert.AreEqual("ProtectedMethod", methods["ProtectedMethod"].Summary);
            Assert.AreEqual("PrivateMethod", methods["PrivateMethod"].Summary);
            Assert.AreEqual("InternalMethod", methods["InternalMethod"].Summary);
            Assert.AreEqual("PublicStaticMethod", methods["PublicStaticMethod"].Summary);
        }
        [TestMethod]
        public void DocXmlReaderExtensions_FieldComments()
        {
            var settings = ReflectionSettings.Default;
            settings.FieldFlags |= BindingFlags.NonPublic; 
            var tc = TypeCollection.ForReferencedTypes(typeof(PropertiesReflectionClass),settings);
            var fields = Reader.Comments(tc.ReferencedTypes[typeof(PropertiesReflectionClass)].Fields)
                .ToDictionary(d => d.Info.Name, d => d.Comments);
            Assert.AreEqual(5, fields.Count);
            Assert.AreEqual("PublicField", fields["PublicField"].Summary);
            Assert.AreEqual("ProtectedField", fields["ProtectedField"].Summary);
            Assert.AreEqual("PrivateField", fields["PrivateField"].Summary);
            Assert.AreEqual("InternalField", fields["InternalField"].Summary);
            Assert.AreEqual("PublicStaticField", fields["PublicStaticField"].Summary);
        }
    }
}
