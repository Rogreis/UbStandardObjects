using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using UbStandardObjects.Objects;
using UBT_WebSite.Classes;

using static System.Net.Mime.MediaTypeNames;

namespace UbStandardObjects.Objects
{
    /// <summary>
    /// Format html to be shown
    /// </summary>
    public class HtmlFormat
    {
        protected const bool RightToLeft = false;

        // Data used to help columns page
        protected double percent = 0;

        protected Parameters Param = null;

        protected short TranslationIdLeft = Translation.NoTranslation;

        protected short TranslationIdMiddle = Translation.NoTranslation;

        protected short TranslationIdRight = Translation.NoTranslation;

        // By default all 3 translations are not right to left written
        protected bool[] TranslationTextDirection = { RightToLeft, RightToLeft, RightToLeft };

        // Padding information
        protected int ParagraphPadding = 10;
        protected int CellPadding = 15;


        public HtmlFormat(Parameters parameters)
        {
            Param = parameters;
        }

        #region Styles

        private string statusStyleHighlightedName(ParagraphStatus ParagraphStatus)
        {
            return "stParHigh" + ParagraphStatus.ToString();
        }

        protected virtual void paragraphStyle(StringBuilder sb, ParagraphStatus paragraphStatus)
        {
            sb.AppendLine("." + statusStyleName(paragraphStatus));
            sb.AppendLine("{  ");
            sb.AppendLine(" font-family: " + Param.FontFamily + ";");
            sb.AppendLine(" font-size: " + Param.FontSize.ToString() + ";  ");
            sb.AppendLine(" background-color: " + Param.BackgroundParagraphColor(paragraphStatus) + ";");
            sb.AppendLine(" padding: 16px; ");
            sb.AppendLine("}  ");
        }

        private void ItalicBoldStyles(StringBuilder sb)
        {
            sb.AppendLine("i, b, em  {  ");
            sb.AppendLine(" font-family: " + Param.FontFamily + ";");
            sb.AppendLine(" font-size: " + Param.FontSize.ToString() + ";  ");
            sb.AppendLine(" color: #FF33CC;  ");
            sb.AppendLine(" font-weight: bold;  ");
            sb.AppendLine("}  ");
        }

        private void HeaderStyle(StringBuilder sb, int header, float size)
        {
            sb.AppendLine($"h{header} {{");
            sb.AppendLine($"font-family: {Param.FontFamily};");
            sb.AppendLine($"font-size: {size}px;");
            sb.AppendLine("text-align: center;");
            sb.AppendLine("font-weight: bold;  ");
            sb.AppendLine("background-color: #0000FF;  ");
            sb.AppendLine("color: #FFFF00;  ");
            sb.AppendLine("}");
        }

