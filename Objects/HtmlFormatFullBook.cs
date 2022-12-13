using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UbStandardObjects.Objects
{
    public class HtmlFormatFullBook : HtmlFormat
    {
        public HtmlFormatFullBook(Parameters parameters) : base(parameters)
        {
        }


        /// <summary>
        /// Create the styles for the page
        /// </summary>
        /// <param name="sb"></param>
        protected override void Styles(StringBuilder sb)
        {

            float size = Param.FontSize;

            sb.AppendLine("<style type=\"text/css\">  ");
            sb.AppendLine("  ");

            // Body and Table
            sb.AppendLine($"body {{font-family: {Param.FontFamily}; font-size: {size + 4}px; color: #000000;}}");
            sb.AppendLine("table {  ");
            sb.AppendLine("    border: 0px solid #CCC;  ");
            sb.AppendLine("    border-collapse: collapse;  ");
            sb.AppendLine("}  ");
            sb.AppendLine($"td   {{font-family: {Param.FontFamily}; padding: 0px; font-size: {size}px; text-align: left; font-style: none; text-transform: none; font-weight: none; border: none;}}");

            // Sup
            sb.AppendLine($"sup  {{font-size: {size - 1}px;  color: #666666;}}");


            // Title
            HeaderStyle(sb, 1, size + 6, "center");
            HeaderStyle(sb, 2, size + 4, "center");
            HeaderStyle(sb, 3, size + 2);


            paragraphStyle(sb, ParagraphStatus.Started);
            paragraphStyle(sb, ParagraphStatus.Working);
            paragraphStyle(sb, ParagraphStatus.Doubt);
            paragraphStyle(sb, ParagraphStatus.Ok);
            paragraphStyle(sb, ParagraphStatus.Closed);

            ItalicBoldStyles(sb);
            sb.AppendLine("  ");

            // Links
            LinkStyles(sb);
            sb.AppendLine("  ");

            sb.AppendLine("</style>  ");
        }

        protected override string makeDIV(Paragraph p, bool selected = false, bool outputAsLink = false)
        {
            string TextClass = $"class=\"{statusStyleName(p.Status)}\"";
            string textDirection = TextDirection(p);

            // Define div name
            string openStyle = "", closeStyle = "";
            switch (p.Format)
            {
                case ParagraphHtmlType.BookTitle:
                    openStyle = "<h1>";
                    closeStyle = "</h1>";
                    break;
                case ParagraphHtmlType.PaperTitle:
                    openStyle = "<h2>";
                    closeStyle = "</h2>";
                    break;
                case ParagraphHtmlType.SectionTitle:
                    openStyle = "<h3>";
                    closeStyle = "</h3>";
                    break;
                case ParagraphHtmlType.NormalParagraph:
                    openStyle = $"<p {TextClass}>{p.Identification}";
                    closeStyle = "</p>";
                    break;
                case ParagraphHtmlType.IdentedParagraph:
                    openStyle = $"<p {TextClass}><bloquote>{p.Identification}";
                    closeStyle = "</bloquote></p>";
                    break;
            }

            string htmlLink = $"{openStyle} {p.Text}";
            return $"<div id=\"{DivName(p)}\" {textDirection}>{htmlLink}{closeStyle}</div>";
        }


        protected override void HtmlFomatPage(StringBuilder sb, short paperNo, List<Paragraph> rightTranslation, List<Paragraph> middleTranslation = null, List<Paragraph> leftTranslation = null, bool showCompare = false)
        {
            try
            {
                //titleLine(sb);
                sb.AppendLine("<table border=\"1\" width=\"100%\" id=\"table1\" cellspacing=\"4\" cellpadding=\"0\">");

                for (int i = 0; i < rightTranslation.Count; i++)
                {
                    sb.AppendLine("<tr>");
                    sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + makeDIV(rightTranslation[i]) + "</td>");
                    if (middleTranslation != null)
                    {
                        sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + makeDIV(middleTranslation[i]) + "</td>");
                    }
                    if (leftTranslation != null)
                    {
                        sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + makeDIV(leftTranslation[i]) + "</td>");
                    }
                    if (showCompare)
                    {
                        //sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + HtmlCompare(p, FullPaperCompareTranslation.Middle) + "</td>");
                    }
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</table>");
            }
            catch (Exception ex)
            {
                sb.AppendLine(ShowErrorMessage(ex.Message));
            }
        }


        //public string End()
        //{
        //    return "</BODY></HTML>";
        //}


        /// <summary>
        /// Generatre an html page with 1, 2 or 3 text columns
        /// </summary>
        /// <param name="paperNo"></param>
        /// <param name="rightTranslation"></param>
        /// <param name="middleTranslation"></param>
        /// <param name="leftTranslation"></param>
        /// <param name="showCompare"></param>
        /// <returns></returns>
        public override string FormatPaper(short paperNo, Translation rightTranslation, Translation middleTranslation = null, Translation leftTranslation = null, bool showCompare = false)
        {
            // Default values
            Paper rightPaper = null, middlePaper = null, leftPaper = null;
            TranslationIdRight = TranslationIdMiddle = TranslationIdLeft = Translation.NoTranslation;
            TranslationTextDirection[0] = TranslationTextDirection[1] = TranslationTextDirection[2] = false;
            List<Paragraph> rightParagraphs = null;
            List<Paragraph> middleParagraphs = null;
            List<Paragraph> leftParagraphs = null;

            // Current values
            if (rightTranslation != null)
            {
                TranslationIdRight = rightTranslation.LanguageID;
                rightPaper = GetPaper(paperNo, rightTranslation);
                TranslationTextDirection[0] = rightTranslation.RightToLeft;
                rightParagraphs = rightPaper.Paragraphs;
            }
            if (middleTranslation != null)
            {
                TranslationIdMiddle = middleTranslation.LanguageID;
                middlePaper = GetPaper(paperNo, middleTranslation);
                TranslationTextDirection[1] = middleTranslation.RightToLeft;
                middleParagraphs = middlePaper.Paragraphs;
            }
            if (leftTranslation != null)
            {
                TranslationIdLeft = leftTranslation.LanguageID;
                leftPaper = GetPaper(paperNo, leftTranslation);
                TranslationTextDirection[2] = leftTranslation.RightToLeft;
                leftParagraphs = leftPaper.Paragraphs;
            }


            CalcColumnsSize(rightTranslation, middleTranslation, leftTranslation, showCompare);
            StringBuilder sb = new StringBuilder();

            HtmlFomatPage(sb, paperNo, rightParagraphs, middleParagraphs, leftParagraphs, showCompare);

            return sb.ToString();
        }



    }
}
