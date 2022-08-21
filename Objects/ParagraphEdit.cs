using System;
using System.IO;

namespace UbStandardObjects.Objects
{
    internal class ParagraphEdit : Paragraph
    {

        public ParagraphEdit(string filePath)
        {
            char[] separators = { '_' };
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string[] parts = fileName.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            Paper = Convert.ToInt16(parts[1]);
            Section = Convert.ToInt16(parts[2]);
            ParagraphNo = Convert.ToInt16(parts[3]);
            Text = File.ReadAllText(filePath);
        }
    }
}
