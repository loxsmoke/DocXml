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
        public class SimpleMarkdownWriter
        {
            public StringBuilder sb = new StringBuilder();

            public void WriteH1(string text)
            {
                WriteLine("# " + text);
            }
            public void WriteH2(string text)
            {
                WriteLine("## " + text);
            }

            public void WriteTableTitle(params string[] text)
            {
                Write("| " + string.Join(" | ", text) + " |");
                Write("|" + string.Join("|", text.Select(x => "---")) + "|");
            }

            public void WriteTableRow(params string[] text)
            {
                Write("| " + string.Join(" | ", text) + " |");
            }

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

        static string Bold(string text)
        {
            return "**" + text + "**";
        }

        static string EscapeSpecialChars(string text)
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
            var markdownWriter = new SimpleMarkdownWriter();
            var docXmlReader = new DocXmlReader();
            var reflectionSettings = ReflectionSettings.Default;
            var myAssembly = Assembly.GetAssembly(typeof(DocXmlReader));
            markdownWriter.WriteH1($"{Path.GetFileName(myAssembly.ManifestModule.Name)} v.{myAssembly.GetName().Version} API documentation");
            var typeCollection = TypeCollection.ForReferencedTypes(myAssembly, reflectionSettings);
            foreach (var typeData in typeCollection.ReferencedTypes.Values
                .OrderBy(t => t.Type.Namespace)
                .ThenByDescending(t => t.ReferencesOut.Count)
                .ThenBy(t => t.Type.Name))
            {
                markdownWriter.WriteH1(typeData.Type.Name + " Class");
                markdownWriter.WriteLine("Namespace: " + typeData.Type.Namespace);
                if (typeData.Type.BaseType != null && typeData.Type.BaseType != typeof(Object))
                {
                    markdownWriter.WriteLine("Base class: " + typeData.Type.BaseType.Name);
                }

                var typeComments = docXmlReader.GetTypeComments(typeData.Type);
                markdownWriter.WriteLine(typeComments.Summary);

                if (!string.IsNullOrEmpty(typeComments.Example))
                {
                    markdownWriter.WriteH2("Examples");
                    markdownWriter.WriteLine(typeComments.Example);
                }

                if (!string.IsNullOrEmpty(typeComments.Remarks))
                {
                    markdownWriter.WriteH2("Remarks");
                    markdownWriter.WriteLine(typeComments.Remarks);
                }

                var allProperties = docXmlReader.Comments(typeData.Properties).ToList();
                var allMethods = docXmlReader.Comments(typeData.Methods).ToList();
                var allFields = docXmlReader.Comments(typeData.Fields).ToList();

                if (allProperties.Count > 0)
                {
                    markdownWriter.WriteH2("Properties");
                    markdownWriter.WriteTableTitle("Name", "Type", "Summary");
                    foreach (var prop in allProperties)
                    {
                        markdownWriter.WriteTableRow(Bold(prop.Info.Name), 
                                ToString(prop.Info.PropertyType),
                                EscapeSpecialChars(prop.Comments.Summary));
                        //WriteLine("Property: " + prop.Info.PropertyType.Name + " " + prop.Info.Name);
                        //WriteLine(prop.Comments.Remarks);
                        //WriteLine(prop.Comments.Example);
                    }
                }

                if (allMethods.Count > 0 && allMethods.Any(m => m.Info is ConstructorInfo))
                {
                    markdownWriter.WriteH2("Constructors");
                    markdownWriter.WriteTableTitle("Name", "Summary");
                    foreach (var prop in allMethods
                        .Where(m => m.Info is ConstructorInfo)
                        .OrderBy(p => p.Info.GetParameters().Length))
                    {
                        markdownWriter.WriteTableRow(
                            Bold(ToString(typeData.Type) + ToString((prop.Info as ConstructorInfo).GetParameters())),
                            EscapeSpecialChars(prop.Comments.Summary));
                    }
                }

                if (allMethods.Count > 0 && allMethods.Any(m => m.Info is MethodInfo))
                {
                    markdownWriter.WriteH2("Methods");
                    markdownWriter.WriteTableTitle("Name", "Returns", "Summary");
                    foreach (var method in allMethods
                        .Where(m => !(m.Info is ConstructorInfo))
                        .OrderBy(p => p.Info.Name)
                        .ThenBy(p => p.Info.GetParameters().Length))
                    {
                        var methodInfo = method.Info as MethodInfo;
                        markdownWriter.WriteTableRow(
                            Bold(methodInfo.Name + ToString(methodInfo.GetParameters())),
                            ToString(methodInfo.ReturnType),
                            EscapeSpecialChars(method.Comments.Summary));
                    }
                }

                if (allFields.Count > 0)
                {
                    markdownWriter.WriteH2("Fields");
                    markdownWriter.WriteTableTitle("Name", "Type", "Summary");
                    foreach (var field in allFields)
                    {
                        markdownWriter.WriteTableRow(
                            Bold(field.Info.Name), 
                            ToString(field.Info.FieldType),
                            EscapeSpecialChars(field.Comments.Summary));
                    }
                }
                File.WriteAllText((args.Length == 0 ? "api-reference.md" : args[0]), 
                    markdownWriter.sb.ToString());
            }
        }
    }
}
