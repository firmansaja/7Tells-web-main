using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Surat;
using BusinessObjects;

namespace ActionServices.Surat
{
    public interface ISuratMasukService
    {
        List<SuratMasukModel> GetList();
        List<SuratMasukModel> Get(string dept, int flag, string status);
        SuratMasukModel GetRow(int Id);
        int Add(SuratMasukModel model, string dept);
        int Edit(SuratMasukModel model);
        int Delete(int Id);
        string GetNoUrutAuto(string dept, int flag);
    }
}
