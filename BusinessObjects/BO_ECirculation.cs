using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
   public class BO_ECirculation
    {

        public string CekStatus { get; set; }
        public string CirculationNo { get; set; }
        public DateTime Requirement { get; set; }
        public string Dept { get; set; }
        public string Status { get; set; }
        public Boolean Requester { get; set; }
        public Boolean DeptHead { get; set; }
        public Boolean DivHead { get; set; }
        public Boolean OtherDept { get; set; }
        public Boolean Accounting { get; set; }
        public Boolean Purch { get; set; }
        public double Amount { get; set; }
        public int CurrentLevel { get; set; }
        public int LevelUser { get; set; }
        public int DeptGroup { get; set; }


        public string Other_DeptHead { get; set; }
        public string Other_Opinion { get; set; }
        public string Other_Deptid { get; set; }
        public Boolean Other_Approve { get; set; }
        public DateTime Other_date { get; set; }
        public Boolean Approve_Opinion { get; set; }




        //Approve History
        public string HisMenuCode { get; set; }
        public string HisStatus { get; set; }
        public int HisisActive { get; set; }
        public int HisLevel { get; set; }



       // public string CirculationNo { get; set; }
        public string CRType { get; set; }
        public string POType { get; set; }
        public string NameofItem { get; set; }
        public string Reason { get; set; }
        public string BudgetType { get; set; }
        public string PONumber { get; set; }
        public string ParentCR { get; set; }
        public string Spesification { get; set; }
        public string Customer { get; set; }
        public DateTime Tanggal { get; set; }
        public int id { get; set; }
        public string Opinion { get; set; }
        // public int DeptGroup { get; set; }


        // public string Dept { get; set; }
        public DateTime Date { get; set; }
        //public string Opinion { get; set; }
        public Boolean Approve { get; set; }

        public List<BO_ECirculation_Detail> CRDetail { get; set; }

    }

    public class BO_ECirculation_Detail
    {
        public string CirculationNo { get; set; }
        public string NameOfGood { get; set; }
        public string Spesification { get; set; }
        public string Model { get; set; }
        public string SalesType { get; set; }
        public string Remark { get; set; }
        public double Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public string Curr { get; set; }
        public double Rate { get; set; }
        public double TotalIDR { get; set; }
        public string Account { get; set; }
        public string AccountName { get; set; }
        public string Category { get; set; }
        public string Note { get; set; }
        public int kode { get; set; }
        public int Check { get; set; }
        public string DeptGroup { get; set; }
    }


    public  class CRAkses
    {
        public  string UserGroupCode { get; set; }
        public  string UserName { get; set; }
        public  int Level { get; set; }
        public  int Division { get; set; }
        public  int Director { get; set; }
        public  int DeptGroup { get; set; }
    }

}
