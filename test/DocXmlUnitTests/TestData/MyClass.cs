using System;
using System.Collections.Generic;

namespace DocXmlUnitTests
{
    /// <summary>
    /// Enum type description
    /// </summary>
    public enum TestEnumWithValueComments
    {
        /// <summary>
        /// Enum value one
        /// </summary>
        Value1 = 10,
        
        /// <summary>
        /// Enum value two
        /// </summary>
        Value2 = 20
    };

    /// <summary>
    /// Enum type description
    /// </summary>
    public enum TestEnumUInt8 : byte
    {
        /// <summary>
        /// Enum value one
        /// </summary>
        Value1 = 10,
        
        /// <summary>
        /// Enum value two
        /// </summary>
        Value2 = 20
    };

    /// <summary>
    /// Enum type description
    /// </summary>
    public enum TestEnumUInt64 : ulong
    {
        /// <summary>
        /// Enum value one
        /// </summary>
        Value1 = 10,
        
        /// <summary>
        /// Enum value two
        /// </summary>
        Value2 = 20
    };

    /// <summary>
    /// Enum type description
    /// </summary>
    public enum TestEnumInt64 : long
    {
        /// <summary>
        /// Enum value one
        /// </summary>
        Value1 = 10,
        
        /// <summary>
        /// Enum value two
        /// </summary>
        Value2 = 20
    };

    /// <summary>
    /// Enum type description
    /// </summary>
    public enum TestEnumWithNegativeValues
    {
        /// <summary>
        /// Enum value one
        /// </summary>
        Value1 = -20,
        
        /// <summary>
        /// Enum value two
        /// </summary>
        Value2 = -10
    };

    /// <summary>
    /// Enum 2 type description
    /// </summary>
    public enum TestEnum2
    {
#pragma warning disable CS1591
        Value21,
        Value22
#pragma warning restore CS1591
    };

    /// <summary>
    /// This is MyClass
    /// </summary>
    public class MyClass
    {
        /// <summary>
        /// Enum property description
        /// </summary>
        public TestEnumWithValueComments ImportantEnum { get; set; }

        /// <summary>
        /// String field description
        /// </summary>
        public string stringField;

        /// <summary>
        /// Const field description
        /// </summary>
        public const double PI = 3.14;

        /// <summary>
        /// Value property description
        /// </summary>
        public int ValProperty { get { return 1; } set { } }

        /// <summary>
        /// Event field description
        /// </summary>
        public event DelegateType eventField;

        /// <summary>
        /// Generic field description
        /// </summary>
        public List<Tuple<int, string>> genericTypeField;

        /// <summary>
        /// Nested class
        /// </summary>
        public class Nested
        {
            /// <summary>
            /// Double nested class
            /// </summary>
            public class DoubleNested
            {
            }
        }

        /// <summary>
        /// Nested generic class
        /// </summary>
        public class Nested<T1>
        {
            /// <summary>
            /// Double nested generic class
            /// </summary>
            public class DoubleNested<T2>
            {
            }
        }

        /// <summary>
        /// Constructor with no parameters
        /// </summary>
        public MyClass() { }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="one">Parameter one</param>
        public MyClass(int one) { }

        /// <summary>
        /// Constructor with one generic parameter
        /// </summary>
        /// <param name="one">Parameter one</param>
        public MyClass(List<int> one) { }

        /// <summary>
        /// Constructor with one array parameter of generic type
        /// </summary>
        /// <param name="one">Parameter one</param>
        public MyClass(List<int>[] one) { }

        /// <summary>
        /// Constructor with one array in parameter of generic type
        /// </summary>
        /// <param name="one">Parameter one</param>
        public MyClass(in List<int>[] one) { }

        /// <summary>
        /// Member function description
        /// </summary>
        /// <returns>Return value description</returns>
        /// <response code="200">OK</response>
        public int MemberFunction() { return 1; }

        /// <summary>
        /// Member function description. 2
        /// </summary>
        /// <param name="one">Parameter one</param>
        /// <param name="two">Parameter two</param>
        /// <returns>Return value description</returns>
        public int MemberFunction2(string one, ref int two) { return 1; }

        /// <summary>
        /// Member function description. Overload 2
        /// </summary>
        /// <param name="one">Parameter one</param>
        /// <param name="two">Parameter two</param>
        /// <returns>Return value description</returns>
        public int MemberFunction2(int one, ref int two) { return 1; }

        /// <summary>
        /// MemberFunctionWithArray description
        /// </summary>
        /// <param name="array1">Parameter array1</param>
        /// <param name="array2">Parameter array2</param>
        /// <returns>Return value description</returns>
        public int MemberFunctionWithArray(short[] array1, int[,] array2) { return 0; }

        /// <summary>
        /// MemberFunctionWithGenericTypeArray description
        /// </summary>
        /// <param name="arrayOfListOfInt">Parameter arrayOfListOfInt</param>
        public static void MemberFunctionWithGenericArray(params List<int>[] arrayOfListOfInt) { }

        /// <summary>
        /// MemberFunctionWithGenericTypeMultiDimArray description
        /// </summary>
        /// <param name="multiDimArrayOfListOfInt">Parameter multiDimArrayOfListOfInt</param>
        public static void MemberFunctionWithGenericMultiDimArray(List<int>[,] multiDimArrayOfListOfInt) { }

