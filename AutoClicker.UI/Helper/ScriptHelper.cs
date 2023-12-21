using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoClicker.Runtime.Core;
using AutoClicker.Runtime.Script;
using Newtonsoft.Json;

namespace AutoClicker.UI.Helper
{
    internal class ScriptHelper
    {
        public static readonly string ScriptBaseFolder = "Script";
        public static void SaveScript(List<MarcoEvent> marcoEvents, string fileName, string subFolderPath = null)
        {
            string filePath = ScriptBaseFolder;
            if (!string.IsNullOrWhiteSpace(subFolderPath)) Path.Combine(filePath, subFolderPath);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(marcoEvents, Formatting.Indented));
        }

        public static List<MarcoEvent> LoadScript(string fileName, string subFolderPath = null)
        {
            string filePath = ScriptBaseFolder;
            if (!string.IsNullOrWhiteSpace(subFolderPath)) Path.Combine(filePath, subFolderPath);
            return JsonConvert.DeserializeObject<List<MarcoEvent>>(File.ReadAllText(filePath));
        }
    }
}
