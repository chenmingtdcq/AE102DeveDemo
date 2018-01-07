using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        public BufferForm(IHookHelper hookHelper)
        {
            InitializeComponent();

            this.m_HookHelper = hookHelper;
            tbDistance.Text = "缓冲距离根据线宽度确定.";
            tbDistance.Enabled = false;
            cbUnit.DataSource = new List<string>() { "米", "千米" };
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dg = new FolderBrowserDialog();
            dg.Description = "请选择文件夹";
            if (dg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbPath.Text = dg.SelectedPath;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (m_HookHelper == null || m_HookHelper.Hook == null)
                return;

            m_MapControl = m_HookHelper.Hook as IMapControl3;
            pFeatureLayer = m_MapControl.CustomProperty as IFeatureLayer;
            if (pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
            {
                MessageBox.Show("请选择线图层!");
                return;
            }

            //获取属性表
            //AttributeTable at = new AttributeTable(GetAttributeTable());
            //at.Show();

            BuildBuffer();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 根据线宽度作为缓冲距离生成缓冲区
        /// </summary>
        private void BuildBuffer()
        {
            ITable pTable = (ITable)pFeatureLayer;
            if (pTable == null)
                return;

            int wIndex = pTable.FindField("Width");
            if (wIndex == -1)
            {
                MessageBox.Show("没有宽度属性!");
                return;
            }

            //IFeatureCursor pFCursor = pFeatureLayer.FeatureClass.Search(null, false);
            //IDataStatistics pData = new DataStatisticsClass();
            //pData.Field = "Width";
            //pData.Cursor = (ICursor)pFCursor;
            //IEnumerator pEnumVar = pData.UniqueValues;
            //pEnumVar.Reset();
            //string[] strValue = new string[pData.UniqueValueCount];
            //int i = 0;
            //while (pEnumVar.MoveNext())
            //{
            //    strValue[++i] = pEnumVar.Current.ToString();
            //}

            //HashSet<string> hsWidth = new HashSet<string>();
            //IQueryFilter filter = new QueryFilterClass();
            //filter.SubFields = "Width";
            //ICursor pCursor = pTable.Search(filter, false);
            //IRow pRow = pCursor.NextRow();
            //while (pRow != null)
            //{
            //    hsWidth.Add(pRow.Value[wIndex].ToString());
            //    pRow = pCursor.NextRow();
            //}
            //ITopologicalOperator pTopo = default(ITopologicalOperator);
            //IGeometryCollection pGeoCol = new GeometryBag() as IGeometryCollection;
            //string[] arrayWidth = hsWidth.ToArray();
            //for (int i = 0; i < arrayWidth.Length; i++)
            //{
            //    double ds = double.Parse(arrayWidth[i]);
            //    filter.WhereClause = string.Format("Width = {0}", ds);
            //    IFeatureCursor pFCursor = pFeatureLayer.FeatureClass.Search(filter, false);
            //    IFeature pF = pFCursor.NextFeature();
            //    while (pF != null)
            //    {
            //        //var value = pF.get_Value(wIndex);
            //        pTopo = (ITopologicalOperator)pF.Shape;
            //        pGeoCol.AddGeometry(pTopo.Buffer(ds));
            //        pF = pFCursor.NextFeature();
            //    }
            //}

            ITopologicalOperator pTopo = default(ITopologicalOperator);
            IGeometryCollection pGeoCol = new GeometryBag() as IGeometryCollection;
            IFeatureCursor pFCursor = pFeatureLayer.FeatureClass.Search(null, false);
            IFeature pF = pFCursor.NextFeature();
            while (pF != null)
            {
                object value = pF.get_Value(wIndex);
                pTopo = (ITopologicalOperator)pF.Shape;
                pGeoCol.AddGeometry(pTopo.Buffer((double)value));
                pF = pFCursor.NextFeature();
            }

            
        }

        private DataTable GetAttributeTable()
        {
            ITable pTable = null;
            pTable = (ITable)pFeatureLayer;
            if (pTable == null)
                return null;

            DataTable dataTable = new DataTable();
            IFields fields = pTable.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                AddTableColumnsByFields(ref dataTable, fields.Field[i]);
            }
            ICursor cursor = pTable.Search(null, false);
            IRow iRow = cursor.NextRow();
            while (iRow != null)
            {
                DataRow row = dataTable.NewRow();
                for (int i = 0; i < iRow.Fields.FieldCount; i++)
                {
                    switch (iRow.Fields.Field[i].Type)
                    {
                        case esriFieldType.esriFieldTypeBlob:
                            row[i] = "Element";
                            break;
                        case esriFieldType.esriFieldTypeGeometry:
                            row[i] = "线";  //GetShapeType();
                            break;
                        default:
                            row[i] = iRow.Value[i];
                            break;
                    }
                }
                dataTable.Rows.Add(row);
                iRow = cursor.NextRow();
            }
            return dataTable;
        }

        private void AddTableColumnsByFields(ref DataTable dataTable, IField field)
        {
            DataColumn column = new DataColumn();
            column.AllowDBNull = field.IsNullable;
            column.ColumnName = field.Name;
            column.Caption = field.AliasName;
            //column.DataType = Type.GetType(ConvertFieldType(field));
            column.DefaultValue = field.DefaultValue;
            dataTable.Columns.Add(column);
        }

        private string ConvertFieldType(IField field)
        {
            string fieldType = "";
            switch (field.Type)
            {
                case esriFieldType.esriFieldTypeBlob:
                    fieldType = "Blob";
                    break;
                case esriFieldType.esriFieldTypeDate:
                    fieldType = "Date";
                    break;
                case esriFieldType.esriFieldTypeDouble:
                    fieldType = "Double";
                    break;
                case esriFieldType.esriFieldTypeGUID:
                    fieldType = "Guid";
                    break;
                case esriFieldType.esriFieldTypeGeometry:
                    fieldType = "Geometry";
                    break;
                case esriFieldType.esriFieldTypeGlobalID:
                    fieldType = "GlobalID";
                    break;
                case esriFieldType.esriFieldTypeInteger:
                    fieldType = "Integer";
                    break;
                case esriFieldType.esriFieldTypeOID:
                    fieldType = "OID";
                    break;
                case esriFieldType.esriFieldTypeRaster:
                    fieldType = "Raster";
                    break;
                case esriFieldType.esriFieldTypeSingle:
                    fieldType = "Single";
                    break;
                case esriFieldType.esriFieldTypeSmallInteger:
                    fieldType = "SmallInteger";
                    break;
                case esriFieldType.esriFieldTypeString:
                    fieldType = "String";
                    break;
                case esriFieldType.esriFieldTypeXML:
                    fieldType = "XML";
                    break;
                default:
                    break;
            }
            return fieldType;
        }

        private string GetShapeType()
        {
            string shapeType = "";
            switch (pFeatureLayer.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryAny:
                    break;
                case esriGeometryType.esriGeometryBag:
                    break;
                case esriGeometryType.esriGeometryBezier3Curve:
                    break;
                case esriGeometryType.esriGeometryCircularArc:
                    break;
                case esriGeometryType.esriGeometryEllipticArc:
                    break;
                case esriGeometryType.esriGeometryEnvelope:
                    break;
                case esriGeometryType.esriGeometryLine:
                    break;
                case esriGeometryType.esriGeometryMultiPatch:
                    break;
                case esriGeometryType.esriGeometryMultipoint:
                    break;
                case esriGeometryType.esriGeometryNull:
                    break;
                case esriGeometryType.esriGeometryPath:
                    break;
                case esriGeometryType.esriGeometryPoint:
                    shapeType = "点";
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    shapeType = "面";
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    shapeType = "线";
                    break;
                case esriGeometryType.esriGeometryRay:
                    break;
                case esriGeometryType.esriGeometryRing:
                    break;
                case esriGeometryType.esriGeometrySphere:
                    break;
                case esriGeometryType.esriGeometryTriangleFan:
                    break;
                case esriGeometryType.esriGeometryTriangleStrip:
                    break;
                case esriGeometryType.esriGeometryTriangles:
                    break;
                default:
                    break;
            }
            return shapeType;
        }
    }
}
