using System.Collections.Generic;

namespace DocXmlUnitTests.TestData
{
    /// <summary>Class having generic type parameters.</summary>
    public class MyTemplateClass<T0, T1>
    {
        /// <summary>Template class ctor</summary>
        public MyTemplateClass() { }

        /// <summary>Method using generic type parameters of class.</summary>
        public void Foo(T0 a, List<T1> b) { }

        /// <summary>Method with own generic type parameters.</summary>
        public void Bar<T2, T3>(T2 a, List<T3> b) { }

        /// <summary>Method mixing generic type parameters of class and method.</summary>
        public void Qux<T2, T3>(T0 a, List<T1> b, T2 c, List<T3> d) { }

        /// <summary>Indexer using generic type parameters of class.</summary>
        public T0 this[T0 a, List<T1> b]
        {
            get { return default; }
            set { }
        }

        /// <summary>Nested class having generic type parameters.</summary>
        public class MyNestedTemplateClass<T2, T3>
        {
            /// <summary>Method mixing generic type parameters of parent class, nested class, and method.</summary>
            public void Baz<T4, T5>(T0 a, List<T1> b, T2 c, List<T3> d, T4 e, List<T5> f) { }
        }
    }
}
