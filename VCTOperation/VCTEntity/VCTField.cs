using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCTOperation.VCTEntity
{
    public class VCTField
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public VCTFieldType FieldType { get; set; }

        /// <summary>
        /// 字段宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 字段精度
        /// </summary>
        public int Precision { get; set; }
    }
}
