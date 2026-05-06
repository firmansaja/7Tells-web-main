using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public interface IComboListService
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
