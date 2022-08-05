using Inventor;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevAddIns
{
    class assemblyDocum : IDocument
    {
        private AssemblyDocument assemblyDoc;
        private Document accessDoc;

        public string PartNumber { get => assemblyDoc.PropertySets[3][2].Value.ToString(); }
        public ImageSource Thumbnail
        {
            get
            {
                using (var ms = new MemoryStream())
                {
                    Support.IPictureDispToImage(assemblyDoc.Thumbnail).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
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
                string bomStruct = assemblyDoc.ComponentDefinition.BOMStructure.ToStringExt();
                if (string.IsNullOrEmpty(bomStruct))
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
                //string material = assemblyDoc.ActiveMaterial.DisplayName.ToString();
                //if (String.IsNullOrEmpty(material))
                //{
                //    return "NONE";
                //}
                //return material;
                return "";
            }
        }
        public string Revision
        {
            get
            {
                string revision = assemblyDoc.PropertySets[1][7].Value.ToString();
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
                if (accessDoc == assemblyDoc)
                {
                    return 1;
                }
                else if (accessDoc.isAssemblyDocument() || accessDoc.isWeldmentDocument())
                {
                    if (accessDoc is AssemblyDocument)
                    {
                        AssemblyDocument tempDOc = (AssemblyDocument)accessDoc;
                        //BOMView sd = tempDOc.ComponentDefinition.BOM.BOMViews[this.BOMStructure];
                        return 2;
                    }
                }
                return 3;
            }
        }
        public string SubType
        {
            get
            {
                return assemblyDoc.SubType;
            }
        }

        public assemblyDocum(Document doc, Document accessDocument)
        {
            this.assemblyDoc = (AssemblyDocument)doc;
            this.accessDoc = accessDocument;
        }
    }
}
