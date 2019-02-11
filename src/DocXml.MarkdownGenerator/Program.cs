using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DocXml.Reflection;
using LoxSmoke.DocXml;
using LoxSmoke.DocXml.Reflection;
using static LoxSmoke.DocXml.Reflection.DocXmlReaderExtensions;

namespace DocXml.MarkdownGenerator
{
    class Program
    {
        static string ToString(ParameterInfo[] parameters)
        {
            if (parameters == null || parameters.Length == 0) return "()";
            return "(" + string.Join(", ", parameters.Select(s => s.ParameterType.ToNameString())) + ")";
        }

        static void Main(string[] args)
        {
            var markdownWriter = new MarkdownWriter();
            var docXmlReader = new DocXmlReader();
            var reflectionSettings = ReflectionSettings.Default;
            var myAssembly = Assembly.GetAssembly(typeof(DocXmlReader));
            markdownWriter.WriteH1($"{Path.GetFileName(myAssembly.ManifestModule.Name)} v.{myAssembly.GetName().Version} API documentation");
            var typeCollection = TypeCollection.ForReferencedTypes(myAssembly, reflectionSettings);

            var typesToDocument = typeCollection.ReferencedTypes.Values
                .OrderBy(t => t.Type.Namespace)
                .ThenBy(t => t.Type.Name).ToList();

            if (typesToDocument.Count > 0)
            {
                markdownWriter.WriteH1("All types");
                foreach (var typeData in typesToDocument.OrderBy(t => t.Type.Name))
                {
                    markdownWriter.WriteLink($"#{typeData.Type.Name}", typeData.Type.Name);
                    markdownWriter.WriteLine("");
                }
                markdownWriter.WriteLine("");
            }

            foreach (var typeData in typesToDocument)
            {
                markdownWriter.WriteH1(typeData.Type.Name + " Class");
                markdownWriter.WriteAnchor(typeData.Type.Name);
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
                        markdownWriter.WriteTableRow(
                            markdownWriter.Bold(prop.Info.Name),
                            prop.Info.PropertyType.ToNameString(),
                            prop.Comments.Summary);
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
                            markdownWriter.Bold(typeData.Type.ToNameString() + 
                                 ToString((prop.Info as ConstructorInfo).GetParameters())),
                            prop.Comments.Summary);
                    }
                }

                if (allMethods.Count > 0 && allMethods.Any(m => m.Info is MethodInfo))
                {
                    markdownWriter.WriteH2("Methods");
                    markdownWriter.WriteTableTitle("Name", "Returns", "Summary");
                    foreach (var method in allMethods
                        .Where(m => m.Info != null && !(m.Info is ConstructorInfo) && (m.Info is MethodInfo))
                        .OrderBy(p => p.Info.Name)
                        .ThenBy(p => p.Info.GetParameters().Length))
                    {
                        var methodInfo = method.Info as MethodInfo;
                        markdownWriter.WriteTableRow(
                            markdownWriter.Bold(methodInfo.Name + ToString(methodInfo.GetParameters())),
                            methodInfo.ReturnType.ToNameString(),
                            method.Comments.Summary);
                    }
                }

                if (allFields.Count > 0)
                {
                    markdownWriter.WriteH2("Fields");
                    markdownWriter.WriteTableTitle("Name", "Type", "Summary");
                    foreach (var field in allFields)
                    {
                        markdownWriter.WriteTableRow(
                            markdownWriter.Bold(field.Info.Name),
                            field.Info.FieldType.ToNameString(),
                            field.Comments.Summary);
                    }
                }
                File.WriteAllText((args.Length == 0 ? "api-reference.md" : args[0]), 
                    markdownWriter.sb.ToString());
            }
        }
    }
}
