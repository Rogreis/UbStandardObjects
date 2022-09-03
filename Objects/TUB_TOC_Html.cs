using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UbStandardObjects.Objects
{

    /// <summary>
    /// Define a table of contents entry for TUB
    /// </summary>
    public class TUB_TOC_Entry
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; } = "";

        public string icon { get; set; }

        public string _class { get; set; }

        [JsonPropertyName("href")]
        public string Url { get; set; }

        [JsonPropertyName("expanded")]
        public bool Expanded { get; set; }

        [JsonPropertyName("nodes")]
        public List<TUB_TOC_Entry> Nodes { get; set; } = new List<TUB_TOC_Entry>();

        public override string ToString()
        {
            return Text;
        }
    }

    /// <summary>
    /// Output table of contents using bootstrap 5
    /// <see href="https://www.w3schools.com/howto/howto_js_treeview.asp"/>
    /// </summary>
    public class TUB_TOC_Html
    {
        private List<TUB_TOC_Entry> TocEntries = null;

        public TUB_TOC_Html(List<TUB_TOC_Entry> toc_entries)
        {
            TocEntries= toc_entries;
        }

        private void HtmlNodes(StringBuilder sb, List<TUB_TOC_Entry> tocEntries, string ident)
        {
            sb.AppendLine($"{ident}<ul class=\"nested\"> ");
            foreach (TUB_TOC_Entry entry in tocEntries)
            {
                bool hasNodes = entry.Nodes != null && entry.Nodes.Count > 0;
                if (hasNodes)
                {
                    sb.AppendLine($"{ident}   <li><span class=\"caret\"><a href=\"{entry.Url}\">{entry.Text}</a></span> ");
                    HtmlNodes(sb, entry.Nodes, ident + "   ");
                }
                else
                {
                    sb.AppendLine($"{ident}   <li><a href=\"{entry.Url}\">{entry.Text}</a> ");
                }
                sb.AppendLine($"{ident}   </li> ");
            }
            sb.AppendLine($"{ident}</ul> ");
        }

        public void Style(StringBuilder sb)
        {
            sb.AppendLine("<style> ");
            sb.AppendLine("ul, #myUL { ");
            sb.AppendLine("  list-style-type: none; ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine("#myUL { ");
            sb.AppendLine("  margin: 0; ");
            sb.AppendLine("  padding: 0; ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine(".caret { ");
            sb.AppendLine("  cursor: pointer; ");
            sb.AppendLine("  -webkit-user-select: none; /* Safari 3.1+ */ ");
            sb.AppendLine("  -moz-user-select: none; /* Firefox 2+ */ ");
            sb.AppendLine("  -ms-user-select: none; /* IE 10+ */ ");
            sb.AppendLine("  user-select: none; ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine(".caret::before { ");
            sb.AppendLine("  content: \"\\25B6\"; ");
            sb.AppendLine("  color: black; ");
            sb.AppendLine("  display: inline-block; ");
            sb.AppendLine("  margin-right: 6px; ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine(".caret-down::before { ");
            sb.AppendLine("  -ms-transform: rotate(90deg); /* IE 9 */ ");
            sb.AppendLine("  -webkit-transform: rotate(90deg); /* Safari */' ");
            sb.AppendLine("  transform: rotate(90deg);   ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine(".nested { ");
            sb.AppendLine("  display: none; ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine(".active { ");
            sb.AppendLine("  display: block; ");
            sb.AppendLine("} ");
            sb.AppendLine("</style> ");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        public void JavaScript(StringBuilder sb)
        {
            sb.AppendLine("<script> ");
            sb.AppendLine("var toggler = document.getElementsByClassName(\"caret\"); ");
            sb.AppendLine("var i; ");
            sb.AppendLine(" ");
            sb.AppendLine("for (i = 0; i < toggler.length; i++) { ");
            sb.AppendLine("  toggler[i].addEventListener(\"click\", function() { ");
            sb.AppendLine("    this.parentElement.querySelector(\".nested\").classList.toggle(\"active\"); ");
            sb.AppendLine("    this.classList.toggle(\"caret-down\"); ");
            sb.AppendLine("  }); ");
            sb.AppendLine("} ");
            sb.AppendLine("</script> ");
        }

        public void Html(StringBuilder sb)
        {
            sb.AppendLine("<ul id=\"myUL\"> ");
            string ident = "";
            foreach (TUB_TOC_Entry entry in TocEntries)
            {
                // <span class="caret">Beverages</span>
                sb.AppendLine($"{ident}   <li><span class=\"caret\">{entry.Text}</span> ");
                if (entry.Nodes != null && entry.Nodes.Count > 0)
                {
                    HtmlNodes(sb, entry.Nodes, ident + "   ");
                }
                sb.AppendLine($"{ident}   </li> ");
            }
           
            sb.AppendLine("</ul> ");
            sb.AppendLine(" ");
        }
    }

}
