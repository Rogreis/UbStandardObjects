using System;
using System.Collections.Generic;

namespace UbStandardObjects.Objects
{
    /// <summary>
    /// Bilingual book main organization
    /// </summary>
    public abstract class Book
    {

        protected GetDataFiles DataFiles = null;


        public Translation LeftTranslation { get; set; }

        public Translation RightTranslation { get; set; }

        public List<Translation> Translations { get; set; }

        public Translation GetTranslation(short id)
        {
            Translation trans = Translations.Find(o => o.LanguageID == id);
            string message = "";
            if (trans == null)
            {
                message = $"Missing translation number {id}. May be you do not have the correct data to use this tool.";
                StaticObjects.Logger.FatalError(message);
            }
            return trans;
        }


        public List<Translation> ObservableTranslations
        {
            get
            {
                List<Translation> list = new List<Translation>();
                list.AddRange(Translations);
                return list;
            }
        }


        public abstract void StoreAnnotations(TOC_Entry entry, List<UbAnnotationsStoreData> annotations);

        public abstract void DeleteAnnotations(TOC_Entry entry);

        public virtual bool Inicialize(GetDataFiles dataFiles, short leftTranslationId, short rightTranslationID)
        {
            try
            {
                DataFiles = dataFiles;
                Translations = DataFiles.GetTranslations();
                LeftTranslation = DataFiles.GetTranslation(leftTranslationId);
                if (!LeftTranslation.CheckData()) return false;
                RightTranslation = DataFiles.GetTranslation(rightTranslationID);
                if (!RightTranslation.CheckData()) return false;
                return true;
            }
            catch (Exception ex)
            {
                string message = $"General error getting translations: {ex.Message}. May be you do not have the correct data to use this tool.";
                StaticObjects.Logger.Error(message, ex);
                StaticObjects.Logger.FatalError(message);
                return false;
            }
        }


    }
}
