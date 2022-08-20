using System.Text.Json;
using UbStandardObjects.Objects;

namespace UbStandardObjects
{
	public delegate void ShowMessage(string message, bool isError = false, bool isFatal = false);

	public delegate void ShowStatusMessage(string message);

	public delegate void ShowPaperNumber(short paperNo);

	public static class StaticObjects
	{
		/// <summary>
		/// Control file name for different translations versions
		/// </summary>
		public const string ControlFileName = "UbControlFile.json";

		/// <summary>
		/// This is the object to store log
		/// </summary>
		public static Log Logger { get; set; }

		public static Parameters Parameters { get; set; }

		public static Book Book { get; set; } = null;

        /// <summary>
        /// Serialize an object to string using json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                WriteIndented = true,
            };
            return JsonSerializer.Serialize<T>(obj, options);
        }

        /// <summary>
        /// Deserialize an object from a json string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }


    }
}
