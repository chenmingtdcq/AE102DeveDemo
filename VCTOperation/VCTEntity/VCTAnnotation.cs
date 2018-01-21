using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCTOperation.VCTEntity
{
    public class VCTAnnotation : VCTCommonParameter
    {
        /// <summary>
        /// 注记类型
        /// </summary>
        public virtual VCTAnnotationType AnnotationType { get; set; }

        /// <summary>
        /// 注记内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 注记集
        /// </summary>
        public virtual List<Annotation> Annotations { get; set; }
    }

    public class Annotation
    {
        public string X { get; set; }
        public string Y { get; set; }
        public double Angle { get; set; }
    }
}
