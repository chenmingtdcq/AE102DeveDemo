using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GISUtil
{
    public class GISUtil
    {
        public static IFeatureWorkspace CreateFeatureWorkspace(string shapefilePath)
        {
            IWorkspaceFactory pWksFactory = new ShapefileWorkspaceFactory();
            IWorkspace pWks = pWksFactory.OpenFromFile(System.IO.Path.GetDirectoryName(shapefilePath), 0);
            IFeatureWorkspace pFeatureWks = pWks as IFeatureWorkspace;
            return pFeatureWks;
        }

        public static IFeatureLayer OpenShapefile(ref string shapefilePath)
        {
            OpenFileDialog dg = new OpenFileDialog();
            dg.Filter = "Shapefile文件(.shp)|*.shp";
            dg.Multiselect = true;
            if (dg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = dg.FileName;
                shapefilePath = fileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("请选择要打开的shapefile文件");
                    return null;
                }

                IWorkspaceFactory pWksFactory = new ShapefileWorkspaceFactory();
                IWorkspace pWks = pWksFactory.OpenFromFile(System.IO.Path.GetDirectoryName(fileName), 0);
                IFeatureWorkspace pFeatureWks = (IFeatureWorkspace)pWks;
                IFeatureClass pFc = pFeatureWks.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(fileName));
                IFeatureLayer pFlayer = new FeatureLayerClass();
                pFlayer.FeatureClass = pFc;
                pFlayer.Name = pFc.AliasName;
                return pFlayer;
            }
            return null;
        }
    }
}
