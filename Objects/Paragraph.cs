using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Globalization;
using System.Text.Json.Serialization;

namespace UbStandardObjects.Objects
{

    /// <summary>
    /// Represents the translation status of each paragraph being worked.
    /// </summary>
    public enum ParagraphStatus
    {
        Started = 0,
        Working = 1,
        Doubt = 2,
        Ok = 3,
        Closed = 4
    }

    /// <summary>
    /// Represents the html format for a paragraph
    /// </summary>
    public enum ParagraphHtmlType
    {
        BookTitle = 0,
        PaperTitle = 1,
        SectionTitle = 2,
        NormalParagraph = 3,
        IdentedParagraph = 4
    }


    public class Paragraph
    {
        [JsonPropertyName("TranslationID")]
        public short TranslationId { get; set; } = 0;
        public short Paper { get; set; }
        public short PK_Seq { get; set; }
        public short Section { get; set; }
        public short ParagraphNo { get; set; }
        public short Page { get; set; }
        public short Line { get; set; }
        public virtual string Text { get; set; } = "";
        public int FormatInt { get; set; }

        [JsonIgnore]
        private TOC_Entry entry = null;

        /// <summary>
        /// Status is not to be exported to json files for UbStudyHelp
        /// </summary>
        [JsonIgnore]
        [JsonPropertyName("Status")]
        public int _status { get; set; }

        [JsonIgnore]
        public ParagraphStatus Status
        {
            get
            {
                return (ParagraphStatus)_status;
            }
        }


        public ParagraphHtmlType Format
        {
            get
            {
                return (ParagraphHtmlType)FormatInt;
            }
        }

        [JsonIgnore]
        public TOC_Entry Entry
        {
            get
            {
                if (entry == null)  
                {
                    entry = new TOC_Entry(TranslationId, Paper, Section, ParagraphNo, Page, Line);
                    entry.Text = Text;
                }
                return entry;
            }
        }


        [JsonIgnore]
        public string Identification
        {
            get
            {
                return string.Format("{0}:{1}-{2} ({3}.{4})", Paper, Section, ParagraphNo, Page, Line); ;
            }
        }

        public string ID
        {
            get
            {
                return string.Format($"{Paper}:{Section}-{ParagraphNo})");
            }
        }


        [JsonIgnore]
        public string AName
        {
            get
            {
                return string.Format("U{0}_{1}_{2}", Paper, Section, ParagraphNo); ;
            }
        }

        [JsonIgnore]
        public bool IsPaperTitle
        {
            get
            {
                return Section == 0 && ParagraphNo == 0;
            }
        }

        [JsonIgnore]
        public bool IsSectionTitle
        {
            get
            {
                return ParagraphNo == 0;
            }
        }

        [JsonIgnore]
        public string ParaIdent
        {
            get
            {
                return Paper.ToString("000") + PK_Seq.ToString("000");
            }
        }


        [JsonIgnore]
        public bool IsEditTranslation { get; set; } = false;


        public Paragraph()
        {
        }


        public Paragraph(short translationId)
        {
            TranslationId = translationId;
        }

        // Add this if using nested MemberwiseClone.
        // This is a class, which is a reference type, so cloning is more difficult.
        public Paragraph DeepCopy()
        {
            // Clone the root ...
            Paragraph other = (Paragraph)this.MemberwiseClone();
            return other;
        }

        public override string ToString()
        {
            string partText = Text;
            if (string.IsNullOrEmpty(Text))
            {
                partText = "";
            }
            return $"{Identification} {partText}";
        }


    }
}
