﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.Json;
using File = System.IO.File;

namespace UbStandardObjects.Objects
{
    public class TranslationEdit : Translation
    {

        private string LocalRepositoryFolder = null;

        public TranslationEdit(string localRepositoryFolder)
        {
            LocalRepositoryFolder = localRepositoryFolder;
        }

        private Paper GetPaperEdit(short paperNo)
        {
            FormatTable table = StaticObjects.Book.GetFormatTable();
            Paper paper = new Paper();
            paper.Paragraphs = new List<Paragraph>();
            Translation EnglishTranslation = StaticObjects.Book.GetTranslation(0);
            string folderPaper = Path.Combine(LocalRepositoryFolder, $"Doc{paperNo:000}");
            foreach (string filePath in Directory.GetFiles(folderPaper, "*.md"))
            {
                ParagraphEdit paragraph = new ParagraphEdit(filePath);
                paragraph.FormatInt = EnglishTranslation.GetFormat(paragraph);
                paper.Paragraphs.Add(paragraph);
            }
            return paper;
        }

        public override Paper Paper(short paperNo)
        {
            return GetPaperEdit(paperNo);
        }

        public ParagraphMarkDown GetParagraph(short paperNo, short section, short paragraphNo)
        {
            ParagraphMarkDown par = new ParagraphMarkDown(LocalRepositoryFolder, ParagraphMarkDown.FilePath(paperNo, section, paragraphNo));
            return par;
        }

        #region Index
        private TUB_TOC_Entry JsonIndexEntry(string fileNamePath, bool isPaperTitle, short paperNoParam = -1)
        {
            char[] separators = { '_' };
            string fileName = Path.GetFileNameWithoutExtension(fileNamePath);
            string[] parts = fileName.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            short paperNo = Convert.ToInt16(parts[1]);
            short sectionNo = Convert.ToInt16(parts[2]);
            short paragraphNo = Convert.ToInt16(parts[3]);
            if (!isPaperTitle && sectionNo == 0 && paragraphNo == 0)
            {
                return null;
            }
            string text = File.ReadAllText(fileNamePath, Encoding.UTF8);
            return new TUB_TOC_Entry()
            {
                Text = paperNoParam > 0 ? paperNoParam.ToString() + " - " + text : text,
                Url = $@"Doc{paperNo:000}\Par_{paperNo:000}_{sectionNo:000}_{paragraphNo:000}.md"
            };
        }

        private TUB_TOC_Entry JsonIndexEntry(string text)
        {
            return new TUB_TOC_Entry()
            {
                Text = text
            };
        }

        /// <summary>
        /// Create an object with all the index entry for the editing translation
        /// </summary>
        /// <returns></returns>
        public List<TUB_TOC_Entry> GetTranslationIndex()
        {
            string indexJsonFilePath = Path.Combine(LocalRepositoryFolder, "index.json");
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                WriteIndented = true,
                IncludeFields = true
            };

            if (File.Exists(indexJsonFilePath))
            {
                string json = File.ReadAllText(indexJsonFilePath);
                //var data= JsonSerializer.Deserialize(json, options);
                //return null;
                return JsonSerializer.Deserialize<List<TUB_TOC_Entry>>(json, options);
            }

            List<TUB_TOC_Entry> list = new List<TUB_TOC_Entry>();
            string pathIntroduction = Path.Combine(LocalRepositoryFolder, "Doc000\\Par_000_000_000.md");
            TUB_TOC_Entry intro = JsonIndexEntry(pathIntroduction, true, 0);
            TUB_TOC_Entry part1 = JsonIndexEntry("Parte I");
            TUB_TOC_Entry part2 = JsonIndexEntry("Parte II");
            TUB_TOC_Entry part3 = JsonIndexEntry("Parte III");
            TUB_TOC_Entry part4 = JsonIndexEntry("Parte IV");

            list.Add(intro);
            list.Add(part1);
            list.Add(part2);
            list.Add(part3);
            list.Add(part4);

            for (short paperNo = 0; paperNo < 197; paperNo++)
            {
                string folderPath = Path.Combine(LocalRepositoryFolder, $"Doc{paperNo:000}");
                TUB_TOC_Entry jsonIndexPaper = null;
                string pathTitle = Path.Combine(folderPath, $"Par_{paperNo:000}_000_000.md");
                if (paperNo == 0)
                {
                    jsonIndexPaper = intro;
                }
                else
                {
                    jsonIndexPaper = JsonIndexEntry(pathTitle, true, paperNo);
                }
                if (paperNo == 0)
                {
                    // do nothing
                }
                else if (paperNo < 32)
                {
                    part1.Nodes.Add(jsonIndexPaper);
                }
                else if (paperNo < 57)
                {
                    part2.Nodes.Add(jsonIndexPaper);
                }
                else if (paperNo < 120)
                {
                    part3.Nodes.Add(jsonIndexPaper);
                }
                else
                {
                    part4.Nodes.Add(jsonIndexPaper);
                }

                foreach (string mdFile in Directory.GetFiles(folderPath, $"Par_{paperNo:000}_???_000*.md"))
                {
                    TUB_TOC_Entry paperSection = JsonIndexEntry(mdFile, false);
                    if (paperSection != null)
                        jsonIndexPaper.Nodes.Add(paperSection);
                };
            }

            // Serialize the index
            string jsonString = JsonSerializer.Serialize<List<TUB_TOC_Entry>>(list, options);
            File.WriteAllText(indexJsonFilePath, jsonString);

            return list;
        }

        #endregion


    }
}
