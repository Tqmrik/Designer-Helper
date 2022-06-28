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
                System.IO.Path.GetFullPath(documentObject.FullDocumentName);

                string outputVal = System.IO.Path.GetDirectoryName(documentObject.FullDocumentName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(documentObject.DisplayName) + ".idw";

                return outputVal;
            }
            catch(ArgumentException e)
            { 
                return null;
            }


        }
    }
}
