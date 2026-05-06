using DataObjects.Surat;

namespace DataObjects
{
    public interface IDaoFactory
    {
        IUserDao UserDao { get; }
        IComboList ComboDao { get; }
        IModuleDao ModuleDao { get; }
        IEbudgetDao BudgetDao { get; }
        IEbudgetPLDao BudgetPLDao { get; }
        IPRDao PRDao { get; }
        IPODao PODao { get; }
        ICirculationDao CRDao { get; }
        IEMobilDao MobilDao { get; }
        IGlobalDao GlobalDao { get; }
        IKlinikDao KlinikDao { get; }
        IMRPDao MRPDao { get; }
        IMaterialKontrolDao MaterialKontrolDao { get; }
        ISoDao SoDao { get; }
        ITrolleyDao TrolleyDao { get; }
        ITrollyDao TrollyDao { get; }
        ITrollyDepoDao TrollyDepoDao { get; }
        ILineStoreDao LineStoreDao { get; }
        IKategoriSuratRepo KategoriSurat { get; }
        ISuratMasukRepo SuratMasuk { get; }
        IBuktiMuatDao BuktiMuat { get; }
        IPPAPDao PPAPDao { get; }
        ISuratSecRepo SuratSec { get; }
        ISuratKeluarRepo SuratKeluar { get; }



    }
}