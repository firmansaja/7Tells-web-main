using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace ActionServices
{
    public class MRPService : IMRPService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IMRPDao mrpDao = factory.MRPDao;

        public List<MRPForecastCust> GetListForecatCust(string Year)
        {
            return mrpDao.GetListForecatCust(Year);
        }

        public List<MRPForecastModel> GetListForecatModel(string Year, string CustID)
        {
            return mrpDao.GetListForecatModel(Year, CustID);
        }

        public List<MRPForecastDetail> GetListForecatDetail(string Year, string CustID, string Model)
        {
            return mrpDao.GetListForecatDetail(Year, CustID, Model);
        }

        public List<MRPBoMCalculate> GetListBoMCalculate(string Year, string Site)
        {
            return mrpDao.GetListBoMCalculate(Year, Site);
        }

        public List<MRPBoMCalculateDetail> GetListBoMCalculateDetail(string Year, string Site, string MaterialID)
        {
            return mrpDao.GetListBoMCalculateDetail(Year, Site, MaterialID);
        }
    }
}
