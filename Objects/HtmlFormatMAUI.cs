using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace UbStandardObjects.Objects
{
    public class HtmlFormatMAUI : HtmlFormat
    {

        private Appearence Visual= new Appearence();

        private class Appearence
        {
            public int FontSizeSmall { get => FontSize - 2; }
            public int FontSizeBig { get => FontSize + 2; }

            public string FontFamily { get; set; } = "Roboto Serif Medium";
            public int FontSize { get; set; } = 18;
            public int TextPadding { get; set; } = 15;

            public string BackColor { get; set; } = "#000000";
            public string TextColor { get; set; } = "#FFFFFF";
            public string ItalicColor { get; set; } = "#FF33CC";

            public string BackColorStarted { get; set; } = "FloralWhite";
            public string TextColorStarted { get; set; } = "#000000";

            public string BackColorWorking { get; set; } = "LemonChiffon";
            public string TextColorWorking { get; set; } = "#000000";

            public string BackColorDoubt { get; set; } = "FireBrick";
            public string TextColorDoubt { get; set; } = "#FFFFFF";

            public string BackColorOk { get; set; } = "Aquamarine";
            public string TextColorOk { get; set; } = "#FFFFFF";

            public string BackColorClosed { get; set; } = "#000000";
            public string TextColorClosed { get; set; } = "#FFFFFF";

            public Appearence(bool isDark= true) 
            { 
            }

        }



        public HtmlFormatMAUI(Parameters parameters) : base(parameters)
        {
        }

        //protected void PrintCssVariables(StringBuilder sb)
        //{
        //    sb.AppendLine(":root { ");
        //    sb.AppendLine($"  --font: '{FontFamily}'; ");
        //    sb.AppendLine($"  --fontSize: {FontSize}px; ");
        //    sb.AppendLine($"  --fontSizeSmall: {FontSizeSmall}px; ");
        //    sb.AppendLine($"  --fontSizeBig: {FontSizeBig}px; ");
        //    sb.AppendLine($"  --backColor: {BackColor}; ");
        //    sb.AppendLine($"  --textColor: {TextColor}; ");
        //    sb.AppendLine($"  --textPadding: {TextPadding}; ");
        //    sb.AppendLine("} ");
        //}

        private void PrintParagraphStyle(StringBuilder sb, string styleName, string backColor, string textColor)
        {
            sb.AppendLine($".{styleName} {{ background-color: {backColor}; color: {textColor}; }} ");
            sb.AppendLine($"a.{styleName}:link    {{text-decoration: none;      color: {textColor}; background-color: {backColor};}}");
            sb.AppendLine($"a.{styleName}:visited {{text-decoration: none;      color: {textColor}; background-color: {backColor};}}");
            sb.AppendLine($"a.{styleName}:hover   {{text-decoration: underline; color: {textColor}; background-color: {backColor};}}");
            sb.AppendLine($"a.{styleName}:active  {{text-decoration: none;      color: {textColor}; background-color: {backColor};}}");
            sb.AppendLine(" ");
        }

        protected override void ItalicBoldStyles(StringBuilder sb)
        {
            sb.AppendLine("i, b, em  {  ");
            sb.AppendLine($" font-family: \"{Visual.FontFamily}\";");
            sb.AppendLine($" font-size: {Visual.FontSize}px;  ");
            sb.AppendLine($" color: {Visual.ItalicColor};  ");
            sb.AppendLine(" font-weight: bold;  ");
            sb.AppendLine("}  ");
        }

        protected override void HeaderStyle(StringBuilder sb, int header, float size, string align= "left")
        {
            sb.AppendLine($"h{header} {{");
            sb.AppendLine($"font-family: \"{Visual.FontFamily}\";");
            sb.AppendLine($"font-size: {size}px;");
            sb.AppendLine($"text-align: {align};");
            sb.AppendLine("font-weight: bold;  ");
            sb.AppendLine($"color: {Visual.ItalicColor};  ");
            sb.AppendLine("}");
        }


        protected override void Styles(StringBuilder sb)
        {
            sb.AppendLine("<style type=\"text/css\">  ");
            sb.AppendLine("  ");

            // Body and Table
            sb.AppendLine($"body {{font-family: \"{Visual.FontFamily}\"; font-size: {Visual.FontSize}px; color: {Visual.TextColor};}}");
            sb.AppendLine("table {  ");
            sb.AppendLine("    border: 0px solid #CCC;  ");
            sb.AppendLine("    border-collapse: collapse;  ");
            sb.AppendLine("}  ");
            sb.AppendLine($"td   {{font-family: \"{Visual.FontFamily}\"; padding: 0px; font-size: {Visual.FontSize}px; text-align: left; font-style: none; text-transform: none; font-weight: none; border: none;}}");

            // Sup
            sb.AppendLine($"sup  {{font-size: {Visual.FontSizeSmall}px;  color: {Visual.TextColor};}}");

            sb.AppendLine(" ");
            sb.AppendLine(".parStarted, .parWorking, .parDoubt, .parOk, .parClosed, .textNormal");
            sb.AppendLine("{ ");
            sb.AppendLine($" font-family: \"{Visual.FontFamily}\"; ");
            sb.AppendLine($" font-size: {Visual.FontSize}px; ");
            sb.AppendLine(" text-align: justify;  ");
            sb.AppendLine(" text-justify: inter-word;  ");
            sb.AppendLine($" padding-top: {Visual.TextPadding}px; ");
            sb.AppendLine($" padding-right: {Visual.TextPadding}px; ");
            sb.AppendLine($" padding-bottom: {Visual.TextPadding}px; ");
            sb.AppendLine($" padding-left: {Visual.TextPadding}px; ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");

            sb.AppendLine(".textNormal ");
            sb.AppendLine("{  ");
            sb.AppendLine($"  background-color: {Visual.BackColor};  ");
            sb.AppendLine($"  color: {Visual.TextColor}; ");
            sb.AppendLine("}  ");

            HeaderStyle(sb, 2, Visual.FontSize + 6);
            HeaderStyle(sb, 3, Visual.FontSize + 2);

            PrintParagraphStyle(sb, "parStarted", Visual.BackColorStarted, Visual.TextColorStarted);
            PrintParagraphStyle(sb, "parWorking", Visual.BackColorWorking, Visual.TextColorWorking);
            PrintParagraphStyle(sb, "parDoubt", Visual.BackColorDoubt, Visual.TextColorDoubt);
            PrintParagraphStyle(sb, "parOk", Visual.BackColorOk, Visual.TextColorOk);
            PrintParagraphStyle(sb, "parClosed", Visual.BackColorClosed, Visual.TextColorClosed);

            ItalicBoldStyles(sb);

            sb.AppendLine("</style>  ");

        }

        protected override string statusStyleName(ParagraphStatus ParagraphStatus)
        {
            return "par" + ParagraphStatus.ToString();
        }

        #region Format Left or Middle Paragraph
        protected void FormatParagraphHeader(StringBuilder sb, Paragraph p, string textDirection, int header)
        {
            sb.AppendLine($"<td width= \"{percent:0.00}%\" valign=\"top\" class=\"textNormal\" {textDirection}>");
            sb.AppendLine($"<h{header}><small>{p.Identification}</small> {p.Text}</h{header}>");
            sb.AppendLine("</td> ");
        }

        protected void FormatParagraphNormal(StringBuilder sb, Paragraph p, string textDirection, bool useBloquote = false)
        {
            sb.AppendLine($"<td width= \"{percent:0.00}%\" valign=\"top\" class=\"textNormal\" {textDirection}>");
            sb.AppendLine($"{(useBloquote ? "\"<bloquote>" : "")}<small>{p.Identification}</small> {p.Text}{(useBloquote ? "\"</bloquote>" : "")}");
            sb.AppendLine("</td> ");
        }

        protected void FormatParagraph(StringBuilder sb, Paragraph p)
        {
            sb.AppendLine(" ");
            string textDirection = TextDirection(p);
            switch (p.Format)
            {
                case ParagraphHtmlType.BookTitle:
                    FormatParagraphHeader(sb, p, textDirection, 1);
                    break;
                case ParagraphHtmlType.PaperTitle:
                    FormatParagraphHeader(sb, p, textDirection, 2);
                    break;
                case ParagraphHtmlType.SectionTitle:
                    FormatParagraphHeader(sb, p, textDirection, 3);
                    break;
                case ParagraphHtmlType.NormalParagraph:
                    FormatParagraphNormal(sb, p, textDirection);
                    break;
                case ParagraphHtmlType.IdentedParagraph:
                    FormatParagraphNormal(sb, p, textDirection, true);
                    break;
            }
        }

        #endregion


        #region Format Right Paragraph
        protected void FormatRightParagraphHeader(StringBuilder sb, Paragraph p, string textDirection, int header)
        {
            sb.AppendLine($"<td width= \"{percent:0.00}%\" valign=\"top\" class=\"{statusStyleName(p.Status)}\" {textDirection}>");
            sb.AppendLine($"<div {DivName(p)} class=\"{statusStyleName(p.Status)}\" {textDirection}>");
            sb.AppendLine($"<h{header}><a href=\"about:ident\" ident=\"{p.AName}\" class=\"{statusStyleName(p.Status)}\"><small>{p.Identification}</small></a> {p.Text}</h{header}>");
            sb.AppendLine("</div> ");
            sb.AppendLine("</td> ");
        }

        protected void FormatRightParagraphNormal(StringBuilder sb, Paragraph p, string textDirection, bool useBloquote = false)
        {
            sb.AppendLine($"<td width= \"{percent.ToString("0.00")}%\" valign=\"top\">");
            sb.AppendLine($"<div {DivName(p)} class=\"{statusStyleName(p.Status)}\" {textDirection}>");
            sb.AppendLine($"{(useBloquote ? "<bloquote>" : "")}<small><a href=\"about:ident\" ident=\"{p.AName}\" class=\"{statusStyleName(p.Status)}\">{p.Identification}</a></small> {p.Text}{(useBloquote ? "</bloquote>" : "")}>");
            sb.AppendLine("</div> ");
            sb.AppendLine("</td> ");
        }

        protected void FormatRightParagraph(StringBuilder sb, Paragraph p)
        {
            string textDirection = TextDirection(p);
            switch (p.Format)
            {
                case ParagraphHtmlType.BookTitle:
                    FormatRightParagraphHeader(sb, p, textDirection, 1);
                    break;
                case ParagraphHtmlType.PaperTitle:
                    FormatRightParagraphHeader(sb, p, textDirection, 2);
                    break;
                case ParagraphHtmlType.SectionTitle:
                    FormatRightParagraphHeader(sb, p, textDirection, 3);
                    break;
                case ParagraphHtmlType.NormalParagraph:
                    FormatRightParagraphNormal(sb, p, textDirection);
                    break;
                case ParagraphHtmlType.IdentedParagraph:
                    FormatRightParagraphNormal(sb, p, textDirection, true);
                    break;
            }
        }

        #endregion

        protected override void HtmlFomatPage(StringBuilder sb, short paperNo, 
            List<Paragraph> leftTranslation, 
            List<Paragraph> middleTranslation = null, 
            List<Paragraph> rightTranslation = null, 
            bool showCompare = false)
        {
            try
            {
                //pageStart(sb, paperNo, true);
                //titleLine(sb);
                Styles(sb);

                sb.AppendLine("<table border=\"1\" width=\"100%\" id=\"table1\" cellspacing=\"4\" cellpadding=\"0\">");

                for (int i = 0; i < rightTranslation.Count; i++)
                {
                    sb.AppendLine("<tr>");
                    FormatParagraph(sb, leftTranslation[i]);
                    if (middleTranslation != null)
                    {
                        FormatParagraph(sb, middleTranslation[i]);
                    }
                    if (rightTranslation != null)
                    {
                        FormatRightParagraph(sb, rightTranslation[i]);
                    }
                    if (showCompare)
                    {
                        //sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + HtmlCompare(p, FullPaperCompareTranslation.Middle) + "</td>");
                    }
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</table>");
                //pageEnd(sb);

            }
            catch (Exception ex)
            {
                sb.AppendLine(ShowErrorMessage(ex.Message));
            }
        }



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
