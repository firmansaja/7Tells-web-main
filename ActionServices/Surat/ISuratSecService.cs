using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Surat;

namespace ActionServices.Surat
{
    public interface ISuratSecService
    {
        List<SuratSecModel> Get();
        SuratSecModel GetRow(int id);
        int Add(SuratSecModel model);
        int Edit(SuratSecModel model);
        int Delete(int id);
    }
}
