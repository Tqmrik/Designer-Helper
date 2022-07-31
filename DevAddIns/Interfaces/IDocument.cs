using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAddIns
{
    interface IDocument
    {
        string PartNumber { get;}
        stdole.IPictureDisp Thumbnail { get;}
        string BOMStructure { get;}
        string Material { get;}
        string Revision { get;}
        int Quantity { get;}
    }

    //Do we really need set?
    //Perhaps only if we are planning to work with Vault
}
