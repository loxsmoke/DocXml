using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DocXml.MarkdownGenerator.MarkdownWriters.Interfaces;

namespace DocXml.MarkdownGenerator.MarkdownWriters
{
    public class BitbucketMarkdownWriter : IMarkdownWriter
    {
        public string FormatName => "bitbucket";

        #region Basic writing
        StringBuilder allText = new StringBuilder();

        public void Write(string text)
        {
            if (text == null) return;
            allText.AppendLine(text);
        }
        public void WriteLine(string text)
        {
            if (text != null) allText.AppendLine(text);
            allText.AppendLine();
        }
        #endregion

        #region Formatted writing
        public void WriteH1(string text)
        {
            allText.AppendLine();
            WriteLine("# " + text);
        }

        public void WriteH2(string text)
        {
            allText.AppendLine();
            WriteLine("## " + text);
        }
        public void WriteLink(string anchorName, string text)
        {
            allText.Append(Link(anchorName, text));
        }
        public void WriteHeadingLink(string text)
        {
            allText.Append(HeadingLink(text));
        }
        public void WriteAnchor(string anchorName)
        {
            allText.Append($"<a name=\"{anchorName}\"></a>");
        }
        #endregion

        #region Tables
        public void WriteTableTitle(params string[] tableHeadings)
        {
            Write("| " + string.Join(" | ", tableHeadings) + " |");
            Write("|" + string.Join("|", tableHeadings.Select(x => "---")) + "|");
        }

        public void WriteTableRow(params string[] texts)
        {
            Write("| " + string.Join(" | ", texts.Select(EscapeSpecialText)) + " |");
        }
        #endregion

        #region Text formatting
        public string EscapeSpecialText(string text)
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

        public string EscapeSpecialChars(string text)
        {
            if (text == null) return "";
            text = text.Replace("&gt;", ">");
            text = text.Replace("&lt;", "<");
            return text.Replace("\r\n", "<br>");
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
            return $"[{text}](#markdown-header-{text.Replace(" ", "-").ToLower()})";
        }

        public string HeadingLink(string anchorName, string text)
        {
            return $"[{text}](#markdown-header-{anchorName.Replace(" ", "-").ToLower()})";
        }
        #endregion

        public string FullText => allText.ToString();
    }
}
