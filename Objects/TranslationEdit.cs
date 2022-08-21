using System.Collections.Generic;
using System.IO;
using UbStandardObjects;
using UbStandardObjects.Objects;

namespace UbStandardObjects.Objects
{
    public class TranslationEdit: Translation
    {
        //private ParameterReviewer Parameters = ((ParameterReviewer)StaticObjects.Parameters);

        private Paper GetPaperEdit(string localRepositoryFolder, short paperNo)
        {
            FormatTable table= StaticObjects.Book.GetFormatTable();
            Paper paper = new Paper();
            paper.Paragraphs = new List<Paragraph>();
            Translation EnglishTranslation = StaticObjects.Book.GetTranslation(0);
            string folderPaper = Path.Combine(localRepositoryFolder, $"Doc{paperNo:000}");
            foreach(string filePath in Directory.GetFiles(folderPaper, "*.md"))
            {
                ParagraphEdit paragraph = new ParagraphEdit(filePath);
                paragraph.FormatInt= EnglishTranslation.GetFormat(paragraph);
                paper.Paragraphs.Add(paragraph);
            }
            return paper;
        }

    }
}
