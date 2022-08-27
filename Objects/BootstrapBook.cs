using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace UbStandardObjects.Objects
{

    public delegate void dlShowMessage(string message);

    /// <summary>
    /// Generates a whole book using bootstrap
    /// </summary>
    public class BootstrapBook : HtmlFormat
    {
        protected string FontName { get; set; } = "Verdana";
        protected float FontSize { get; set; } = 14;

        public event dlShowMessage ShowMessage = null;

        public BootstrapBook(HtmlFormatParameters parameters) : base(parameters)
        {
        }


        protected override void Styles(StringBuilder sb)
        {
            sb.AppendLine("<style type=\"text/css\">  ");

            //sb.AppendLine("@page { ");
            //sb.AppendLine("  size: 7in 9.25in; ");
            //sb.AppendLine("  margin: 27mm 16mm 27mm 16mm; ");
            //sb.AppendLine("} ");

            //sb.AppendLine("div.chapter, div.appendix { ");
            //sb.AppendLine("  page-break-after: always; ");
            //sb.AppendLine("} ");

            //sb.AppendLine("div.titlepage { ");
            //sb.AppendLine("  page: blank; ");
            //sb.AppendLine("} ");

            //sb.AppendLine("@page :left { ");
            //sb.AppendLine("  @top-left { ");
            //sb.AppendLine("    content: \"Cascading Style Sheets\"; ");
            //sb.AppendLine("  } ");
            //sb.AppendLine("} ");

            //sb.AppendLine("@page blank :left { ");
            //sb.AppendLine("  @top-left { ");
            //sb.AppendLine("    content: normal; ");
            //sb.AppendLine("  } ");
            //sb.AppendLine("} ");


            //sb.AppendLine("@page :right { ");
            //sb.AppendLine("  @top-right { ");
            //sb.AppendLine("    content: string(header, first);  ");
            //sb.AppendLine("  } ");
            //sb.AppendLine("} ");

            //sb.AppendLine("# page numbers ");
            //sb.AppendLine("@page :left { ");
            //sb.AppendLine("  @bottom-left { ");
            //sb.AppendLine("    content: counter(page); ");
            //sb.AppendLine("  } ");
            //sb.AppendLine("} ");

            //sb.AppendLine("@page front-matter :left { ");
            //sb.AppendLine("  @bottom-left { ");
            //sb.AppendLine("    content: counter(page, lower-roman); ");
            //sb.AppendLine("  } ");
            //sb.AppendLine("} ");

            sb.AppendLine(".list-group{ ");
            sb.AppendLine("    max-height: 85vh; ");
            sb.AppendLine("    margin-bottom: 5px; ");
            sb.AppendLine("    overflow-y:auto; ");
            sb.AppendLine("    -webkit-overflow-scrolling: touch; ");
            sb.AppendLine("} ");

            sb.AppendLine("</style>  ");
            sb.AppendLine(" ");
        }


        private void PageStart(StringBuilder sb, string bookTitle, string bookSubTitle)
        {
            // 
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\">");
            sb.AppendLine($"<title>{bookTitle}</title>");
            sb.AppendLine("<meta charset=\"utf-8\"> ");
            sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"> ");

            // Bootstrap 5.1.3
            sb.AppendLine("<link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\"> ");
            sb.AppendLine("<script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js\"></script> ");

            // Bootstrap 3.4.1
            sb.AppendLine("  <link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css\"> ");
            sb.AppendLine("  <script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js\"></script> ");
            sb.AppendLine("  <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js\"></script> ");


            Styles(sb);
            sb.AppendLine("<BODY>");
            sb.AppendLine("<div class=\"container\"> ");

            //  Jumbotron
            sb.AppendLine("  <div class=\"mt-4 p-5 bg-primary text-white rounded\"> ");
            sb.AppendLine($"    <h1>{bookTitle}</h1>  ");
            sb.AppendLine($"    <p>{bookSubTitle}</p>  ");
            sb.AppendLine("     <button class=\"btn btn-warning\" type=\"button\" data-bs-toggle=\"offcanvas\" data-bs-target=\"#offcanvasTUB\"> ");
            //sb.AppendLine("     <button class=\"btn btn-warning\" type=\"button\" class=\"btn-close\">Print</button> ");
            sb.AppendLine("    Open Index ");
            sb.AppendLine("  </button> ");
            sb.AppendLine("  </div> ");
            sb.AppendLine("  <p></p> ");
            sb.AppendLine("  <base target=\"_blank\"> ");
        }

        private void PageEnd(StringBuilder sb)
        {
            sb.AppendLine("</div> ");
            sb.AppendLine("</BODY>");
            sb.AppendLine("</HTML>");
        }

        private void PrintIndex(StringBuilder sb, List<BookIndex> list, short paperNo)
        {
            sb.AppendLine("  <div class=\"offcanvas offcanvas-start\" id=\"offcanvasTUB\" data-bs-scroll=\"true\"> ");
            sb.AppendLine("    <div class=\"offcanvas-header\"> ");
            sb.AppendLine("      <h3 class=\"offcanvas-title\">Index - Livro de Urântia PT-BR</h3> ");
            sb.AppendLine("      <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"offcanvas\"></button> ");
            sb.AppendLine("    </div> ");
            sb.AppendLine("    <div class=\"offcanvas-body\"> ");


            sb.AppendLine("<ol class=\"list-group\"> ");
            foreach (BookIndex index in list)
            {
                string active = index.PaperNo == paperNo ? "active" : "";
                // {index.PaperNo:000} - 
                sb.AppendLine($"  <li class=\"list-group-item {active}\"><a class=\"page-link\" href=\"{index.Href}\">{index.PaperNo:000} - {index.Text}</a></li> ");
            }
            sb.AppendLine("</ol> ");


            //sb.AppendLine("<ol class=\"list-group\"> ");
            //foreach (BookIndex index in list)
            //{
            //    string active = index.PaperNo == paperNo ? "active" : "";
            //    sb.AppendLine($"  <p><a href = \"{index.Href}\">{index.PaperNo:000} - {index.Text}</a></p> ");
                
            //}
            //sb.AppendLine("</ol> ");



            //sb.AppendLine("  <div class=\"list-group\"> ");
            //foreach (BookIndex index in list)
            //{
            //    sb.AppendLine($"    <a href=\"{index.Href}\" class=\"list-group-item list-group-item-info\">{index.PaperNo:000} - {index.Text}</a> ");
            //}
            //sb.AppendLine("  </div> ");

            sb.AppendLine("    </div> ");
            sb.AppendLine("  </div> ");
        }

        private void PrintPager(StringBuilder sb, List<BookIndex> list, short paperNo)
        {
            sb.AppendLine("<ul class=\"pagination\"> ");
            sb.AppendLine("  <li class=\"page-item\"><a class=\"page-link\" href=\"#\">Previous</a></li> ");
            foreach (BookIndex index in list)
            {
                string active = index.PaperNo == paperNo ? "active" : "";
                sb.AppendLine($"  <li class=\"page-item {active}\"><a class=\"page-link\" href=\"{index.Href}\">{index.PaperNo:000}</a></li> ");
            }
            sb.AppendLine("  <li class=\"page-item\"><a class=\"page-link\" href=\"#\">Next</a></li> ");
            sb.AppendLine("</ul> ");
        }

        private string IdentLink(Paragraph p)
        {
            string href= $"https://github.com/Rogreis/PtAlternative/blob/correcoes/Doc{p.Paper:000}/Par_{p.Paper:000}_{p.Section:000}_{p.ParagraphNo:000}.md";
            return $"<a href=\"{href}\">{p.Identification}</a>";
        }


        protected void MakeDIV(StringBuilder sb, Paragraph p, bool useLink= false)
        {
            string TextClass = $"class=\"{statusStyleName(p.Status)}\"";
            string textDirection = TextDirection(p);
            string identification = useLink ? IdentLink(p) : p.Identification;

            // Define div name
            string openStyle = "", closeStyle = "";
            switch (p.Format)
            {
                case ParagraphHtmlType.BookTitle:
                    openStyle = "<h1>";
                    closeStyle = "</h1>";
                    break;
                case ParagraphHtmlType.PaperTitle:
                    openStyle = $"<h2 {TextClass}>{identification}";
                    closeStyle = "</h2>";
                    break;
                case ParagraphHtmlType.SectionTitle:
                    openStyle = $"<h3 {TextClass}>{identification}";
                    closeStyle = "</h3>";
                    break;
                case ParagraphHtmlType.NormalParagraph:
                    openStyle = $"<p {TextClass}>{identification}";
                    closeStyle = "</p>";
                    break;
                case ParagraphHtmlType.IdentedParagraph:
                    openStyle = $"<p {TextClass}><bloquote>{identification}";
                    closeStyle = "</bloquote></p>";
                    break;
            }

            string htmlLink = $"{openStyle} {p.Text}";
            sb.AppendLine($"    <div id=\"{DivName(p)}\" class=\"col-sm-6\" {textDirection}>{htmlLink}{closeStyle}</div>");
        }


        private void PrintLine(StringBuilder sb, Paragraph pLeft, Paragraph pRight)
        {
            sb.AppendLine("  <div class=\"row\"> ");
            MakeDIV(sb, pLeft);
            MakeDIV(sb, pRight, true);
            sb.AppendLine("  </div> ");
        }

        private void PrintPaper(string destinationFolder, short paperNo, List<BookIndex> list, Paper leftPaper, Paper rightPaper)
        {
            StringBuilder sb = new StringBuilder();
            PageStart(sb, $"O Livro de Urântia - Documento {paperNo}", "Versão Bilingue - edição PT-BR");
            PrintIndex(sb, list, paperNo);

            for (int i = 0; i < leftPaper.Paragraphs.Count; i++)
            {
                try
                {
                    PrintLine(sb, leftPaper.Paragraphs[i], rightPaper.Paragraphs[i]);
                }
                catch (Exception EX)
                {
                string SSS= EX.Message; 
                }
            }

            PrintPager(sb, list, paperNo);
            PageEnd(sb);


            var filePath = Path.Combine(destinationFolder, $"Doc{paperNo:000}.html");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        public void Generate(string destinationFolder, Translation leftTranslation, Translation rightTranslation)
        {

            List<BookIndex> indexList = ((TranslationEdit)rightTranslation).GetTranslationIndex();


            try
            {
                Paper rightPaper = null, leftPaper = null;
                TranslationIdRight = TranslationIdLeft = Translation.NoTranslation;
                TranslationTextDirection[0] = TranslationTextDirection[2] = false;

                if (rightTranslation != null)
                {
                    TranslationIdRight = rightTranslation.LanguageID;
                    TranslationTextDirection[0] = rightTranslation.RightToLeft;
                }

                if (leftTranslation != null)
                {
                    TranslationIdLeft = leftTranslation.LanguageID;
                    TranslationTextDirection[2] = leftTranslation.RightToLeft;
                }

                for (short paperNo = 0; paperNo < 197; paperNo++)
                {
                    leftPaper = GetPaper(paperNo, leftTranslation);
                    rightPaper = GetPaper(paperNo, rightTranslation);
                    ShowMessage?.Invoke(leftPaper.ToString());
                    PrintPaper(destinationFolder, paperNo, indexList, leftPaper, rightPaper);
                }
            }
            catch (Exception)
            {

                throw;
            }


            //Process.Start(filePath);
            //Process.Start("chrome.exe", "--incognito");
        }
    }
}
