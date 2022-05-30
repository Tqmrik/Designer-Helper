using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAddIns
{
    //is there other ways to do so?
    class MultilanguageDictionary
    {
        public Dictionary<string, string> dictionaryOrigin = new Dictionary<string, string>();
        public Dictionary<string, string> dictionaryBendPart = new Dictionary<string, string>();
        public Dictionary<string, List<string>> dictionaryPlanesName = new Dictionary<string, List<string>>();

        public MultilanguageDictionary()
        {
            dictionaryOrigin.Add("en-US", "Origin");
            dictionaryOrigin.Add("ru-RU", "Начало");

            dictionaryBendPart.Add("en-US", "Folded Model");
            dictionaryBendPart.Add("ru-RU", "Модель после гибки");

            dictionaryPlanesName.Add("en-US", new List<string> { "YZ Plane", "XZ Plane", "XY Plane" });
            dictionaryPlanesName.Add("ru-RU", new List<string> { "Плоскость YZ", "Плоскость XZ", "Плоскость XY" });
        }
        
    }
}
