using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using System.Windows.Forms;

namespace DevAddIns
{
    class RevisionHelper
    {
        public static string addRevisionLetter(Document documentObject, string path, string extension)
        {
            //Basically document object defines the revision letter
            //path defines the path that the revision will be attached to, path needs to be without extension!!!
            //extension defines the final attachment to the file path
            try
            {
                if(!String.IsNullOrEmpty(path))
                {
                    string revisionLetter = documentObject.PropertySets[1][7].Value.ToString();

                    if (!String.IsNullOrEmpty(revisionLetter))
                    {
                        path += $"_{revisionLetter}.{extension}";
                    }
                    else
                    {
                        path += $".{extension}";
                    }
                }
                return path;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");
                return string.Empty;
            }

            
        }
    }
}
