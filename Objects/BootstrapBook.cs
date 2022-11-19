using System;
using System.IO;
using System.Text;

namespace UbStandardObjects.Objects
{

    /// <summary>
    /// Generates a whole book using bootstrap
    /// </summary>
    public class BootstrapBook : HtmlFormat
    {
        public event dlShowMessage ShowMessage = null;

        public BootstrapBook(Parameters parameters) : base(parameters)
        {
        }

        private string Link(string href, string text)
        {
            return $"<a class=\"page-link\" href=\"{href}\">{text}</a>";
        }

        private string IdentLink(Paragraph p)
        {
            string href = $"https://github.com/Rogreis/PtAlternative/blob/correcoes/Doc{p.Paper:000}/Par_{p.Paper:000}_{p.Section:000}_{p.ParagraphNo:000}.md";
            return $"<a href=\"{href}\" class=\"{Param.ParagraphClass(p)}\">{p.Identification}</a>";
            //string href = $"javascript:openEdit('{p.Paper};{p.Section};{p.ParagraphNo};');";
            //return $"<a href=\"{href}\" class=\"{Param.BackgroundParagraphColor(p.Status)}\">{p.Identification}</a>";
        }


        //private void PrintIndexOffCanvas(StringBuilder sb, List<BookIndex> list, short paperNo)
        //{
        //    sb.AppendLine("  <div class=\"offcanvas offcanvas-start\" id=\"offcanvasTUB\" data-bs-scroll=\"true\"> ");
        //    sb.AppendLine("    <div class=\"offcanvas-header\"> ");
        //    sb.AppendLine("      <h3 class=\"offcanvas-title\">Index - Livro de Urântia PT-BR</h3> ");
        //    sb.AppendLine("      <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"offcanvas\"></button> ");
        //    sb.AppendLine("    </div> ");
        //    sb.AppendLine("    <div class=\"offcanvas-body\"> ");


        //    sb.AppendLine("<ol class=\"list-group\"> ");
        //    foreach (BookIndex index in list)
        //    {
        //        string active = index.PaperNo == paperNo ? "active" : "";
        //        // {index.PaperNo:000} - 
        //        sb.AppendLine($"  <li class=\"list-group-item {active}\"><a class=\"page-link\" href=\"{index.Href}\">{index.PaperNo:000} - {index.Text}</a></li> ");
        //    }
        //    sb.AppendLine("</ol> ");


        //    //sb.AppendLine("<ol class=\"list-group\"> ");
        //    //foreach (BookIndex index in list)
        //    //{
        //    //    string active = index.PaperNo == paperNo ? "active" : "";
        //    //    sb.AppendLine($"  <p><a href = \"{index.Href}\">{index.PaperNo:000} - {index.Text}</a></p> ");

        //    //}
        //    //sb.AppendLine("</ol> ");



        //    //sb.AppendLine("  <div class=\"list-group\"> ");
        //    //foreach (BookIndex index in list)
        //    //{
        //    //    sb.AppendLine($"    <a href=\"{index.Href}\" class=\"list-group-item list-group-item-info\">{index.PaperNo:000} - {index.Text}</a> ");
        //    //}
        //    //sb.AppendLine("  </div> ");

        //    sb.AppendLine("    </div> ");
        //    sb.AppendLine("  </div> ");
        //}

        //private void PrintPager(StringBuilder sb, List<BookIndex> list, short paperNo)
        //{
        //    sb.AppendLine("<ul class=\"pagination\"> ");
        //    if (paperNo > 0)
        //    {
        //        BookIndex provious = list[paperNo - 1];
        //        sb.AppendLine($"  <li class=\"page-item\"><a class=\"page-link\" href=\"{provious.Href}\">Previous</a></li> ");
        //    }
        //    foreach (BookIndex index in list)
        //    {
        //        string active = index.PaperNo == paperNo ? "active" : "";
        //        sb.AppendLine($"  <li class=\"page-item {active}\"><a class=\"page-link\" href=\"{index.Href}\">{index.PaperNo:000}</a></li> ");
        //    }
        //    if (paperNo < 196)
        //    {
        //        BookIndex next = list[paperNo + 1];
        //        sb.AppendLine($"  <li class=\"page-item\"><a class=\"page-link\" href=\"{next.Href}\">Next</a></li> ");
        //    }
        //    sb.AppendLine("</ul> ");
        //}





