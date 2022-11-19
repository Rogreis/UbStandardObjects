using System;
using System.Collections.Generic;
using System.Text;

namespace UbStandardObjects.Objects
{
    public class TUB_PT_BR_Page : BootstrapBook
    {
        public TUB_PT_BR_Page(Parameters parameters) : base(parameters)
        {
        }

        public override void GeneratePaper(string destinationFolder, Translation leftTranslation, Paper rightPaper, TUB_TOC_Html toc_table, short paperNo)
        {
            try
            {
                Paper leftPaper = null;
                TranslationIdRight = TranslationIdLeft = Translation.NoTranslation;
                TranslationTextDirection[0] = TranslationTextDirection[2] = false;

                if (leftTranslation != null)
                {
                    TranslationIdLeft = leftTranslation.LanguageID;
                    TranslationTextDirection[2] = leftTranslation.RightToLeft;
                }

                leftPaper = GetPaper(paperNo, leftTranslation);
                FireSendMessage(leftPaper.ToString());
                PrintPaperForGitHubWebSite(destinationFolder, paperNo, leftPaper, rightPaper, toc_table);

            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
