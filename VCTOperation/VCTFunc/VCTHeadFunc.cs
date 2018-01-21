using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VCTOperation.VCTFunc
{
    /// <summary>
    /// 文件头
    /// </summary>
    public class VCTHeadFunc
    {
        public virtual string DataMark { get; set; }
        public virtual string Version { get; set; }
        public virtual string CoordinateSystemType { get; set; }
        public virtual int Dim { get; set; }
        public virtual string XAxisDirection { get; set; }
        public virtual string YAxisDirection { get; set; }
        public virtual string XYUnit { get; set; }
        public virtual string ZUnit { get; set; }
        public virtual string Spheriod { get; set; }
        public virtual string PrimeMeridian { get; set; }
        public virtual string Projection { get; set; }
        public virtual string Paramters { get; set; }
        public virtual string VerticalDatum { get; set; }
        public virtual string TemporalReferenceSystem { get; set; }
        public virtual string ExtentMin { get; set; }
        public virtual string ExtentMax { get; set; }
        public virtual int MapScale { get; set; }
        public virtual string Offset { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Separator { get; set; }

        /// <summary>
        /// 读取文件头
        /// </summary>
        public virtual void SetParameter(string header)
        {
            if (header.IsNullOrWhiteSpace())
                return;
            string[] splitHeader = header.Split(new string[] { VCTConst.CR }, StringSplitOptions.None);
            splitHeader.ToList().ForEach(parameter =>
            {
                if (parameter.IsNullOrWhiteSpace())
                    return;
                if (parameter == VCTEnum.HeadBegin.ToString() || parameter == VCTEnum.HeadEnd.ToString())
                    return;
                var splitPara = parameter.Split(new string[] { VCTConst.Colon }, StringSplitOptions.None);
                if (splitPara == null || splitPara.Length != 2)
                    return;
                PropertyInfo propertyInfo = typeof(VCTHeadFunc).GetProperties().Where(p => p.Name == splitPara[0]).FirstOrDefault();
                if (propertyInfo == null)
                    return;
                if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(this, splitPara[1].Trim().ToInt32());
                }
                else if (propertyInfo.PropertyType == typeof(DateTime))
                {
                    propertyInfo.SetValue(this, splitPara[1].Trim().ToDateTime());
                }
                else
                {
                    propertyInfo.SetValue(this, splitPara[1].Trim());
                }
            });
        }

        /// <summary>
        /// 导出文件头
        /// </summary>
        public virtual string ExportVCTHeader()
        {
            ///TODO
            return null;
        }
    }
}
