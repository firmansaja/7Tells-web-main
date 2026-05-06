using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public interface IModuleService
    {
        Module GetModuleById(int Id);

        List<Module> GetModules();
        List<Module> GetModuleById(string Username);

        int Update(Module Module);
        int Insert(Module module);
        int Delete(Module module);
    }
}
