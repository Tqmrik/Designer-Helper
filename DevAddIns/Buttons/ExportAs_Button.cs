using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevAddIns
{
    internal class ExportAs_Button : Button_Object
    {
        #region "Constructors"
        //Use constructors of the base class
        public ExportAs_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public ExportAs_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion

        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            /*
             * A File object represents a storage in the file system (i.e. a file on disk). A Document object represents an instance of a model or drawing in memory. A Document can only have a single associated File object. However, since it is possible to have multiple instances (or level of detail representations) of a file in memory that are persisted in the same storage on the file system, multiple Document objects may be associated with the same File object.

            File and Document References
            A FileDescriptor object describes the reference from a File to another File. A DocumentDescriptor describes the reference from a Document to another Document. A descriptor contains all the information needed to find the referenced file/document as well as the state of the reference (healthy, unresolved, replaced, etc.). The File and FileDescriptor objects represent the consolidated view of all of the representations of a Document. The figure below shows the relationships between the FileDescriptor, File, DocumentDescriptor and Document objects.

             */

            //Get document that the drawing refernces
            //Create step file of such document

            //TODO: Wrap in the try's; check to see if it's possible to create step without opening the actual document; play with the algorith to see other errors in documents with null references and so on

            if (InventorApplication.ActiveDocument == null) return;
            try
            {
                ExportAsForm exportForm = new ExportAsForm();
                exportForm.ShowDialog();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace);
            }

            //var refDocs = activeDocument.ReferencedDocuments.Type;
            //var refFiles = activeDocument.ReferencedFiles.Type;
            //var refDocsDesct = activeDocument.ReferencedDocumentDescriptors.Type;
            //var refFileDesct = activeDocument.ReferencedFileDescriptors.Type;
            //var referencingDocs = activeDocument.ReferencingDocuments.Type;

            //FileDescriptor fd = (FileDescriptor)activeDocument.ReferencedFileDescriptors; 
            #endregion
        }

       
    }
}

