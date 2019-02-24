using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DocXml.MarkdownGenerator
{
    public class MarkdownWriter
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
            Write("| " + string.Join(" | ", text.Select(EscapeSpecialText)) + " |");
        }

        string EscapeSpecialText(string text)
        {
            if (text == null) return "";
            text = ResolveTag(text, "paramref", "name");
            return EscapeSpecialChars(text);
        }

        string ResolveTag(string text, string tagName, string attributeName)
        {
            var regex = new Regex("<" + tagName + "( +)" + attributeName + "( *)=( *)\"(.*?)\"( *)/>");
            for (; ; )
            { 
                var match = regex.Match(text);
                if (!match.Success) return text;

                var attributeValue = match.Groups[4].Value;
                text = text.Substring(0, match.Index) + Bold(attributeValue) + text.Substring(match.Index + match.Length);
            }
        }

        static string EscapeSpecialChars(string text)
        {
            if (text == null) return "";
            text = text.Replace("<", "\\<");
            text = text.Replace(">", "\\>");
            return text.Replace("\r\n", "<br>");
        }

        public void WriteLine(string text)
        {
            if (text != null) sb.AppendLine(text);
            sb.AppendLine();
        }

        public void Write(string text)
        {
            if (text == null) return;
            sb.AppendLine(text);
        }

        public void WriteLink(string anchorName, string text)
        {
            sb.Append(Link(anchorName, text));
        }
        public void WriteHeadingLink(string text)
        {
            sb.Append(HeadingLink(text));
        }
        public void WriteAnchor(string anchorName)
        {
            sb.Append($"<a name=\"{anchorName}\"></a>");
        }

        public string Bold(string text)
        {
            return "**" + text + "**";
        }
        public string Link(string anchorName, string text)
        {
            return $"[{text}]({anchorName})";
        }
        public string HeadingLink(string text)
        {
            return $"[{text}](#{text.Replace(" ", "-").ToLower()})";
        }
        public string HeadingLink(string anchorName, string text)
        {
            return $"[{text}](#{anchorName.Replace(" ", "-").ToLower()})";
        }
    }
}
