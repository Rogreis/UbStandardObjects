using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace UbStandardObjects.Objects
{
    public class FormatTable
    {

        #region Classes to read json file
        private class FormatTableRootobject
        {
            public Paragraphsformat[] ParagraphsFormat { get; set; }
        }

        private class Paragraphsformat
        {
            public string FormatIdentity { get; set; }
            public int Paper { get; set; }
            public int Section { get; set; }
            public int Paragraph { get; set; }
            public int Page { get; set; }
            public int Line { get; set; }
            public int Format { get; set; }
        }
        #endregion

        private List<Paragraphsformat> Formats = new List<Paragraphsformat>();

        public FormatTable(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            };

            FormatTableRootobject tableRoot = JsonSerializer.Deserialize<FormatTableRootobject>(jsonString, options);
            Formats.AddRange(tableRoot.ParagraphsFormat);
        }

    }




}
