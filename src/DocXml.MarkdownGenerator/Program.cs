using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using DocXml.MarkdownGenerator.MarkdownWriters;
using DocXml.MarkdownGenerator.MarkdownWriters.Interfaces;
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
            Console.WriteLine("   <assembly>   - The name of the assembly to document.");
            Console.WriteLine("   <output_md>  - Optional. The name of the markdown output file.");
            Console.WriteLine($"   <format>     - Optional. The markdown file format. Valid values: {MarkdownFormatNames}.");
        }

        static (string AssemblyName, string OutputFile, string Format) Parse(string [] args)
        {
            string assemblyName = null, outputFile = null, format = MarkdownWriters.First().FormatName;
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

        static List<IMarkdownWriter> MarkdownWriters = new List<IMarkdownWriter>()
        {
            new GithubMarkdownWriter(),
            new BitbucketMarkdownWriter()
        };
        static string MarkdownFormatNames => string.Join(",", MarkdownWriters.Select(md => md.FormatName));

        static void Main(string[] args)
        {
            var (assemblyName, outputFile, format) = Parse(args);
            if (assemblyName == null)
            {
                Help();
                return;
            }

            var writer = MarkdownWriters.FirstOrDefault(md => md.FormatName.Equals(format, StringComparison.OrdinalIgnoreCase));

            if (format == null)
            {
                writer = MarkdownWriters.First();
                Console.WriteLine($"Markdown format not specified. Assuming {writer.FormatName}.");
            }
            if (writer == null)
            {
                Console.WriteLine($"Error: invalid markdown format specified. Valid values: {MarkdownFormatNames}");
                return;
            }

            try
            {
                var myAssembly = Assembly.LoadFrom(assemblyName);
                if (myAssembly == null)
                {
                    Console.WriteLine($"Could not load assembly \'{assemblyName}\'");
                }
                DocumentationGenerator.GenerateMarkdown(myAssembly, writer, outputFile, false, true);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Error: {exc.Message}");
            }
        }
    }
}
