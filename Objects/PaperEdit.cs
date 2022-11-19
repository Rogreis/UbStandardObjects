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

        private string RepositoryFolder { get; set; } = "";
        private short paperEditNo = -1;
        private FormatTable Format = null;

        public PaperEdit(short paperNo, string repositoryFolder)
        {
            paperEditNo = paperNo;
            RepositoryFolder = repositoryFolder;
            if (Format == null)
            {
                Format= StaticObjects.Book.GetFormatTable();
            }
            else
            {
                throw new Exception("Error in PaperEdit Constructor: Format table not available - could not generate edit paper.");
            }
            GetParagraphsFromRepository();
        }

        /// <summary>
        /// Read all paragraph from disk
        /// </summary>
        private void GetParagraphsFromRepository()
        {
            foreach (string pathParagraphFile in Directory.GetFiles(RepositoryFolder, $@"Doc{paperEditNo:000}\Par_{paperEditNo:000}_*.md"))
            {
                ParagraphMarkDown p = new ParagraphMarkDown(pathParagraphFile);
                Format.GetParagraphFormatData(p);
                Note note= Notes.GetNote(p);
                p._status = note.Status;
                Paragraphs.Add(p);
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
        /// Get all availble notes data
        /// </summary>
        /// <param name="paragraph"></param>
        public void GetNotesData(ParagraphMarkDown paragraph)
        {
            Note note = Notes.GetNote(paragraph);
            paragraph.SetNote(note);
        }

        /// <summary>
        /// Return an specific paragraph
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public override Paragraph GetParagraph(TOC_Entry entry)
        {
            //if (Paragraphs.Count == 0)
            //{
            //    GetParagraphsFromRepository();
            //}
            // Always get the paragraph from repository
            string filePath = ParagraphMarkDown.FullPath(RepositoryFolder, entry.Paper, entry.Section, entry.ParagraphNo);
            if (!File.Exists(filePath)) return null;
            ParagraphMarkDown par = new ParagraphMarkDown(filePath);
            GetNotesData(par);
            return par;
        }


    }
}
