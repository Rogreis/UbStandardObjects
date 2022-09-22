using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.Json.Serialization;

namespace UbStandardObjects.Objects
{
    public class HtmlFormatParameters
    {
        // From https://getbootstrap.com/docs/5.0/utilities/background/
        // https://www.w3schools.com/tags/ref_colornames.asp
        private const string ClassParagraphStarted = "p-3 mb-2 bg-secondary text-white";
        private const string ClassParagraphWorking = "p-3 mb-2 bg-warning text-dark";
        private const string ClassParagraphDoubt = "p-3 mb-2 bg-danger text-white";
        private const string ClassParagraphOk = "p-3 mb-2 bg-primary text-white";

        private const string ClassParagraphDarkTheme = "p-3 mb-2 bg-dark text-white";
        private const string ClassParagraphLightTheme = "p-3 mb-2 bg-transparent text-dark";

        private const string darkTheme = "bg-dark text-white";
        private const string lightTheme = "bg-light text-black";



        public float FontSize { get; set; } = 14;

        public string FontFamily { get; set; } = "Verdana,Arial,Helvetica";

        public bool IsDarkTheme { get; set; } = true;  

        public string DarkText { get; set; } = "black";
        
        public string LightText { get; set; } = "white";

        public string DarkTextHighlihted { get; set; } = "yellow";

        public string LightTextHighlihted { get; set; } = "blue";

        public string DarkTextGray { get; set; } = "yellow";

        public string LightTextGray { get; set; } = "bisque";

        public string BackTextColor
        {
            get
            {
                return IsDarkTheme ? darkTheme : lightTheme;
            }
        }

        //// Colors
        //// Color code source: https://rgbcolorcode.com/color/FFB3B3
        //[JsonPropertyName("ForegroundColor")]
        //public int _foregroundColor { get; set; } = Color.Black.ToArgb();

        //[JsonIgnore]
        //public Color ForegroundColor
        //{
        //    get { return Color.FromArgb(_foregroundColor); }
        //    set { _foregroundColor = value.ToArgb(); }
        //}

        //// Colors
        //// Color code source: https://rgbcolorcode.com/color/FFB3B3
        //[JsonPropertyName("ItalicForegroundColor")]
        //public int _italicforegroundColor { get; set; } = Color.Magenta.ToArgb();

        //[JsonIgnore]
        //public Color ItalicForegroundColor
        //{
        //    get { return Color.FromArgb(_italicforegroundColor); }
        //    set { _italicforegroundColor = value.ToArgb(); }
        //}


        //[JsonPropertyName("BackgroundStarted")]
        //public int _backgroundStarted = Color.White.ToArgb();

        //[JsonIgnore]
        //public Color BackgroundStarted
        //{
        //    get { return Color.FromArgb(_backgroundStarted); }
        //    set { _backgroundStarted = value.ToArgb(); }
        //}

        //[JsonPropertyName("BackgroundWorking")]
        //public int _backgroundWorking = Color.FromArgb(238, 255, 204).ToArgb();

        //[JsonIgnore]
        //public Color BackgroundWorking
        //{
        //    get { return Color.FromArgb(_backgroundWorking); }
        //    set { _backgroundWorking = value.ToArgb(); }
        //}

        //[JsonPropertyName("BackgroundDoubt")]
        //public int _backgroundDoubt = Color.FromArgb(255, 179, 179).ToArgb();

        //[JsonIgnore]
        //public Color BackgroundDoubt
        //{
        //    get { return Color.FromArgb(_backgroundDoubt); }
        //    set { _backgroundDoubt = value.ToArgb(); }
        //}

        //[JsonPropertyName("BackgroundOk")]
        //public int _backgroundOk = Color.FromArgb(204, 255, 230).ToArgb();

        //[JsonIgnore]
        //public Color BackgroundOk
        //{
        //    get { return Color.FromArgb(_backgroundOk); }
        //    set { _backgroundOk = value.ToArgb(); }
        //}

        //[JsonPropertyName("BackgroundClosed")]
        //public int _backgroundClosed = Color.FromArgb(212, 212, 212).ToArgb();

        //[JsonIgnore]
        //public Color BackgroundClosed
        //{
        //    get { return Color.FromArgb(_backgroundClosed); }
        //    set { _backgroundClosed = value.ToArgb(); }
        //}


        /// <summary>
        /// Returns the classes for a paragraph depending on IsEditTranslation and IsDarkTheme
        /// </summary>
        /// <param name="ParagraphStatus"></param>
        /// <returns></returns>
        public string ParagraphClass(Paragraph p)
        {
            if (p != null && p.IsEditTranslation)
            {
                switch (p.Status)
                {
                    case ParagraphStatus.Started:
                        return "parStarted";
                    case ParagraphStatus.Working:
                        return "parWorking";
                    case ParagraphStatus.Doubt:
                        return "parDoubt";
                    case ParagraphStatus.Ok:
                        return "parOk";
                    case ParagraphStatus.Closed:
                        return "parClosed";
                }
            }
            return "parClosed";
        }
    }
}
