using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace UbStandardObjects.Objects
{
    public class ParagraphMarkDown : Paragraph
    {
        public string RelativeFilePath { get; set; } = "";

        public string RepositoryPath { get; set; } = "";

        public string RelativeFilePathWindows
        {
            get
            {
                return RelativeFilePath.Replace("/", "\\");
            }
        }

        public string BuildRelativePath
        {
            get
            {
                return $"Doc{Paper:000}/Par_{Paper:000}_{Section:000}_{ParagraphNo:000}.md";
            }
        }

        public string FullPath
        {
            get
            {
                return Path.Combine(RepositoryPath, RelativeFilePathWindows);
            }
        }

        public string Ident
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FullPath);
            }
        }

        public string LocalPathFile
        {
            get { return Path.GetDirectoryName(RepositoryPath); }
        }

        public ParagraphMarkDown(string baseRepositoryPath, string relativeFilePath)
        {
            RepositoryPath = baseRepositoryPath;
            RelativeFilePath = relativeFilePath;
            Text = MarkdownToHtml(File.ReadAllText(FullPath));
            char[] sep = { '_' };
            string fileName = Path.GetFileNameWithoutExtension(FullPath).Remove(0, 4);
            string[] parts = fileName.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            Paper = Convert.ToInt16(parts[0]);
            Section = Convert.ToInt16(parts[1]);
            ParagraphNo = Convert.ToInt16(parts[2]);
        }

        public ParagraphMarkDown(string filePath)
        {
            char[] sep = { '_' };
            string fileName = Path.GetFileNameWithoutExtension(filePath).Remove(0, 4);
            string[] parts = fileName.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            Paper = Convert.ToInt16(parts[0]);
            Section = Convert.ToInt16(parts[1]);
            ParagraphNo = Convert.ToInt16(parts[2]);
            Text = MarkdownToHtml(File.ReadAllText(filePath));
            RelativeFilePath = BuildRelativePath;
            RepositoryPath = filePath.Replace(RelativeFilePathWindows, "");
        }


        /// <summary>
        /// Convert paragraph markdown to HTML
        /// The markdown used for TUB paragraphs has just italics
        /// </summary>
        /// <param name="markDownText"></param>
        /// <returns></returns>
        private string MarkdownToHtml(string markDownText)
        {
            int position = 0;
            bool openItalics = true;

            string newText = markDownText;
            var regex = new Regex(Regex.Escape("*"));
            while (position >= 0)
            {
                position = newText.IndexOf('*');
                if (position >= 0)
                {
                    newText = regex.Replace(newText, openItalics ? "<i>" : "</i>", 1);
                    openItalics = !openItalics;
                }
            }
            return newText;
        }

        public static string FolderPath(short paperNo)
        {
            return $"Doc{paperNo:000}";
        }


        public static string FilePath(short paperNo, short section, short paragraphNo)
        {
            return $"{FolderPath(paperNo)}\\Par_{paperNo:000}_{section:000}_{paragraphNo:000}.md";
        }

        public static string FilePath(string ident)
        {
            // 120:0-1 (0.0)
            char[] separators = { ':', '-', ' ' };
            string[] parts = ident.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            short paperNo = Convert.ToInt16(parts[0]);
            short section = Convert.ToInt16(parts[1]);
            short paragraphNo = Convert.ToInt16(parts[2]);
            return $"Doc{paperNo:000}\\Par_{paperNo:000}_{section:000}_{paragraphNo:000}.md";
        }

        public bool Save(string text, Note note)
        {
            try
            {
                Text = text;
                File.WriteAllText(FullPath, text);
                return true;
            }
            catch (Exception ex)
            {
                throw;
                //return false;
            }
        }

        /// <summary>
        /// Get the zipped format table json and unzipp it to return
        /// </summary>
        /// <returns></returns>
        public Note GetNotes(string repositoryFolder, short paperNo)
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
                    return root.Notes.ToList().Find(n => n.Paper == paperNo && n.Section == Section && n.Paragraph == ParagraphNo);
                }
                return null;
            }
            catch (Exception ex)
            {
                StaticObjects.Logger.Error("GetNotes", ex);
                return null;
            }
        }

        // ----------------------------------
        // Classes uded to get data from file

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
        }



    }
}
