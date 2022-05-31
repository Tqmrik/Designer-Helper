using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DevAddIns
{
    class DrawingPropertiesSet
    {
        //Make as a property????
        public Dictionary<string,string> propNamesDictionary = new Dictionary<string,string>();
        public DrawingPropertiesSet()
        {
            propNamesDictionary.Add("DATE1","Date");
            propNamesDictionary.Add("DATE2", "Date");
            propNamesDictionary.Add("DATE3", "Date");
            propNamesDictionary.Add("DocNumber", "Text");
            propNamesDictionary.Add("HARDNESS", "Text");
            propNamesDictionary.Add("MADE1", "Text");
            propNamesDictionary.Add("MADE2", "Text");
            propNamesDictionary.Add("MADE3", "Text");
            propNamesDictionary.Add("NC1", "Text");
            propNamesDictionary.Add("NC2", "Text");
            propNamesDictionary.Add("NC3", "Text");
            propNamesDictionary.Add("NE1", "Text");
            propNamesDictionary.Add("NE2", "Text");
            propNamesDictionary.Add("NE3", "Text");
            propNamesDictionary.Add("REV1", "Text");
            propNamesDictionary.Add("REV2", "Text");
            propNamesDictionary.Add("REV3", "Text");
            propNamesDictionary.Add("REVIEWED1", "Text");
            propNamesDictionary.Add("REVIEWED2", "Text");
            propNamesDictionary.Add("REVIEWED3", "Text");
            propNamesDictionary.Add("SURFACE", "Text");
            propNamesDictionary.Add("VDS_Category", "Text");
        }
    }
}
