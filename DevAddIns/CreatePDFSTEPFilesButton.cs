using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevAddIns
{
    internal class CreatePDFSTEPFilesButton : Button
    {
        #region "Constructors"
        //Use constructors of the base class
        public CreatePDFSTEPFilesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public CreatePDFSTEPFilesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            //Get path
            //Get name
            //Create pdf file with the name in the current path
            //Get the revision

            //Browse the possible options

            //Get document that the drawing refernces
            //Create step file of such document
            Document activeDocument = InventorApplication.ActiveDocument;

            var refDocs = activeDocument.ReferencedDocuments;
            var refFiles = activeDocument.ReferencedFiles;
            var refDocsDesct = activeDocument.ReferencedDocumentDescriptors;
            var refFileDesct = activeDocument.ReferencedFileDescriptors;
            var referencingDocs = activeDocument.ReferencingDocuments;

            if (!(activeDocument.SubType == "{BBF9FDF1-52DC-11D0-8C04-0800090BE8EC}")) return;

            TranslatorAddIn pdfTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}"];
            string filePath = "";
            string revision = activeDocument.PropertySets[1][7].Value.ToString();

            if (activeDocument.FullDocumentName != null)
            {
                filePath = activeDocument.FullDocumentName.Replace(".idw", "");
                if (!String.IsNullOrEmpty(revision))
                {
                    filePath += $"_{revision}.pdf";
                }
                else
                {
                    filePath += ".pdf";
                }
            }
            else
            {
                //Add file check and then increment
                filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                int iterator = 1;
                while (System.IO.File.Exists(filePath + ".pdf"))
                {
                    filePath = filePath.Remove(filePath.Length - 1) + iterator.ToString();
                    iterator++;
                }
                filePath += ".pdf";
            }


            TranslationContext oContext = InventorApplication.TransientObjects.CreateTranslationContext();
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism; //Just specifies the type of the operation

            NameValueMap oOptions = InventorApplication.TransientObjects.CreateNameValueMap();//??

            DataMedium oDataMedium = InventorApplication.TransientObjects.CreateDataMedium();//???

            if (pdfTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions] == true)
            {
                oOptions.Value["All_Color_AS_Black"] = 0;
                //What are the other options?
            }

            oDataMedium.FileName = filePath;

            try
            {
                //Will adding the transaction alter the operation????
                pdfTranslator.SaveCopyAs(activeDocument, oContext, oOptions, oDataMedium);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");

            }
            #endregion
        }
    }
}

