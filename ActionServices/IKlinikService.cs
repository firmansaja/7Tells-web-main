using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace ActionServices
{
    public interface IKlinikService
    {
        List<KlinikPeriksa> GetKlinikPeriksa(DateTime TglDari, DateTime TglSampai);
    }
}
