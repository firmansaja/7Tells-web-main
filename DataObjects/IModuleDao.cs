using BusinessObjects;
using System.Collections.Generic;

namespace DataObjects
{
    public interface IModuleDao
    {
        Module GetModuleById(int Id);

        List<Module> GetModules();

        List<Module> GetModuleById(string Username);

        int Update(Module Module);

        int Insert(Module module);

        int Delete(Module module);
    }
}