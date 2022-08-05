using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DevAddIns
{
    class weldmentDocum : IDocument
    {
        public string PartNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ImageSource Thumbnail { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string BOMStructure { get => "Unseperatable"; }
        public string Material { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Revision { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Quantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SubType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
