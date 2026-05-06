using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace ActionServices
{
    public class ComboListService : IComboListService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IComboList comboList = factory.ComboDao;

        public List<ComboList> GetModuleParent()
        {
            return comboList.GetModuleParent();
        }

        public List<ComboList> GetRole()
        {
            return comboList.GetRole();
        }

        public List<ComboList> GetDeptPR()
        {
            return comboList.GetDeptPR();
        }

        public List<ComboList> GetSubAcct(string UserName)
        {
            return comboList.GetSubAcct(UserName);
        }

        public List<ComboList> GetSupplier()
        {
            return comboList.GetSupplier();
        }

        public List<ComboList> GetSubMenuPR(string Akses)
        {
            return comboList.GetSubMenuPR(Akses);
        }
        public List<ComboList> GetKategori()
        {
            return comboList.GetKategori();
        }

        public List<ComboList> GetDept()
        {
            return comboList.GetDept();
        }
    }
}
