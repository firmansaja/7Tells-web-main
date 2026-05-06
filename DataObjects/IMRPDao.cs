using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IMRPDao
    {
        List<MRPForecastCust> GetListForecatCust(string Year);

        List<MRPForecastModel> GetListForecatModel(string Year, string CustID);

        List<MRPForecastDetail> GetListForecatDetail(string Year, string CustID, string Model);

        List<MRPBoMCalculate> GetListBoMCalculate(string Year, string Site);

        List<MRPBoMCalculateDetail> GetListBoMCalculateDetail(string Year, string Site, string MaterialID);
    }
}
