using System;
using System.Collections.Generic;
using System.Text;

namespace DocXmlUnitTests.TestData.Reflection
{
    /// <summary>
    /// Test data class with methods
    /// </summary>
    public class MethodsReflectionClass
    {
        /// <summary>
        /// PublicMethod
        /// </summary>
        public void PublicMethod()
        {
        }

        /// <summary>
        /// ProtectedMethod
        /// </summary>
        protected void ProtectedMethod()
        {
        }

        /// <summary>
        /// PrivateMethod
        /// </summary>
        private void PrivateMethod()
        {
        }

        /// <summary>
        /// InternalMethod
        /// </summary>
        internal void InternalMethod()
        {
        }

        /// <summary>
        /// PublicStaticMethod
        /// </summary>
        public static void PublicStaticMethod()
        {
        }

        /// <summary>
        /// MethodWithRefParam
        /// </summary>
        public void MethodWithRefParam(ref int refParam) { }

        /// <summary>
        /// MethodWithOutParam
        /// </summary>
        public void MethodWithOutParam(out int outParam) { outParam = 0; }

        /// <summary>
        /// MethodWithInParam
        /// </summary>
        public void MethodWithInParam(in int inParam) { }

        /// <summary>
        /// MethodWithNullableParam
        /// </summary>
        public void MethodWithNullableParam(int? nullableParam) { }

        /// <summary>
        /// MethodWithNullableInParam
        /// </summary>
        public void MethodWithNullableInParam(in int? nullableInParam) { }

        /// <summary>
        /// Return one tuple
        /// </summary>
        /// <returns></returns>
        public (string One, string Two) GetTuple1()
        {
            return ("", "");
        }

        /// <summary>
        /// Return one tuple and param tuple
        /// </summary>
        /// <returns></returns>
        public (string One, string Two) GetTuple2((string Three, string Four) tupleParam)
        {
            return ("", "");
        }

        /// <summary>
        /// Return one tuple and ref param tuple
        /// </summary>
        /// <returns></returns>
        public (string One, string Two) GetTuple2Ref(ref (string Three, string Four) tupleParam) {
            return ("", "");
        }

        /// <summary>
        /// Return one tuple and unnamed param tuple
        /// </summary>
        /// <returns></returns>
        public (string One, string Two) GetTuple3((string, string) unnamedTupleParam)
        {
            return ("", "");
        }

        /// <summary>
        /// Return one tuple and unnamed ref param tuple
        /// </summary>
        /// <returns></returns>
        public (string One, string Two) GetTuple3Ref(ref (string, string) unnamedTupleParam) {
            return ("", "");
        }

        /// <summary>
        /// Return tuple and mixed named / unnamed param tuple
        /// </summary>
        /// <returns></returns>
        public (string One, string Two) GetTuple4((string, string) unnamedTupleParam, (string Three, string Four) tupleParam)
        {
            return ("", "");
        }

        /// <summary>
        /// Long param tuple
        /// </summary>
        /// <returns></returns>
        public void GetTuple5((string A1, string A2, string A3, string A4, string A5, string A6, string A7) tupleParam)
        {
        }

        /// <summary>
        /// Tuple in long tuple
        /// </summary>
        /// <returns></returns>
        public void GetTuple6((string A1, string A2, string A3, string A4, string A5, string A6, (string A8, string A9)) tupleParam)
        {
        }

        /// <summary>
        /// Tuple in tuple in tuple
        /// </summary>
        /// <returns></returns>
        public void GetTuple7((string A1, (string A2, string A3), (string, string) A4, (string A5, string A6) A7) tupleParam)
        {
        }

    }
}
