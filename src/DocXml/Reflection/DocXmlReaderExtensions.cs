using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace LoxSmoke.DocXml.Reflection
{
    /// <summary>
    /// DocXmlReader extension methods to retrieve type properties, methods and fields
    /// using reflection information.
    /// </summary>
    public static class DocXmlReaderExtensions
    {
        public static IEnumerable<(PropertyInfo Info, CommonComments Comments)>
            PropertyComments(this DocXmlReader reader, Type type, ReflectionSettings settings = null)
        {
            settings = settings ?? ReflectionSettings.Default;
            foreach (var info in type.GetProperties(settings.PropertyFlags))
            {
                yield return (info, reader.GetMemberComments(info));
            }
        }

        public static IEnumerable<(MethodBase Info, MethodComments Comments)>
            MethodComments(this DocXmlReader reader, Type type, ReflectionSettings settings = null)
        {
            settings = settings ?? ReflectionSettings.Default;
            foreach (var info in type.GetMethods(settings.MethodFlags))
            {
                yield return (info, reader.GetMethodComments(info));
            }
        }

        public static IEnumerable<(FieldInfo Info, CommonComments Comments)>
            FieldComments(this DocXmlReader reader, Type type, ReflectionSettings settings = null)
        {
            settings = settings ?? ReflectionSettings.Default;
            foreach (var info in type.GetFields(settings.FieldFlags))
            {
                yield return (info, reader.GetMemberComments(info));
            }
        }
    }
}
