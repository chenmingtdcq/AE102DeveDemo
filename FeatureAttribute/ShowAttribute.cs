using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAttribute
{
    public class ShowAttribute : BaseCommand
    {
        private IHookHelper m_HookHelper;
        private IMapControl3 m_MapControl;
        private IFeatureLayer pFeatureLayer;

        public ShowAttribute()
        {
            this.m_caption = "属性表";
        }

        public override void OnCreate(object hook)
        {
            if (hook == null) return;

            if (m_HookHelper == null)
                m_HookHelper = new HookHelperClass();
            m_HookHelper.Hook = hook;
            m_MapControl = (IMapControl3)hook;
            pFeatureLayer = (IFeatureLayer)m_MapControl.CustomProperty;
        }

        public override void OnClick()
        {
            //获取属性表
            AttributeTable at = new AttributeTable(GetAttributeTable());
            at.Show();
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
                            row[i] = GetShapeType();
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
