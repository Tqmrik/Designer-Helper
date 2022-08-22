using Inventor;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DevAddIns
{
    class STEP_Translator : Translator_Object
    {
        string filePath;
        readonly string extension = "stp";

        public STEP_Translator() : base()
        {
            oTranslator = (TranslatorAddIn)_inventorApplication.ApplicationAddIns.ItemById["{90AF7F40-0C01-11D5-8E83-0010B541CD80}"];
        }

        public STEP_Translator(Dictionary<string, string> oOptionsDictionary, string filePath) : base(oOptionsDictionary, filePath)
        {
            oTranslator = (TranslatorAddIn)_inventorApplication.ApplicationAddIns.ItemById["{90AF7F40-0C01-11D5-8E83-0010B541CD80}"];
            this.filePath = filePath;
        }

        public void CreateSTEP(Document doc)
        {
            //Wrap in the try/catch???
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism;

            if (oTranslator.Equals(null))
            {
                MessageBox.Show("Couldn't connect to the STEP translator");
                return;
            }

            if (doc.IsDrawingDocument())
            {
                if(packAssembly)
                {
                    foreach (Document oFD in doc.AllReferencedDocuments)
                    {
                        
                        StepCreate(oFD);
                    }
                    return;
                }
                else
                {
                    foreach (Document oFD in doc.ReferencedDocuments)
                    {
                        StepCreate(oFD);
                    }
                }
            }
            else if (doc.IsAssemblyDocument() || doc.IsWeldmentDocument())
            {
                if (((AssemblyDocument)doc).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                {

                }
                else
                {
                    StepCreate(doc);
                }

                if(packAssembly)
                {
                    StepCreate(doc);
                    foreach (Document oFD in doc.AllReferencedDocuments)
                    { 
                        StepCreate(oFD);
                    }
                    return;
                }

                if (includeParts)
                {
                    foreach (Document oFD in doc.ReferencedDocuments)
                    {//Check for every referenced document in the drawing and create step file of each
                        if (oFD.IsAssemblyDocument())
                        {
                            if (((AssemblyDocument)oFD).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        else if (oFD.IsPartDocument())
                        {
                            if (((PartDocument)oFD).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        StepCreate(oFD);
                    }
                }
            }
            else if (doc.IsPartDocument() || doc.IsSheetMetalDocument())
            {
                if (((PartDocument)doc).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure)
                {
                    return;
                }
                StepCreate(doc);               
            }

            oTranslator = null;
            oContext = null;
            oDataMedium = null;
            oDataMedium = null;
        }

        private void FilePathHelper(Document doc) //Add a revision letter to the output file name
        {
            if (!String.IsNullOrEmpty(doc.FullDocumentName))
            {
                //Add revision letter to the file name
                filePath = RevisionHelper.addRevisionLetter(doc, PathConverter.clearExtension(doc), extension);
            }
            else
            {
                //Try to save to the desktop
                filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
                int iterator = 1;
                while (System.IO.File.Exists(filePath + $".{extension}"))
                {
                    filePath = filePath.Remove(filePath.Length - 1) + $"{iterator}";
                    iterator++;
                }
                filePath += $".{extension}";
            }
        }
        private void OptionsSetter(Document doc)
        {
            oOptions.Value["ApplicationProtocolType"] = 3;
            oOptions.Value["Author"] = doc.PropertySets[3][24].Value;
            //oOptions.Value("Authorization") = ""
            oOptions.Value["Description"] = doc.PropertySets[3][14].Value;
            oOptions.Value["Organization"] = doc.PropertySets[2][3].Value;
        }
        private void StepCreate(Document doc)
        {
            if(!(doc is null))
            {
                //Document referencedDocumentObject = InventorApplication.Documents.ItemByName[doc.FullFileName];

                 if (oTranslator.HasSaveCopyAsOptions[doc, oContext, oOptions])
                 {

                    if(String.IsNullOrEmpty(filePath))
                    {
                        FilePathHelper(doc);
                        if (oTranslator.HasSaveCopyAsOptions[doc, oContext, oOptions])
                        {
                            OptionsSetter(doc);
                        }
                        //TODO: Somehow transfer oOptions setter into constructor 
                    }


                    oDataMedium.FileName = filePath;

                    try
                    {
                        oTranslator.SaveCopyAs(doc, oContext, oOptions, oDataMedium);
                        if(doc != _inventorApplication.ActiveDocument)
                        {
                            doc.Close();
                        }
                         //Do i even need this????
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");
                    }
                 }
            }
            else { return; }
        }
    }
}