        private void LinkStyles(StringBuilder sb)
        {
            sb.AppendLine("a:link { ");
            sb.AppendLine($"  color: {(Param.IsDarkTheme ? Param.LightText : Param.DarkText)}; ");
            sb.AppendLine("  background-color: transparent; ");
            sb.AppendLine("  text-decoration: none; ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine("a:visited { ");
            sb.AppendLine($"  color: {(Param.IsDarkTheme ? Param.LightText : Param.DarkText)}; ");
            sb.AppendLine("  background-color: transparent; ");
            sb.AppendLine("  text-decoration: none; ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine("a:hover { ");
            sb.AppendLine($"  color: {(Param.IsDarkTheme ? Param.LightTextGray : Param.DarkTextGray)}; ");
            sb.AppendLine("  background-color: transparent; ");
            sb.AppendLine("  text-decoration: underline; ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine("a:active { ");
            sb.AppendLine($"  color: {(Param.IsDarkTheme ? Param.LightTextHighlihted : Param.DarkTextHighlihted)}; ");
            sb.AppendLine("  background-color: transparent; ");
            sb.AppendLine("  text-decoration: none; ");
            sb.AppendLine("} ");
        }





        protected virtual void javaScript(StringBuilder sb)
        {
            sb.AppendLine("<script> ");
            sb.AppendLine(" ");
            sb.AppendLine("function changeStyleHigh(id) {  ");
            sb.AppendLine("  var el_up = document.getElementById(id);  ");
            sb.AppendLine("  el_up.style[\"border\"] = '3px dotted #66FF33';");
            sb.AppendLine("  el_up.style[\"mix-blend-mode\"] = 'difference';");
            sb.AppendLine("}          ");
            sb.AppendLine(" ");
            sb.AppendLine("function changeStyleNormal(id) {  ");
            sb.AppendLine("  var el_up = document.getElementById(id);  ");
            sb.AppendLine("  el_up.style[\"border\"] = '0px';  ");
            sb.AppendLine("}          ");
            sb.AppendLine("        ");
            sb.AppendLine("</script> ");
            sb.AppendLine(" ");
        }


        protected virtual string statusStyleName(ParagraphStatus ParagraphStatus)
        {
            return "stPar" + ParagraphStatus.ToString();
        }

        /// <summary>
        /// Calculate the text direction for a translation
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        protected string TextDirection(Paragraph p)
        {
            if (p.TranslationId == TranslationIdLeft)
            {
                return TranslationTextDirection[0] ? " dir=\"rtl\"" : "";
            }
            if (p.TranslationId == TranslationIdMiddle)
            {
                return TranslationTextDirection[1] ? " dir=\"rtl\"" : "";
            }
            return TranslationTextDirection[2] ? " dir=\"rtl\"" : "";
        }

        private void CssVariables(StringBuilder sb)
        {
            sb.AppendLine(":root { ");
            sb.AppendLine("  --font: 'Roboto Serif Medium'; ");
            sb.AppendLine("  --fontSize: 18px; ");
            sb.AppendLine("} ");
        }


        /// <summary>
        /// Create the styles for the page
        /// </summary>
        /// <param name="sb"></param>
        protected virtual void Styles(StringBuilder sb)
        {
            sb.AppendLine("<style type=\"text/css\">  ");

            CssVariables(sb);

            string backColorCommon = "#262626";
            string textColorCommon = "#FFFFFF";
            string fontFamily = $"'{Param.FontFamilyInfo}'";
            string fontSize = $"{Param.FontSize}px";

            sb.AppendLine(".parStarted, .parWorking, .parDoubt, .parOk, .parClosed, .commonText ");
            sb.AppendLine("{  ");
            sb.AppendLine($" font-family: {fontFamily};  ");
            sb.AppendLine($" font-size: {fontSize};  ");
            sb.AppendLine(" text-align: justify;   ");
            sb.AppendLine(" text-justify: inter-word; ");
            sb.AppendLine($" padding: {ParagraphPadding}px;   ");
            sb.AppendLine("}  ");
            sb.AppendLine(" ");

            sb.AppendLine($".commonText {{ background-color: {backColorCommon};  color: {textColorCommon}; }}");
            sb.AppendLine(".parStarted { background-color: FloralWhite;  color: black; }");
            sb.AppendLine(".parWorking { background-color: LemonChiffon;  color: black; }");
            sb.AppendLine(".parDoubt { background-color: FireBrick;  color: white; }");
            sb.AppendLine(".parOk { background-color: Aquamarine;  color: black; }");
            sb.AppendLine(".parClosed { background-color: rgb(236, 236, 236);  color: rgb(111, 109, 109);; }");

            sb.AppendLine("sup  {font-size: 9px;  color: #666666;}  ");

            ItalicBoldStyles(sb);

            sb.AppendLine("table {  ");
            sb.AppendLine("    border: 0px solid;  ");
            sb.AppendLine("    border-collapse: collapse;  ");
            sb.AppendLine("}  ");
            sb.AppendLine($"td {{ padding: {CellPadding}px; border: none;}}");

            sb.AppendLine("</style>  ");

            //ItalicBoldStyles(sb);
            //sb.AppendLine("  ");

            //// Links
            //LinkStyles(sb);


        }

        protected string ShowErrorMessage(string Message)
        {
            return "<P>" + Message + "</P>";
        }

        protected Paper GetPaper(short paperNo, Translation translation)
        {
            return translation.Paper(paperNo);
        }

        #endregion


        #region Html Start and End
        protected virtual void pageStart(StringBuilder sb, int paperNo, bool compareStyles = false)
        {
            sb.AppendLine("<!DOCTYPE html>  ");
            sb.AppendLine("<html>  ");
            sb.AppendLine("  ");
            sb.AppendLine("<head>   ");
            sb.AppendLine("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\">  ");
            sb.AppendLine($"    <title>Paper {paperNo}</title>  ");
            sb.AppendLine("    <meta charset=\"utf-8\">   ");
            sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">   ");
            sb.AppendLine("    <link href=\"css/bootstrap.min.css\" rel=\"stylesheet\">    ");

            Styles(sb);

            sb.AppendLine(" ");
            sb.AppendLine("</head>   ");
            sb.AppendLine("  ");
            sb.AppendLine("<body class=\"commonText\" \">   ");
            sb.AppendLine("<div class=\"container-fluid mt-5 commonText\">    ");

        }

        protected void pageEnd(StringBuilder sb)
        {
            sb.AppendLine("</BODY>");
            sb.AppendLine("</HTML>");
        }

        #endregion


        #region Working with DIV's
        protected virtual string DivName(Paragraph p)
        {
            return $"id=\"divr{p.ParaIdent}\"";
        }

        protected virtual string makeDIV(Paragraph p, bool selected = false, bool outputAsLink = false)
        {
            string TextClass = (selected) ? "class=\"" + statusStyleHighlightedName(p.Status) + "\"" : "class=\"" + statusStyleName(p.Status) + "\"";
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
                    openStyle = $"{p.Identification}";
                    closeStyle = "";
                    break;
                case ParagraphHtmlType.IdentedParagraph:
                    openStyle = $"<bloquote>{p.Identification}";
                    closeStyle = "</bloquote>";
                    break;
            }

            return $"<div {textDirection}>{openStyle} {p.Text}{closeStyle}</div>";
        }


        protected virtual string makeEditDIV(Paragraph p)
        {
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
                    openStyle = $"{p.Identification}";
                    closeStyle = "";
                    break;
                case ParagraphHtmlType.IdentedParagraph:
                    openStyle = $"<bloquote>{p.Identification}";
                    closeStyle = "</bloquote>";
                    break;
            }

            return $"<div id=\"{DivName(p)}\" class=\"{statusStyleName(p.Status)}\" {textDirection}><a href=\"about:ident\" ident=\"{p.AName}\" class=\"{statusStyleName(p.Status)}\">{p.Identification}</a>{openStyle} {p.Text}{closeStyle}</div>";
        }
        #endregion



        #region Private format routines

        /// <summary>
        /// Calculate the number of columns as a poercent to split the page
        /// </summary>
        /// <param name="rightTranslation"></param>
        /// <param name="middleTranslation"></param>
        /// <param name="leftTranslation"></param>
        /// <param name="showCompare"></param>
        protected void CalcColumnsSize(Translation rightTranslation, Translation middleTranslation = null, Translation leftTranslation = null, bool showCompare = false)
        {
            int fator = 1;
            if (middleTranslation != null)
            {
                fator++;
            }

            if (leftTranslation != null)
            {
                fator++;
            }

            if (showCompare)
            {
                fator++;
            }
            percent = 100.0 / fator;
        }


        protected virtual void HtmlFomatPage(StringBuilder sb, short paperNo, List<Paragraph> rightTranslation, List<Paragraph> middleTranslation = null, List<Paragraph> leftTranslation = null, bool showCompare = false)
        {
            try
            {
                pageStart(sb, paperNo, true);
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
                        sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + makeEditDIV(leftTranslation[i]) + "</td>");
                    }
                    if (showCompare)
                    {
                        //sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + HtmlCompare(p, FullPaperCompareTranslation.Middle) + "</td>");
                    }
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</table>");
                pageEnd(sb);

            }
            catch (Exception ex)
            {
                sb.AppendLine(ShowErrorMessage(ex.Message));
            }
        }


        #endregion


        /// <summary>
        /// Generatre an html page with 1, 2 or 3 text columns
        /// </summary>
        /// <param name="paperNo"></param>
        /// <param name="rightTranslation"></param>
        /// <param name="middleTranslation"></param>
        /// <param name="leftTranslation"></param>
        /// <param name="showCompare"></param>
        /// <returns></returns>
        public virtual string FormatPaper(short paperNo, Translation rightTranslation, Translation middleTranslation = null, Translation leftTranslation = null, bool showCompare = false)
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
            pageStart(sb, paperNo);

            HtmlFomatPage(sb, paperNo, rightParagraphs, middleParagraphs, leftParagraphs, showCompare);

            pageEnd(sb);
            return sb.ToString();
        }

