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
        private Notes NotesObject = null;

        public PaperEdit(Notes notes, short paperNo, string repositoryFolder)
        {
            NotesObject = notes;    
            paperEditNo = paperNo;
            RepositoryFolder = repositoryFolder;
            GetParagraphsFromRepository();
        }

        /// <summary>
        /// Read all paragraph from disk
        /// </summary>
        private void GetParagraphsFromRepository()
        {
            foreach (string pathParagraphFile in Directory.GetFiles(RepositoryFolder, $"Par_{paperEditNo:000}_*.md"))
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

        /// <summary>
        /// Get all availble notes data
        /// </summary>
        /// <param name="paragraph"></param>
        public void GetNotesData(ParagraphMarkDown paragraph)
        {
            Note note = NotesObject.GetNote(paragraph);
            paragraph.SetNote(note);
        }

    }
}
