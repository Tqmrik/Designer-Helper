using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Compatibility.VB6;
using System.Drawing;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace DevAddIns
{
    class partDocum : IDocument
    {
        private PartDocument partDoc;
        private Document accessDoc;


        public string PartNumber { get => partDoc.PropertySets[3][2].Value.ToString(); }
        public ImageSource Thumbnail
        {
            get
            {
                using (var ms = new MemoryStream())
                {
                    Support.IPictureDispToImage(partDoc.Thumbnail).Save(ms,System.Drawing.Imaging.ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);

                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();

                    return bitmapImage;
                }
            }
        }
        public string BOMStructure
        {
            get
            {
                string bomStruct = partDoc.ComponentDefinition.BOMStructure.ToStringExt();
                if(string.IsNullOrEmpty(bomStruct))
                {
                    return "NONE";
                }
                return bomStruct;
            }
        }
        public string Material 
        {
            get
            {
                string material = partDoc.ActiveMaterial.DisplayName.ToString();
                if (String.IsNullOrEmpty(material))
                {
                    return "NONE";
                }
                return material;

            }
        }
        public string Revision
        {
            get
            {
                string revision = partDoc.PropertySets[1][7].Value.ToString();
                if(String.IsNullOrEmpty(revision))
                {
                    return "NONE";
                }
                return revision;
            }
        }
        public int Quantity
        {
            get
            {
                if (accessDoc == partDoc)
                {
                    return 1;
                }
                else if(accessDoc.isAssemblyDocument() || accessDoc.isWeldmentDocument())
                {
                    if(accessDoc is AssemblyDocument)
                    {
                        AssemblyDocument tempDOc= (AssemblyDocument)accessDoc;
                        BOMView sd = tempDOc.ComponentDefinition.BOM.BOMViews[this.BOMStructure];
                        return 2;
                    }
                }
                return 3;
            }
        }

        public partDocum(Document partDocument, Document accessDocument)
        {
            this.partDoc = (PartDocument)partDocument;
            this.accessDoc = accessDocument;
        }

        
    }
}



//TODO: Qunatity for the partDocument