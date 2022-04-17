using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbot.Types
{
    public static class Translations
    {
        private static List<Translation> _translations;
        private static bool changed;

        public static List<Translation> GetTranslations()
        {
            return _translations;
        }

        public static async Task AddTranslation(string name)
        {
            Translation translation = new Translation();
            translation.name = name;
            translation.filename = name + ".json";
            _translations.Add(translation);

            changed = true;
            await WriteToJson();
        }

        public static async Task InitTranslations()
        {
            JsonHandler jsonHandler = new JsonHandler();
            _translations = await jsonHandler.GetCurrTranslations();
            if (_translations == null)
            {
                _translations = new List<Translation>();
            }
        }

        public static async Task WriteToJson()
        {
            JsonHandler jsonHandler = new JsonHandler();
            if (changed)
            {
                await jsonHandler.WriteTranslations(_translations);
                changed = false;
                Console.WriteLine("Wrote Translations");
            }
        }
    }
}
