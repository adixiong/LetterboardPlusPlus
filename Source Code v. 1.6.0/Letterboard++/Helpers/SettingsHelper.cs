using Letterboard__.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letterboard__.Helpers
{
    public class SettingsHelper
    {
        public static void Save(Settings settings)
        {
            string[] lines = new string[] { settings.KeyboardDelayTime.ToString(), settings.Color };
            File.WriteAllLines("settings.txt", lines);
        }

        public static Settings GetSetting()
        {
            string[] lines = File.ReadAllLines("settings.txt");
            Settings settings = new Settings()
            {
                KeyboardDelayTime = Convert.ToInt32(lines[0]),
                Color = lines[1]
            };

            return settings;
        }
    }
}
