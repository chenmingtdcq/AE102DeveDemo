using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCTOperation.VCTEntity
{
    public class VCTCommonParameter
    {
        /// <summary>
        /// 要素类型编码
        /// </summary>
        public virtual string FeatureTypeCode { get; set; }
        /// <summary>
        /// 要素类型名称
        /// </summary>
        public virtual string FeatureTypeName { get; set; }
        /// <summary>
        /// 对象标识码
        /// </summary>
        public virtual int BSM { get; set; }
        /// <summary>
        /// 图形表现编码
        /// </summary>
        public virtual string TXBXBM { get; set; }

        /// <summary>
        /// 点数
        /// </summary>
        public virtual int PointCount { get; set; }

        /// <summary>
        /// 空间数据
        /// </summary>
        public virtual ESRI.ArcGIS.Geometry.IGeometry Shape { get; set; }
    }
}
