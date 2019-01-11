using System;
using System.Collections.Generic;
using System.Text;

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
        public class Nested { }

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
        public int this[string parameter] { get { return 1; } }

        /// <summary>
        /// ItemGetSetProperty description
        /// </summary>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public int this[int parameter]
        {
            get { return 1; }
            set { }
        }

        /// <summary>
        /// Operator description
        /// </summary>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public static explicit operator int(MyClass parameter) { return 1; }

        /// <summary>
        /// TemplateMethod description
        /// </summary>
        /// <typeparam name="T">Type parameter</typeparam>
        /// <returns>Return value description</returns>
        public List<T> TemplateMethod<T>()
        {
            return null;
        }

        /// <summary>
        /// TemplateMethod2 description
        /// </summary>
        /// <typeparam name="T">Type parameter</typeparam>
        /// <param name="parameter">Parameter description</param>
        /// <returns>Return value description</returns>
        public List<T> TemplateMethod2<T>(List<T> parameter)
        {
            return null;
        }

        /// <summary>
        /// TemplateMethod3 description
        /// </summary>
        /// <typeparam name="X">Type parameter</typeparam>
        /// <typeparam name="Y">Type parameter</typeparam>
        /// <param name="parameter1">Parameter description</param>
        /// <param name="parameter2">Parameter description</param>
        /// <returns>Return value description</returns>
        public List<X> TemplateMethod3<X,Y>(List<X> parameter1, List<Y> parameter2)
        {
            return null;
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
    }
}

