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
    interface IDocument
    {
        string PartNumber { get;}
        ImageSource Thumbnail { get;}
        string BOMStructure { get;}
        string Material { get;}
        string Revision { get;}
        int Quantity { get;}
    }

    //Do we really need set?
    //Perhaps only if we are planning to work with Vault
}
