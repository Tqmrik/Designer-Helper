using Inventor;
using System;
using System.Windows.Forms;

namespace DevAddIns
{
    class PDFTranslator : Translators
    {
        string filePath;
        const string extension = "pdf";

        public PDFTranslator() : base()
        {
            oTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}"];
        }
        public void createPDF(Document document)
        {
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism;

            if (oTranslator.Equals(null))
            { 
                MessageBox.Show("Couldn't connect to the PDF translator");
                return;
            }

            if (document.isDrawingDocument())
            {
                filePathHelper(document);

                if (oTranslator.HasSaveCopyAsOptions[document, oContext, oOptions])
                {
                    oOptionsSetter();
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
            else if (document.isAssemblyDocument() || document.isWeldmentDocument())
            {
               
                //Make pdf for the assembly drawing
                if (document != null)
                {
                    if (((AssemblyDocument)document).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                    {

                    }
                    else
                    {
                        pdfCreate(document);
                    }

                    if (packAssembly)
                    {
                        pdfCreate(document);
                        foreach (Document dasd in document.AllReferencedDocuments)
                        {
                            pdfCreate(dasd);
                        }
                        return;
                    }
                }
                //Make pdfs for the parts drawings
                if (includeParts)
                {
                    foreach (Document referencedDocument in document.ReferencedDocuments)
                    {
                        if (referencedDocument.isAssemblyDocument())
                        {
                            if (((AssemblyDocument)referencedDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        else if (referencedDocument.isPartDocument())
                        {
                            if (((PartDocument)referencedDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }

                        pdfCreate(referencedDocument);
                    }
                }
            }
            else if (document.isPartDocument() || document.isSheetMetalDocument())
            {
                if (((PartDocument)activeDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                {

                }
                else
                {
                    pdfCreate(document);
                }
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
        private void oOptionsSetter()
        {
            oOptions.Value["All_Color_AS_Black"] = 0;
            oOptions.Value["Remove_Line_Weights"] = 0;

            //oOptions.Value["Password"] = 0;

            //oOptions.Value["Custom_Begin_Sheet"] = 1;
            //oOptions.Value["Custom_End_Sheet"] = 1;

            //oOptions.Value["Vector_Resolution"] = 400;
        }
        private void pdfCreate(Document document)
        {
            //Takes not a drawing file -> tries to find a drawing for it in the active project -> creates pdf of it with revision from drawing
            //Not document dependent(can take part as well as assembly documents)
            Document drawingDocumentObject = null;
            string referencedDocumentDrawingPath = null;
            string partDirectory = System.IO.Path.GetDirectoryName(document.FullDocumentName);
            string partDrawingPath = PathConverter.guessDrawingPath(document);

            if (!String.IsNullOrEmpty(partDrawingPath)) //If there is invalid path
            {
                referencedDocumentDrawingPath = InventorApplication.DesignProjectManager.ResolveFile(partDirectory, partDrawingPath);
                //Resolve file also searches all the subdirectories that it could find
            }
            else return;

            if (!String.IsNullOrEmpty(referencedDocumentDrawingPath))
            {
                //If drawing is placed in the folder, save it to the folder as well
                drawingDocumentObject = InventorApplication.Documents.Open(referencedDocumentDrawingPath, OpenVisible: false);
                filePathHelper(drawingDocumentObject);

                if (oTranslator.HasSaveCopyAsOptions[drawingDocumentObject, oContext, oOptions])
                {
                    oOptionsSetter();
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
