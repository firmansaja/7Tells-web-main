using BusinessObjects;
using System.Collections.Generic;

namespace DataObjects
{
    public interface IComboList
    {
        List<ComboList> GetRole();

        List<ComboList> GetModuleParent();
        List<ComboList> GetDeptPR();
        List<ComboList> GetSubAcct(string UserName);
        List<ComboList> GetSupplier();
        List<ComboList> GetSubMenuPR(string Akses);
        List<ComboList> GetKategori();
        List<ComboList> GetDept();
    }
}