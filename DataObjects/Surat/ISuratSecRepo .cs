using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Surat;

namespace DataObjects.Surat
{
   public interface ISuratSecRepo
    {
        List<SuratSecModel> Get();
        SuratSecModel GetRow(int Id);
        int Add(SuratSecModel model);
        int Edit(SuratSecModel model);
        int Delete(int Id);
    }
}
