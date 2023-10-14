using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MeNoisySoundboard.App.Base
{
    public class JsonContextHandler
    {
        public static T? Load<T>(string fileName)
        {
            // TODO : move app name to config
            string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigurationManager.AppSettings["AppName"]);
            string filePath = Path.Combine(appFolder, $"{fileName}.json");

            if (!File.Exists(filePath)) return default(T);

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonString);
        }

        public static void Save(object context, string fileName)
        {
            string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigurationManager.AppSettings["AppName"]);

            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);

            string filePath = Path.Combine(appFolder, $"{fileName}.json");
            string jsonString = JsonSerializer.Serialize(context);
            File.WriteAllText(filePath, jsonString);
        }
    }
}
