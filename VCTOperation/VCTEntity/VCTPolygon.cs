using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCTOperation.VCTEntity
{
    public class VCTPolygon : VCTCommonParameter
    {
        /// <summary>
        /// 面几何形状
        /// </summary>
        public virtual VCTPolygonGeometryType PolygonGeometryType { get; set; }
    }
}
