using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UbStandardObjects;
using UbStandardObjects.Objects;

namespace UbStandardObjects.Objects
{
    public class TranslationEdit: Translation
    {

        private string LocalRepositoryFolder = null;

        public TranslationEdit(string localRepositoryFolder)
        {
            LocalRepositoryFolder= localRepositoryFolder;
        }

        private Paper GetPaperEdit(short paperNo)
        {
            FormatTable table= StaticObjects.Book.GetFormatTable();
            Paper paper = new Paper();
            paper.Paragraphs = new List<Paragraph>();
            Translation EnglishTranslation = StaticObjects.Book.GetTranslation(0);
            string folderPaper = Path.Combine(LocalRepositoryFolder, $"Doc{paperNo:000}");
            foreach(string filePath in Directory.GetFiles(folderPaper, "*.md"))
            {
                ParagraphEdit paragraph = new ParagraphEdit(filePath);
                paragraph.FormatInt= EnglishTranslation.GetFormat(paragraph);
                paper.Paragraphs.Add(paragraph);
            }
            return paper;
        }

        public override Paper Paper(short paperNo)
        {
            return GetPaperEdit(paperNo);
        }

        public List<BookIndex> GetTranslationIndex()
        {
            List<BookIndex> list = new List<BookIndex>();

            for (short paperNo= 0; paperNo<197; paperNo++)
            {
                string folderPath= Path.Combine(LocalRepositoryFolder, $"Doc{paperNo:000}");
                string filePaperTitle= Path.Combine(folderPath, $"Par_{paperNo:000}_000_000.md");
                BookIndex index = new BookIndex()
                {
                    PaperNo = paperNo,
                    Text = File.ReadAllText(filePaperTitle, Encoding.UTF8),
                };
                list.Add(index);
                foreach (string mdFile in Directory.GetFiles(folderPath, $"Par_{paperNo:000}_*.md"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(mdFile);
                    BookIndex indexSection = new BookIndex()
                    {
                        Section = Convert.ToInt16(fileName.Substring(8, 3)),
                        ParagraphNo = Convert.ToInt16(fileName.Substring(12, 3))
                    };
                    index.SubItems.Add(indexSection);
                };


            }
            return list;
        }


    }
}
