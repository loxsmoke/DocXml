using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocXml.Reflection
{
    /// <summary>
    /// Reflection extension methods with supporting properties.
    /// </summary>
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
        /// <returns>True if type is nullable like int? or Nullable&lt;Something&gt;</returns>
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Convert type to the proper type name.
        /// Optional <paramref name="typeNameConverter"/> function can convert type names
        /// to strings if type names should be decorated in some way either by adding links
        /// or formatting.
        /// </summary>
        /// <param name="type">Type information.</param>
        /// <param name="typeNameConverter">Optional function that converts type name to string.</param>
        /// <returns>Full type name</returns>
        public static string ToNameString(this Type type, Func<Type, string> typeNameConverter = null)
        {
            var typeName = typeNameConverter?.Invoke(type);
            if (typeName != null) return typeName;

            if (KnownTypeNames.ContainsKey(type))
            {
                return KnownTypeNames[type];
            }
            if (IsNullable(type))
            {
                return type.GenericTypeArguments[0].ToNameString(typeNameConverter) + "?";
            }
            if (type.IsGenericType)
            {
                return type.Name.Substring(0, type.Name.IndexOf('`')) + "<" +
                       string.Join(", ", type.GetGenericArguments()
                           .Select(argType => argType.ToNameString(typeNameConverter))) + ">";
            }
            if (type.IsArray)
            {
                return type.GetElementType().ToNameString(typeNameConverter) +
                       "[" + 
                       (type.GetArrayRank() > 1 ? new string(',', type.GetArrayRank() - 1) : "") + 
                       "]";
            }
            return type.Name;
        }
    }
}
