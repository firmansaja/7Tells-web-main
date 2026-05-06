using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace ActionServices
{
    public class CirculationServices : ICirculationService
    {

        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly ICirculationDao moduleDao = factory.CRDao;

        public CRAkses GetAkses(string UserName)
        {
            return moduleDao.GetAkses(UserName);
        }

        public List<BO_ECirculation_Detail> GetCRApproveDetail(string _id)
        {
            return moduleDao.GetCRApproveDetail(_id);
        }

        public List<BO_ECirculation> GetCRApproveHead(string _dept, int level, int div, int dir, string user)
        {
            return moduleDao.GetCRApproveHead(_dept,level,div,dir,user);
        }

        public List<BO_ECirculation> GetCRApproveHeadOpinion(CRAkses _cRAkses)
        {
            return moduleDao.GetCRApproveHeadOpinion(_cRAkses);
        }

        public List<BO_ECirculation> GetCRApproveHeadSearch(CRAkses _CRAkses, DateTime TanggalAwal, DateTime TanggalAhir)
        {
            return moduleDao.GetCRApproveHeadSearch(_CRAkses,TanggalAwal, TanggalAhir);
        }

        public List<BO_ECirculation> GetCRHeadDetail(string Circulation, int DeptGroup)
        {
            return moduleDao.GetCRHeadDetail(Circulation, DeptGroup);
        }

        public List<BO_ECirculation_Detail> GetDataCRApproveDetailOpinion(string _id)
        {
            return moduleDao.GetDataCRApproveDetailOpinion(_id);
        }

        public List<BO_ECirculation> GetDataOpinionSearch(CRAkses _CRAkses, DateTime TanggalAwal, DateTime TanggalAhir)
        {
            return moduleDao.GetDataOpinionSearch(_CRAkses, TanggalAwal,TanggalAhir);
        }

        public List<BO_ECirculation> GetOtherDeptList(string Circulation)
        {
            return moduleDao.GetOtherDeptList(Circulation);
        }

        public int Get_Level(string _menu, string _user)
        {
            return moduleDao.Get_Level(_menu, _user);
        }

        public DataTable Get_Root_Approve(string _dept)
        {
            return moduleDao.Get_Root_Approve(_dept);
        }

        public int Update(BO_ECirculation FData, CRAkses _cRAkses)
        {
            return moduleDao.Update(FData, _cRAkses);
        }

        public int Reject(BO_ECirculation FData, CRAkses _cRAkses)
        {
            return moduleDao.Reject(FData, _cRAkses);
        }

        public int UpdateOpinion(BO_ECirculation FData, CRAkses _cRAkses)
        {
            return moduleDao.UpdateOpinion(FData, _cRAkses);
        }
    }
}
