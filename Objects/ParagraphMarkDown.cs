using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            Text= MarkdownToHtml(File.ReadAllText(FullPath));
            char[] sep = { '_' };
            string fileName = Path.GetFileNameWithoutExtension(FullPath).Remove(0, 4);
            string[] parts= fileName.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            Paper= Convert.ToInt16(parts[0]);
            Section = Convert.ToInt16(parts[0]);
            ParagraphNo = Convert.ToInt16(parts[0]);
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
            int position= 0;
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

    }
}
