using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocXml.Reflection
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Dictionary containing mapping of type to type names.
        /// </summary>
        public static Dictionary<Type, string> KnownTypeNames
            => _knownTypeNames ?? (_knownTypeNames = CreateKnownTypeNamesDictionary());

        private static Dictionary<Type, string> _knownTypeNames;

        /// <summary>
        /// Creates default dictionary of standard value types plus string type. 
        /// </summary>
        /// <returns>Dictionary of type to type names</returns>
        public static Dictionary<Type, string> CreateKnownTypeNamesDictionary()
        {
            return new Dictionary<Type, string>()
            {
                {typeof(DateTime), "DateTime"},
                {typeof(double), "double"},
                {typeof(float), "float"},
                {typeof(decimal), "decimal"},
                {typeof(sbyte), "sbyte"},
                {typeof(byte), "byte"},
                {typeof(char), "char"},
                {typeof(short), "short"},
                {typeof(ushort), "ushort"},
                {typeof(int), "int"},
                {typeof(uint), "uint"},
                {typeof(long), "long"},
                {typeof(ulong), "ulong"},
                {typeof(bool), "bool"},

                {typeof(void), "void"},

                {typeof(string), "string" }
            };
        }

        /// <summary>
        /// Check if this is nullable type. 
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>True if type is nullable</returns>
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Convert type to the proper type name.
        /// </summary>
        /// <param name="type">Type information.</param>
        /// <returns>Full type name</returns>
        public static string ToNameString(this Type type)
        {
            if (KnownTypeNames.ContainsKey(type))
            {
                return KnownTypeNames[type];
            }
            if (IsNullable(type))
            {
                return ToNameString(type.GenericTypeArguments[0]) + "?";
            }
            if (type.IsGenericType)
            {
                return type.Name.Substring(0, type.Name.IndexOf('`')) + "<" +
                       string.Join(", ", type.GetGenericArguments().Select(ToNameString)) + ">";
            }
            if (type.IsArray)
            {
                return type.GetElementType().ToNameString() +
                       "[" + 
                       (type.GetArrayRank() > 1 ? new string(',', type.GetArrayRank() - 1) : "") + 
                       "]";
            }
            return type.Name;
        }
    }
}