        private string PrintText(Paragraph p, bool useLink, bool useAnchor)
        {
            string identification = useLink ? IdentLink(p) : p.Identification;
            string anchor = useAnchor ? $"<a name=\"p{p.Paper:000}_{p.Section:000}_{p.ParagraphNo:000}\"/>  " : "";
            switch (p.Format)
            {
                case ParagraphHtmlType.BookTitle:
                    return $"<h1>{anchor}{p.Text}</h1>";
                case ParagraphHtmlType.PaperTitle:
                    return $"<h2>{anchor}{p.Text}</h2>";
                case ParagraphHtmlType.SectionTitle:
                    return $"<h3>{anchor}{p.Text}</h3>";
                case ParagraphHtmlType.NormalParagraph:
                    return $"{identification}  {p.Text}";
                case ParagraphHtmlType.IdentedParagraph:
                    return $"<bloquote>{identification}  {p.Text}</bloquote>";
            }
            return "";
        }


        protected void FireSendMessage(string message)
        {
            ShowMessage?.Invoke(message);
        }


        protected void MakeColumn(StringBuilder sb, Paragraph p)
        {
            string textDirection = TextDirection(p);
            sb.AppendLine($"   <td{textDirection}>");
            sb.AppendLine($"      <div class=\"p-3 mb-2 parClosed\">");
            sb.AppendLine($"          {PrintText(p, false, false)}");
            sb.AppendLine($"      <div>");
            sb.AppendLine($"   </td>");
        }



        protected void MakeColumnWithDiv(StringBuilder sb, Paragraph p)
        {
            string textDirection = TextDirection(p);
            string divId = $"d{p.Paper}_{p.Section}_{p.ParagraphNo}";
            string htmlClass = p.IsEditTranslation ? Param.ParagraphClass(p) : Param.BackgroundParagraphColor(p.Status);
            sb.AppendLine($"   <td{textDirection}>");
            sb.AppendLine($"      <div id=\"{divId}\" class=\"p-3 mb-2 {htmlClass}\">");
            sb.AppendLine($"          {PrintText(p, true, true)}");
            sb.AppendLine($"      <div>");
            sb.AppendLine($"   </td>");
        }

        public void Test()
        {
            PaperEdit paper = new PaperEdit(1, Param.EditParagraphsRepositoryFolder);
            StringBuilder sb = new StringBuilder();
            Paragraph p = paper.Paragraphs[3];
            MakeColumnWithDiv(sb, p);
            //string s = sb.ToString();
        }


        protected void PrintLine(StringBuilder sb, Paragraph pLeft, Paragraph pRight)
        {
            sb.AppendLine("<tr>");
            MakeColumn(sb, pLeft);
            MakeColumnWithDiv(sb, pRight);
            sb.AppendLine("</tr>");
        }



        ///// <summary>
        ///// Main page structure
        ///// </summary>
        ///// <param name="destinationFolder"></param>
        ///// <param name="paperNo"></param>
        ///// <param name="leftPaper"></param>
        ///// <param name="rightPaper"></param>
        ///// <param name="toc_table"></param>
        //private void PrintPaper(string destinationFolder, short paperNo, Paper leftPaper, Paper rightPaper, TUB_TOC_Html toc_table)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    PageStart(sb, toc_table, paperNo);

        //    PrintJumbotron(sb, $"O Livro de Urântia - Documento {paperNo}", $"PT-BR version: {DateTime.Now.ToString("dd-MM-yyyy")}", paperNo);

        //    string textColor = Param.IsDarkTheme ? Param.DarkText : Param.LightText;
        //    string backColor = Param.IsDarkTheme ? Param.DarkText : Param.LightText;

        //    sb.AppendLine("<div class=\"container-fluid mt-5 \"> ");
        //    sb.AppendLine("  <div class=\"row\"> ");
        //    sb.AppendLine(" ");

        //    // Index
        //    sb.AppendLine($"    <div class=\"col-sm-3 {textColor}\"> ");
        //    sb.AppendLine("      <h3>Index</h3> ");
        //    toc_table.Html(sb);
        //    sb.AppendLine("    </div> ");
        //    sb.AppendLine(" ");

        //    sb.AppendLine($"<div class=\"col-sm-9 {textColor}\"> ");
        //    sb.AppendLine("	  <table class=\"table table-borderless\"> ");

        //    // Page title
        //    sb.AppendLine("	    <thead> ");
        //    sb.AppendLine("	      <tr> ");
        //    sb.AppendLine($"	        <th><h2 {TextClass()}>Paper {paperNo}</h2></th> ");
        //    sb.AppendLine($"	        <th><h2 {TextClass()}>Documento {paperNo}</h2></th> ");
        //    sb.AppendLine("	      </tr> ");
        //    sb.AppendLine("	    </thead> ");

