using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace UbStandardObjects.Objects
{
    // ----------------------------------
    // Classes used to get data from file


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


    public class Notes
    {
        private class NotesRoot
        {
            public Note[] Notes { get; set; }
        }

        private List<Note> ListNotes = null;

        private string RepositoryFolder = null;

        public Notes(string repositoryFolder)
        {
            RepositoryFolder = repositoryFolder;
        }

        private void GetNotes(short paperNo)
        {
            string filePath = Path.Combine(RepositoryFolder, $@"{ParagraphMarkDown.FolderPath(paperNo)}\Notes.json");
            string jsonString = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                WriteIndented = true,
            };
            NotesRoot root = JsonSerializer.Deserialize<NotesRoot>(jsonString, options);
            if (root != null)
            {
                ListNotes = new List<Note>(root.Notes);
            }
        }

        public Note GetNote(Paragraph p)
        {
            if (ListNotes == null || ListNotes[0].Paper != p.Paper)
            {
                GetNotes(p.Paper);
            }
            return ListNotes.Find(n => n.Paper == p.Paper && n.Section == p.Section && n.Paragraph == p.ParagraphNo);
        }
    }
}
