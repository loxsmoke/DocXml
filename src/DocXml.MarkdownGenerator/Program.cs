using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using DocXml.Reflection;
using LoxSmoke.DocXml;
using LoxSmoke.DocXml.Reflection;

namespace DocXml.MarkdownGenerator
{
    /// <summary>
    /// <typeparamref name=""/>
    /// <paramref name=""/>
    /// <![CDATA[]]>
    /// <c></c>
    /// <code></code>
    /// <example></example>
    /// <exception cref=""></exception>
    /// <list type=""></list>
    /// <para></para>
    /// <see cref=""/>
    /// <seealso cref=""/>
    /// </summary>

    class Program
    {
        static void Help()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("   docxml2md <assembly> [--output <output_md>] [--format <format>]");
            Console.WriteLine("   <assembly>   - the name of the assembly to document.");
            Console.WriteLine("   <output_md>  - the optional name of the output file.");
            Console.WriteLine("   <format>     - markdown file format. Valid values: github, bitbucket. Github is assumed if not specified.");
        }

        static (string AssemblyName, string OutputFile, string Format) Parse(string [] args)
        {
            string assemblyName = null, outputFile = null, format = "github";
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] == "--output" || args[i] == "-o")
                {
                    if (++i == args.Length) return (null, null, null);
                    outputFile = args[i];
                }
                else if (args[i] == "--format" || args[i] == "-f")
                {
                    if (++i == args.Length) return (null, null, null);
                    format = args[i];
                }
                else if (assemblyName == null) assemblyName = args[i];
            }

            if (assemblyName != null && outputFile == null)
            {
                outputFile = Path.GetFileNameWithoutExtension(assemblyName) + ".md";
            }

            return (assemblyName, outputFile, format);
        }

        static void Main(string[] args)
        {
            var (assemblyName, outputFile, format) = Parse(args);
            if (assemblyName == null)
            {
                Help();
                return;
            }

            if (format != "github" && format != "bitbucket")
            {
                Console.WriteLine("Error: invalid markdown format specified. Expected github or bitbucket");
                return;
            }
            try
            {
                var myAssembly = Assembly.LoadFrom(assemblyName);

                var markdownWriter = format == "github" ? (IMarkdownWriter)new GithubMarkdownWriter() : new BitbucketMarkdownWriter();
                var typeCollection = TypeCollection.ForReferencedTypes(myAssembly, ReflectionSettings.Default);
                var generator = new DocumentationGenerator(markdownWriter, typeCollection);

                generator.WriteDocumentTitle(myAssembly);
                generator.WriteTypeIndex();
                generator.DocumentTypes();
                File.WriteAllText(outputFile, markdownWriter.FullText);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Error: {exc.Message}");
            }
        }
    }
}
