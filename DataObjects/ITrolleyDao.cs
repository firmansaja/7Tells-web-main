using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface ITrolleyDao
    {
        int InsertTrolleyIn();

        DataTable CheckTrolley(string KodeTrolley);

        int SimpanTrolleyInOut(Trolley model, string deptID);
    }
}
