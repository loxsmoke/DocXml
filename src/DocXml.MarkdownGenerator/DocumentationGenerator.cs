using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DocXml.Reflection;
using LoxSmoke.DocXml;
using LoxSmoke.DocXml.Reflection;
using Newtonsoft.Json;
using static LoxSmoke.DocXml.Reflection.DocXmlReaderExtensions;
using static DocXml.Reflection.ReflectionExtensions;
using DocXml.MarkdownGenerator.MarkdownWriters.Interfaces;

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
            TypeCollection typeCollection,
            Type firstType = null)
        {
            Reader = new DocXmlReader();
            Writer = writer;
            TypeCollection = typeCollection;
            TypesToDocument = typeCollection.ReferencedTypes.Values
                .OrderBy(t => t.Type.Namespace)
                .ThenBy(t => t.Type.Name).ToList();
            if (firstType != null)
            {
                var typeDesc = TypesToDocument.FirstOrDefault(t => t.Type == firstType);
                if (typeDesc != null)
                {
                    TypesToDocument.Remove(typeDesc);
                    TypesToDocument.Insert(0, typeDesc);
                }
            }

            TypesToDocumentSet = new HashSet<Type>(TypesToDocument.Select(t => t.Type));
            typeLinkConverter = (type, _) => TypesToDocumentSet.Contains(type) ?
                Writer.HeadingLink(TypeTitle(type), type.Name) : null;
        }

        public static void GenerateMarkdown(
            Assembly assembly,
            IMarkdownWriter writer,
            string outputFileName,
            bool doNotDocumentJsonIgnore,
            bool documentMethods)
        {
            GenerateMarkdown(assembly, null, writer, outputFileName, doNotDocumentJsonIgnore, documentMethods);
        }

        public static void GenerateMarkdown(
            Type rootType,
            IMarkdownWriter writer,
            string outputFileName,
            bool doNotDocumentJsonIgnore,
            bool documentMethods)
        {
            GenerateMarkdown(null, rootType, writer, outputFileName, doNotDocumentJsonIgnore, documentMethods);
        }

        public static void GenerateMarkdown(
            Assembly assembly,
            Type rootType, 
            IMarkdownWriter writer,
            string outputFileName, 
            bool doNotDocumentJsonIgnore,
            bool documentMethods)
        {
            var reflectionSettings = ReflectionSettings.Default;
            var prevPropertyFilter = reflectionSettings.PropertyFilter;
            reflectionSettings.PropertyFilter = info => 
                (prevPropertyFilter == null || prevPropertyFilter(info)) &&
                (!doNotDocumentJsonIgnore || 
                doNotDocumentJsonIgnore &&
                info.GetCustomAttribute(typeof(JsonIgnoreAttribute)) == null);
            reflectionSettings.MethodFilter = _ => documentMethods;
            var typeCollection = assembly != null ?
                TypeCollection.ForReferencedTypes(assembly, reflectionSettings) :
                TypeCollection.ForReferencedTypes(rootType, reflectionSettings);

            var generator = new DocumentationGenerator(writer, typeCollection, rootType);
            if (assembly != null) generator.WriteDocumentTitle(assembly);
            generator.WriteTypeIndex();
            generator.DocumentTypes();
            File.WriteAllText(outputFileName, generator.Writer.FullText);
        }


        public void WriteDocumentTitle(Assembly assembly, string titleText = "API documentation")
        {
            Writer.WriteH1($"{Path.GetFileName(assembly.ManifestModule.Name)} v.{assembly.GetName().Version} " +
                           titleText ?? "");
        }

        static string TypeTitle(Type type)
        {
            return type.Name + (type.IsEnum ? " Enum" : " Class");
        }

        static (string cref, string innerText, string beforeText, string afterText) FindTagWithCref(string text, string tag)
        {
            if (!text.Contains(tag)) return (null, null, text, null);
            var simpleTag = new Regex("<" + tag + "( +)cref( *)=( *)\"(.*?)\"( *)/>");
            var match = simpleTag.Match(text);
            if (match.Success)
            {
                return (match.Groups[4].Value, "", text.Substring(0, match.Index),
                    text.Substring(match.Index + match.Length));
            }

            var bigTag = new Regex("<" + tag + "( +)cref( *)=( *)\"(.*?)\"( *)>(.*?)</" + tag + ">");
            match = bigTag.Match(text);
            if (match.Success)
            {
                return (match.Groups[4].Value, "", text.Substring(0, match.Index),
                    text.Substring(match.Index + match.Length));
            }
            return (null, null, text, null);
        }

        static string ProcessTags(string text)
        {
            for (; ; )
            {
                var (cref, innerText, beforeText, afterText) = FindTagWithCref(text, "seealso");
                if (cref != null)
                {
                    text = beforeText + "**" + FixCref(cref) + "**" + afterText;
                    continue;
                }
                (cref, innerText, beforeText, afterText) = FindTagWithCref(text, "see");
                if (cref != null)
                {
                    text = beforeText + "**" + FixCref(cref) + "**" + afterText;
                    continue;
                }

                return text;
            }
        }

        static string FixCref(string crefText)
        {
            if (crefText.Contains(":")) // XML doc Id
            {
                return crefText.Substring(crefText.IndexOf(":") + 1);
            }
            return crefText;
        }

        /// <summary>
        /// Write table of contents. It is a three column table with each cell containing 
        /// the link to the heading of the type.
        /// </summary>
        /// <param name="indexTitleText"></param>
        public void WriteTypeIndex(string indexTitleText = "All types")
        {
            var namesForTOC = TypesToDocument
                .Select(typeData => Writer.HeadingLink(TypeTitle(typeData.Type), TypeTitle(typeData.Type))).ToList();
            if (namesForTOC.Count == 0) return;

            if (indexTitleText != null) Writer.WriteH1(indexTitleText);
            Writer.WriteTableTitle(" ", " ", " ");
            var rowCount = namesForTOC.Count / 3 + (((namesForTOC.Count % 3) == 0) ? 0 : 1);
            for (var i = 0; i < rowCount; i++)
            {
                Writer.WriteTableRow(namesForTOC[i],
                    rowCount + i < namesForTOC.Count ? namesForTOC[rowCount + i] : " ",
                    rowCount * 2 + i < namesForTOC.Count ? namesForTOC[rowCount * 2 + i] : " ");
            }
        }

        /// <summary>
        /// Write markdown documentation for the enum type:
        /// Examples, Remarks, 
        /// </summary>
        /// <param name="enumType"></param>
        public void DocumentEnum(Type enumType)
        {
            Writer.WriteH1(TypeTitle(enumType));
            Writer.WriteLine("Namespace: " + enumType.Namespace);

            var enumComments = Reader.GetEnumComments(enumType, true);
            Writer.WriteLine(ProcessTags(enumComments.Summary));

            if (!string.IsNullOrEmpty(enumComments.Example))
            {
                Writer.WriteH2("Examples");
                Writer.WriteLine(ProcessTags(enumComments.Example));
            }

            if (!string.IsNullOrEmpty(enumComments.Remarks))
            {
                Writer.WriteH2("Remarks");
                Writer.WriteLine(ProcessTags(enumComments.Remarks));
            }

            if (enumComments.ValueComments.Count > 0)
            {
                Writer.WriteH2("Values");
                Writer.WriteTableTitle("Name", "Summary");
                foreach (var prop in enumComments.ValueComments)
                {
                    Writer.WriteTableRow(Writer.Bold(prop.Name),
                        ProcessTags(prop.Summary));
                }
            }
        }

        /// <summary>
        /// Write markdown documentation for the class:
        /// Base class,  summary, remarks, Properties, constructors, methods and fields
        /// </summary>
        /// <param name="typeData"></param>
        public void DocumentClass(TypeCollection.TypeInformation typeData)
        {
            Writer.WriteH1(TypeTitle(typeData.Type));
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
                Writer.WriteLine(ProcessTags(typeComments.Example));
            }

            if (!string.IsNullOrEmpty(typeComments.Remarks))
            {
                Writer.WriteH2("Remarks");
                Writer.WriteLine(ProcessTags(typeComments.Remarks));
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
                        ProcessTags(prop.Comments.Summary));
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
                        ProcessTags(field.Comments.Summary));
                }
            }
        }

        public void DocumentTypes()
        {
            foreach (var typeData in TypesToDocument)
            {
                if (typeData.Type.IsEnum)
                {
                    DocumentEnum(typeData.Type);
                    continue;
                }
                DocumentClass(typeData);
            }
        }
    }
}
