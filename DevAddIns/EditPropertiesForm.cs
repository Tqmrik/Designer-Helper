using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevAddIns
{
    public partial class EditPropertiesForm : Form
    {
        public EditPropertiesForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}


/*
 * 

Option Explicit

Private Sub fillButton_Click()

    Dim fso As Object
    Dim textFile As Object
    Dim checkByName As String
    Dim companyName As String
    Dim filePath As String

    Set fso = CreateObject("Scripting.FileSystemObject")
        
    filePath = GetFilePath 'Retrive active document path
    
    checkByName = checkedByField.Value 'Read from form's field
    companyName = companyNameField.Value
    
    filePath = "C:\Temp\InventorMacrosSetIProperties.txt" 'Specify file path and name
    
    If checkByName = "" Or companyName = "" Then 'If no field is filled exit sub
        textFile.Close
        Unload setPropertiesForm
        Exit Sub
    End If
    
    
    
    Set textFile = fso.OpenTextFile(filePath, 2, True) 'Creates new text file and opens it for writting
    textFile.WriteLine ("Checked : " & checkByName) 'Write Data
    textFile.WriteLine ("Company : " & companyName)
    
    textFile.Close
    
    Unload setPropertiesForm
    
End Sub


Private Sub UserForm_Click()

End Sub

 */