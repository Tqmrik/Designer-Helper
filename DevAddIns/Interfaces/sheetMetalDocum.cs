using Inventor;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevAddIns
{
    class sheetMetalDocum : IDocument
    {
        private Document partDoc;
        private Document accessDoc;
        private SheetMetalComponentDefinition oSMCD;

        public string PartNumber { get => partDoc.PropertySets[3][2].Value.ToString(); }
        public ImageSource Thumbnail
        {
            get
            {
                using (var ms = new MemoryStream())
                {
                    Support.IPictureDispToImage(partDoc.Thumbnail).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
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
                string bomStruct = ((PartDocument)partDoc).ComponentDefinition.BOMStructure.ToStringExt();
                if (string.IsNullOrEmpty(bomStruct))
                {
                    return "NONE";
                }
                return bomStruct;
            }
        }

        string SheetThickness
        {
            get
            {
                if (partDoc.isSheetMetalDocument())
                {
                    oSMCD = (SheetMetalComponentDefinition)((PartDocument)partDoc).ComponentDefinition;
                    string thickness = oSMCD.Thickness.ToString();
                    if (String.IsNullOrEmpty(thickness))
                    {
                        return "NONE";
                    }
                    return "asd";
                }

                //TODO: Change
                return "0";
            }
            
        }
        public string Material
        {
            get
            {
                string material = ((PartDocument)partDoc).ActiveMaterial.DisplayName.ToString();
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
                if (String.IsNullOrEmpty(revision))
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
                else if (accessDoc.isAssemblyDocument() || accessDoc.isWeldmentDocument())
                {
                    if (accessDoc is AssemblyDocument)
                    {
                        AssemblyDocument tempDOc = (AssemblyDocument)accessDoc;
                        BOMView sd = tempDOc.ComponentDefinition.BOM.BOMViews[this.BOMStructure];
                        return 2;
                    }
                }
                return 3;
            }
        }

        public sheetMetalDocum(Document partDocument, Document accessDocument)
        {
            this.partDoc = partDocument;
            this.accessDoc = accessDocument;
        }

    }
}
