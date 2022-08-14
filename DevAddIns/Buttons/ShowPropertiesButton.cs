using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace DevAddIns
{
    internal class ShowPropertiesButton : Button
    {
        #region "Constructors"
        //Use constructors of the base class
        public ShowPropertiesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public ShowPropertiesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            Document activeDocument = InventorApplication.ActiveDocument;

            string path = $"{activeDocument.FullFileName.Substring(0, activeDocument.FullFileName.Length - 4)}IProperties.txt";

            if (activeDocument.isDrawingDocument()) path = path.Replace("IProperties", "DrawingIProperties");

            StreamWriter SW = new StreamWriter(path);
            SW.WriteLine($"Path: {path}");
            SW.WriteLine($"File: {activeDocument.DisplayName}");
            SW.WriteLine($"FilePath: {activeDocument.FullDocumentName}");
            SW.WriteLine("");
            foreach (PropertySet propTab in activeDocument.PropertySets)
            {
                SW.WriteLine(StringConverters.ToStringExt(propTab));
                SW.WriteLine("\t==================================");

                foreach (Property proper in propTab)
                {
                    foreach (var line in StringConverters.ToStringExt(proper).Split('\n'))
                    {
                        SW.WriteLine("\t" + line);
                    }

                    SW.WriteLine("\t==================================");
                }
            }
            SW.Close();
            System.Diagnostics.Process.Start(path);
        }
        #endregion
    }
}