        //    // Text
        //    sb.AppendLine("	    <tbody> ");

        //    for (int i = 0; i < leftPaper.Paragraphs.Count; i++)
        //    {
        //        try
        //        {
        //            PrintLine(sb, leftPaper.Paragraphs[i], rightPaper.Paragraphs[i]);
        //        }
        //        catch (Exception EX)
        //        {
        //            string SSS = EX.Message;
        //        }
        //    }

        //    sb.AppendLine("	    </tbody> ");

        //    sb.AppendLine("	  </table> ");
        //    sb.AppendLine("  </div> ");
        //    sb.AppendLine("</div> ");
        //    sb.AppendLine("</div> ");

        //    //PrintPager(sb, paperNo);
        //    PageEnd(sb, toc_table);

        //    var filePath = Path.Combine(destinationFolder, $"Doc{paperNo:000}.html");
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }

        //    File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        //}

        /// <summary>
        /// Main page structure
        /// </summary>
        /// <param name="destinationFolder"></param>
        /// <param name="paperNo"></param>
        /// <param name="leftPaper"></param>
        /// <param name="rightPaper"></param>
        /// <param name="toc_table"></param>
        protected void PrintPaperForGitHubWebSite(string destinationFolder, short paperNo, Paper leftPaper, Paper rightPaper, TUB_TOC_Html toc_table)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("	  <table class=\"table table-borderless\"> ");

            // Page title
            sb.AppendLine("	    <thead>");
            sb.AppendLine("	      <tr>");
            sb.AppendLine($"        <th><div class=\"p-3 mb-2 parClosed\"><h2>Paper {paperNo}</h2> <div></th>");
            sb.AppendLine($"        <th><div class=\"p-3 mb-2 parClosed\"><h2>Documento {paperNo}</h2> <div></th>");
            sb.AppendLine("	      </tr>");
            sb.AppendLine("	    </thead>");

            // Text
            sb.AppendLine("	    <tbody> ");

            for (int i = 0; i < leftPaper.Paragraphs.Count; i++)
            {
                try
                {
                    PrintLine(sb, leftPaper.Paragraphs[i], rightPaper.Paragraphs[i]);
                }
                catch (Exception EX)
                {
                    string SSS = EX.Message;
                }
            }

            sb.AppendLine("	    </tbody> ");

            sb.AppendLine("	  </table> ");

            var filePath = Path.Combine(destinationFolder, $@"content\Doc{paperNo:000}.html");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }



        public void MainPage(StringBuilder sb)
        {
            // main page is no more generated 
        }



        public virtual void GeneratePaper(string destinationFolder, Translation leftTranslation, Paper rightPaper, TUB_TOC_Html toc_table, short paperNo)
        {
            try
            {
                Paper leftPaper = null;
                TranslationIdRight = TranslationIdLeft = Translation.NoTranslation;
                TranslationTextDirection[0] = TranslationTextDirection[2] = false;

                if (leftTranslation != null)
                {
                    TranslationIdLeft = leftTranslation.LanguageID;
                    TranslationTextDirection[2] = leftTranslation.RightToLeft;
                }

                leftPaper = GetPaper(paperNo, leftTranslation);
                FireSendMessage(leftPaper.ToString());
                PrintPaperForGitHubWebSite(destinationFolder, paperNo, leftPaper, rightPaper, toc_table);

            }
            catch (Exception)
            {

                throw;
            }
        }



        //public void GeneratBook(string destinationFolder, Translation leftTranslation, Translation rightTranslation)
        //{
        //    try
        //    {
        //        Paper rightPaper = null, leftPaper = null;
        //        TranslationIdRight = TranslationIdLeft = Translation.NoTranslation;
        //        TranslationTextDirection[0] = TranslationTextDirection[2] = false;

        //        if (rightTranslation != null)
        //        {
        //            TranslationIdRight = rightTranslation.LanguageID;
        //            TranslationTextDirection[0] = rightTranslation.RightToLeft;
        //        }

        //        if (leftTranslation != null)
        //        {
        //            TranslationIdLeft = leftTranslation.LanguageID;
        //            TranslationTextDirection[2] = leftTranslation.RightToLeft;
        //        }

        //        for (short paperNo = 0; paperNo < 197; paperNo++)
        //        {
        //            leftPaper = GetPaper(paperNo, leftTranslation);
        //            rightPaper = GetPaper(paperNo, rightTranslation);
        //            ShowMessage?.Invoke(leftPaper.ToString());
        //            PrintPaper(destinationFolder, paperNo, leftPaper, rightPaper);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}



    }
}