        /// <summary>
        /// MemberFunctionWithGenericTypeJaggedArray description
        /// </summary>
        /// <param name="jaggedArrayOfListOfInt">Parameter jaggedArrayOfListOfInt</param>
        public static void MemberFunctionWithGenericJaggedArray(List<int>[][] jaggedArrayOfListOfInt) { }

        /// <summary>MemberFunctionWithGenericTypeOutArray description</summary>
        /// <param name="outArrayOfListOfInt">Parameter outArrayOfListOfInt</param>
        public static void MemberFunctionWithGenericOutArray(out List<float>[] outArrayOfListOfInt)
            => outArrayOfListOfInt = new[] { new List<float>() };

        /// <summary>MemberFunctionWithReadOnlyStringCollection description</summary>
        /// <param name="readOnlyStringCollection">Parameter readOnlyStringCollection</param>
        public static void MemberFunctionWithReadOnlyStringCollection(IReadOnlyCollection<string> readOnlyStringCollection)
        { }

        /// <summary>
        /// <para>
        /// First paragraph.
        /// </para>
        /// <para>
        /// Second paragraph.
        /// </para>
        /// </summary>
        public void MemberFunctionWithParaTagsInSummary() { }

        /// <summary>
        /// Delegate type description
        /// </summary>
        /// <param name="parameter">Parameter description</param>
        public delegate void DelegateType(int parameter);

        /// <summary>
        /// Operator description
        /// </summary>
        /// <param name="param1">Parameter param1</param>
        /// <param name="param2">Parameter param2</param>
        /// <returns>Return value description</returns>
        public static MyClass operator +(MyClass param1, MyClass param2) { return param1; }

        /// <summary>
        /// Property description
        /// </summary>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public int this[string parameter] => 1;

        /// <summary>
        /// ItemGetSetProperty description
        /// </summary>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public int this[int parameter]
        {
            get => 1;
            set { }
        }

        /// <summary>
        /// ItemPropertyTwoParams description
        /// </summary>
        /// <param name="parameter1">Parameter1 description</param>
        /// <param name="parameter2">Parameter2 description</param>
        /// <returns>Return value description</returns>
        public int this[int parameter1, string parameter2]
        {
            get => 1;
            set { }
        }

        /// <summary>
        /// ItemPropertyInParam description
        /// </summary>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public int this[in int parameter]  => 1;

        /// <summary>
        /// ItemPropertyGenericParam description
        /// </summary>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public int this[List<int> parameter] => 1;

        /// <summary>
        /// ItemPropertyGenericArrayParam description
        /// </summary>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public int this[List<int>[] parameter] => 1;

        /// <summary>
        /// ItemPropertyGenericMultiDimArrayInParam description
        /// </summary>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public int this[in List<int>[,,] parameter] => 1;

        /// <summary>
        /// Operator description
        /// </summary>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public static explicit operator int(MyClass parameter) => 1;

        /// <summary>
        /// TemplateMethod description
        /// </summary>
        /// <typeparam name="T">Type parameter</typeparam>
        /// <returns>Return value description</returns>
        public List<T> TemplateMethod<T>() => null;

        /// <summary>
        /// TemplateMethod2 description
        /// </summary>
        /// <typeparam name="T">Type parameter</typeparam>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public List<T> TemplateMethod2<T>(List<T> parameter) => null;

        /// <summary>
        /// TemplateMethod3 description
        /// </summary>
        /// <typeparam name="X">Type parameter</typeparam>
        /// <typeparam name="Y">Type parameter</typeparam>
        /// <param name="parameter1">Parameter description</param>
        /// <param name="parameter2">Parameter description</param>
        /// <returns>Return value description</returns>
        public List<X> TemplateMethod3<X, Y>(List<X> parameter1, List<Y> parameter2) => null;

        /// <summary>
        /// TemplateMethod4 description
        /// </summary>
        /// <typeparam name="T">Type parameter</typeparam>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public List<T> TemplateMethod4<T>(in List<T>[][][] parameter)
        {
            return null;
        }

        /// <summary>
        /// TemplateMethod5 description 
        /// </summary>
        /// <param name="parameter">parameter description</param>
        public void TemplateMethod5<T>(params T[] parameter)
        {

        }

        /// <summary>
        /// TemplateMethod6 description 
        /// </summary>
        /// <param name="parameter1">parameter1 description</param>
        /// <param name="parameter2">parameter2 description</param>
        public bool TemplateMethod6<TEnum>(object parameter1, out TEnum parameter2) where TEnum : struct//, Enum 
        {
            parameter2 = default;
            return false;
        }

        /// <summary>
        /// GetSetProperty comment
        /// </summary>
        /// <returns>prop return</returns>
        public string GetSetProperty
        {
            get => "test";
            set { }
        }

        /// <summary>
        /// NestedClass
        /// </summary>
        public class NestedClass
        {
#pragma warning disable CS1591
            public int Item { set { _ = value; } }
#pragma warning restore CS1591
        }

        /// <summary>Class having a get only indexer.</summary>
        public class ClassWithGetOnlyIndexer
        {
            /// <summary>Indexer allowing only get.</summary>
            public bool this[int i, string s] { get => true; }
        }

        /// <summary>Class having a set only indexer.</summary>
        public class ClassWithSetOnlyIndexer
        {
            /// <summary>Indexer allowing only set.</summary>
            public bool this[int i, string s] { set { } }
        }
    }
}

