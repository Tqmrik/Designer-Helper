using System;
using System.Windows.Forms;
using System.Drawing;
using Inventor;


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

			}
			catch(Exception e)
            {
				MessageBox.Show(e.Message + "\n AddIn: Sedenum Pack");
            }
			

		}
		#endregion
	}
}


/*
 *  If Not TypeOf ThisApplication.ActiveEditObject Is PlanarSketch Then 'Check if active object is a sketch
        Exit Sub
    End If

    '-------------------------------
    'Declarations

    Dim doc As Document
    Dim oTopNode As Variant
    Dim oGeomLine As SketchEntity 'Define object type that you are projecting
    Dim oSketch As PlanarSketch 'Enter current Sketch
    Dim oTrans As Transaction
    Dim langCode As String
    Dim mL As New Multilang
    Dim pathToOrigin As Variant
    Dim actSketch As PlanarSketch
    Dim planes As Variant
    
    Set doc = ThisApplication.ActiveDocument
    langCode = ThisApplication.LanguageCode
    Set oSketch = ThisApplication.ActiveEditObject
    
    '-------------------------------
    'Guard clauses
    
    If doc.SubType = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}" Then
        Set pathToOrigin = doc.BrowserPanes("PmDefault").TopNode.BrowserNodes(mL.dictBend(langCode)) _
        .BrowserNodes(mL.dictOrigin(langCode)) 'Sheet metal
    ElseIf doc.SubType = "{E60F81E1-49B3-11D0-93C3-7E0706000000}" Then
        Set pathToOrigin = doc.BrowserPanes("AmBrowserArrangement").TopNode _
        .BrowserNodes(mL.dictOrigin(langCode)) 'Assembly
    ElseIf doc.SubType = "{4D29B490-49B2-11D0-93C3-7E0706000000}" Then
        Set pathToOrigin = doc.BrowserPanes("PmDefault").TopNode _
        .BrowserNodes(mL.dictOrigin(langCode)) 'Part
    End If
     
    If doc Is Nothing Then 'Exit sub on you are on an Zero Document
        Exit Sub
    End If
    
    '-------------------------------
    'Logic

    On Error Resume Next
    planes = mL.dictPlanes(langCode)
    
    Set doc = ThisApplication.ActiveDocument
    If doc.ActivatedObject.Type = kPlanarSketchObject Then
        Set actSketch = doc.ActivatedObject
    End If
    
    Dim Line As SketchLine
    For Each Line In actSketch.SketchLines
    
        If Line.ReferencedEntity Is Nothing Then GoTo continueLoop
        
        If IsInArray(Line.ReferencedEntity.Name, planes) Then
            Line.Delete
        End If
        
continueLoop:
        
    Next Line
    
    On Error Resume Next 'Apparently Cannot project a plane sometimes
    'Possible solutions: 1) leave it as it is, pretty much nothing will change in a whole
    '2) try to avoid error by using different object
    
    Set oTrans = ThisApplication.TransactionManager.StartTransaction( _
    ThisApplication.ActiveDocument, _
    "Change IProperties")
    
    'X Axis
    Set oTopNode = pathToOrigin.BrowserNodes.Item(1).NativeObject
    Set oGeomLine = oSketch.AddByProjectingEntity(oTopNode)
    oGeomLine.Construction() = True
    
    'Y Axis
    Set oTopNode = pathToOrigin.BrowserNodes.Item(2).NativeObject
    Set oGeomLine = oSketch.AddByProjectingEntity(oTopNode)
    oGeomLine.Construction() = True 'So you make a part, and only then edit it's properties!!!!!
    
    'Z Axis
    Set oTopNode = pathToOrigin.BrowserNodes.Item(3).NativeObject
    Set oGeomLine = oSketch.AddByProjectingEntity(oTopNode)
    oGeomLine.Construction() = True
       
    oTrans.End
 * 
 */