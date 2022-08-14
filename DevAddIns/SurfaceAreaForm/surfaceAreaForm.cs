using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;

namespace DevAddIns
{

    
    public partial class surfaceAreaForm : Form
    {



        private static Inventor.Application sdsd;
        public static Inventor.Application invApp
        {
            get
            {
                return sdsd;
            }
            set
            {
                sdsd = value;
            }
        }
        public surfaceAreaForm()
        {
            InitializeComponent();
        }

        public void OntrackChange()
        {
            
            //If change area is not null call delegate
            double totalArea = 0;
            foreach (var sd in sdsd.ActiveDocument.SelectSet)
            {
                if (sd is Face)
                {
                    totalArea += ((Face)sd).Evaluator.Area; //Aparently Evaluator shows area in cm^2, though I don't know why 
                }
            }
            totalArea *= 10e1;

            label2.Text = totalArea.ToString("#.###") + " mm^2"; //+ sdsd.ActiveDocument.UnitsOfMeasure.LengthUnits.ToStringExt();

        }
    }
}


//TODO: The problem is simple, though I don't know how to cope with it