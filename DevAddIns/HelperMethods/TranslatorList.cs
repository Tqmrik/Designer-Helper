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
            StreamWriter sw = new StreamWriter($@"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)}/FileDir/AddInsList.txt");
            var invdoc = InventorApplication.ApplicationAddIns;
            int index = 0;
            sw.WriteLine(DateTime.Now);
            sw.WriteLine();
            foreach (ApplicationAddIn addin in invdoc)
            {
                if(addin is TranslatorAddIn)
                {
                    TranslatorAddIn transAddIn = (TranslatorAddIn)addin;
                    sw.WriteLine($"Index: {index}");
                    sw.WriteLine(transAddIn.ToStringExt());
                }
                else
                {
                    sw.WriteLine($"Index: {index}");
                    sw.WriteLine($"Class Id string: {addin.ClassIdString}");
                    sw.WriteLine($"Client Id: {addin.ClientId}");
                    sw.WriteLine($"Display name: {addin.DisplayName}");
                }
                //Add extension to the application add in

                sw.WriteLine();
                index++;
            }
            sw.Dispose();
        }
    }
}
