using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAddIns
{
    class weldmentDocum : IDocument
    {
        public string PartNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public stdole.IPictureDisp Thumbnail { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string BOMStructure { get => "Unseperatable"; }
        public string Material { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Revision { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Quantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
