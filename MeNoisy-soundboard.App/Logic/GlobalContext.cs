using MeNoisySoundboard.App.Logic.Params;
using MeNoisySoundboard.App.Logic.Sounds.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MeNoisySoundboard.App.Logic
{
    public class GlobalContext
    {
        public SoundsContext SoundsContext { get; set; } = new SoundsContext();
        public ParamsContext ParamsContext { get; set; } = new ParamsContext();

        public static GlobalContext Load()
        {
            // TODO : move app name to config
            string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigurationManager.AppSettings["AppName"]);
            string filePath = Path.Combine(appFolder, "data.json");
            
            if (!File.Exists(filePath)) return new GlobalContext();

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<GlobalContext>(jsonString);
        }

        public void Save()
        {
            string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigurationManager.AppSettings["AppName"]);

            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);

            string filePath = Path.Combine(appFolder, "data.json");
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(filePath, jsonString);
        }
    }
}
