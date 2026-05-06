using DataObjects.Surat;

namespace DataObjects
{
    public class DaoFactory : IDaoFactory
    {
        public IUserDao UserDao { get { return new UserDao(); } }
        public IComboList ComboDao { get { return new ComboListDao(); } }
        public IModuleDao ModuleDao { get { return new ModuleDao(); } }
        public IEbudgetDao BudgetDao { get { return new EBudgetDao(); } }
        public IPRDao PRDao { get { return new PRDao(); } }

        public IEbudgetPLDao BudgetPLDao { get { return new EbudgetPLDao(); } }

        public IPODao PODao { get { return new PODao(); } }

        public ICirculationDao CRDao { get { return new CirculationDao(); } }

        public IEMobilDao MobilDao { get { return new EMobilDao(); } }

        public IGlobalDao GlobalDao { get { return new GlobalDao(); } }

        public IKlinikDao KlinikDao { get { return new KlinikDao(); } }

        public IMRPDao MRPDao { get { return new MRPDao(); } }

        public IMaterialKontrolDao MaterialKontrolDao { get { return new MaterialKontrolDao(); } }

        public ISoDao SoDao { get { return new SoDao(); } }

        public ITrolleyDao TrolleyDao { get { return new TrolleyDao(); } }

        public ITrollyDao TrollyDao { get { return new TrollyDao(); } }

        public ITrollyDepoDao TrollyDepoDao { get { return new TrollyDepoDao(); } }

        public ILineStoreDao LineStoreDao { get { return new LineStoreDao(); } }

        public IKategoriSuratRepo KategoriSurat { get { return new KategoriSuratRepo(); } }

        public ISuratMasukRepo SuratMasuk { get { return new SuratMasukRepo(); } }

        public IBuktiMuatDao BuktiMuat { get { return new BuktiMuatDao(); } }

        public ISuratSecRepo SuratSec { get { return new SuratSecRepo(); } }

        public ISuratKeluarRepo SuratKeluar { get { return new SuratKeluarRepo(); } }

        public IPPAPDao PPAPDao { get { return new PPAPDao(); } }
    }
}