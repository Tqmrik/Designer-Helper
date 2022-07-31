using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;

namespace DevAddIns
{
    class sheetMetalDocum : IDocument
    {
        public string PartNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ImageSource Thumbnail { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string BOMStructure { get => "Normal"; }
        public string Material { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Revision { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Quantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string SheetThickness { get; set; }
    }
}
