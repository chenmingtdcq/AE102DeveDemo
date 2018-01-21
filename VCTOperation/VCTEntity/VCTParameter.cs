using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCTOperation.VCTEntity
{
    public class VCTParameter : VCTCommonParameter
    {
        /// <summary>
        /// 几何类型
        /// </summary>
        public virtual VCTGeometryType GeometryType { get; set; }

        /// <summary>
        /// 属性表名
        /// </summary>
        public virtual string TableName { get; set; }

        /// <summary>
        /// 字段个数
        /// </summary>
        public virtual int FieldCount { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public virtual List<VCTField> FieldInfos { get; set; }

        /// <summary>
        /// 点的特征类型
        /// </summary>
        public virtual VCTPointType PointType { get; set; }

        /// <summary>
        /// 线的特征类型
        /// </summary>
        public virtual VCTPolylineType PolylineType { get; set; }

        /// <summary>
        /// 线段条数
        /// </summary>
        public virtual int SegmentCount { get; set; }

        /// <summary>
        /// 线段集
        /// </summary>
        public virtual List<VCTSegment> VCTSegments { get; set; }

        /// <summary>
        /// 面特征类型
        /// </summary>
        public virtual VCTPolygonType PolygonType { get; set; }

        /// <summary>
        /// 标识点X坐标
        /// </summary>
        public virtual string BSD_X { get; set; }

        /// <summary>
        /// 标识点Y坐标
        /// </summary>
        public virtual string BSD_Y { get; set; }

        /// <summary>
        /// 圈数
        /// </summary>
        public virtual int CircleCount { get; set; }

        /// <summary>
        /// 面集
        /// </summary>
        public virtual List<VCTPolygon> VCTPolygons { get; set; }

        /// <summary>
        /// 注记
        /// </summary>
        public virtual VCTAnnotation VCTAnnotation { get; set; }

        /// <summary>
        /// 属性集
        /// </summary>
        public virtual string[] Attributes { get; set; } 
    }
}
