using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace DevAddIns
{
    class TranslatorOptions
    {
        public static Inventor.Application InventorApplication;
        public void DisplayTranslatorOptions()
        {
            StreamWriter sw = new StreamWriter($@"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)}/FileDir/TranslatorOptions.txt");
            
            int indexPath = 1;

            if(System.IO.File.Exists($@"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)}/FileDir/TranslatorOptions.txt"))
            {
                while (System.IO.File.Exists($@"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)}/FileDir/TranslatorOptions{indexPath}.txt"))
                {//Since we are going to work with different active documents it's preferable to check if the file already exists
                    sw.Dispose();
                    sw = new StreamWriter($@"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)}/FileDir/TranslatorOptions{indexPath}.txt");
                    indexPath++;
                }
            }
            var invdoc = InventorApplication.ApplicationAddIns;
            int index = 0;
            sw.WriteLine(DateTime.Now);
            sw.WriteLine();
            foreach (ApplicationAddIn addin in invdoc)
            {
                if (addin is TranslatorAddIn)
                {
                    TranslatorAddIn translator = (TranslatorAddIn)addin;
                    TranslationContext oContext = InventorApplication.TransientObjects.CreateTranslationContext();
                    oContext.Type = IOMechanismEnum.kUnspecifiedIOMechanism;
                    NameValueMap oOptions = InventorApplication.TransientObjects.CreateNameValueMap();
                    DataMedium oDataMedium = InventorApplication.TransientObjects.CreateDataMedium();
                    try
                    {
                        if (translator.HasSaveCopyAsOptions[InventorApplication.ActiveDocument, oContext, oOptions] && translator.SupportsSaveCopyAsFrom.Contains(System.IO.Path.GetExtension(InventorApplication.ActiveDocument.FullFileName)))
                        {
                            sw.WriteLine($"Index: {index}");
                            sw.WriteLine(translator.ToStringExt());
                            sw.WriteLine("----Options----");
                            translator.ShowSaveCopyAsOptions(InventorApplication.ActiveDocument, oContext, oOptions);
                            for (int i = 1; i < oOptions.Count; i++)
                            {
                                sw.WriteLine($"{oOptions.Name[i]} : {oOptions.Value[oOptions.Name[i]]}");
                            }
                            
                        }
                    }
                    catch(Exception ex)
                    {
                        //Unsafe code, be careful
                        //Some translators don't have 'SupportsSaveCopyAsFrom property so migth skip a bit 
                        goto cont;
                    }
                }
                else
                {

                }
            cont:
                sw.WriteLine();
                index++;
            }
            sw.Dispose();
        }
    }
}
