using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;


namespace DevAddIns
{
    
    class TranslatorList
    {
        public static Inventor.Application InventorApplication;
        public void DisplasyTranslatorOutput()
        {
            StreamWriter sw = new StreamWriter(@"C:/Users/User/Desktop/AddInsList.txt");
            var invdoc = InventorApplication.ApplicationAddIns;
            int index = 0;
            foreach(ApplicationAddIn addin in invdoc)
            {
                //Add extension to the application add in
                sw.WriteLine($"Index: {index}");
                sw.WriteLine($"Class Id string: {addin.ClassIdString}");
                sw.WriteLine($"Client Id: {addin.ClientId}");
                sw.WriteLine($"Display name: {addin.DisplayName}");
                sw.WriteLine();
                index++;
            }
        }
    }
}
