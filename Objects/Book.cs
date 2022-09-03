using System;
using System.Collections.Generic;

namespace UbStandardObjects.Objects
{
    /// <summary>
    /// Bilingual book main organization
    /// </summary>
    public class Book
    {

        protected GetDataFiles DataFiles = null;


        public Translation LeftTranslation { get; set; }

        public Translation RightTranslation { get; set; }

        public List<Translation> Translations { get; set; } = null;

        public List<Translation> ObservableTranslations
        {
            get
            {
                List<Translation> list = new List<Translation>();
                list.AddRange(Translations);
                return list;
            }
        }

        public FormatTable FormatTableObject { get; set; } = null;

        /// <summary>
        /// Inicialize the list of available translations
        /// </summary>
        /// <param name="dataFiles"></param>
        /// <returns></returns>
        protected bool InicializeTranslations(GetDataFiles dataFiles)
        {
            try
            {
                DataFiles = dataFiles;
                if (Translations == null)
                {
                    Translations = DataFiles.GetTranslations();
                }
                return true;
            }
            catch (Exception ex)
            {
                string message = $"Could not initialize available translations. See log.";
                StaticObjects.Logger.Error(message, ex);
                StaticObjects.Logger.FatalError(message);
                return false;
            }
        }

        /// <summary>
        /// Get a translation from the list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Initialize the format table used for editing translations
        /// </summary>
        public FormatTable GetFormatTable()
        {
            try
            {
                if (FormatTableObject == null)
                {
                    FormatTableObject = new FormatTable(DataFiles.GetFormatTable());
                }
                return FormatTableObject;
            }
            catch (Exception ex)
            {
                StaticObjects.Logger.Error($"Missing format table. May be you do not have the correct data to use this tool.", ex);
                return null;
            }
        }


        /// <summary>
        /// Inicialize book and 2 translations
        /// </summary>
        /// <param name="dataFiles"></param>
        /// <param name="leftTranslationId"></param>
        /// <param name="rightTranslationID"></param>
        /// <returns></returns>
        public virtual bool Inicialize(GetDataFiles dataFiles, short leftTranslationId, short rightTranslationID)
        {
            try
            {
                if (!InicializeTranslations(dataFiles))
                {
                    return false;
                }
                LeftTranslation = DataFiles.GetTranslation(leftTranslationId);
                if (!LeftTranslation.CheckData()) return false;
                RightTranslation = DataFiles.GetTranslation(rightTranslationID);
                if (!RightTranslation.CheckData()) return false;
                return true;
            }
            catch (Exception ex)
            {
                string message = $"Could not initialize translations {leftTranslationId} and {rightTranslationID}. See log.";
                StaticObjects.Logger.Error(message, ex);
                StaticObjects.Logger.FatalError(message);
                return false;
            }
        }

        /// <summary>
        /// Inicialize only the book object
        /// </summary>
        /// <param name="dataFiles"></param>
        /// <returns></returns>
        public virtual bool Inicialize(GetDataFiles dataFiles)
        {
            return InicializeTranslations(dataFiles);
        }

        /// <summary>
        /// Inicialize only the book object
        /// </summary>
        /// <param name="dataFiles"></param>
        /// <returns></returns>
        public virtual Translation InicializeTranslations(GetDataFiles dataFiles, short translationId)
        {
            try
            {
                if (!InicializeTranslations(dataFiles))
                {
                    return null;
                }
                return DataFiles.GetTranslation(translationId);
            }
            catch (Exception ex)
            {
                string message = $"Could not initialize translation {translationId}. See log.";
                StaticObjects.Logger.Error(message, ex);
                StaticObjects.Logger.FatalError(message);
                return null;
            }
        }

        public virtual void StoreAnnotations(TOC_Entry entry, List<UbAnnotationsStoreData> annotations)
        {

        }

        public virtual void DeleteAnnotations(TOC_Entry entry)
        {

        }



}
}
