using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;


namespace DevAddIns
{
    class PathConverter
    {
        public static string guessDrawingPath(Document documentObject)
        {
            try
            {
                //Regex??
                //return System.IO.Path.GetDirectoryName(documentObject.FullDocumentName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(documentObject.DisplayName) + ".idw";
                return System.IO.Path.GetFileNameWithoutExtension(documentObject.FullFileName) + ".idw";
            }
            catch(ArgumentException e)
            { 
                return null;
            }
        }

        public static string clearExtension(Document documentObject)
        {
            try
            {
                return System.IO.Path.GetDirectoryName(documentObject.FullDocumentName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(documentObject.DisplayName);
            }
            catch (ArgumentException e)
            {
                return null;
            }
        }
    }
}
