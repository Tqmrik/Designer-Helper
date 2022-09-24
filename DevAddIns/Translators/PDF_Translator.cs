using Inventor;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DevAddIns
{
    class PDF_Translator : Translator_Object
    {
        string filePath;
        readonly string extension = "pdf";


        public PDF_Translator() : base()
        {
            oTranslator = (TranslatorAddIn)_inventorApplication.ApplicationAddIns.ItemById["{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}"];
            oOptions.Value["All_Color_AS_Black"] = 0;
            oOptions.Value["Remove_Line_Weights"] = 0;
        }

        public PDF_Translator(Dictionary<string, string> oOptionsDictionary, string filePath) : base(oOptionsDictionary, filePath)
        {
            oTranslator = (TranslatorAddIn)_inventorApplication.ApplicationAddIns.ItemById["{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}"];
            this.filePath = filePath;
        }

        public void CreatePDF(Document document)
        {
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism;

            if (oTranslator.Equals(null))
            { 
                MessageBox.Show("Couldn't connect to the PDF translator");
                return;
            }

            if (document.IsDrawingDocument())
            {
                if (String.IsNullOrEmpty(this.filePath))
                {
                    filePathHelper(document);
                    if (oTranslator.HasSaveCopyAsOptions[document, oContext, oOptions])
                    {
                        OptionsSetter();
                    }
                }

                oDataMedium.FileName = filePath;

                try
                {
                    //TODO: Check if document is opened if so: 1)Try to close(kinda intrusive); 2)don't perform an export and display message
                    oTranslator.SaveCopyAs(document, oContext, oOptions, oDataMedium);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");

                }
            }
            else if (document.IsAssemblyDocument() || document.IsWeldmentDocument())
            {
                //Make pdf for the assembly drawing
                if (document != null)
                {
                    if (((AssemblyDocument)document).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                    {

                    }
                    else
                    {
                        PDF_Create(document);
                    }

                    if (packAssembly)
                    {
                        PDF_Create(document);
                        foreach (Document allReferencedDocuments in document.AllReferencedDocuments)
                        {
                            PDF_Create(allReferencedDocuments);
                        }
                        return;
                    }
                }
                //Make pdfs for the parts drawings
                if (includeParts)
                {
                    foreach (Document referencedDocument in document.ReferencedDocuments)
                    {
                        if (referencedDocument.IsAssemblyDocument())
                        {
                            if (((AssemblyDocument)referencedDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        else if (referencedDocument.IsPartDocument())
                        {
                            if (((PartDocument)referencedDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }

                        PDF_Create(referencedDocument);
                    }
                }
            }
            else if (document.IsPartDocument() || document.IsSheetMetalDocument())
            {
                if (((PartDocument)activeDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                {

                }
                else
                {
                    PDF_Create(document);
                }
            }

            oTranslator = null;
            oContext = null;
            oDataMedium = null;
            oDataMedium = null;
        }

        private void filePathHelper(Document document) //Add a revision letter to the output file name
        { 
            if (!String.IsNullOrEmpty(document.FullDocumentName))
            {
                //Add revision letter to the file name
                filePath = RevisionHelper.addRevisionLetter(document, PathConverter.clearExtension(document), extension);
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
        private void OptionsSetter()
        {
            oOptions.Value["All_Color_AS_Black"] = 0;
            oOptions.Value["Remove_Line_Weights"] = 0;

            //oOptions.Value["Password"] = 0;

            //oOptions.Value["Custom_Begin_Sheet"] = 1;
            //oOptions.Value["Custom_End_Sheet"] = 1;

            //oOptions.Value["Vector_Resolution"] = 400;
        }
        private void PDF_Create(Document document)
        {
            string partDirectory = System.IO.Path.GetDirectoryName(document.FullDocumentName);
            string partDrawingPath = PathConverter.guessDrawingPath(document);

            string referencedDocumentDrawingPath;
            if (!string.IsNullOrEmpty(partDrawingPath)) //If there is invalid path
            {
                referencedDocumentDrawingPath = _inventorApplication.DesignProjectManager.ResolveFile(partDirectory, partDrawingPath);
                //Resolve file also searches all the subdirectories that it could find
            }
            else
            {
                return;
            }

            if (!string.IsNullOrEmpty(referencedDocumentDrawingPath))
            {
                //Takes not a drawing file -> tries to find a drawing for it in the active project -> creates pdf of it with revision from drawing
                //Not document dependent(can take part as well as assembly documents)
                //If drawing is placed in the folder, save it to the folder as well
                Document drawingDocumentObject = _inventorApplication.Documents.Open(referencedDocumentDrawingPath, OpenVisible: false);

                if (string.IsNullOrEmpty(this.filePath))
                {
                    filePathHelper(drawingDocumentObject);
                    if (oTranslator.HasSaveCopyAsOptions[drawingDocumentObject, oContext, oOptions])
                    {
                        OptionsSetter();
                    }
                }
                
                oDataMedium.FileName = filePath;

                try
                {
                    oTranslator.SaveCopyAs(drawingDocumentObject, oContext, oOptions, oDataMedium);
                    drawingDocumentObject.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");
                }
            }
            else
            {
                return;
            }
        }


    }
}
