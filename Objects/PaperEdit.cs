using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;

namespace UbStandardObjects.Objects
{
    public class PaperEdit : Paper
    {

        private string FolderPaper { get; set; } = "";
        private short paperEditNo = -1;

        public PaperEdit(short paperNo, string folderPaper)
        {
            paperEditNo = paperNo;
            FolderPaper = folderPaper;
            GetParagraphsFromRepository();
        }

        /// <summary>
        /// Read all paragraph from disk
        /// </summary>
        private void GetParagraphsFromRepository()
        {
            foreach (string pathParagraphFile in Directory.GetFiles(FolderPaper, $"Par_{paperEditNo:000}_*.md"))
            {
                Paragraphs.Add(new ParagraphMarkDown(pathParagraphFile));
            }
            // Sort

            Paragraphs.Sort(delegate (Paragraph p1, Paragraph p2)
            {
                if (p1.Section < p2.Section)
                {
                    return -1;
                }
                if (p1.Section > p2.Section)
                {
                    return 1;
                }
                if (p1.ParagraphNo < p2.ParagraphNo)
                {
                    return -1;
                }
                if (p1.ParagraphNo > p2.ParagraphNo)
                {
                    return 1;
                }
                return 0;
            });

        }

        /// <summary>
        /// Return an specific paragraph
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public override Paragraph GetParagraph(TOC_Entry entry)
        {
            if (Paragraphs.Count == 0)
            {
                GetParagraphsFromRepository();
            }
            return Paragraphs.Find(p => p.Section == entry.Section && p.ParagraphNo == entry.ParagraphNo);
        }

        private List<Note> Notes = null;

        /// <summary>
        /// Get the zipped format table json and unzipp it to return
        /// </summary>
        /// <returns></returns>
        public bool GetNotes(string repositoryFolder, short paperNo)
        {
            try
            {
                string filePath = Path.Combine(repositoryFolder, $@"{ParagraphMarkDown.FolderPath(paperNo)}\Notes.json");
                string jsonString = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    WriteIndented = true,
                };
                NotesRoot root = JsonSerializer.Deserialize<NotesRoot>(jsonString, options);
                if (root != null)
                {
                    Notes = new List<Note>(root.Notes);
                    foreach (ParagraphMarkDown p in Paragraphs)
                    {
                        GetStatus(p);
                    }
                    return true;
                }
                // Fill complementary data
                return false;
            }
            catch (Exception ex)
            {
                StaticObjects.Logger.Error("GetNotes", ex);
                return false;
            }
        }

        public void GetStatus(ParagraphMarkDown paragraph)
        {
            Note note= Notes.Find(n => n.Paper == PaperNo && n.Section == paragraph.Section && n.Paragraph == paragraph.ParagraphNo);
            if (note != null)
            {
                paragraph.SetStatusDate(note.Status, note.LastDate, note.Format);
            }
            else
            {
                paragraph.SetStatusDate(0, DateTime.MinValue, 1);
            }
        }

        // ----------------------------------
        // Classes used to get data from file

        private class NotesRoot
        {
            public Note[] Notes { get; set; }
        }

        public class Note
        {
            public int Paper { get; set; }
            public int Section { get; set; }
            public int Paragraph { get; set; }
            public string TranslatorNote { get; set; }
            public string Notes { get; set; }
            public DateTime LastDate { get; set; }
            public short Status { get; set; } = 0;
            public short Format { get; set; } = 0;
        }

    }
}
