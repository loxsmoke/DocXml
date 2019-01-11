using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LoxSmoke.DocXml.Reflection
{
    public class ReflectionSettings
    {
        public static ReflectionSettings Default => new ReflectionSettings()
        {
            PropertyFlags = 
                BindingFlags.DeclaredOnly |
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static,

            MethodFlags =
                BindingFlags.DeclaredOnly |
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static,

            FieldFlags =
                BindingFlags.DeclaredOnly |
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static,

            NestedTypeFlags =
                BindingFlags.DeclaredOnly |
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static
        };

        public BindingFlags PropertyFlags { get; set; }
        public BindingFlags MethodFlags { get; set; }
        public BindingFlags FieldFlags { get; set; }
        public BindingFlags NestedTypeFlags { get; set; }
        /// <summary>
        /// Checks if specified type should be added to the set of referenced types.
        /// Function should return false if type should be ignored.
        /// </summary>
        public Func<Type,bool> ExamineTypes { get; set; }
        /// <summary>
        /// Checks if specified types of assembly should be added to the set of the 
        /// referencedd types.
        /// </summary>
        public Func<Assembly,bool> ExamineAssemblies { get; set; }
    }
}
