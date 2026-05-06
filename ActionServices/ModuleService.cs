using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace ActionServices
{
    public class ModuleService : IModuleService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IModuleDao moduleDao = factory.ModuleDao;
        public int Delete(Module module)
        {
            return moduleDao.Delete(module);
        }

        public Module GetModuleById(int Id)
        {
            return moduleDao.GetModuleById(Id);
        }

        public List<Module> GetModuleById(string Username)
        {
            throw new NotImplementedException();
        }

        public List<Module> GetModules()
        {
            return moduleDao.GetModules();
        }

        public int Insert(Module module)
        {
            return moduleDao.Insert(module);
        }

        public int Update(Module Module)
        {
            throw new NotImplementedException();
        }
    }
}
