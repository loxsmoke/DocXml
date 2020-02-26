﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace DocXml.Reflection
{
    /// <summary>
    /// Reflection extension methods with supporting properties.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// A dictionary containing a mapping of type to type names.
        /// </summary>
        public static Dictionary<Type, string> KnownTypeNames
            => _knownTypeNames ?? (_knownTypeNames = CreateKnownTypeNamesDictionary());

        private static Dictionary<Type, string> _knownTypeNames;

        /// <summary>
        /// Create a dictionary of standard value types and a string type. 
        /// </summary>
        /// <returns>Dictionary mapping types to type names</returns>
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
        /// Checks if the specified type is a nullable value type. 
        /// Returns false for object references.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>True if the type is nullable like int? or Nullable&lt;Something&gt;</returns>
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Convert type to the proper type name.
        /// Optional <paramref name="typeNameConverter"/> function can convert type names to strings 
        /// if type names should be decorated in some way either by converting text to markdown or 
        /// HTML links or adding some formatting.
        /// 
        /// This method returns ValueTuple types without field names. 
        /// </summary>
        /// <param name="type">Type information.</param>
        /// <param name="typeNameConverter">The optional function that converts type name to string.</param>
        /// <returns>Full type name</returns>
        public static string ToNameString(this Type type, Func<Type, string> typeNameConverter = null)
        {
            return type.ToNameString(null, typeNameConverter== null ? (Func<Type, Queue<string>, string>)null : (t, _) => typeNameConverter(t));
        }

        /// <summary>
        /// Convert type to the proper type name.
        /// Optional <paramref name="typeNameConverter"/> function can convert type names to strings 
        /// if type names should be decorated in some way either by converting text to markdown or 
        /// HTML links or adding some formatting.
        /// 
        /// This method returns ValueTuple types without field names. 
        /// </summary>
        /// <param name="type">Type information.</param>
        /// <param name="typeNameConverter">The optional function that converts type name to string.</param>
        /// <param name="invokeTypeNameConverterForGenericType">
        /// True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List&lt;SomeType&gt;
        /// If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.
        /// If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling 
        /// ToNameString() to avoid infinite recursion.  
        /// This is an optional parameter with default value of false.</param>
        /// <returns>Full type name</returns>
        public static string ToNameString(this Type type, Func<Type, Queue<string>, string> typeNameConverter,
            bool invokeTypeNameConverterForGenericType = false)
        {
            return type.ToNameString(null, typeNameConverter, invokeTypeNameConverterForGenericType);
        }

        /// <summary>
        /// Convert method parameters to the string. If method has no parameters then returned string is ()
        /// If parameters are present then returned string contains parameter names with their type names.
        /// Optional <paramref name="typeNameConverter"/> function can convert type names to strings 
        /// if type names should be decorated in some way either by converting text to markdown or 
        /// HTML links or adding some formatting.
        /// 
        /// This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). 
        /// </summary>
        /// <param name="methodInfo">Method information</param>
        /// <param name="typeNameConverter">The optional function that converts type name to string.</param>
        /// <param name="invokeTypeNameConverterForGenericType">
        /// True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List&lt;SomeType&gt;
        /// If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.
        /// If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling 
        /// ToNameString() to avoid infinite recursion.  
        /// This is an optional parameter with default value of false.</param>
        /// <returns>Full list of parameter types and their names</returns>
        public static string ToParametersString(this MethodBase methodInfo, Func<Type, Queue<string>, string> typeNameConverter = null,
            bool invokeTypeNameConverterForGenericType = false)
        {
            var parameters = methodInfo.GetParameters();
            if (parameters.Length == 0) return "()";
            return "(" + string.Join(", ", parameters.Select(s => s.ToTypeNameString(typeNameConverter, invokeTypeNameConverterForGenericType) + " " + s.Name)) + ")";
        }

        /// <summary>
        /// Convert method parameter type to the string.
        /// Optional <paramref name="typeNameConverter"/> function can convert type names to strings 
        /// if type names should be decorated in some way either by converting text to markdown or 
        /// HTML links or adding some formatting.
        /// 
        /// This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). 
        /// </summary>
        /// <param name="parameterInfo">Parameter information.</param>
        /// <param name="typeNameConverter">The optional function that converts type name to string.</param>
        /// <param name="invokeTypeNameConverterForGenericType">
        /// True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List&lt;SomeType&gt;
        /// If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.
        /// If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling 
        /// ToNameString() to avoid infinite recursion.  
        /// This is an optional parameter with default value of false.</param>
        /// <returns>Full type name of the parameter</returns>
        public static string ToTypeNameString(this ParameterInfo parameterInfo, Func<Type, Queue<string>, string> typeNameConverter = null,
            bool invokeTypeNameConverterForGenericType = false)
        {
            return parameterInfo.ParameterType.ToNameStringWithValueTupleNames(
                parameterInfo.GetCustomAttribute<TupleElementNamesAttribute>()?.TransformNames, typeNameConverter,
                invokeTypeNameConverterForGenericType);
        }

        /// <summary>
        /// Convert method return value type to the string.
        /// Optional <paramref name="typeNameConverter"/> function can convert type names to strings 
        /// if type names should be decorated in some way either by converting text to markdown or 
        /// HTML links or adding some formatting.
        /// 
        /// This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). 
        /// </summary>
        /// <param name="methodInfo">Method information.</param>
        /// <param name="typeNameConverter">The optional function that converts type name to string.</param>
        /// <param name="invokeTypeNameConverterForGenericType">
        /// True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List&lt;SomeType&gt;
        /// If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.
        /// If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling 
        /// ToNameString() to avoid infinite recursion.  
        /// This is an optional parameter with default value of false.</param>
        /// <returns>Full type name of the return value</returns>
        public static string ToTypeNameString(this MethodInfo methodInfo, Func<Type, Queue<string>, string> typeNameConverter = null,
            bool invokeTypeNameConverterForGenericType = false)
        {
            return methodInfo.ReturnType.ToNameStringWithValueTupleNames(
                methodInfo.ReturnParameter?.GetCustomAttribute<TupleElementNamesAttribute>()?.TransformNames, typeNameConverter,
                invokeTypeNameConverterForGenericType);
        }

        /// <summary>
        /// Convert property type to the string.
        /// Optional <paramref name="typeNameConverter"/> function can convert type names to strings 
        /// if type names should be decorated in some way either by converting text to markdown or 
        /// HTML links or adding some formatting.
        /// 
        /// This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). 
        /// </summary>
        /// <param name="propertyInfo">Property information.</param>
        /// <param name="typeNameConverter">The optional function that converts type name to string.</param>
        /// <param name="invokeTypeNameConverterForGenericType">
        /// True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List&lt;SomeType&gt;
        /// If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.
        /// If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling 
        /// ToNameString() to avoid infinite recursion.  
        /// This is an optional parameter with default value of false.</param>
        /// <returns>Full type name of the property</returns>
        public static string ToTypeNameString(this PropertyInfo propertyInfo, Func<Type, Queue<string>, string> typeNameConverter = null,
            bool invokeTypeNameConverterForGenericType = false)
        {
            return propertyInfo.PropertyType.ToNameStringWithValueTupleNames(
                propertyInfo.GetCustomAttribute<TupleElementNamesAttribute>()?.TransformNames, typeNameConverter,
                invokeTypeNameConverterForGenericType);
        }

        /// <summary>
        /// Convert field type to the string.
        /// Optional <paramref name="typeNameConverter"/> function can convert type names to strings 
        /// if type names should be decorated in some way either by converting text to markdown or 
        /// HTML links or adding some formatting.
        /// 
        /// This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). 
        /// </summary>
        /// <param name="fieldInfo">Field information.</param>
        /// <param name="typeNameConverter">The optional function that converts type name to string.</param>
        /// <param name="invokeTypeNameConverterForGenericType">
        /// True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List&lt;SomeType&gt;
        /// If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.
        /// If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling 
        /// ToNameString() to avoid infinite recursion.  
        /// This is an optional parameter with default value of false.</param>
        /// <returns>Full type name of the field</returns>
        public static string ToTypeNameString(this FieldInfo fieldInfo, Func<Type, Queue<string>, string> typeNameConverter = null,
            bool invokeTypeNameConverterForGenericType = false)
        {
            return fieldInfo.FieldType.ToNameStringWithValueTupleNames(
                fieldInfo.GetCustomAttribute<TupleElementNamesAttribute>()?.TransformNames, typeNameConverter,
                invokeTypeNameConverterForGenericType);
        }

        /// <summary>
        /// Convert type to the string.
        /// Optional <paramref name="typeNameConverter"/> function can convert type names to strings 
        /// if type names should be decorated in some way either by converting text to markdown or 
        /// HTML links or adding some formatting.
        /// 
        /// This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tupleNames">The names of the tuple fields from compiler-generated TupleElementNames attribute</param>
        /// <param name="typeNameConverter">The optional function that converts type name to string.</param>
        /// <param name="invokeTypeNameConverterForGenericType">
        /// True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List&lt;SomeType&gt;
        /// If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.
        /// If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling 
        /// ToNameString() to avoid infinite recursion.  
        /// This is an optional parameter with default value of false.</param>
        /// <returns>Full name of the specified type</returns>
        public static string ToNameStringWithValueTupleNames(this Type type, IList<string> tupleNames, Func<Type, Queue<string>, string> typeNameConverter = null,
            bool invokeTypeNameConverterForGenericType = false)
        {
            var tq = tupleNames == null ? null : new Queue<string>(tupleNames);
            return ToNameString(type, tq, typeNameConverter, invokeTypeNameConverterForGenericType);
        }

        /// <summary>
        /// Convert type to the proper type name.
        /// Optional <paramref name="typeNameConverter"/> function can convert type names to strings 
        /// if type names should be decorated in some way either by converting text to markdown or 
        /// HTML links or adding some formatting.
        /// 
        /// This method returns named tuples with field names like this (Type1 field1, Type2 field2).  <paramref name="tupleFieldNames"/> parameter
        /// must be specified with all tuple field names stored in the same order as they are in compiler-generated TupleElementNames attribute.
        /// If you do not know what it is then the better and easier way is to use ToTypeNameString() methods that retrieve field names from attributes.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tupleFieldNames">The names of value tuple fields as stored in TupleElementNames attribute. This queue is modified during call.</param>
        /// <param name="typeNameConverter">The optional function that converts type name to string.</param>
        /// <param name="invokeTypeNameConverterForGenericType">
        /// True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List&lt;SomeType&gt;
        /// If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.
        /// If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling 
        /// ToNameString() to avoid infinite recursion.  
        /// This is an optional parameter with default value of false.</param>
        /// <returns>Full type name</returns>
        public static string ToNameString(this Type type, Queue<string> tupleFieldNames, Func<Type, Queue<string>, string> typeNameConverter = null, 
            bool invokeTypeNameConverterForGenericType = false)
        {
            var refSymbol = type.IsByRef ? "&" : "";
            type = type.IsByRef ? type.GetElementType() : type;

            var decoratedTypeName = type.IsGenericType ? null : typeNameConverter?.Invoke(type, tupleFieldNames);

            if (decoratedTypeName != null &&
                (tupleFieldNames == null || tupleFieldNames.Count == 0))
            {
                // If there are no tuple field names then return the name from converter
                // Otherwise do full type name conversion to remove the proper number of tuple field names from the queue and then discard that name
                return decoratedTypeName;
            }

            string newTypeName = null;
            if (KnownTypeNames.ContainsKey(type))
            {
                newTypeName = KnownTypeNames[type];
            }
            else if (IsNullable(type))
            {
                newTypeName = type.GenericTypeArguments[0].ToNameString(tupleFieldNames, typeNameConverter, invokeTypeNameConverterForGenericType) + "?";
            }
            else if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (GenericTuples.Contains(genericTypeDefinition))
                {
                    // Tuple fields must not go breadth first as that is the order of names in the tupleFieldNamesQueue
                    var tupleFields = type.GetGenericArguments().Select((arg) => (argumentType: arg, argumentName: tupleFieldNames?.Dequeue())).ToList();
                    newTypeName = "(" +
                           string.Join(", ", tupleFields
                               .Select(arg => arg.argumentType.ToNameString(tupleFieldNames, typeNameConverter, invokeTypeNameConverterForGenericType) +
                               (arg.argumentName == null ? "" : (" " + arg.argumentName)))) + ")";
                }
                else if (type.Name.Contains('`'))
                {
                    var genericTypeName = invokeTypeNameConverterForGenericType ?
                        typeNameConverter?.Invoke(genericTypeDefinition, tupleFieldNames) : null;
                    newTypeName =
                        (genericTypeName ?? type.Name.CleanGenericTypeName()) + "<" +
                        string.Join(", ", type.GetGenericArguments()
                           .Select(argType => argType.ToNameString(tupleFieldNames, typeNameConverter, invokeTypeNameConverterForGenericType))) + ">";
                }
                else
                {
                    newTypeName = type.Name;
                }
            }
            else if (type.IsArray)
            {
                newTypeName = type.GetElementType().ToNameString(tupleFieldNames, typeNameConverter, invokeTypeNameConverterForGenericType) +
                       "[" +
                       (type.GetArrayRank() > 1 ? new string(',', type.GetArrayRank() - 1) : "") +
                       "]";
            }
            else
            {
                newTypeName = type.Name;
            }

            // If decoratedTypeName is not null then all formatting above was just for tuple name removal from the queue
            var typeName = decoratedTypeName ?? newTypeName;
            return typeName + refSymbol;
        }

        /// <summary>
        /// Hash of all possible ValueTuple type definitions for quick check if type is value tuple.
        /// </summary>
        static HashSet<Type> GenericTuples = new HashSet<Type>(new Type[] {
            typeof(ValueTuple<>),
            typeof(ValueTuple<,>),
            typeof(ValueTuple<,,>),
            typeof(ValueTuple<,,,>),
            typeof(ValueTuple<,,,,>),
            typeof(ValueTuple<,,,,,>),
            typeof(ValueTuple<,,,,,,>),
            typeof(ValueTuple<,,,,,,,>) });

        /// <summary>
        /// Remove the parameter count part of the generic type name. 
        /// For example the generic list type name is List`1.
        /// This method leaves only the name part of the type such as List.
        /// If specified string does not contain the number of parameters 
        /// part then the same string is returned.
        /// </summary>
        /// <param name="genericTypeName">Type name</param>
        /// <returns>Type name without the number of parameters.</returns>
        public static string CleanGenericTypeName(this string genericTypeName)
        {
            var index = genericTypeName.IndexOf('`');
            return (index < 0) ? genericTypeName : genericTypeName.Substring(0, index);
        }
    }
}
