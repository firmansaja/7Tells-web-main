using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IMaterialKontrolDao
    {
        List<BO_MaterialKontrol> GetStokMaterial(string Year, string account, int month);
        List<BO_MaterialKontrol> GetStokMaterialByAccount(string Year, string Site);
        List<BO_MaterialKontrol_ByAccount> GetStokMaterialByAccountList(string Year);
        List<BO_Fifo> GetFifo(BO_Fifo_Excell Filter);
        List<BO_Fifo> GetFifo_DownloadExcel(BO_Fifo_Excell Filter);
        List<BO_Fifo_Stok> GetStokFifo(DateTime tgl, string site);
        List<BO_Fifo_Stok> GetStokFifo_Auto(DateTime tgl, string site);
        List<BO_Fifo_Stok> GetStokDetail(string invtid, string site);
        List<BO_HasilProduksi> GetLPH_auto(DateTime tgl, string site, string area, string noMesin);
        List<BO_NoMesin> GetNoMesin();
        List<BO_HasilProduksiMesin> GetLPHByMesin(DateTime tgl, string noMesin);
    }
}
