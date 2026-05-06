using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BusinessObjects;

namespace DataObjects
{
    public interface ICirculationDao
    {

        CRAkses GetAkses(string UserName);
        int Get_Level(string _menu, string _user);
        DataTable Get_Root_Approve(string _dept);
        List<BO_ECirculation> GetCRApproveHead(string _dept, int level, int div, int dir, string user);
        List<BO_ECirculation> GetCRApproveHeadSearch(CRAkses _CRAkses, DateTime TanggalAwal, DateTime TanggalAhir);
        List<BO_ECirculation_Detail> GetCRApproveDetail(string _id);
        List<BO_ECirculation> GetCRHeadDetail(string Circulation, int DeptGroup);
        List<BO_ECirculation> GetOtherDeptList(string Circulation);
        int Update(BO_ECirculation FData, CRAkses _cRAkses);
        int Reject(BO_ECirculation FData, CRAkses _cRAkses);
        List<BO_ECirculation> GetCRApproveHeadOpinion(CRAkses _cRAkses);
        List<BO_ECirculation> GetDataOpinionSearch(CRAkses _CRAkses, DateTime TanggalAwal, DateTime TanggalAhir);
        List<BO_ECirculation_Detail> GetDataCRApproveDetailOpinion(string _id);

        int UpdateOpinion(BO_ECirculation FData, CRAkses _cRAkses);

    }
}
