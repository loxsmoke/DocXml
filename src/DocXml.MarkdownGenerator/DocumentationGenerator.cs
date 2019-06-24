using System;
using System.Collections.Generic;
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
    public class DocumentationGenerator
    {
        public DocXmlReader Reader { get; }
        public IMarkdownWriter Writer { get; }
        public TypeCollection TypeCollection { get; }
        public List<TypeCollection.TypeInformation> TypesToDocument { get; }
        public HashSet<Type> TypesToDocumentSet { get; set; }
        private Func<Type, Queue<string>, string> typeLinkConverter;

        public DocumentationGenerator(
            IMarkdownWriter writer,
            TypeCollection typeCollection)
        {
            Reader = new DocXmlReader();
            Writer = writer;
            TypeCollection = typeCollection;
            TypesToDocument = typeCollection.ReferencedTypes.Values
                .OrderBy(t => t.Type.Namespace)
                .ThenBy(t => t.Type.Name).ToList();
            TypesToDocumentSet = new HashSet<Type>(TypesToDocument.Select(t => t.Type));
            typeLinkConverter = (type, _) => TypesToDocumentSet.Contains(type) ?
                Writer.HeadingLink(type.Name + " class", type.Name) : null;
        }

        public void WriteDocumentTitle(Assembly assembly, string titleText = "API documentation")
        {
            Writer.WriteH1($"{Path.GetFileName(assembly.ManifestModule.Name)} v.{assembly.GetName().Version} " +
                           titleText ?? "");
        }

        public void WriteTypeIndex()
        {
            var listEmpty = true;
            foreach (var typeData in TypesToDocument.OrderBy(t => t.Type.Name))
            {
                if (listEmpty)
                {
                    Writer.WriteH1("All types");
                    listEmpty = false;
                }
                Writer.WriteHeadingLink(typeData.Type.Name + " class");
                Writer.WriteLine("");
            }
            if (!listEmpty) Writer.WriteLine("");
        }

        public void DocumentTypes()
        {
            foreach (var typeData in TypesToDocument)
            {
                Writer.WriteH1(typeData.Type.Name + " Class");
                Writer.WriteLine("Namespace: " + typeData.Type.Namespace);
                if (typeData.Type.BaseType != null && typeData.Type.BaseType != typeof(Object))
                {
                    Writer.WriteLine("Base class: " + typeData.Type.BaseType.ToNameString(typeLinkConverter));
                }

                var typeComments = Reader.GetTypeComments(typeData.Type);
                Writer.WriteLine(typeComments.Summary);

                if (!string.IsNullOrEmpty(typeComments.Example))
                {
                    Writer.WriteH2("Examples");
                    Writer.WriteLine(typeComments.Example);
                }

                if (!string.IsNullOrEmpty(typeComments.Remarks))
                {
                    Writer.WriteH2("Remarks");
                    Writer.WriteLine(typeComments.Remarks);
                }

                var allProperties = Reader.Comments(typeData.Properties).ToList();
                var allMethods = Reader.Comments(typeData.Methods).ToList();
                var allFields = Reader.Comments(typeData.Fields).ToList();

                if (allProperties.Count > 0)
                {
                    Writer.WriteH2("Properties");
                    Writer.WriteTableTitle("Name", "Type", "Summary");
                    foreach (var prop in allProperties)
                    {
                        Writer.WriteTableRow(
                            Writer.Bold(prop.Info.Name),
                            prop.Info.ToTypeNameString(typeLinkConverter),
                            prop.Comments.Summary);
                    }
                }

                if (allMethods.Count > 0 && allMethods.Any(m => m.Info is ConstructorInfo))
                {
                    Writer.WriteH2("Constructors");
                    Writer.WriteTableTitle("Name", "Summary");
                    foreach (var prop in allMethods
                        .Where(m => m.Info is ConstructorInfo)
                        .OrderBy(p => p.Info.GetParameters().Length))
                    {
                        Writer.WriteTableRow(
                            Writer.Bold(typeData.Type.ToNameString() + prop.Info.ToParametersString(typeLinkConverter)),
                            prop.Comments.Summary);
                    }
                }

                if (allMethods.Count > 0 && allMethods.Any(m => m.Info is MethodInfo))
                {
                    Writer.WriteH2("Methods");
                    Writer.WriteTableTitle("Name", "Returns", "Summary");
                    foreach (var method in allMethods
                        .Where(m => m.Info != null && !(m.Info is ConstructorInfo) && (m.Info is MethodInfo))
                        .OrderBy(p => p.Info.Name)
                        .ThenBy(p => p.Info.GetParameters().Length))
                    {
                        var methodInfo = method.Info as MethodInfo;
                        Writer.WriteTableRow(
                            Writer.Bold(methodInfo.Name + methodInfo.ToParametersString(typeLinkConverter)),
                            methodInfo.ToTypeNameString(typeLinkConverter),
                            method.Comments.Summary);
                    }
                }

                if (allFields.Count > 0)
                {
                    Writer.WriteH2("Fields");
                    Writer.WriteTableTitle("Name", "Type", "Summary");
                    foreach (var field in allFields)
                    {
                        Writer.WriteTableRow(
                            Writer.Bold(field.Info.Name),
                            field.Info.ToTypeNameString(typeLinkConverter),
                            field.Comments.Summary);
                    }
                }
            }
        }
    }
}
