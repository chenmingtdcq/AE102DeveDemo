using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LineToPolygonByWidthAttribute;

namespace ArcEngine10._2_Project
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开shapefile文件
        /// </summary>
        private void btnOpenShapfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dg = new OpenFileDialog();
            dg.Filter = "Shapefile文件(.shp)|*.shp";
            dg.Multiselect = true;
            if (dg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = dg.FileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("请选择要打开的shapefile文件");
                    return;
                }

                IWorkspaceFactory pWksFactory = new ShapefileWorkspaceFactory();
                IWorkspace pWks = pWksFactory.OpenFromFile(System.IO.Path.GetDirectoryName(fileName), 0);
                IFeatureWorkspace pFeatureWks = (IFeatureWorkspace)pWks;
                IFeatureClass pFc = pFeatureWks.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(fileName));
                IFeatureLayer pFlayer = new FeatureLayerClass();
                pFlayer.FeatureClass = pFc;
                pFlayer.Name = pFc.AliasName;
                this.axMapControl1.Map.AddLayer((ILayer)pFlayer);
                this.axMapControl1.ActiveView.Extent = this.axMapControl1.ActiveView.FullExtent;
                this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            IBasicMap pMap = null;
            ILayer pLayer = null;
            object unk = null, data = null;
            ESRI.ArcGIS.Controls.esriTOCControlItem pItem = ESRI.ArcGIS.Controls.esriTOCControlItem.esriTOCControlItemNone;
            try
            {
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
            }
            catch (Exception)
            {
                throw;
            }
            switch (e.button)
            {
                case 2:
                    //右键
                    if (pItem == ESRI.ArcGIS.Controls.esriTOCControlItem.esriTOCControlItemMap)
                    {
                        axTOCControl1.SelectItem(pMap, null);
                    }
                    else if (pItem == ESRI.ArcGIS.Controls.esriTOCControlItem.esriTOCControlItemLayer)
                    {
                        axTOCControl1.SelectItem(pLayer, null);
                    }

                    axMapControl1.CustomProperty = pLayer;
                    if (pItem == ESRI.ArcGIS.Controls.esriTOCControlItem.esriTOCControlItemMap)
                    {
                        //
                    }
                    if (pItem == ESRI.ArcGIS.Controls.esriTOCControlItem.esriTOCControlItemLayer)
                    {
                        var m_pMenuLayer = new ToolbarMenu();
                        m_pMenuLayer.AddItem(new BuildLineBuffer(), -1, 0, false, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleTextOnly);
                        m_pMenuLayer.SetHook(axMapControl1);
                        m_pMenuLayer.PopupMenu(e.x, e.y, axTOCControl1.hWnd);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
