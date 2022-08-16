using Inventor;
using System;
using System.Windows.Forms;

namespace DevAddIns
{
    class STEPTranslator : Translators
    {
        string filePath = "";
        string extension = "stp";

        public STEPTranslator() : base()
        {
            oTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{90AF7F40-0C01-11D5-8E83-0010B541CD80}"];
        }

        public void createSTEP(Document doc)
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
                        
                        stepCreate(oFD);
                    }
                    return;
                }
                else
                {
                    foreach (Document oFD in doc.ReferencedDocuments)
                    {
                        stepCreate(oFD);
                    }
                }
               
            }
            else if ((doc.IsAssemblyDocument() || doc.IsWeldmentDocument()))
            {
                if (((AssemblyDocument)doc).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                {

                }
                else
                {
                    filePathHelper(doc);
                    stepCreate(doc);
                }

                if(packAssembly)
                {
                    stepCreate(doc);
                    foreach (Document oFD in doc.AllReferencedDocuments)
                    { 
                        stepCreate(oFD);
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
                        stepCreate(oFD);
                    }
                }
            }
            else if (doc.IsPartDocument() || doc.IsSheetMetalDocument())
            {
                if (((PartDocument)doc).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure)
                {
                    return;
                }
                stepCreate(doc);               
            }

            oTranslator = null;
            oContext = null;
            oDataMedium = null;
            oDataMedium = null;
        }

        private void filePathHelper(Document doc) //Add a revision letter to the output file name
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
        private void oOptionsSetter(Document doc)
        {
            oOptions.Value["ApplicationProtocolType"] = 3;
            oOptions.Value["Author"] = doc.PropertySets[3][24].Value;
            //oOptions.Value("Authorization") = ""
            oOptions.Value["Description"] = doc.PropertySets[3][14].Value;
            oOptions.Value["Organization"] = doc.PropertySets[2][3].Value;
        }
        private void stepCreate(Document doc)
        {
            if(!(doc is null))
            {
                //Document referencedDocumentObject = InventorApplication.Documents.ItemByName[doc.FullFileName];

                 if (oTranslator.HasSaveCopyAsOptions[doc, oContext, oOptions])
                 {

                    filePathHelper(doc);
                    oOptionsSetter(doc);
                    oDataMedium.FileName = filePath;

                    try
                    {
                        oTranslator.SaveCopyAs(doc, oContext, oOptions, oDataMedium);
                        if(doc != InventorApplication.ActiveDocument)
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
