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

            if (doc.isDrawingDocument())
            {
                if(packAssembly)
                {
                    foreach (Document oFD in doc.AllReferencedDocuments)
                    {
                        TestMeth(oFD);
                    }
                    return;
                }
                else
                {
                    foreach (Document oFD in doc.ReferencedDocuments)
                    {
                        TestMeth(oFD);
                    }
                }
               
            }
            else if ((doc.isAssemblyDocument() || doc.isWeldmentDocument()))
            {
                if (((AssemblyDocument)doc).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                {

                }
                else
                {
                    filePathHelper(doc);
                    TestMeth(doc);
                }

                if(packAssembly)
                {
                    TestMeth(doc);
                    foreach (Document oFD in doc.AllReferencedDocuments)
                    { 
                        TestMeth(oFD);
                    }
                    return;
                }

                if (includeParts)
                {
                    foreach (Document oFD in doc.ReferencedDocuments)
                    {//Check for every referenced document in the drawing and create step file of each
                        if (oFD.isAssemblyDocument())
                        {
                            if (((AssemblyDocument)oFD).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        else if (oFD.isPartDocument())
                        {
                            if (((PartDocument)oFD).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        TestMeth(oFD);
                    }
                }
            }
            else if (doc.isPartDocument() || doc.isSheetMetalDocument())
            {
                if (((PartDocument)doc).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure)
                {
                    return;
                }
                TestMeth(doc);               
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
        private void TestMeth(Document doc)
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
