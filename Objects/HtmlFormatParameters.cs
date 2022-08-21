using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.Json.Serialization;

namespace UbStandardObjects.Objects
{
    public class HtmlFormatParameters
    {

        public string FontFamily { get; set; } = "Verdana,Arial,Helvetica";

        public float FontSize { get; set; } = 10;

        // Colors
        // Color code source: https://rgbcolorcode.com/color/FFB3B3
        [JsonPropertyName("ForegroundColor")]
        public int _foregroundColor { get; set; } = Color.Black.ToArgb();

        [JsonIgnore]
        public Color ForegroundColor
        {
            get { return Color.FromArgb(_foregroundColor); }
            set { _foregroundColor = value.ToArgb(); }
        }

        // Colors
        // Color code source: https://rgbcolorcode.com/color/FFB3B3
        [JsonPropertyName("ItalicForegroundColor")]
        public int _italicforegroundColor { get; set; } = Color.Magenta.ToArgb();

        [JsonIgnore]
        public Color ItalicForegroundColor
        {
            get { return Color.FromArgb(_italicforegroundColor); }
            set { _italicforegroundColor = value.ToArgb(); }
        }


        [JsonPropertyName("BackgroundStarted")]
        public int _backgroundStarted = Color.White.ToArgb();

        [JsonIgnore]
        public Color BackgroundStarted
        {
            get { return Color.FromArgb(_backgroundStarted); }
            set { _backgroundStarted = value.ToArgb(); }
        }

        [JsonPropertyName("BackgroundWorking")]
        public int _backgroundWorking = Color.FromArgb(238, 255, 204).ToArgb();

        [JsonIgnore]
        public Color BackgroundWorking
        {
            get { return Color.FromArgb(_backgroundWorking); }
            set { _backgroundWorking = value.ToArgb(); }
        }

        [JsonPropertyName("BackgroundDoubt")]
        public int _backgroundDoubt = Color.FromArgb(255, 179, 179).ToArgb();

        [JsonIgnore]
        public Color BackgroundDoubt
        {
            get { return Color.FromArgb(_backgroundDoubt); }
            set { _backgroundDoubt = value.ToArgb(); }
        }

        [JsonPropertyName("BackgroundOk")]
        public int _backgroundOk = Color.FromArgb(204, 255, 230).ToArgb();

        [JsonIgnore]
        public Color BackgroundOk
        {
            get { return Color.FromArgb(_backgroundOk); }
            set { _backgroundOk = value.ToArgb(); }
        }

        [JsonPropertyName("BackgroundClosed")]
        public int _backgroundClosed = Color.FromArgb(212, 212, 212).ToArgb();

        [JsonIgnore]
        public Color BackgroundClosed
        {
            get { return Color.FromArgb(_backgroundClosed); }
            set { _backgroundClosed = value.ToArgb(); }
        }

        public virtual string statusBackgroundColor(ParagraphStatus ParagraphStatus)
        {
            /*
             * WPF:
                    System.Drawing.Color myColor = System.Drawing.ColorTranslator.FromHtml("#EFF3F7");
                    this.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(myColor.A, myColor.R, myColor.G, myColor.B));
            * */
            return "#000000";

            //switch (ParagraphStatus)
            //{
            //    case ParagraphStatus.Started:
            //        return System.Drawing.ColorTranslator.ToHtml(Param.BackgroundStarted).Trim();
            //    case ParagraphStatus.Working:
            //        return System.Drawing.ColorTranslator.ToHtml(Param.BackgroundWorking).Trim();
            //    case ParagraphStatus.Doubt:
            //        return System.Drawing.ColorTranslator.ToHtml(Param.BackgroundDoubt).Trim();
            //    case ParagraphStatus.Ok:
            //        return System.Drawing.ColorTranslator.ToHtml(Param.BackgroundOk).Trim();
            //    case ParagraphStatus.Closed:
            //        return System.Drawing.ColorTranslator.ToHtml(Param.BackgroundClosed).Trim();
            //}
            //return System.Drawing.ColorTranslator.ToHtml(Param.BackgroundStarted).Trim();
        }


        //public Font CreateFont()
        //{
        //    FontFamily fontFamily = new FontFamily(FontFamily);
        //    Font font = new Font(
        //        fontFamily,
        //        FontSize,
        //        FontStyle.Regular,
        //        GraphicsUnit.Pixel);
        //    return font;
        //}

    }
}
