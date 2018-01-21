using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCTOperation.VCTEntity;

namespace VCTOperation.VCTInterface
{
    public interface IVCTOperation
    {
        IList<VCTParameter> GetVCT(string paragraph);

        string ExportVCT(IList<VCTParameter> entities);
    }
}
