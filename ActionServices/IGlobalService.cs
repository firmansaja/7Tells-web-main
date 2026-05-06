using BusinessObjects;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public interface IGlobalService
    {
        int InsertApvHistory(ApproveHistory ApvHistory);
    }
}
