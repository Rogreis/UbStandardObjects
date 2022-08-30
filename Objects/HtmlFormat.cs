using System;
using System.Collections.Generic;
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

        protected HtmlFormatParameters Param = null;

        protected short TranslationIdLeft = Translation.NoTranslation;

        protected short TranslationIdMiddle = Translation.NoTranslation;

        protected short TranslationIdRight = Translation.NoTranslation;

        // By default all 3 translations are not right to left written
        protected bool[] TranslationTextDirection = { RightToLeft, RightToLeft, RightToLeft };


        public HtmlFormat(HtmlFormatParameters parameters)
        {
            Param = parameters;
        }

        #region Styles

        protected void paragraphStyle(StringBuilder sb, ParagraphStatus ParagraphStatus)
        {
            sb.AppendLine("." + statusStyleName(ParagraphStatus));
            sb.AppendLine("{  ");
            sb.AppendLine(" font-family: " + Param.FontFamily + ";");
            sb.AppendLine(" font-size: " + Param.FontSize.ToString() + ";  ");
            sb.AppendLine(" background-color: " + statusBackgroundColor(ParagraphStatus) + ";");
            sb.AppendLine(" padding: 16px; ");
            sb.AppendLine("}  ");
        }

        protected void javaScript(StringBuilder sb)
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

        protected void ItalicBoldStyles(StringBuilder sb)
        {
            sb.AppendLine("i, b, em  {  ");
            sb.AppendLine(" font-family: " + Param.FontFamily + ";");
            sb.AppendLine(" font-size: " + Param.FontSize.ToString() + ";  ");
            sb.AppendLine(" color: #FF33CC;  ");
            sb.AppendLine(" font-weight: bold;  ");
            sb.AppendLine("}  ");
        }

        protected void HeaderStyle(StringBuilder sb, int header, float size)
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

        /// <summary>
        /// Create the styles for the page
        /// </summary>
        /// <param name="sb"></param>
        protected virtual void Styles(StringBuilder sb)
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

            sb.AppendLine($"td   {{font-family: {Param.FontFamily}; padding: 10px; font-size: {size + 4}px; color: #000000; text-align: left; font-style: none; text-transform: none; font-weight: none; border: none;}}");

            // Sup
            sb.AppendLine($"sup  {{font-size: {size - 1}px;  color: #666666;}}");


            // Title
            HeaderStyle(sb, 1, size + 6);
            HeaderStyle(sb, 2, size + 4);
            HeaderStyle(sb, 3, size + 2);


            paragraphStyle(sb, ParagraphStatus.Started);
            paragraphStyle(sb, ParagraphStatus.Working);
            paragraphStyle(sb, ParagraphStatus.Doubt);
            paragraphStyle(sb, ParagraphStatus.Ok);
            paragraphStyle(sb, ParagraphStatus.Closed);

            ItalicBoldStyles(sb);
            sb.AppendLine("  ");

            // Links
            sb.AppendLine(" ");
            sb.AppendLine("/* unvisited link */ ");
            sb.AppendLine("a:link { ");
            sb.AppendLine("  color: black; ");
            sb.AppendLine("  text-decoration: none; ");
            sb.AppendLine(" } ");
            sb.AppendLine(" ");
            sb.AppendLine("/* visited link */ ");
            sb.AppendLine("a:visited { ");
            sb.AppendLine("  color: gray; ");
            sb.AppendLine("  text-decoration: none; ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");
            sb.AppendLine("/* mouse over link */ ");

            //sb.AppendLine("a:hover { ");
            ////sb.AppendLine("  color: blue; ");
            ////sb.AppendLine("  text-decoration: none; ");
            //sb.AppendLine("  font-weight: bold; ");
            //sb.AppendLine("} ");
            //sb.AppendLine(" ");
            //sb.AppendLine("/* selected link */ ");

            //sb.AppendLine("a:active { ");
            //sb.AppendLine("  color: blue; ");
            //sb.AppendLine("  text-decoration: none; ");
            ////sb.AppendLine("  font-weight: bold; ");
            //sb.AppendLine("} ");

            sb.AppendLine(" ");

            // Italic
            //sb.AppendLine($".ColItal {{font-family: {Param.FontFamily}; font-size: {size}px; color: #663333; font-style: italic;}}");
            //sb.AppendLine(".Colored {font-family: " + Param.FontFamily + "; font-size: 14px; color: #663333;}");
            //sb.AppendLine(".ppg     {font-family: " + Param.FontFamily + "; font-size: 9px;  color: #999999;  vertical-align:top;}");
            //sb.AppendLine(".super   {font-family: " + Param.FontFamily + "; font-size: 9px;  color: #000000;  vertical-align:top;}");

            sb.AppendLine("</style>  ");
        }

        protected virtual string statusBackgroundColor(ParagraphStatus ParagraphStatus)
        {
            return Param.statusBackgroundColor(ParagraphStatus);
        }

        protected string statusStyleName(ParagraphStatus ParagraphStatus)
        {
            return "stPar" + ParagraphStatus.ToString();
        }
        protected string statusStyleHighlightedName(ParagraphStatus ParagraphStatus)
        {
            return "stParHigh" + ParagraphStatus.ToString();
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


        #endregion


        #region Html Start and End
        protected void pageStart(StringBuilder sb, int paperNo, bool compareStyles = false)
        {
            sb.AppendLine("<HTML>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\">");
            sb.AppendLine("<title>Paper " + paperNo.ToString() + "</title>");
            javaScript(sb);
            Styles(sb);
            sb.AppendLine("<BODY>");
        }

        protected void pageEnd(StringBuilder sb)
        {
            sb.AppendLine("</BODY>");
            sb.AppendLine("</HTML>");
        }

        #endregion


        #region Working with DIV's
        protected string DivName(Paragraph p)
        {
            if (p.TranslationId == TranslationIdLeft)
            {
                return $"divl{p.ParaIdent}";
            }
            if (p.TranslationId == TranslationIdMiddle)
            {
                return $"divm{p.ParaIdent}";
            }
            if (p.TranslationId == TranslationIdRight)
            {
                return $"divr{p.ParaIdent}";
            }
            return $"divc{p.ParaIdent}";
        }


        protected virtual string makeDIV(Paragraph p, bool selected = false, bool outputAsLink= false)
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
                    openStyle = $"<p>{p.Identification}";
                    closeStyle = "</p>";
                    break;
                case ParagraphHtmlType.IdentedParagraph:
                    openStyle = $"<p><bloquote>{p.Identification}";
                    closeStyle = "</bloquote></p>";
                    break;
            }

            string htmlLink = "";
            if (outputAsLink)
            {
                htmlLink = $"<a href=\"about:ident\" ident=\"{p.Identification}\">{openStyle} {p.Text}</a>";
            }
            else
            {
                htmlLink = $"<p>{openStyle} {p.Text}</p>";
            }

            return $"<div id=\"{DivName(p)}\" {textDirection}>{htmlLink}{closeStyle}</div>";
        }
        #endregion

        protected string ShowErrorMessage(string Message)
        {
            return "<P>" + Message + "</P>";
        }


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
                        sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + makeDIV(leftTranslation[i], false, true) + "</td>");
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

        protected Paper GetPaper(short paperNo, Translation translation)
        {
            if (translation is TranslationEdit)
            {
                return ((TranslationEdit)translation).Paper(paperNo);
            }
            else
            {
                return translation.Papers[paperNo]; ;
            }
        }

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
