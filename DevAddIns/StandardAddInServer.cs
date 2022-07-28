using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using Inventor;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic.Compatibility.VB6;


namespace DevAddIns
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("fb869b0a-a71f-4590-89fc-ff707daa96c3")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        //Inventor application object
        private Inventor.Application m_inventorApplication;
        private UserInterfaceManager m_userInterfaceManager;

        //buttons
        private SetPropertiesButton m_setPropertiesButton;
        private EditPropertiesButton m_editPropertiesButton;
        private UpdatePropertiesRevisionButton m_updatePropertiesRevesionButton;
        private ChangeToMetricButton m_ChangeToMetric;
        private ProjectSketchAxisButton m_projectSketchAxisButton;
        private ExportAsButton m_exportAsButton;
        private ShowPropertiesButton m_showProperties;
        private TestFunctionButton m_testFunctionButton;
        private BalloonsEndArrow m_balloonsEndArrow;
        private ExecuteOnStartup m_ExecuteOnStartUp;
        private NewExportAsButton m_newExportAsButton;

        //comboBoxes
        private DrawingStyleComboBox m_drawingStyleComboBox;

        //user interface event
        //Only for combobox events, could be deleted
        private UserInterfaceEvents m_userInterfaceEvents;

        // ribbon panel
        //RibbonPanel m_partSketchSlotRibbonPanel;

        //event handler delegates

        #region Events
        private Inventor.ComboBoxDefinitionSink_OnSelectEventHandler SlotWidthComboBox_OnSelectEventDelegate;
        private Inventor.ComboBoxDefinitionSink_OnSelectEventHandler SlotHeightComboBox_OnSelectEventDelegate;


        private Inventor.UserInterfaceEventsSink_OnResetCommandBarsEventHandler UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;
        private Inventor.UserInterfaceEventsSink_OnResetEnvironmentsEventHandler UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;
        private Inventor.UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;
        #endregion

        public StandardAddInServer()
        {
        }




        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {

            //TODO: ADD Choose language combobox in the ribbon 

            /*
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.
             * Sample to illustrate creating a button definition.
        'Dim largeIcon As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(My.Resources.YourBigImage)
        'Dim smallIcon As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(My.Resources.YourSmallImage)
        'Dim controlDefs As Inventor.ControlDefinitions = g_inventorApplication.CommandManager.ControlDefinitions
        'm_sampleButton = controlDefs.AddButtonDefinition("Command Name", "Internal Name", CommandTypesEnum.kShapeEditCmdType, AddInClientID)
            */
            try
            {
                //the Activate method is called by Inventor when it loads the addin
                //the AddInSiteObject provides access to the Inventor Application object
                //the FirstTime flag indicates if the addin is loaded for the first time

                //initialize AddIn members
                m_inventorApplication = addInSiteObject.Application;
                Button.InventorApplication = m_inventorApplication;
                ComboBox.InventorApplication = m_inventorApplication;
                ExportAsForm.InventorApplication = m_inventorApplication;
                Translators.InventorApplication = m_inventorApplication;
                TranslatorList.InventorApplication = m_inventorApplication;
                NewExportAsButton.InventorApplication = m_inventorApplication;


                m_ExecuteOnStartUp = new ExecuteOnStartup();


                //m_userInterfaceManager = m_inventorApplication.UserInterfaceManager;

                //initialize event delegates
                //m_userInterfaceEvents = m_inventorApplication.UserInterfaceManager.UserInterfaceEvents;


                //It's so bad but whatever for now
                string applicationInstallationPath = m_inventorApplication.InstallPath;
                string allUserAppDataPath = m_inventorApplication.AllUsersAppDataPath;
                allUserAppDataPath = allUserAppDataPath.Replace("\\Inventor 2021", "") + "\\ApplicationPlugins\\DevAddIns\\ResourcesSedenumPack";

                //CopyDirectory(currentUserAppDataPath + "\\ApplicationPlugins\\DevAddIns\\ResourcesSedenumPack", applicationInstallationPath + "Bin\\ResourcesSedenumPack", false);

                //UserInterfaceEventsSink_OnResetCommandBarsEventDelegate = new UserInterfaceEventsSink_OnResetCommandBarsEventHandler(UserInterfaceEvents_OnResetCommandBars);
                //m_userInterfaceEvents.OnResetCommandBars += UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;

                //UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate = new UserInterfaceEventsSink_OnResetEnvironmentsEventHandler(UserInterfaceEvents_OnResetEnvironments);
                //m_userInterfaceEvents.OnResetEnvironments += UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;

                //UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate = new UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler(UserInterfaceEvents_OnResetRibbonInterface);
                //m_userInterfaceEvents.OnResetRibbonInterface += UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;

                //Load image icons for UI items
                //Wrap in a different method + use the class name for the names

                //var svgDoc = SvgDocument.Open<SvgDocument>(new MemoryStream(Properties.Resources.ProjectSketchAxis));
                

                Icon setPropertiesIconStandart = Properties.Resources.SetPropertiesIconStandart;
                Icon setPropertiesIconLarge = Properties.Resources.SetPropertiesIconLarge;
                //new Bitmap(currentUserAppDataPath + "\\SetPropertiesIconLarge.ico");

                Image changeMassLengthUnitsToMetricIconStandart = Properties.Resources.ProjectSketchAxisStandart;
                //new Bitmap(currentUserAppDataPath + "\\ChangeMassLengthUnitsToMetricIconStandart.png");
                Image changeMassLengthUnitsToMetricIconLarge = Properties.Resources.ProjectSketchAxisLarge;
                //new Bitmap(currentUserAppDataPath + "\\ChangeMassLengthUnitsToMetricIconLarge.png");

                
                //svgDoc.Ppi = 200;
                Image projectSketchAxisIconStandart = setPropertiesIconStandart.ToBitmap();
                Image projectSketchAxisIconLarge = setPropertiesIconLarge.ToBitmap();

                //new Bitmap(currentUserAppDataPath + "\\ProjectSketchAxisStandart.png");
                //stdole.IPictureDisp abs = PictureDispConverter.ToIPictureDisp();
                //IPictureDisp is needed as the button icon


                #region comboBoxes
                //create the comboboxes
                // m_slotWidthComboBoxDefinition = m_inventorApplication.CommandManager.ControlDefinitions.AddComboBoxDefinition("Slot Width", "Autodesk:SimpleAddIn:SlotWidthCboBox", CommandTypesEnum.kShapeEditCmdType, 100, addInCLSIDString, "Slot width", "Slot width", Type.Missing, Type.Missing, ButtonDisplayEnum.kDisplayTextInLearningMode);
                // m_slotHeightComboBoxDefinition = m_inventorApplication.CommandManager.ControlDefinitions.AddComboBoxDefinition("Slot Height", "Autodesk:SimpleAddIn:SlotHeightCboBox", CommandTypesEnum.kShapeEditCmdType, 100, addInCLSIDString, "Slot height", "Slot height", Type.Missing, Type.Missing, ButtonDisplayEnum.kDisplayTextInLearningMode);

                //add some initial items to the comboboxes
                //m_slotWidthComboBoxDefinition.AddItem("1 cm", 0);
                //m_slotWidthComboBoxDefinition.AddItem("2 cm", 0);
                //m_slotWidthComboBoxDefinition.AddItem("3 cm", 0);
                //m_slotWidthComboBoxDefinition.AddItem("4 cm", 0);
                //m_slotWidthComboBoxDefinition.AddItem("5 cm", 0);
                //m_slotWidthComboBoxDefinition.ListIndex = 1;
                //m_slotWidthComboBoxDefinition.ToolTipText = m_slotWidthComboBoxDefinition.Text;
                //m_slotWidthComboBoxDefinition.DescriptionText = "Slot width: " + m_slotWidthComboBoxDefinition.Text;

                //SlotWidthComboBox_OnSelectEventDelegate = new ComboBoxDefinitionSink_OnSelectEventHandler(SlotWidthComboBox_OnSelect);
                //m_slotWidthComboBoxDefinition.OnSelect += SlotWidthComboBox_OnSelectEventDelegate;

                //m_slotHeightComboBoxDefinition.AddItem("1 cm", 0);
                //m_slotHeightComboBoxDefinition.AddItem("2 cm", 0);
                //m_slotHeightComboBoxDefinition.AddItem("3 cm", 0);
                //m_slotHeightComboBoxDefinition.AddItem("4 cm", 0);
                //m_slotHeightComboBoxDefinition.AddItem("5 cm", 0);
                //m_slotHeightComboBoxDefinition.ListIndex = 1;
                //m_slotHeightComboBoxDefinition.ToolTipText = m_slotHeightComboBoxDefinition.Text;
                //m_slotHeightComboBoxDefinition.DescriptionText = "Slot height: " + m_slotHeightComboBoxDefinition.Text;

                //SlotHeightComboBox_OnSelectEventDelegate = new ComboBoxDefinitionSink_OnSelectEventHandler(SlotHeightComboBox_OnSelect);
                //m_slotHeightComboBoxDefinition.OnSelect += SlotHeightComboBox_OnSelectEventDelegate;
                #endregion

                //Create buttons
                m_setPropertiesButton = new SetPropertiesButton(
                    "Set Properties", "SetPropertiesSedenum", CommandTypesEnum.kFilePropertyEditCmdType,
                    AddInClientID(), "Change IProperties of the file according to the current company standart",
                    "Change IProperties", setPropertiesIconStandart, setPropertiesIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);

                m_editPropertiesButton = new EditPropertiesButton("Edit Properties", "EditPropertiesSedenum", CommandTypesEnum.kFilePropertyEditCmdType, AddInClientID(), "Edit values of the Properties to set", "Edit IProperties", changeMassLengthUnitsToMetricIconStandart, changeMassLengthUnitsToMetricIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);

                m_updatePropertiesRevesionButton = new UpdatePropertiesRevisionButton("Update Revision", "UpdateDrawingRevision", CommandTypesEnum.kFilePropertyEditCmdType, AddInClientID(), "Update the revision number of the drawing", "Update revision", changeMassLengthUnitsToMetricIconStandart, changeMassLengthUnitsToMetricIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);

                m_ChangeToMetric = new ChangeToMetricButton("Metric units", "UnitsToMetricSedenum", CommandTypesEnum.kFilePropertyEditCmdType, AddInClientID(), "Changes document's unit to metric", "Change units", changeMassLengthUnitsToMetricIconStandart, changeMassLengthUnitsToMetricIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);

                m_projectSketchAxisButton = new ProjectSketchAxisButton("Project Axis", "ProjectSketchAxisSedenum", CommandTypesEnum.kShapeEditCmdType, AddInClientID(), "Project axis to the planar sketch", "Project Axis", projectSketchAxisIconStandart, projectSketchAxisIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);

                m_exportAsButton = new ExportAsButton("Export", "ExportToSedenum", CommandTypesEnum.kFileOperationsCmdType, AddInClientID(), "Export document to the file with the desired extension", "Export document to the file with the desired extension", changeMassLengthUnitsToMetricIconStandart, changeMassLengthUnitsToMetricIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);

                m_showProperties = new ShowPropertiesButton("Show Properies", "ShowPropertiesSedenum", CommandTypesEnum.kFilePropertyEditCmdType, AddInClientID(), "Copy all IProperties of the file to the notepad document", "Copy all IProperties of the file to the notepad document", changeMassLengthUnitsToMetricIconStandart, changeMassLengthUnitsToMetricIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);

                m_testFunctionButton = new TestFunctionButton("Test Funcion", "TestFunctionSedenum", CommandTypesEnum.kFilePropertyEditCmdType, AddInClientID(), "Test the dev function", "Test the dev function", changeMassLengthUnitsToMetricIconStandart, changeMassLengthUnitsToMetricIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);

                m_balloonsEndArrow = new BalloonsEndArrow("Change arrowtype", "ChangeBallonArrowHeadSedenum", CommandTypesEnum.kFileOperationsCmdType, AddInClientID(), "Change arrowheads", "Change arrowheads", changeMassLengthUnitsToMetricIconStandart, changeMassLengthUnitsToMetricIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);

                //m_drawingStyleComboBox = new DrawingStyleComboBox("123", "312", CommandTypesEnum.kSchemaChangeCmdType, 100, AddInClientID(), "ddesc", "asda",ButtonDisplayEnum.kDisplayTextInLearningMode);
                //Create comboBoxes

                //Apparently doesn't work with icons????
                m_drawingStyleComboBox = new DrawingStyleComboBox("Change drawing style", "ChangeDrawingStyleComboBoxSedenum", CommandTypesEnum.kSchemaChangeCmdType, 100, AddInClientID(), "Change drawing style", "Change drawing style", ButtonDisplayEnum.kDisplayTextInLearningMode);

                m_newExportAsButton = new NewExportAsButton("NewExport", "NewExportToSedenum", CommandTypesEnum.kFileOperationsCmdType, AddInClientID(), "Export document to the file with the desired extension", "Export document to the file with the desired extension", changeMassLengthUnitsToMetricIconStandart, changeMassLengthUnitsToMetricIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);

                
                    
                    
                    //("Change drawing style", "ChangeDrawingStyleComboBoxSedenum",
                    //CommandTypesEnum.kSchemaChangeCmdType, 100, AddInClientID(), "Change drawing style sistem", "Change drawing style", setPropertiesIconStandart, setPropertiesIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);



                //create the command category
                //CommandCategory slotCmdCategory = m_inventorApplication.CommandManager.CommandCategories.Add("Slot", "Autodesk:SimpleAddIn:SlotCmdCat", addInCLSIDString);

                //slotCmdCategory.Add(m_slotWidthComboBoxDefinition);
                //slotCmdCategory.Add(m_slotHeightComboBoxDefinition);
                //slotCmdCategory.Add(m_setPropertiesButton.ButtonDefinition);

                if (firstTime == true)
                {
                    AddToUserInterface();
                    #region Old
                    ////access user interface manager
                    //UserInterfaceManager userInterfaceManager;
                    //userInterfaceManager = m_inventorApplication.UserInterfaceManager;

                    //InterfaceStyleEnum interfaceStyle;
                    //interfaceStyle = userInterfaceManager.InterfaceStyle;

                    ////create the UI for classic interface
                    //if (interfaceStyle == InterfaceStyleEnum.kClassicInterface)
                    //{
                    //    //create toolbar
                    //    CommandBar slotCommandBar;
                    //    slotCommandBar = userInterfaceManager.CommandBars.Add("Slot", "Autodesk:SimpleAddIn:SlotToolbar", CommandBarTypeEnum.kRegularCommandBar, AddInClientID());

                    //    //add comboboxes to toolbar
                    //    //slotCommandBar.Controls.AddComboBox(m_slotWidthComboBoxDefinition, 0);
                    //    //slotCommandBar.Controls.AddComboBox(m_slotHeightComboBoxDefinition, 0);

                    //    //add buttons to toolbar
                    //    slotCommandBar.Controls.AddButton(m_setPropertiesButton.ButtonDefinition, 0);


                    //    //Get the 2d sketch environment base object
                    //    //Inventor.Environment partSketchEnvironment;
                    //    //partSketchEnvironment = userInterfaceManager.Environments["PMxPartSketchEnvironment"];

                    //    //make this command bar accessible in the panel menu for the 2d sketch environment.
                    //    //partSketchEnvironment.PanelBar.CommandBarList.Add(slotCommandBar);
                    //}
                    ////create the UI for ribbon interface

                }
                //else
                //{
                //    //get the ribbon associated with part document
                //    Inventor.Ribbons ribbons;
                //    ribbons = userInterfaceManager.Ribbons;

                //    Inventor.Ribbon partRibbon;
                //    partRibbon = ribbons["Part"];

                //    //get the tabls associated with part ribbon
                //    RibbonTabs ribbonTabs;
                //    ribbonTabs = partRibbon.RibbonTabs;

                //    RibbonTab partToolsTab;
                //    partToolsTab = ribbonTabs["id_TabTools"];

                //    //create a new panel with the tab
                //    RibbonPanel ribbonPanel;
                //    ribbonPanel = partToolsTab.RibbonPanels.Add("SampleTab", "SampleTabSeden", AddInClientID());

                //    ribbonPanel.CommandControls.AddButton(m_setPropertiesButton.ButtonDefinition);

                //    //m_partSketchSlotRibbonPanel = ribbonPanels.Add("Slot", "Autodesk:SimpleAddIn:SlotRibbonPanel", "{DB59D9A7-EE4C-434A-BB5A-F93E8866E872}", "", false);

                //    //add controls to the slot panel
                //    CommandControls partSketchSlotRibbonPanelCtrls;
                //    partSketchSlotRibbonPanelCtrls = m_partSketchSlotRibbonPanel.CommandControls;

                //    //add the combo boxes to the ribbon panel  
                //    //CommandControl slotWidthCmdCboBoxCmdCtrl;
                //    //slotWidthCmdCboBoxCmdCtrl = partSketchSlotRibbonPanelCtrls.AddComboBox(m_slotWidthComboBoxDefinition, "", false);

                //    //CommandControl slotHeightCmdCboBoxCmdCtrl;
                //    //slotHeightCmdCboBoxCmdCtrl = partSketchSlotRibbonPanelCtrls.AddComboBox(m_slotHeightComboBoxDefinition, "", false);

                //    //add the buttons to the ribbon panel
                //    //CommandControl drawSlotCmdBtnCmdCtrl;
                //    //drawSlotCmdBtnCmdCtrl = partSketchSlotRibbonPanelCtrls.AddButton(m_setPropertiesButton.ButtonDefinition, false, true, "", false);


                //}
                //}
                #endregion
                }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString() + $"\n AddIn: Sedenum Pack");
            }
        }


        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated
            try
            {
                //m_userInterfaceEvents.OnResetCommandBars -= UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;
                //m_userInterfaceEvents.OnResetEnvironments -= UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;

                UserInterfaceEventsSink_OnResetCommandBarsEventDelegate = null;
                UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate = null;
                
                //Dispose interface objects
                m_userInterfaceEvents = null;
                m_userInterfaceManager = null;

                //Dispose buttons
                m_setPropertiesButton = null;
                m_editPropertiesButton = null;
                m_updatePropertiesRevesionButton = null;
                m_ChangeToMetric = null;
                m_projectSketchAxisButton = null;
                m_exportAsButton = null;
                m_showProperties = null;
                m_testFunctionButton = null;
                m_balloonsEndArrow = null;
                m_newExportAsButton = null;


                //Dispose comboboxes
                m_drawingStyleComboBox = null;


                //if (m_partSketchSlotRibbonPanel != null)
                //{
                //    m_partSketchSlotRibbonPanel.Delete();
                //}

                //release inventor Application object
                Marshal.ReleaseComObject(m_inventorApplication);
                m_inventorApplication = null;

                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get
            {
                // TODO: Ignore -> Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }

        #region "HelpMethods"
        public string AddInClientID()
        //Don't know the exact purpose but whatev
        {
            string guid = "";
            try
            {
                var t = typeof(DevAddIns.StandardAddInServer);
                var customAttributes = t.GetCustomAttributes(typeof(GuidAttribute), false);
                GuidAttribute guidAttrib = (GuidAttribute)customAttributes[0];
                guid = "{" + guidAttrib.Value.ToString() + "}";
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            return guid;
        }


        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);
            var destDir = new DirectoryInfo(destinationDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            if(!destDir.Exists)
            {
                Directory.CreateDirectory(destinationDir);
            }
            

            

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = System.IO.Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath, true);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = System.IO.Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }


        
        #endregion
        #endregion

        #region UI

        public void AddToUserInterface()
        {
            try
            {
                string panelName = "Sedenum";

                List<Button> buttonsList = new List<Button>();

                buttonsList.Add(m_ChangeToMetric);
                buttonsList.Add(m_projectSketchAxisButton);
                buttonsList.Add(m_testFunctionButton);
                buttonsList.Add(m_showProperties);


                List<Button> iPropertiesPanelButtons = new List<Button>();
                iPropertiesPanelButtons.Add(m_setPropertiesButton);
                iPropertiesPanelButtons.Add(m_editPropertiesButton);


                m_drawingStyleComboBox.Populate(new string[]{"ESKD", "ISO" });
                /* 
                '' Get the part ribbon.
                'Dim partRibbon As Ribbon = g_inventorApplication.UserInterfaceManager.Ribbons.Item("Part")

                '' Get the "Tools" tab.
                'Dim toolsTab As RibbonTab = partRibbon.RibbonTabs.Item("id_TabTools")

                '' Create a new panel.
                'Dim customPanel As RibbonPanel = toolsTab.RibbonPanels.Add("Sample", "MysSample", AddInClientID)

                '' Add a button.
                'customPanel.CommandControls.AddButton(m_sampleButton)
                */

                //Adding buttons to the ribbon
                //TODO: Redo all that part
                //Ribbons ribbons = m_inventorApplication.UserInterfaceManager.Ribbons;


                Ribbon partRibbon = m_inventorApplication.UserInterfaceManager.Ribbons["Part"];
                Ribbon assemblyRibbon = m_inventorApplication.UserInterfaceManager.Ribbons["Assembly"];
                Ribbon drawingRibbon = m_inventorApplication.UserInterfaceManager.Ribbons["Drawing"];
                //RibbonTabs partTabs = m_inventorApplication.UserInterfaceManager.Ribbons["Part"].RibbonTabs;



                RibbonTab assemblyToolTab = assemblyRibbon.RibbonTabs["id_TabTools"];
                RibbonTab partToolsTab = partRibbon.RibbonTabs["id_TabTools"];
                RibbonTab drawingToolsTab = drawingRibbon.RibbonTabs["id_TabTools"];



                RibbonPanel assemblyPanelSed = assemblyToolTab.RibbonPanels.Add(panelName, "assemblyPanelSed", AddInClientID());
                RibbonPanel partPanelSed = partToolsTab.RibbonPanels.Add(panelName, "partPanelSed", AddInClientID());
                RibbonPanel drawingPanelSed = drawingToolsTab.RibbonPanels.Add(panelName, "drawingPanelSed", AddInClientID());



                RibbonPanel iPropertiesDrawingPanel = drawingToolsTab.RibbonPanels.Add("IProperties", "iPropertiesDrawingPanelSed", AddInClientID());
                RibbonPanel iPropertiesPartPanel = partToolsTab.RibbonPanels.Add("IProperties", "iPropertiesPartPanelSed", AddInClientID());
                RibbonPanel iPropertiesAssemblyPanel = assemblyToolTab.RibbonPanels.Add("IProperties", "iPropertiesAssemblyPanelSed", AddInClientID());



                //SketchEnvironment
                //Inventor.Environment partSketchEnvironment;
                //partSketchEnvironment = m_userInterfaceManager.Environments["PMxPartSketchEnvironment"];
                //RibbonPanel customSketchPanel = partSketchEnvironment.Ribbon.RibbonTabs["id_TabTools"].RibbonPanels.Add("Sample", "SampleSketchSedenum", AddInClientID());


                //Add Buttons

                foreach (Button button in buttonsList)
                {
                    if (!button.Equals(null))
                    {
                        partPanelSed.CommandControls.AddButton(button.ButtonDefinition);
                        assemblyPanelSed.CommandControls.AddButton(button.ButtonDefinition);
                        drawingPanelSed.CommandControls.AddButton(button.ButtonDefinition);
                    }
                }

                foreach(Button propButton in iPropertiesPanelButtons)
                {
                    if(!propButton.Equals(null))
                    {
                        iPropertiesAssemblyPanel.CommandControls.AddButton(propButton.ButtonDefinition);
                        iPropertiesPartPanel.CommandControls.AddButton(propButton.ButtonDefinition);
                        iPropertiesDrawingPanel.CommandControls.AddButton(propButton.ButtonDefinition);
                    }
                }



                drawingPanelSed.CommandControls.AddButton(m_balloonsEndArrow.ButtonDefinition);


                assemblyPanelSed.CommandControls.AddButton(m_exportAsButton.ButtonDefinition);
                assemblyPanelSed.CommandControls.AddButton(m_newExportAsButton.ButtonDefinition);


                partPanelSed.CommandControls.AddButton(m_exportAsButton.ButtonDefinition);
                partPanelSed.CommandControls.AddButton(m_newExportAsButton.ButtonDefinition);


                iPropertiesDrawingPanel.CommandControls.AddButton(m_exportAsButton.ButtonDefinition);
                iPropertiesDrawingPanel.CommandControls.AddButton(m_newExportAsButton.ButtonDefinition);
                iPropertiesDrawingPanel.CommandControls.AddButton(m_updatePropertiesRevesionButton.ButtonDefinition);

                //assemblyPanelSed.CommandControls.AddButton(m_showProperties.ButtonDefinition);
                //partPanelSed.CommandControls.AddButton(m_showProperties.ButtonDefinition);
                //drawingPanelSed.CommandControls.AddButton(m_showProperties.ButtonDefinition);

                //Add ComboBoxes

                //drawingPanelSed.CommandControls.AddComboBox(m_drawingStyleComboBox.ComboBoxDefinition);


            }

            catch(Exception e)
            {
                MessageBox.Show(e.Message + "\n AddIn: Sedenum Pack");
            }
            
            //customSketchPanel.CommandControls.AddButton(m_projectSketchAxis.ButtonDefinition);


            //Custom tab, tab won't show until there is some element(i.e. button, etc.) in it

            //RibbonTab seden = partTabs.Add("Sedenum", "Sedenum Pack Tab", AddInClientID());
            //RibbonPanel sedenPanel = seden.RibbonPanels.Add("Sample", "Sedenpanel", AddInClientID());

            //sedenPanel.CommandControls.AddButton(m_setPropertiesButton.ButtonDefinition);

        }

        #region SampleButtonEventHandler
        //Sample event handler

        //public void m_sampleButton_OnExecute(Inventor.NameValueMap Context)
        //{
        //    MessageBox.Show("hello");
        #endregion


        //        private void UserInterfaceEvents_OnResetCommandBars(ObjectsEnumerator commandBars, NameValueMap context)
        //        {
        //            try
        //            {
        //                CommandBar commandBar;
        //                for (int commandBarCt = 1; commandBarCt <= commandBars.Count; commandBarCt++)
        //                {
        //                    commandBar = (Inventor.CommandBar)commandBars[commandBarCt];
        //                    if (commandBar.InternalName == "Autodesk:SimpleAddIn:SlotToolbar")
        //                    {
        //                        //add comboboxes to toolbar
        //                        //commandBar.Controls.AddComboBox(m_slotWidthComboBoxDefinition, 0);
        //                       // commandBar.Controls.AddComboBox(m_slotHeightComboBoxDefinition, 0);

        //                        //add buttons to toolbar
        //                        commandBar.Controls.AddButton(m_setPropertiesButton.ButtonDefinition, 0);
        //;

        //                        return;
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                MessageBox.Show(e.ToString());
        //            }
        //        }

        //        private void UserInterfaceEvents_OnResetEnvironments(ObjectsEnumerator environments, NameValueMap context)
        //        {
        //            try
        //            {
        //                Inventor.Environment environment;
        //                for (int environmentCt = 1; environmentCt <= environments.Count; environmentCt++)
        //                {
        //                    environment = (Inventor.Environment)environments[environmentCt];
        //                    if (environment.InternalName == "PMxPartSketchEnvironment")
        //                    {
        //                        //make this command bar accessible in the panel menu for the 2d sketch environment.
        //                        environment.PanelBar.CommandBarList.Add(m_inventorApplication.UserInterfaceManager.CommandBars["Autodesk:SimpleAddIn:SlotToolbar"]);

        //                        return;
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                MessageBox.Show(e.ToString());
        //            }
        //        }

        //        private void UserInterfaceEvents_OnResetRibbonInterface(NameValueMap context)
        //        {
        //            try
        //            {

        //                UserInterfaceManager userInterfaceManager;
        //                userInterfaceManager = m_inventorApplication.UserInterfaceManager;

        //                //get the ribbon associated with part document
        //                Inventor.Ribbons ribbons;
        //                ribbons = userInterfaceManager.Ribbons;

        //                Inventor.Ribbon partRibbon;
        //                partRibbon = ribbons["Part"];

        //                //get the tabls associated with part ribbon
        //                RibbonTabs ribbonTabs;
        //                ribbonTabs = partRibbon.RibbonTabs;

        //                RibbonTab partSketchRibbonTab;
        //                partSketchRibbonTab = ribbonTabs["id_TabSketch"];

        //                //create a new panel with the tab
        //                RibbonPanels ribbonPanels;
        //                ribbonPanels = partSketchRibbonTab.RibbonPanels;

        //                m_partSketchSlotRibbonPanel = ribbonPanels.Add("Slot", "Autodesk:SimpleAddIn:SlotRibbonPanel",
        //                                                             "{DB59D9A7-EE4C-434A-BB5A-F93E8866E872}", "", false);

        //                //add controls to the slot panel
        //                CommandControls partSketchSlotRibbonPanelCtrls;
        //                partSketchSlotRibbonPanelCtrls = m_partSketchSlotRibbonPanel.CommandControls;

        //                //add the combo boxes to the ribbon panel  
        //                //CommandControl slotWidthCmdCboBoxCmdCtrl;
        //                //slotWidthCmdCboBoxCmdCtrl = partSketchSlotRibbonPanelCtrls.AddComboBox(m_slotWidthComboBoxDefinition, "", false);

        //                //CommandControl slotHeightCmdCboBoxCmdCtrl;
        //                //slotHeightCmdCboBoxCmdCtrl = partSketchSlotRibbonPanelCtrls.AddComboBox(m_slotHeightComboBoxDefinition, "", false);

        //                //add the buttons to the ribbon panel
        //                CommandControl drawSlotCmdBtnCmdCtrl;
        //                drawSlotCmdBtnCmdCtrl = partSketchSlotRibbonPanelCtrls.AddButton(m_setPropertiesButton.ButtonDefinition, false, true, "", false);

        //            }
        //            catch (Exception e)
        //            {
        //                MessageBox.Show(e.ToString());
        //            }
        //        }

        //private void SlotWidthComboBox_OnSelect(NameValueMap context)
        //{
        //    m_slotWidthComboBoxDefinition.ToolTipText = m_slotWidthComboBoxDefinition.Text;
        //    m_slotWidthComboBoxDefinition.DescriptionText = "Slot width: " + m_slotWidthComboBoxDefinition.Text;
        //}

        //private void SlotHeightComboBox_OnSelect(NameValueMap context)
        //{
        //    m_slotHeightComboBoxDefinition.ToolTipText = m_slotHeightComboBoxDefinition.Text;
        //    m_slotHeightComboBoxDefinition.DescriptionText = "Slot height: " + m_slotHeightComboBoxDefinition.Text;
        //}
        #endregion
    }

}



#region "TODO land"
    // TODO: Add web pages into the form???
    // TODO: Work with WPF
#endregion