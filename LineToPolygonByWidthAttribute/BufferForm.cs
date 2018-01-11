using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineToPolygonByWidthAttribute
{
    public partial class BufferForm : Form
    {
        private IHookHelper m_HookHelper;
        private IMapControl3 m_MapControl;
        private IFeatureLayer pFeatureLayer;
        private string bufferSavefile;

        private IFeatureClass pFc; //线转面
        private string distanceField; //作为缓冲区距离取值字段
        private IFeatureLayer pDLTBFeatureLayer;
        private string shapefileDLTB = "";

        private esriFieldType currentFieldType; //面合并时当前所选字段类型

        public BufferForm(IHookHelper hookHelper)
        {
            InitializeComponent();

            this.m_HookHelper = hookHelper;
            m_MapControl = m_HookHelper.Hook as IMapControl3;
            pFeatureLayer = m_MapControl.CustomProperty as IFeatureLayer;
            InitialControl();
        }

        private void InitialControl()
        {
            cbFields.Enabled = false;
            cbFieldValues.Enabled = false;
            btnUnion.Enabled = false;
            btnImputDLTB.Enabled = false;
            tbInputDLTB.Enabled = false;
            button1.Enabled = false;
            cbUnit.DataSource = new List<string>() { "米", "千米" };

            ITable pLineTable = (ITable)pFeatureLayer;
            if (pLineTable == null)
                return;

            List<string> lineFields = new List<string>(pLineTable.Fields.FieldCount);
            for (int i = 0; i < pLineTable.Fields.FieldCount; i++)
            {
                IField field = pLineTable.Fields.Field[i];
                if (field.Type == esriFieldType.esriFieldTypeGeometry || field.Type == esriFieldType.esriFieldTypeOID)
                    continue;
                lineFields.Add(field.Name);
            }
            this.cbDictanceField.DataSource = lineFields;
            int widthIndex = lineFields.IndexOf("Width");
            this.cbDictanceField.SelectedIndex = widthIndex < 0 ? 0 : widthIndex;
            this.distanceField = widthIndex < 0 ? lineFields[0] : lineFields[widthIndex];
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.FolderBrowserDialog dg = new FolderBrowserDialog();
            //dg.Description = "请选择文件夹";
            //if (dg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    tbPath.Text = dg.SelectedPath;
            //}

            SaveFileDialog dg = new SaveFileDialog();
            dg.Filter = "Shapefile文件(.shp)|*.shp";
            if (dg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbPath.Text = dg.FileName;
                bufferSavefile = dg.FileName;
            }
            if (string.IsNullOrEmpty(tbPath.Text))
            {
                MessageBox.Show("请选择保存路径！");
                tbPath.Focus();
            }
        }

        /// <summary>
        /// 1.执行线转面
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (m_HookHelper == null || m_HookHelper.Hook == null)
                return;
            if (string.IsNullOrEmpty(bufferSavefile))
            {
                MessageBox.Show("请选择线转面保存路径！");
                return;
            }


            if (pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
            {
                MessageBox.Show("请选择线图层!");
                return;
            }

            BuildBuffer();
            SetControlDataSource();
        }

        private void cbFields_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var selectItem = cbFields.SelectedItem;
            if (selectItem == null)
                return;

            string fieldName = selectItem.ToString();
            ITable pTable = (ITable)pFc;
            currentFieldType = pTable.Fields.Field[pTable.Fields.FindField(fieldName)].Type;
            ICursor pCursor = pTable.Search(null, true);
            IRow pRow = pCursor.NextRow();
            HashSet<string> hsFieldValues = new HashSet<string>();
            while (pRow != null)
            {
                object value = pRow.get_Value(pRow.Fields.FindField(fieldName));
                if (value != null)
                    hsFieldValues.Add(value.ToString());
                pRow = pCursor.NextRow();
            }
            string[] arrayFieldValues = hsFieldValues.ToArray();
            cbFieldValues.Properties.Items.Clear();
            for (int i = 0; i < arrayFieldValues.Length; i++)
            {
                //CheckedListBoxItem item = new CheckedListBoxItem();
                cbFieldValues.Properties.Items.Add(arrayFieldValues[i], arrayFieldValues[i]);
            }
        }

        /// <summary>
        /// 2.执行面合并
        /// </summary>
        private void btnUnion_Click(object sender, EventArgs e)
        {
            object editValue = cbFieldValues.EditValue;
            if (editValue == null || string.IsNullOrEmpty(editValue.ToString()))
            {
                DialogResult result = MessageBox.Show("未选择合并类型值！ 是否要继续执行面擦除操作？", "面合并", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    btnImputDLTB.Enabled = true;
                    tbInputDLTB.Enabled = true;
                    button1.Enabled = true;
                }
                return;
            }

            string[] splitValues = editValue.ToString().Split(',');
            string type = (cbFields.SelectedItem).ToString().Trim();
            for (int i = 0; i < splitValues.Length; i++)
            {
                UnionBuffer(type, splitValues[i].Trim());
            }

            IFeatureLayer pFLayer = new FeatureLayerClass();
            pFLayer.Name = pFc.AliasName + "_Polygon";
            pFLayer.FeatureClass = pFc;
            m_MapControl.Map.AddLayer(pFLayer);
            m_MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            btnImputDLTB.Enabled = true;
            tbInputDLTB.Enabled = true;
        }

        /// <summary>
        /// 选择目标字段作为缓冲区生成距离取值
        /// </summary>
        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            object obj = this.cbDictanceField.SelectedItem;
            if (obj == null)
                return;

            distanceField = obj.ToString();

        }

        /// <summary>
        /// 输入被擦除地类图斑
        /// </summary>
        private void btnImputDLTB_Click(object sender, EventArgs e)
        {
            pDLTBFeatureLayer = GISUtil.GISUtil.OpenShapefile(ref shapefileDLTB);
            tbInputDLTB.Text = shapefileDLTB;
            if (pDLTBFeatureLayer == null)
            {
                MessageBox.Show("选择图斑数据无效，无法打开!");
                return;
            }
            button1.Enabled = true;
            m_MapControl.Map.AddLayer(pDLTBFeatureLayer);
            m_MapControl.ActiveView.Extent = m_MapControl.ActiveView.FullExtent;
            m_MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        /// <summary>
        /// 3.执行擦除
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            string erasePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(bufferSavefile), pDLTBFeatureLayer.Name + "_Erase.shp");
            IFeatureWorkspace pFWks = null;
            if (System.IO.File.Exists(erasePath))
            {
                pFWks = GISUtil.GISUtil.CreateFeatureWorkspace(erasePath);
                IDataset pDs = (IDataset)pFWks.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(erasePath));
                pDs.Delete();
            }
            ESRI.ArcGIS.AnalysisTools.Erase erase = new ESRI.ArcGIS.AnalysisTools.Erase(shapefileDLTB, bufferSavefile, erasePath);
            try
            {
                SimplifyDLTB();
                ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
                IGeoProcessorResult result = (IGeoProcessorResult)gp.Execute(erase, null);
            }
            catch (Exception ex)
            {
                LogHelper.LogHelper.WriteLog(typeof(BufferForm), ex.Message + ex.StackTrace);
                throw new Exception("执行擦除有误！ 具体原因是：" + ex.Message + ex.StackTrace);
            }
            finally
            {

            }
            if (pFWks == null)
                pFWks = GISUtil.GISUtil.CreateFeatureWorkspace(erasePath);
            IFeatureClass pEraseFc = pFWks.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(erasePath));
            IFeatureLayer pEraseLayer = new FeatureLayerClass();
            pEraseLayer.Name = pEraseFc.AliasName;
            pEraseLayer.FeatureClass = pEraseFc;
            m_MapControl.Map.AddLayer(pEraseLayer);
            m_MapControl.ActiveView.Extent = m_MapControl.ActiveView.FullExtent;
            m_MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 对地类图斑进行拓扑纠正
        /// </summary>
        public void SimplifyDLTB()
        {
            IFeatureCursor dltbCursor = pDLTBFeatureLayer.FeatureClass.Search(null, true);
            IFeature feature = dltbCursor.NextFeature();
            while (feature != null)
            {
                ITopologicalOperator2 pTopo = (ITopologicalOperator2)feature.ShapeCopy;
                //if (pTopo.IsSimple)
                //{
                //    feature = dltbCursor.NextFeature();
                //    continue;
                //}
                pTopo.IsKnownSimple_2 = false;
                pTopo.Simplify();
                feature.Shape = pTopo as IGeometry;
                feature.Store();
                feature = dltbCursor.NextFeature();
            }
            Marshal.ReleaseComObject(dltbCursor);
        }

        /// <summary>
        /// 根据线宽度作为缓冲距离生成缓冲区
        /// </summary>
        private void BuildBuffer()
        {
            ITable pTable = (ITable)pFeatureLayer;
            if (pTable == null)
                return;

            int wIndex = pTable.FindField(distanceField);
            if (wIndex == -1)
            {
                MessageBox.Show("请选择面转线距离字段!");
                return;
            }
            string shapeName = System.IO.Path.GetFileNameWithoutExtension(bufferSavefile);
            IFeatureWorkspace pFeatureWks = GISUtil.GISUtil.CreateFeatureWorkspace(bufferSavefile);
            IFeatureClass pFeatureClass = null;
            if (System.IO.File.Exists(bufferSavefile))
            {
                pFeatureClass = pFeatureWks.OpenFeatureClass(shapeName);
                IDataset pDs = (IDataset)pFeatureClass;
                pDs.Delete();
            }
            pFeatureClass = pFeatureWks.CreateFeatureClass(shapeName, CreateFeatureField(pTable), null, null, esriFeatureType.esriFTSimple, "SHAPE", "");

            IFeatureCursor pFCursor = pFeatureLayer.FeatureClass.Search(null, true);
            IFeature pF = pFCursor.NextFeature();

            /***test***/
            //buffer = new ESRI.ArcGIS.AnalysisTools.Buffer(pFeatureLayer, System.IO.Path.Combine(tbPath.Text, "bufer.shp"), "5 Meters");
            //IGeoProcessorResult result1 = (IGeoProcessorResult)gp.Execute(buffer, null);

            ITopologicalOperator pTopo = default(ITopologicalOperator);
            while (pF != null)
            {
                double distance = 0.0;
                bool parseSu = double.TryParse(pF.get_Value(wIndex) == null ? "" : pF.get_Value(wIndex).ToString(), out distance);
                if (distance == 0.0 || parseSu == false)
                {
                    pF = pFCursor.NextFeature();
                    continue;
                }

                IFeature pFeature = null;
                try
                {
                    pTopo = (ITopologicalOperator)pF.Shape;
                    pFeature = pFeatureClass.CreateFeature();
                    pFeature.Shape = pTopo.Buffer(distance);
                }
                catch (Exception ex)
                {
                    LogHelper.LogHelper.WriteLog(typeof(BufferForm), ex.Message + ex.StackTrace);
                }

                //字段赋值
                for (int i = 0; i < pFeature.Fields.FieldCount; i++)
                {
                    IField field = pFeature.Fields.Field[i];
                    if (field.Type == esriFieldType.esriFieldTypeGeometry || field.Type == esriFieldType.esriFieldTypeOID)
                        continue;

                    int fieldIndex = pF.Fields.FindField(field.Name);
                    if (fieldIndex == -1)
                        continue;
                    object value = pF.get_Value(fieldIndex);
                    pFeature.set_Value(i, value);
                }
                pFeature.Store();
                pF = pFCursor.NextFeature();
            }
            Marshal.ReleaseComObject(pFCursor);
        }

        private void SetControlDataSource()
        {
            IFeatureWorkspace pFWks = GISUtil.GISUtil.CreateFeatureWorkspace(bufferSavefile);
            pFc = pFWks.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(bufferSavefile));
            ITable pTable = (ITable)pFc;
            if (pTable == null)
                return;

            cbFieldValues.Enabled = true;
            cbFields.Enabled = true;
            btnUnion.Enabled = true;

            IFields pFields = pTable.Fields;
            List<string> fieldName = new List<string>(pFields.FieldCount);
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField field = pFields.Field[i];
                if (field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeGeometry)
                    continue;

                fieldName.Add(field.Name);
            }
            cbFields.DataSource = fieldName;
            cbFields.SelectedIndex = 0;
        }

        /// <summary>
        /// 利用缓冲区图层，遍历每一个要素，找出与其相交且要素代码相同的所有要素，进行合并
        /// </summary>
        private void UnionBuffer(string unionType, string unionValue)
        {
            //IFeatureWorkspace pFWks = GISUtil.GISUtil.CreateFeatureWorkspace(bufferSavefile);
            //IFeatureClass pFc = pFWks.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(bufferSavefile));
            if (string.IsNullOrWhiteSpace(unionType) || string.IsNullOrWhiteSpace(unionValue))
                return;

            IQueryFilter filter = new QueryFilterClass();
            if (currentFieldType == esriFieldType.esriFieldTypeString)
            {
                filter.WhereClause = unionType + " = " + "'" + unionValue + "'";
            }
            else if (currentFieldType == esriFieldType.esriFieldTypeDate)
            {
                //filter.WhereClause = unionType + " = " + "'" + unionValue + "'";
            }
            else
            {
                filter.WhereClause = unionType + " = " + unionValue;
            }
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            IFeatureCursor pFCursor = pFc.Search(filter, true);
            IFeature pFeature = pFCursor.NextFeature();
            while (pFeature != null)
            {
                ArrayList fIDList = new ArrayList();
                IGeometryCollection pGeoCollection = GetIntersectSameTypeFeatures(pFc, pFeature, spatialFilter, filter, unionType, unionValue, ref fIDList);
                if (pGeoCollection.GeometryCount == 1)
                {
                    pFeature = pFCursor.NextFeature();
                    continue;
                }
                try
                {
                    ITopologicalOperator unionGeometry = new PolygonClass();
                    unionGeometry.ConstructUnion((IEnumGeometry)pGeoCollection);
                    ITopologicalOperator2 pTopo2 = (ITopologicalOperator2)unionGeometry; //pFeature.Shape;
                    pTopo2.IsKnownSimple_2 = false;
                    pTopo2.Simplify();
                    pFeature.Shape = unionGeometry as IGeometry;
                    pFeature.Store();
                    //删除参与Union的非自身要素
                    int pFID = (int)pFeature.get_Value(pFeature.Fields.FindField("FID"));
                    for (int i = 0; i < fIDList.Count; i++)
                    {
                        if ((int)fIDList[i] == pFID)
                            continue;
                        IFeature delFeature = pFc.GetFeature((int)fIDList[i]);
                        if (delFeature != null)
                            delFeature.Delete();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogHelper.WriteLog(typeof(BufferForm), ex.Message + ex.StackTrace);
                }
                pFeature = pFCursor.NextFeature();
            }
            Marshal.ReleaseComObject(pFCursor);
        }

        private IGeometryCollection GetIntersectSameTypeFeatures(IFeatureClass pFc, IFeature pFeature, ISpatialFilter spatialFilter, IQueryFilter filter, string unionType, string unionValue, ref ArrayList fIDs)
        {
            object missing = Type.Missing;
            //object fID = pFeature.get_Value(pFeature.Fields.FindField("FID"));
            spatialFilter.GeometryField = "Shape";
            spatialFilter.Geometry = pFeature.Shape;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            if (currentFieldType == esriFieldType.esriFieldTypeString)
            {
                spatialFilter.WhereClause = unionType + " = " + "'" + unionValue + "'";
            }
            else if (currentFieldType == esriFieldType.esriFieldTypeDate)
            {
                //filter.WhereClause = unionType + " = " + "'" + unionValue + "'";
            }
            else
            {
                spatialFilter.WhereClause = unionType + " = " + unionValue;
            }
            filter = spatialFilter;
            IGeometryCollection pGeoCollection = new GeometryBagClass() as IGeometryCollection;
            IFeatureCursor pFeatureCursor = pFc.Search(filter, true);
            IFeature pF = pFeatureCursor.NextFeature();
            while (pF != null)
            {
                fIDs.Add(pF.get_Value(pF.Fields.FindField("FID")));
                pGeoCollection.AddGeometry(pF.ShapeCopy, ref missing, ref missing);
                pF = pFeatureCursor.NextFeature();
            }
            Marshal.ReleaseComObject(pFeatureCursor);
            return pGeoCollection;
        }

        private IFields CreateFeatureField(ITable pTable)
        {
            IFields pFields = new Fields();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

            IField pField = new Field();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "SHAPE";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDefEdit pGeoDef = new GeometryDef() as IGeometryDefEdit;
            pGeoDef.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            //TODO 坐标系问题
            //pGeoDef.SpatialReference_2 = pFeatureLayer.SpatialReference;
            pFieldEdit.GeometryDef_2 = pGeoDef;
            pFieldsEdit.AddField(pField);

            for (int i = 0; i < pTable.Fields.FieldCount; i++)
            {
                IField field = pTable.Fields.Field[i];
                if (field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeGeometry)
                    continue;

                pField = new Field();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = field.Name;  //"YSDM";
                pFieldEdit.Type_2 = field.Type; //esriFieldType.esriFieldTypeString;
                pFieldsEdit.AddField(pField);
            }

            return pFields;
        }

        private string ConvertUnit()
        {
            string unit = "Meters";
            switch (cbUnit.SelectedItem.ToString())
            {
                case "米":
                    unit = "Meters";
                    break;
                case "千米":
                    unit = "Kilometers";
                    break;
                default:
                    break;
            }
            return unit;
        }
    }
}
