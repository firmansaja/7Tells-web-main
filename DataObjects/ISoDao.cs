using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface ISoDao
    {
        List<BO_So> GetOs_SO(string CustOrdNbr);
        List<BO_So> GetByOrdeNbr(string OrdNbr);
    }
}
