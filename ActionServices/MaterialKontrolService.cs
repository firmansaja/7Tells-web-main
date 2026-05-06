using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace ActionServices
{
    public class MaterialKontrolService : IMaterialKontrolService 

    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IMaterialKontrolDao MaterialKontrolDao = factory.MaterialKontrolDao;

        public List<BO_Fifo> GetFifo(BO_Fifo_Excell Filter)
        {
            return MaterialKontrolDao.GetFifo(Filter);
        }

        public List<BO_Fifo> GetFifo_DownloadExcel(BO_Fifo_Excell Filter)
        {
            return MaterialKontrolDao.GetFifo_DownloadExcel(Filter);
        }

        //public List<BO_Fifo_Stok> GetLPH_auto(DateTime tgl, string site)
        //{
        //    throw new NotImplementedException();
        //}

        public List<BO_Fifo_Stok> GetStokDetail(string invtid, string site)
        {
            return MaterialKontrolDao.GetStokDetail(invtid, site);
        }

        public List<BO_Fifo_Stok> GetStokFifo(DateTime tgl, string site)
        {
            return MaterialKontrolDao.GetStokFifo(tgl,site);
        }

        public List<BO_Fifo_Stok> GetStokFifo_auto(DateTime tgl, string site)
        {
            return MaterialKontrolDao.GetStokFifo_Auto(tgl, site);
        }

        //static readonly IDaoFactory factory = DaoFactories.GetFactory();
        //static readonly IPRDao moduleDao = factory.PRDao;
        public List<BO_MaterialKontrol> GetStokMaterial(string Year, string Account, int month)
        {
            return MaterialKontrolDao.GetStokMaterial(Year, Account,month);
        }

        public List<BO_MaterialKontrol> GetStokMaterialByAccount(string Year, string Site)
        {
            return MaterialKontrolDao.GetStokMaterialByAccount(Year, "");
        }

        public List<BO_MaterialKontrol_ByAccount> GetStokMaterial_ByAccountList(string Year)
        {
            return MaterialKontrolDao.GetStokMaterialByAccountList(Year);
        }

        public List<BO_HasilProduksi> GetLPH_auto(DateTime tgl, string site, string area, string noMesin)
        {
            return MaterialKontrolDao.GetLPH_auto(tgl, site, area, noMesin);

        }

        public List<BO_NoMesin> GetNoMesin()
        {
            return MaterialKontrolDao.GetNoMesin();

        }

        public List<BO_HasilProduksiMesin> GetLPHByMesin(DateTime tgl, string noMesin)
        {
            return MaterialKontrolDao.GetLPHByMesin(tgl, noMesin);

        }
    }
}
