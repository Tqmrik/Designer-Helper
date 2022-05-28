using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using Inventor;
using Microsoft.Win32;



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

        //buttons
        private SetPropertiesButton m_setPropertiesButton;
        private ChangeMassLengthUnitsToMetric m_ChangeMassLengthUnitsToMetric;


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

                //initialize event delegates
                m_userInterfaceEvents = m_inventorApplication.UserInterfaceManager.UserInterfaceEvents;

                //UserInterfaceEventsSink_OnResetCommandBarsEventDelegate = new UserInterfaceEventsSink_OnResetCommandBarsEventHandler(UserInterfaceEvents_OnResetCommandBars);
                //m_userInterfaceEvents.OnResetCommandBars += UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;

                //UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate = new UserInterfaceEventsSink_OnResetEnvironmentsEventHandler(UserInterfaceEvents_OnResetEnvironments);
                //m_userInterfaceEvents.OnResetEnvironments += UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;

                //UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate = new UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler(UserInterfaceEvents_OnResetRibbonInterface);
                //m_userInterfaceEvents.OnResetRibbonInterface += UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;

                //load image icons for UI items

                //Wrap in a different method???
                Image setPropertiesIconStandart = new Bitmap("ResourcesSedenumPack\\SetPropertiesIconStandart.ico");
                Image setPropertiesIconLarge = new Bitmap("ResourcesSedenumPack\\SetPropertiesIconLarge.ico");

                Image ChangeMassLengthUnitsToMetricIconStandart = new Bitmap("ResourcesSedenumPack\\ChangeMassLengthUnitsToMetricIconStandart.png");
                Image ChangeMassLengthUnitsToMetricIconLarge = new Bitmap("ResourcesSedenumPack\\ChangeMassLengthUnitsToMetricIconLarge.png");
                

                

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
                //create buttons

                m_setPropertiesButton = new SetPropertiesButton(
                    "Set Properties", "AbcSed", CommandTypesEnum.kFilePropertyEditCmdType,
                    AddInClientID(), "Adds option for slot width/height",
                    "Add slot option", setPropertiesIconStandart, setPropertiesIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);

                m_ChangeMassLengthUnitsToMetric = new ChangeMassLengthUnitsToMetric("Change Units", "ChangeMassLengthUnitsToMetricSedenum", CommandTypesEnum.kFilePropertyEditCmdType, AddInClientID(), "Changes document's unit of length to mm and unit of mass to kg", "Change units", ChangeMassLengthUnitsToMetricIconStandart, ChangeMassLengthUnitsToMetricIconLarge, ButtonDisplayEnum.kDisplayTextInLearningMode);




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

            // TODO: Add ApplicationAddInServer.Deactivate implementation
            try
            {
                m_userInterfaceEvents.OnResetCommandBars -= UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;
                m_userInterfaceEvents.OnResetEnvironments -= UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;

                UserInterfaceEventsSink_OnResetCommandBarsEventDelegate = null;
                UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate = null;
                m_userInterfaceEvents = null;
                m_setPropertiesButton = null;
                m_ChangeMassLengthUnitsToMetric = null;

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
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }

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

        #endregion

        #region UI
        
        public void AddToUserInterface()
        {

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
            Ribbons ribbons = m_inventorApplication.UserInterfaceManager.Ribbons;
            

            Ribbon partRibbon = m_inventorApplication.UserInterfaceManager.Ribbons["Part"];
            RibbonTabs partTabs = m_inventorApplication.UserInterfaceManager.Ribbons["Part"].RibbonTabs;


            RibbonTab toolsTab = partRibbon.RibbonTabs["id_TabTools"];
            RibbonPanel customPanel = toolsTab.RibbonPanels.Add("Sample", "SampleSedenum", AddInClientID());

            customPanel.CommandControls.AddButton(m_setPropertiesButton.ButtonDefinition);
            customPanel.CommandControls.AddButton(m_ChangeMassLengthUnitsToMetric.ButtonDefinition);


            //Custom tab, tab won't show until there is some element(i.e. button, etc.) in it
            RibbonTab seden = partTabs.Add("Sedenum", "Sedenum Pack Tab", AddInClientID());
            RibbonPanel sedenPanel = seden.RibbonPanels.Add("Sample", "Sedenpanel", AddInClientID());
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

