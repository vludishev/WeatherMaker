using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLocalizeExtension.Engine;

namespace WeatherMaker
{
    public class LocalizedLogic
    {
        private LocalizedLogic()
        {
            
        }

        public static LocalizedLogic Instance { get; } = new LocalizedLogic();

        public void SetCulture(string cultureCode)
        {
            var newCulture = new CultureInfo(cultureCode);
            LocalizeDictionary.Instance.Culture = newCulture;
        }

        public string this[string key]
        {
            get
            {
                var result = LocalizeDictionary.Instance.GetLocalizedObject("WeatherMaker", "Localization", key, LocalizeDictionary.Instance.Culture);
                return result as string;
            }
        }
    }
}
