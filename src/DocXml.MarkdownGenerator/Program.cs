using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using LoxSmoke.DocXml;
using LoxSmoke.DocXml.Reflection;
using static LoxSmoke.DocXml.Reflection.DocXmlReaderExtensions;

namespace DocXml.MarkdownGenerator
{
    class Program
    {
        public class QQ
        {
            public StringBuilder sb = new StringBuilder();
            public void WriteLine(string text)
            {
                if (string.IsNullOrEmpty(text)) return;
                sb.AppendLine(text);
                sb.AppendLine();
                Console.WriteLine(text);
            }
            public void Write(string text)
            {
                if (string.IsNullOrEmpty(text)) return;
                sb.AppendLine(text);
                Console.WriteLine(text);
            }
        }

        static string Endlines(string text)
        {
            if (text == null) return "";
            text = text.Replace("<", "\\<");
            text = text.Replace(">", "\\>");
            return text.Replace("\r\n", "<br>");
        }

        static string ToString(ParameterInfo[] parameters)
        {
            if (parameters == null || parameters.Length == 0) return "()";
            return "(" + string.Join(", ", parameters.Select(s => ToString(s.ParameterType))) + ")";
        }

        static string ToString(Type type)
        {
            if (type.IsValueType)
            {
                if (type == typeof(int)) return "int";
                if (type == typeof(bool)) return "bool";
                if (type == typeof(char)) return "char";
                if (type == typeof(long)) return "long";
            }
            if (type == typeof(string)) return "string";
            if (type.IsGenericType)
            {
                return type.Name.Substring(0, type.Name.IndexOf('`')) + "\\<" + 
                       string.Join(", ", type.GetGenericArguments().Select(ToString)) + "\\>";
            }
            return type.Name;
        }

        static void Main(string[] args)
        {
            var q = new QQ();
            var doc = new DocXmlReader();
            var s = ReflectionSettings.Default;
            s.FieldFlags = BindingFlags.DeclaredOnly |
                           BindingFlags.Instance |
                           BindingFlags.Public |
                           BindingFlags.Static;
            s.MethodFlags = BindingFlags.DeclaredOnly |
                           BindingFlags.Instance |
                           BindingFlags.Public |
                           BindingFlags.Static;
            var myAssembly = Assembly.GetAssembly(typeof(DocXmlReader));
            q.WriteLine($"# {Path.GetFileName(myAssembly.ManifestModule.Name)} v.{myAssembly.GetName().Version} API documentation");
            var tc = TypeCollection.ForReferencedTypes(myAssembly, s);
            foreach (var t in tc.ReferencedTypes.Values
                .OrderBy(tt => tt.Type.Namespace)
                .ThenByDescending(tt => tt.ReferencesOut.Count)
                .ThenBy(tt => tt.Type.Name))
            {
                q.WriteLine("# " + t.Type.Name + " Class");
                q.WriteLine("Namespace: " + t.Type.Namespace);
                if (t.Type.BaseType != null && t.Type.BaseType != typeof(Object))
                {
                    q.WriteLine("Base class: " + t.Type.BaseType.Name);
                }

                var tcc = doc.GetTypeComments(t.Type);
                q.WriteLine(tcc.Summary);

                if (!string.IsNullOrEmpty(tcc.Example))
                {
                    q.WriteLine("## Examples");
                    q.WriteLine(tcc.Example);
                }

                if (!string.IsNullOrEmpty(tcc.Remarks))
                {
                    q.WriteLine("## Remarks");
                    q.WriteLine(tcc.Remarks);
                }

                var allProperties = doc.Comments(t.Properties).ToList();
                var allMethods = doc.Comments(t.Methods).ToList();
                var allFields = doc.Comments(t.Fields).ToList();

                if (allProperties.Count > 0)
                {
                    q.WriteLine("## Properties");
                    q.Write("| Name | Type | Summary |");
                    q.Write("|---|---|---|");
                    foreach (var prop in allProperties)
                    {
                        q.Write("| **" + prop.Info.Name + "** | " + 
                                ToString(prop.Info.PropertyType)+ " | " +
                                Endlines(prop.Comments.Summary) + " |");
                        //WriteLine("Property: " + prop.Info.PropertyType.Name + " " + prop.Info.Name);
                        //WriteLine(prop.Comments.Remarks);
                        //WriteLine(prop.Comments.Example);
                    }
                }

                if (allMethods.Count > 0 && allMethods.Any(m => m.Info is ConstructorInfo))
                {
                    q.WriteLine("## Constructors");
                    q.Write("| Name | Summary |");
                    q.Write("|---|---|");
                    foreach (var prop in allMethods
                        .Where(m => m.Info is ConstructorInfo)
                        .OrderBy(p => p.Info.GetParameters().Length))
                    {
                        q.Write("| **" + ToString(t.Type) + ToString((prop.Info as ConstructorInfo).GetParameters()) + "** | " +
                                Endlines(prop.Comments.Summary) + " |");
                    }
                }

                if (allMethods.Count > 0 && allMethods.Any(m => m.Info is MethodInfo))
                {
                    q.WriteLine("## Methods");
                    q.Write("| Name | Returns | Summary |");
                    q.Write("|---|---|---|");
                    foreach (var prop in allMethods
                        .Where(m => !(m.Info is ConstructorInfo))
                        .OrderBy(p => p.Info.Name)
                        .ThenBy(p => p.Info.GetParameters().Length))
                    {
                        var methodInfo = prop.Info as MethodInfo;
                        //todo: method name with params
                        q.Write("| **" + methodInfo.Name + ToString(methodInfo.GetParameters()) + "** | " +
                                ToString(methodInfo.ReturnType) + " | " +
                                Endlines(prop.Comments.Summary) + " |");
                    }
                }

                if (allFields.Count > 0)
                {
                    q.WriteLine("## Fields");
                    q.Write("| Name | Type |  Summary |");
                    q.Write("|---|---|---|");
                    foreach (var field in allFields)
                    {
                        q.Write("| **" + field.Info.Name + "** | " + 
                                ToString(field.Info.FieldType) + " | " +
                                Endlines(field.Comments.Summary) + " |");
                    }
                }
                File.WriteAllText("text.md", q.sb.ToString());
            }
        }
    }
}
