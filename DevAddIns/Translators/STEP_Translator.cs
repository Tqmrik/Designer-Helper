using Inventor;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DevAddIns
{
    class STEP_Translator : Translator_Object
    {
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

        public void CreateSTEP(Document document)
        {
            //Wrap in the try/catch???
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism;

            if (oTranslator.Equals(null))
            {
                MessageBox.Show("Couldn't connect to the STEP translator");
                return;
            }

            if (document.IsDrawingDocument())
            {
                if(packAssembly)
                {
                    foreach (Document referencedDocumentInDrawing in document.AllReferencedDocuments)
                    {
                        
                        StepCreate(referencedDocumentInDrawing);
                    }
                    return;
                }
                else
                {
                    foreach (Document referencedDocumentInDrawing in document.ReferencedDocuments)
                    {
                        StepCreate(referencedDocumentInDrawing);
                    }
                }
            }
            else if (document.IsAssemblyDocument() || document.IsWeldmentDocument())
            {
                if (((AssemblyDocument)document).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                {

                }
                else
                {
                    StepCreate(document);
                }

                if(packAssembly)
                {
                    StepCreate(document);
                    foreach (Document referencedDocumentInDrawing in document.AllReferencedDocuments)
                    { 
                        StepCreate(referencedDocumentInDrawing);
                    }
                    return;
                }

                if (includeParts)
                {
                    foreach (Document referencedDocumentInDocument in document.ReferencedDocuments)
                    {//Check for every referenced document in the drawing and create step file of each
                        if (referencedDocumentInDocument.IsAssemblyDocument())
                        {
                            if (((AssemblyDocument)referencedDocumentInDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        else if (referencedDocumentInDocument.IsPartDocument())
                        {
                            if (((PartDocument)referencedDocumentInDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        StepCreate(referencedDocumentInDocument);
                    }
                }
            }
            else if (document.IsPartDocument() || document.IsSheetMetalDocument())
            {
                if (((PartDocument)document).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure)
                {
                    return;
                }
                StepCreate(document);               
            }

            oTranslator = null;
            oContext = null;
            oDataMedium = null;
            oDataMedium = null;
        }

        private void OptionsSetter(Document document)
        {
            oOptions.Value["ApplicationProtocolType"] = 3;
            oOptions.Value["Author"] = document.PropertySets[3][24].Value;
            //oOptions.Value("Authorization") = ""
            oOptions.Value["Description"] = document.PropertySets[3][14].Value;
            oOptions.Value["Organization"] = document.PropertySets[2][3].Value;
        }
        private void StepCreate(Document document)
        {
            if(!(document is null))
            {
                //Document referencedDocumentObject = InventorApplication.Documents.ItemByName[doc.FullFileName];

                 if (oTranslator.HasSaveCopyAsOptions[document, oContext, oOptions])
                 {

                    if(String.IsNullOrEmpty(filePath))
                    {
                        FilePathHelper(document, extension);
                        if (oTranslator.HasSaveCopyAsOptions[document, oContext, oOptions])
                        {
                            OptionsSetter(document);
                        }
                    }


                    oDataMedium.FileName = filePath;

                    try
                    {
                        oTranslator.SaveCopyAs(document, oContext, oOptions, oDataMedium);
                        if(document != _inventorApplication.ActiveDocument)
                        {
                            document.Close();
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