        public string FormatParagraph(string title, string text)
        {
            StringBuilder sb = new StringBuilder();
            pageStart(sb, 1, true);
            sb.AppendLine($"<h2>{title}</h2>");
            sb.AppendLine($"<p>{text}</p>");
            pageEnd(sb);
            return sb.ToString();
        }

        public string FormatParagraph(Paragraph p)
        {
            StringBuilder sb = new StringBuilder();
            ParagraphPadding = 1;
            CellPadding = 5;
            pageStart(sb, 1, true);
            sb.AppendLine($"<p>{p.Text}</p>");
            pageEnd(sb);
            return sb.ToString();
        }


        public string FormatParagraph(Paragraph pLeft, Paragraph pMiddle, Paragraph pRight, bool showCompare= false)
        {
            // Column percent value
            percent = 100.0 / (showCompare? 4 : 3);

            StringBuilder sb = new StringBuilder();
            ParagraphPadding = 1;
            CellPadding = 5;

            pageStart(sb, pLeft.Paper, false);
            sb.AppendLine("<table border=\"1\" width=\"100%\" id=\"table1\" cellspacing=\"4\" cellpadding=\"0\">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + makeDIV(pLeft) + "</td>");
            sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + makeDIV(pMiddle) + "</td>");
            sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + makeEditDIV(pRight) + "</td>");
            if (showCompare)
            {
                //sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + HtmlCompare(p, FullPaperCompareTranslation.Middle) + "</td>");
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            pageEnd(sb);
            return sb.ToString();
        }


        public string HtmlCompare(string textOld, string textNew)
        {
            try
            {
                Merger merger = null;
                merger = new Merger(textOld, textNew);
                return FormatParagraph("Compare", merger.merge());
            }
            catch (Exception ex)
            {
                return FormatParagraph("Error", $"MergeEngine error: {ex.Message}"); ;
            }
        }

    }
}
