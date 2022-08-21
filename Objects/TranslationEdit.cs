using System.Collections.Generic;
using System.IO;
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



    }
}
