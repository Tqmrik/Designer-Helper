using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace DevAddIns
{
    class ProjectSketchAxis : Button
    {
        #region "Constructors"
        public ProjectSketchAxis(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public ProjectSketchAxis(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion

        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            if (!(InventorApplication.ActiveEditObject is PlanarSketch)) return;

            try
            {
                Document activeDocument = InventorApplication.ActiveDocument;
                MultilanguageDictionary MLDict = new MultilanguageDictionary();
                string languageCode = InventorApplication.LanguageCode;
                PlanarSketch oSketch = (PlanarSketch)InventorApplication.ActiveEditObject;
                BrowserNode pathToOrigin = null;
                object oTopNode;
                SketchEntity oGeomLine;

                Transaction oTransaction = InventorApplication.TransactionManager.StartTransaction(InventorApplication.ActiveDocument, "Project sketch axis");

                //Is there other way
                //Guard clauses for different types of documents
                if (activeDocument.SubType == "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}")
                {
                    pathToOrigin = activeDocument.BrowserPanes["PmDefault"].TopNode.BrowserNodes[MLDict.dictionaryBendPart[languageCode]].BrowserNodes[MLDict.dictionaryBendPart[languageCode]]; //Sheet metal
                }
                else if (activeDocument.SubType == "{E60F81E1-49B3-11D0-93C3-7E0706000000}")
                {
                    pathToOrigin = activeDocument.BrowserPanes["PmDefault"].TopNode.BrowserNodes[MLDict.dictionaryOrigin[languageCode]]; //Assembly
                }
                else if (activeDocument.SubType == "{4D29B490-49B2-11D0-93C3-7E0706000000}")
                {
                    pathToOrigin = activeDocument.BrowserPanes["PmDefault"].TopNode.BrowserNodes[MLDict.dictionaryOrigin[languageCode]]; //Part
                }


                //Logic, I know that goto is not safe code but cannot handle an error that occurs for now
                //X Axis
                try
                {
                    oTopNode = pathToOrigin.BrowserNodes[MLDict.dictionaryPlanesName[languageCode][0]].NativeObject;
                    oGeomLine = oSketch.AddByProjectingEntity(oTopNode);
                    oGeomLine.Construction = true;
                }
                catch(Exception e)
                {
                    goto ignoreException;
                }

            ignoreException:


                try
                {
                    //Y Axis
                    oTopNode = pathToOrigin.BrowserNodes[MLDict.dictionaryPlanesName[languageCode][1]].NativeObject;
                    oGeomLine = oSketch.AddByProjectingEntity(oTopNode);
                    oGeomLine.Construction = true;
                }
                catch(Exception e)
                {
                    goto ignoreException2;
                }

            ignoreException2:

                try
                {
                    //Z Axis
                    oTopNode = pathToOrigin.BrowserNodes[MLDict.dictionaryPlanesName[languageCode][2]].NativeObject;
                    oGeomLine = oSketch.AddByProjectingEntity(oTopNode);
                    oGeomLine.Construction = true;
                }
                catch(Exception e)
                {
                    goto ignoreException3;
                }

            ignoreException3:



                oTransaction.End();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n AddIn: Sedenum Pack");
            }


        }
        #endregion
    }
}
