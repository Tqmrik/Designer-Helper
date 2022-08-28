using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevAddIns
{
    /// <summary>
    /// Button object for creating copy of the active document
    /// </summary>
    internal class SaveFileCopy_Button : Button_Object
    {
        #region "Constructors"
        //Use constructors of the base class
        public SaveFileCopy_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public SaveFileCopy_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        /// <summary>
        /// Handler for button click of SaveFileCopy_Button
        /// </summary>
        /// <param name="context">Input object that can be used to determine the context of why the event fired</param>
        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            Document editDocument = InventorApplication.ActiveDocument;
            string fileName = System.IO.Path.GetDirectoryName(editDocument.FullFileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(editDocument.FullDocumentName);
            string extension = System.IO.Path.GetExtension(editDocument.FullFileName);
            for (int i = 1; ; i++)
            {
                string copyName = $"{fileName}_{i}{extension}";
                if (!System.IO.File.Exists(copyName))
                {
                    editDocument.SaveAs(copyName, true);
                    //Opens a directory in a explorer and highlights the file
                    System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{copyName}\"");
                    return;
                }
            }
            
        }
        #endregion
    }
}

