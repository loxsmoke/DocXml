using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace LoxSmoke.DocXml
{
    /// <summary>
    /// Class that constructs IDs for XML documentation comments.
    /// </summary>
    public static class XmlDocId
    {
        // Element prefixes in XML documentation
        public const char MemberPrefix = 'M';
        public const char FieldPrefix = 'F';
        public const char PropertyPrefix = 'P';
        public const char EventPrefix = 'E';
        public const char TypePrefix = 'T';

        // Reserved constructor name in XML
        public const string ConstructorNameID = "#ctor";

        /// <summary>
        /// Get XML Id of type definition.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string TypeId(this Type type)
        {
            return TypePrefix + ":" + GetTypeXmlId(type);
        }

        /// <summary>
        /// Get XML Id of a class method
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static string MethodId(this MethodBase methodInfo)
        {
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            return (IsIndexerProperty(methodInfo) ? PropertyPrefix : MemberPrefix) + ":" +
                   GetTypeXmlId(methodInfo.DeclaringType) + "." +
                   GetMethodXmlId(methodInfo);
        }

        /// <summary>
        /// Get XML Id of any member of the type. 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static string MemberId(this MemberInfo memberInfo)
        {
            if (memberInfo == null) throw new ArgumentNullException(nameof(memberInfo));
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Constructor:
                case MemberTypes.Method:
                    return MethodId(memberInfo as MethodBase);
                case MemberTypes.Property:
                    return PropertyId(memberInfo);
                case MemberTypes.Field:
                    return FieldId(memberInfo);
                case MemberTypes.NestedType:
                    return TypeId(memberInfo as Type);
                case MemberTypes.TypeInfo:
                    break;
                case MemberTypes.Event:
                    return EventId(memberInfo);
            }
            throw new NotSupportedException($"{memberInfo.MemberType}");
        }

        /// <summary>
        /// Get XML Id of property
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static string PropertyId(this MemberInfo propertyInfo)
        {
            if (propertyInfo == null) throw new ArgumentNullException(nameof(propertyInfo));
            if ((propertyInfo.MemberType & MemberTypes.Property) == 0) throw new ArgumentException(nameof(propertyInfo));
            return PropertyPrefix + ":" + GetTypeXmlId(propertyInfo.DeclaringType) + "." + propertyInfo.Name;
        }

        /// <summary>
        /// Get XML Id of field
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public static string FieldId(this MemberInfo fieldInfo)
        {
            if (fieldInfo == null) throw new ArgumentNullException(nameof(fieldInfo));
            if ((fieldInfo.MemberType & MemberTypes.Field) == 0) throw new ArgumentException(nameof(fieldInfo));
            return FieldPrefix + ":" + GetTypeXmlId(fieldInfo.DeclaringType) + "." + fieldInfo.Name;
        }

        /// <summary>
        /// Get XML Id of event field
        /// </summary>
        /// <param name="eventInfo"></param>
        /// <returns></returns>
        public static string EventId(this MemberInfo eventInfo)
        {
            if (eventInfo == null) throw new ArgumentNullException(nameof(eventInfo));
            if ((eventInfo.MemberType & MemberTypes.Event) == 0) throw new ArgumentException(nameof(eventInfo));
            return EventPrefix + ":" + GetTypeXmlId(eventInfo.DeclaringType) + "." + eventInfo.Name;
        }

        /// <summary>
        /// Get XML Id of specified value of the enum type. 
        /// </summary>
        /// <param name="enumType">Enum type</param>
        /// <param name="enumName">The name of the value without type and namespace</param>
        /// <returns></returns>
        public static string EnumValueId(this Type enumType, string enumName)
        {
            if (enumType == null) throw new ArgumentNullException(nameof(enumType));
            if (enumName == null) throw new ArgumentNullException(nameof(enumName));
            if (!enumType.IsEnum) throw new ArgumentException(nameof(enumType));
            return FieldPrefix + ":" + GetTypeXmlId(enumType) + "." + enumName;
        }

        #region Non-public functions
        /// <summary>
        /// Gets the type's full name prepared for xml documentation format.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="isOut">Whether the declaring member for this type is an out directional parameter.</param>
        /// <param name="isMethodParameter">If the type is being used has a method parameter.</param>
        /// <returns>The full name.</returns>
        static string GetTypeXmlId(Type type, bool isOut = false, bool isMethodParameter = false)
        {
            // Generic parameters are referred as ``N where N is position of generic type
            if (type.IsGenericParameter) return $"``{type.GenericParameterPosition}";

            Type[] args = type.GetGenericArguments();
            string fullTypeName;
            var typeNamespace = type.Namespace == null ? "" : $"{type.Namespace}.";
            var outString = isOut ? "@" : "";

            if (type.MemberType == MemberTypes.TypeInfo && (type.IsGenericType || args.Length > 0) && (!type.IsClass || isMethodParameter))
            {
                var typeName = Regex.Replace(type.Name, "`[0-9]+", "");
                var paramString = string.Join(",",
                    args.Select(o => GetTypeXmlId(o, false, isMethodParameter)).ToArray());
                fullTypeName = $"{typeNamespace}{typeName}{{{paramString}}}{outString}";
            }
            else if (type.IsNested)
            {
                fullTypeName = $"{typeNamespace}{type.DeclaringType.Name}.{type.Name}{outString}";
            }
            else
            {
                fullTypeName = $"{typeNamespace}{type.Name}{outString}";
            }

            fullTypeName = fullTypeName.Replace("&", "");

            // Multi-dimensional arrays must have 0: for each dimension. Eg. [,,] has to become [0:,0:,0:]
            while (fullTypeName.Contains("[,"))
            {
                var index = fullTypeName.IndexOf("[,");
                var lastIndex = fullTypeName.IndexOf(']', index);
                fullTypeName = fullTypeName.Replace(
                    fullTypeName.Substring(index, lastIndex - index + 1),
                    "[" + new string('x', lastIndex - index - 1).Replace("x", "0:,") + "0:]");
            }
            return fullTypeName;
        }

        /// <summary>
        /// Get method element Id in XML document
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        static string GetMethodXmlId(MethodBase methodInfo)
        {
            // Calculate the parameter string as this is in the member name in the XML
            var parameters = new List<string>();
            foreach (var parameterInfo in methodInfo.GetParameters())
            {
                parameters.Add(GetTypeXmlId(parameterInfo.ParameterType, parameterInfo.IsOut | parameterInfo.ParameterType.IsByRef, true));
            }
            return $"{ShortMethodName(methodInfo)}" +
                   ((parameters.Count > 0) ? $"({string.Join(",", parameters)})" : "") + 
                   ExplicitImplicitPostfix(methodInfo);
        }

        /// <summary>
        /// Return true if this method is actually an indexer property.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        static bool IsIndexerProperty(MethodBase methodInfo)
        {
            return methodInfo.IsSpecialName && (methodInfo.Name == "get_Item" || methodInfo.Name == "set_Item");
        }

        /// <summary>
        /// Explicit/implicit operator may have return value appended to the name. 
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        static string ExplicitImplicitPostfix(MethodBase methodInfo)
        {
            if (!methodInfo.IsSpecialName || 
                (methodInfo.Name != "op_Explicit" && methodInfo.Name != "op_Implicit")) return "";
            return "~" + GetTypeXmlId((methodInfo as MethodInfo).ReturnType);
        }


        /// <summary>
        /// Get method name. Some methods have special names or like generic methods some extra information.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        static string ShortMethodName(MethodBase methodInfo)
        {
            if (methodInfo.IsConstructor) return ConstructorNameID;
            return (IsIndexerProperty(methodInfo) ? "Item" : methodInfo.Name) + 
                   ((methodInfo.IsGenericMethod ? ("``" + methodInfo.GetGenericArguments().Length) : ""));
        }
        #endregion
    }
}
