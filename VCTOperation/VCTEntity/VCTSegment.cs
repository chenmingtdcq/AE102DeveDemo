using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCTOperation.VCTEntity
{
    public class VCTSegment : VCTCommonParameter
    {
        /// <summary>
        /// 线段类型
        /// </summary>
        public virtual VCTSegmentType SegmentType { get; set; }
    }
}
