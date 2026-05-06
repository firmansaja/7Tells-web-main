using BusinessObjects;
using DataObjects.Helpers;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataObjects
{
    public class CirculationDao : ICirculationDao
    {
        static Db db = new Db();
        public CRAkses GetAkses(string UserName)
        {
            try
            {
                string Query = @"Select Distinct A.Username
				                    ,A.UserGroupCode
				                    ,B.div_Id
                                    ,B.director_Id
				                    ,B.dept_group
				                    ,isnull(C.LevelApprove,0) as [Level]
                                from S_User A
                                inner join M_Root_Approval B on A.UserGroupCode =B.DeptID
                                left join M_Approve C on A.Username = C.UserName
                                where A.Username = '" + UserName + "' and C.MenuCode = 'CIRCULATION'";
                return db.Read(Query, CommandType.Text, MakeAkses).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_ECirculation> GetCRApproveHead(string _dept, int level, int div, int dir, string user)
        {

            try
            {
                string P1 = "Submit";
                string P2 = "Approve 1";
                string P3 = "Approve 2";
                string P4 = "Other Dept";

                List<BO_ECirculation> Result = new List<BO_ECirculation>();

                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "User", Value = user });
                Params.Add(new SqlParameter() { ParameterName = "P1", Value = P1 });
                Params.Add(new SqlParameter() { ParameterName = "P2", Value = P2 });
                Params.Add(new SqlParameter() { ParameterName = "P3", Value = P3 });
                Params.Add(new SqlParameter() { ParameterName = "P4", Value = P4 });
                Params.Add(new SqlParameter() { ParameterName = "Level", Value = level });
                Params.Add(new SqlParameter() { ParameterName = "div_id", Value = div });
                Params.Add(new SqlParameter() { ParameterName = "director_id", Value = dir });

                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("CR_Get_Approve", CommandType.StoredProcedure, Params, db.connectionString);
                foreach (DataRow dr in dt.Rows)


                {
                    Result.Add(
                        new BO_ECirculation
                        {
                            CirculationNo = Convert.ToString(dr["Circulation No"]),
                            Requirement = Convert.ToDateTime(dr["Requirement Date"]),
                            Dept = Convert.ToString(dr["Dept"]),
                            Status = Convert.ToString(dr["Status"]),
                            Requester = Convert.ToBoolean(dr["Requester"]),
                            DeptHead = Convert.ToBoolean(dr["Dept Head"]),
                            DivHead = Convert.ToBoolean(dr["Div Head"]),
                            OtherDept = Convert.ToBoolean(dr["Other Dept"]),
                            Accounting = Convert.ToBoolean(dr["Accounting"]),
                            Purch = Convert.ToBoolean(dr["Purch"]),
                            Amount = Convert.ToDouble(dr["Amount"]),
                            NameofItem = Convert.ToString(dr["Name Of Item"]),
                            //LevelUser = UserHelper.LevelHistory,

                        });
                }
                return Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_ECirculation> GetCRApproveHeadSearch(CRAkses _CRAkses, DateTime TanggalAwal, DateTime TanggalAhir)
        {
            try
            {

                List<BO_ECirculation> Result = new List<BO_ECirculation>();

                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "Dept", Value = _CRAkses.UserGroupCode });
                Params.Add(new SqlParameter() { ParameterName = "pDate1", Value = TanggalAwal });
                Params.Add(new SqlParameter() { ParameterName = "pDate2", Value = TanggalAhir });
                Params.Add(new SqlParameter() { ParameterName = "Level", Value = _CRAkses.Level });
                Params.Add(new SqlParameter() { ParameterName = "div_id", Value = _CRAkses.Division });
                Params.Add(new SqlParameter() { ParameterName = "director_id", Value = _CRAkses.Director });
                Params.Add(new SqlParameter() { ParameterName = "User", Value = _CRAkses.UserName });

                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("CR_Get_Approve_Search", CommandType.StoredProcedure, Params, db.connectionString);
                foreach (DataRow dr in dt.Rows)


                {
                    Result.Add(
                        new BO_ECirculation
                        {
                            CirculationNo = Convert.ToString(dr["Circulation No"]),
                            Requirement = Convert.ToDateTime(dr["Requirement Date"]),
                            Dept = Convert.ToString(dr["Dept"]),
                            Status = Convert.ToString(dr["Status"]),
                            Requester = Convert.ToBoolean(dr["Requester"]),
                            DeptHead = Convert.ToBoolean(dr["Dept Head"]),
                            DivHead = Convert.ToBoolean(dr["Div Head"]),
                            OtherDept = Convert.ToBoolean(dr["Other Dept"]),
                            Accounting = Convert.ToBoolean(dr["Accounting"]),
                            Purch = Convert.ToBoolean(dr["Purch"]),
                            Amount = Convert.ToDouble(dr["Amount"]),
                            //LevelUser = UserHelper.LevelHistory,

                        });
                }
                return Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Get_Level(string _menu, string _user)
        {
            try
            {
                int _result = 0;
                //string Query = @"M_Approve_GetUserLevel";
                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "form", Value = _menu });
                Params.Add(new SqlParameter() { ParameterName = "username", Value = _user });

                //param[0] = new SqlParameter("@form", SqlDbType.VarChar);
                //param[1] = new SqlParameter("@username", SqlDbType.VarChar);

                //param[0].Value = _menu;
                //param[1].Value = _user;
                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("M_Approve_GetUserLevel", CommandType.StoredProcedure, Params, db.connectionString);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        _result = Convert.ToInt16(dr["Level"]);
                    }
                }

                return _result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Get_Root_Approve(string _dept)
        {
            try
            {
                DataTable dt = new DataTable();
                //string Query = @"Get_Root_Approve";
                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "Dept", Value = _dept });

                dt = SQLHelpers.GetDataTableByParam("Get_Root_Approve", CommandType.StoredProcedure, Params, db.connectionString);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BO_ECirculation_Detail> GetCRApproveDetail(string _id)
        {
            try
            {

                List<BO_ECirculation_Detail> Result = new List<BO_ECirculation_Detail>();

                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "CirculationNo", Value = _id });

                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("CR_Get_Detail_DescriptionOfCost", CommandType.StoredProcedure, Params, db.connectionString);
                foreach (DataRow dr in dt.Rows)
                {
                    Result.Add(
                        new BO_ECirculation_Detail
                        {
                            NameOfGood = Convert.ToString(dr["Name Of Goods"]),
                            Spesification = Convert.ToString(dr["Spesification"]),
                            Model = Convert.ToString(dr["Model"]),
                            SalesType = Convert.ToString(dr["Sales Type"]),
                            Remark = Convert.ToString(dr["Remark"]),
                            Qty = Convert.ToDouble(dr["Qty"]),
                            Price = Convert.ToDouble(dr["Price"]),
                            Amount = Convert.ToDouble(dr["Total Amount Currency"]),
                            Curr = Convert.ToString(dr["Curr"]),
                            Rate = Convert.ToDouble(dr["Rate"]),
                            TotalIDR = Convert.ToDouble(dr["Total IDR"]),
                            Account = Convert.ToString(dr["Account"]),
                            AccountName = Convert.ToString(dr["Account Name"]),
                            Category = Convert.ToString(dr["Category"]),
                            kode = Convert.ToInt32(dr["Id"]),

                        });
                }
                return Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_ECirculation> GetCRHeadDetail(string Circulation, int DeptGroup)
        {
            try
            {
                List<BO_ECirculation> Result = new List<BO_ECirculation>();

                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "Circulation", Value = Circulation });

                DataTable dt = new DataTable();
                dt = SQLHelpers.GetDataTableByParam("CR_Get_Request_ByID", CommandType.StoredProcedure, Params, db.connectionString);

                foreach (DataRow dr in dt.Rows)
                {
                    Result.Add(
                        new BO_ECirculation
                        {
                            CirculationNo = Convert.ToString(dr["CirculationNo"]),
                            CRType = Convert.ToString(dr["CR_Type"]),
                            POType = Convert.ToString(dr["PoType"]),
                            NameofItem = Convert.ToString(dr["NameItem"]),
                            Reason = Convert.ToString(dr["Reason"]),
                            BudgetType = Convert.ToString(dr["Budget"]),
                            PONumber = Convert.ToString(dr["PO_No"]),
                            ParentCR = Convert.ToString(dr["Parent_Circuation"]),
                            Spesification = Convert.ToString(dr["Spesification"]),
                            Customer = Convert.ToString(dr["Customer"]),
                            Tanggal = Convert.ToDateTime(dr["RequirementDate"]),
                            id = Convert.ToInt32(dr["Record_id"]),
                            DeptGroup = DeptGroup,
                        });
                }
                return Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BO_ECirculation> GetOtherDeptList(string Circulation)
        {
            try
            {
                List<BO_ECirculation> Result = new List<BO_ECirculation>();
                string Query = @"select id
                                    ,CirculationNo
                                    ,DeptID
                                    ,ISNULL('',opinion) as opinion
                                    ,ISNULL('False',Approve) as Approve
                                    ,ISNULL('1990-01-01',[date]) as [date]
                                    from CR_Other_Dept
                                Where CirculationNo = '" + Circulation + "'";



                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "Circulation", Value = Circulation });


                DataTable dt = new DataTable();
                dt = SQLHelpers.GetDataTableByParam(Query, CommandType.Text, Params, db.connectionString);

                foreach (DataRow dr in dt.Rows)
                {
                    // int a = Convert.ToInt32(dr["id"]);
                    Result.Add(
                        new BO_ECirculation
                        {
                            id = Convert.ToInt32(dr["id"]),
                            CirculationNo = Convert.ToString(dr["CirculationNo"]),
                            Dept = Convert.ToString(dr["DeptID"]),
                            Opinion = Convert.ToString(dr["DeptID"]),
                            Approve = Convert.ToBoolean(dr["Approve"]),
                            Date = Convert.ToDateTime(dr["date"]),
                        });
                }
                return Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public List<BO_ECirculation> GetCRApproveHeadOpinion(CRAkses _cRAkses)
        {
            try
            {
                string P1 = "Approve 1";
                string P2 = "Approve 2";

                List<BO_ECirculation> Result = new List<BO_ECirculation>();

                DataTable dt = new DataTable();
                //string Query = @"Get_Root_Approve";
                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "User", Value = _cRAkses.UserName });
                Params.Add(new SqlParameter() { ParameterName = "P1", Value = P1 });
                Params.Add(new SqlParameter() { ParameterName = "P2", Value = P2 });
                Params.Add(new SqlParameter() { ParameterName = "Dept", Value = _cRAkses.UserGroupCode });
                Params.Add(new SqlParameter() { ParameterName = "Group", Value = _cRAkses.DeptGroup });

                dt = SQLHelpers.GetDataTableByParam("CR_Web_GetHeader_Opinion", CommandType.StoredProcedure, Params, db.connectionString);

                foreach (DataRow dr in dt.Rows)


                {
                    Result.Add(
                        new BO_ECirculation
                        {
                            CirculationNo = Convert.ToString(dr["Circulation No"]),
                            Requirement = Convert.ToDateTime(dr["Requirement Date"]),
                            Dept = Convert.ToString(dr["Dept"]),
                            Status = Convert.ToString(dr["Status"]),
                            //Requester = Convert.ToBoolean(dr["Requester"]),
                            //DeptHead = Convert.ToBoolean(dr["Dept Head"]),
                            //DivHead = Convert.ToBoolean(dr["Div Head"]),
                            //OtherDept = Convert.ToBoolean(dr["Other Dept"]),
                            //Accounting = Convert.ToBoolean(dr["Accounting"]),
                            //Purch = Convert.ToBoolean(dr["Purch"]),
                            Amount = Convert.ToDouble(dr["Amount"]),
                            Approve_Opinion = Convert.ToBoolean(dr["Approve"]),
                            DeptGroup = Convert.ToInt32(dr["DeptGroup"]),
                            LevelUser = _cRAkses.Level,
                            NameofItem = Convert.ToString(dr["Name Of Item"]),
                            Other_Opinion = Convert.ToString(dr["Opinion"]),

                        });
                }
                return Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public List<BO_ECirculation> GetDataOpinionSearch(CRAkses _CRAkses, DateTime TanggalAwal, DateTime TanggalAhir)
        {
            try
            {
                List<BO_ECirculation> Result = new List<BO_ECirculation>();

                DataTable dt = new DataTable();
                //string Query = @"Get_Root_Approve";
                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "Dept", Value = _CRAkses.UserGroupCode });
                Params.Add(new SqlParameter() { ParameterName = "CurrentLevel", Value = 4 });
                Params.Add(new SqlParameter() { ParameterName = "Date1", Value = TanggalAwal });
                Params.Add(new SqlParameter() { ParameterName = "Date2", Value = TanggalAhir });
                Params.Add(new SqlParameter() { ParameterName = "User", Value = _CRAkses.UserName });

                dt = SQLHelpers.GetDataTableByParam("CR_Request_Get_DeptComment_Search", CommandType.StoredProcedure, Params, db.connectionString);

                foreach (DataRow dr in dt.Rows)


                {
                    Result.Add(
                        new BO_ECirculation
                        {

                            CirculationNo = Convert.ToString(dr["Circulation No"]),
                            Requirement = Convert.ToDateTime(dr["Requirement Date"]),
                            Dept = Convert.ToString(dr["Dept"]),
                            Status = Convert.ToString(dr["Status"]),
                            Requester = Convert.ToBoolean(dr["Requester"]),
                            DeptHead = Convert.ToBoolean(dr["Dept Head"]),
                            DivHead = Convert.ToBoolean(dr["Div Head"]),
                            OtherDept = Convert.ToBoolean(dr["Other Dept"]),
                            Accounting = Convert.ToBoolean(dr["Accounting"]),
                            Purch = Convert.ToBoolean(dr["Purch"]),
                            Amount = Convert.ToDouble(dr["Amount"]),
                            LevelUser = _CRAkses.Level,

                        });
                }
                return Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BO_ECirculation_Detail> GetDataCRApproveDetailOpinion(string _id)
        {
            try
            {
                //List<BO_ECirculation_Detail> Result = new List<BO_ECirculation_Detail>();

                //string Query = @"CR_Get_Detail_DescriptionOfCost";
                //SqlParameter[] param = new SqlParameter[1];
                //param[0] = new SqlParameter("@CirculationNo", SqlDbType.VarChar);

                //param[0].Value = Circulation;
                //DataTable dt = new DataTable();
                //dt = SQLHelpers.GetDataTableByCommand_SP_Use(Query, param);

                List<BO_ECirculation_Detail> Result = new List<BO_ECirculation_Detail>();

                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "CirculationNo", Value = _id });

                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("CR_Get_Detail_DescriptionOfCost", CommandType.StoredProcedure, Params, db.connectionString);

                foreach (DataRow dr in dt.Rows)
                {
                    Result.Add(
                        new BO_ECirculation_Detail
                        {
                            NameOfGood = Convert.ToString(dr["Name Of Goods"]),
                            Spesification = Convert.ToString(dr["Spesification"]),
                            Model = Convert.ToString(dr["Model"]),
                            SalesType = Convert.ToString(dr["Sales Type"]),
                            Remark = Convert.ToString(dr["Remark"]),
                            Qty = Convert.ToDouble(dr["Qty"]),
                            Price = Convert.ToDouble(dr["Price"]),
                            Amount = Convert.ToDouble(dr["Total Amount Currency"]),
                            Curr = Convert.ToString(dr["Curr"]),
                            Rate = Convert.ToDouble(dr["Rate"]),
                            TotalIDR = Convert.ToDouble(dr["Total IDR"]),
                            Account = Convert.ToString(dr["Account"]),
                            AccountName = Convert.ToString(dr["Account Name"]),
                            Category = Convert.ToString(dr["Category"]),
                            kode = Convert.ToInt32(dr["Id"]),


                        });
                }
                return Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Update(BO_ECirculation FData, CRAkses _cRAkses)
        {
            try
            {
                int Result = -1;
                DataTable dt1 = new DataTable();

                try
                {
                    using (SqlConnection Conn = new SqlConnection(SQLHelpers.strConn))
                    {
                        Conn.Open();
                        using (SqlTransaction Transact = Conn.BeginTransaction())
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = Conn;
                            cmd.Transaction = Transact;

                            try
                            {

                                if (FData.HisStatus == "Reject")
                                {

                                    string QUERY = @"Update T_ApproveHistory 
                                                    SET IsActive = '" + 0 + "'" +
                                                  " where [NoTransaksi] = '" + FData.CirculationNo + "' and MenuCode = '" + FData.HisMenuCode + "' ";
                                    SQLHelpers.ExecQuery(QUERY);

                                }
                                else
                                {


                                    EmailHelper mailHelper = new EmailHelper()
                                    {
                                        Sender = "",
                                        Subject = "Sirkulasi Baru No '" + FData.CirculationNo + "'"
                                        //Body = "Sirkulasi No tess membutuhkan approval"
                                    };

                                    List<string> Recipients = new List<string>();

                                    //SmtpMail oMail = new SmtpMail("TryIt");
                                    //{
                                    //    oMail.Subject = "Sirkulasi Baru No '" + FData.CirculationNo + "'";
                                    //};


                                    if (FData.HisLevel == 2)
                                    {
                                        string Query = @"Select isnull([Email],'') AS EMAIL,isnull([Name],'') AS Name  from CR_Request
                                                        inner join M_Division on CR_Request.div_Id = M_Division.div_Id
                                                        inner join S_User on M_Division.div_Pic = s_user.Username
                                        where CirculationNo = '" + FData.CirculationNo + "'";


                                        dt1 = SQLHelpers.GetDataTable(Query);
                                        string cekEmail;
                                        foreach (DataRow dr in dt1.Rows)
                                        {
                                            cekEmail = Convert.ToString(dr[0]);
                                            if (cekEmail == "")
                                            {
                                                //oMail.To.Add("miftah-mis@tsmu.co.id");
                                                Recipients.Add("haris@tsmu.co.id");
                                            }
                                            else
                                            {
                                                //m.To.Add(Convert.ToString(dr[0]));
                                                //oMail.To.Add(Convert.ToString(Convert.ToString(dr["Email"])));
                                                string to = dr["Email"].AsString();
                                                Recipients.Add(to);
                                                //Recipients.Add("hamidi-mis@tsmu.co.id");
                                                //message.To.Add(new MailboxAddress(to, to));
                                            }

                                        }
                                        //oMail.HtmlBody = "Sirkulasi No '" + FData.CirculationNo + "' Membutuhkan Approval";

                                        //message.Body = new TextPart("plain")
                                        //{
                                        //    Text = @"Sirkulasi No '" + FData.CirculationNo + "' Membutuhkan Approval"
                                        //};
                                        mailHelper.Body = @"
                                            Sirkulasi '" + FData.CirculationNo + "' membutuhkan Approval " + 
                                            @"Silahkan klik link di bawah untuk masuk ke aplikasi 7Tells. 
                                            Email ini adalah auto generate dari aplikasi, mohon tidak membalas email ini.
                                            <br>Terimakasih
                                        ";

                                    }
                                    else if (FData.HisLevel == 3)
                                    {
                                        string Query =
                                        @"Select isnull([Email],'') AS EMAIL,isnull([Name],'') AS Name
                                        FROM CR_Request inner join CR_Other_Dept
	                                    inner join M_Approve on CR_Other_Dept.DeptID  = M_Approve.DeptID
	                                    INNER JOIN S_User ON M_Approve.UserName = S_User.Username
                                        on  CR_Request.CirculationNo = CR_Other_Dept.CirculationNo
                                        where  CR_Other_Dept.CirculationNo = '" + FData.CirculationNo + "' " +
                                        "and M_Approve.MenuCode = 'CIRCULATION' AND M_Approve.LevelApprove = 2";

                                        dt1 = SQLHelpers.GetDataTable(Query);

                                        string cekEmail;
                                        foreach (DataRow dr in dt1.Rows)
                                        {
                                            cekEmail = Convert.ToString(dr[0]);
                                            if (cekEmail == "")
                                            {
                                                Recipients.Add("haris@tsmu.co.id");
                                            }
                                            else
                                            {
                                                string to = dr["Email"].AsString();
                                                Recipients.Add(to);
                                                //Recipients.Add("hamidi-mis@tsmu.co.id");
                                            }

                                        }
                                        // m.Body = "Sirkulasi No : '" + FData.CirculationNo + "' Membutuhkan Opinion";
                                        //oMail.HtmlBody = "Sirkulasi No : '" + FData.CirculationNo + "' Membutuhkan Opinion";
                                        mailHelper.Body = @"
                                            Sirkulasi '" + FData.CirculationNo + "' membutuhkan Opinion " +
                                              @"Silahkan klik link di bawah untuk masuk ke aplikasi 7Tells. 
                                            Email ini adalah auto generate dari aplikasi, mohon tidak membalas email ini.
                                            <br>Terimakasih
                                        ";
                                    }

                                    //m.From = new MailAddress("circulation@tsmu.co.id", "CIRCULATION");
                                    //m.Sender = TO;

                                    //oMail.From = "circulation@tsmu.co.id";
                                    //oMail.Cc.Add("miftah-mis@tsmu.co.id");
                                    //oMail.Cc.Add("log@tsmu.co.id");

                                    //SmtpServer oServer = new SmtpServer("mail.tsmu.co.id");
                                    //oServer.User = "circulation@tsmu.co.id";
                                    //oServer.Password = "k(z^Rknv4i5H";

                                    //// Set 25 or 587 SMTP Port
                                    //oServer.Port = 465;

                                    //// Detect TLS connection automatically
                                    //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                                    //SmtpClient oSmtp = new SmtpClient();
                                    //oSmtp.SendMail(oServer, oMail);
                                    mailHelper.Recipients = Recipients;
                                    mailHelper.Send();

                                }

                                string QUERY1 = @"Update CR_Request 
                                           SET Current_Level = '" + FData.CurrentLevel + "'" +
                                               ",Status = '" + FData.Status + "'" +
                                               " where [CirculationNo] = '" + FData.CirculationNo + "'";
                                SQLHelpers.ExecQuery(QUERY1);


                                DateTime time = DateTime.Now;
                                string QCek = "Select * from T_ApproveHistory where NoTransaksi = '" + FData.CirculationNo + "' and LevelApproved = '" + FData.CurrentLevel + "' and IsActive = '" + 1 + "' ";

                                List<SqlParameter> Params = new List<SqlParameter>();
                                Params.Add(new SqlParameter() { ParameterName = "CirculationNo", Value = FData.CirculationNo });
                                Params.Add(new SqlParameter() { ParameterName = "LevelApproved", Value = FData.CurrentLevel });



                                DataTable dt = new DataTable();
                                dt = SQLHelpers.GetDataTableByParam(QCek, CommandType.Text, Params, db.connectionString);

                                if (dt.Rows.Count <= 0)
                                {
                                    string QUERY2 = @"INSERT INTO [T_ApproveHistory]
                                               ([UserName]
                                               ,[MenuCode]
                                               ,[DeptID]
                                               ,[NoTransaksi]
                                               ,[LevelApproved]
                                               ,[StatusApproved]
                                               ,[ApprovedBy]
                                               ,[ApprovedDate]
                                               ,[IsActive])
                                         VALUES
                                               ('" + _cRAkses.UserName + "'," +
                                                               "'" + FData.HisMenuCode + "'," +
                                                               "'" + _cRAkses.UserGroupCode + "'," +
                                                               "'" + FData.CirculationNo + "'," +
                                                               "'" + FData.HisLevel + "'," +
                                                               "'" + FData.HisStatus + "'," +
                                                                "'" + _cRAkses.UserName + "'," +
                                                              "GETDATE() ," +
                                                               "'" + FData.HisisActive + "')";

                                    int res = SQLHelpers.ExecQuery(QUERY2);
                                }



                                for (int i = 0; i < FData.CRDetail.Count; i++)
                                {
                                    string QUERY3 = @"Update CR_Description_Of_Cost 
                                           SET [Note] = '" + FData.CRDetail[i].Note + "'" +
                                              " where [id] = '" + FData.CRDetail[i].kode + "'";
                                    SQLHelpers.ExecQuery(QUERY3);
                                }
                                //Transact.Rollback();
                                Transact.Commit();
                            }
                            catch (Exception ex)
                            {
                                Transact.Rollback();
                                throw ex;
                            }
                            finally
                            {
                                cmd = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int Reject(BO_ECirculation FData, CRAkses _cRAkses)
        {
            try
            {
                int Result = -1;
                DataTable dt1 = new DataTable();

                try
                {
                    using (SqlConnection Conn = new SqlConnection(SQLHelpers.strConn))
                    {
                        Conn.Open();
                        using (SqlTransaction Transact = Conn.BeginTransaction())
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = Conn;
                            cmd.Transaction = Transact;

                            try
                            {

                                //string QUERY = @"Update T_ApproveHistory 
                                //   SET IsActive = '" + 0 + "'" +
                                //              " where [NoTransaksi] = '" + FData.CirculationNo + "' and MenuCode = '" + FData.HisMenuCode + "' ";
                                //SQLHelpers.ExecQuery(QUERY);

                                cmd.CommandText = "Update T_ApproveHistory set IsActive = '" + 0 + "'" +
                                                 " where [NoTransaksi] = '" + FData.CirculationNo + "' and MenuCode = '" + FData.HisMenuCode + "' ";
                                cmd.ExecuteNonQuery();

                                string QUERY = @"Update CR_Other_Dept 
                                               SET Date = NULL" +
                                            ",Opinion = NULL" +
                                            ",Approve = 0" +
                                           " where [CirculationNo] = '" + FData.CirculationNo + "'";
                                SQLHelpers.ExecQuery(QUERY);

                                //string QUERY1 = @"Update CR_Request 
                                //           SET Current_Level = '" + FData.CurrentLevel + "'" +
                                //               ",Status = '" + FData.Status + "'" +
                                //               " where [CirculationNo] = '" + FData.CirculationNo + "'";
                                //SQLHelpers.ExecQuery(QUERY1);

                                cmd.CommandText = "Update CR_Request SET Current_Level = '" + FData.CurrentLevel + "'" +
                                                   ",Status = '" + FData.Status + "'" +
                                                   " where [CirculationNo] = '" + FData.CirculationNo + "'";
                                cmd.ExecuteNonQuery();


                                DateTime time = DateTime.Now;
                                //string QUERY2 = @"INSERT INTO [T_ApproveHistory]
                                //               ([UserName]
                                //               ,[MenuCode]
                                //               ,[DeptID]
                                //               ,[NoTransaksi]
                                //               ,[LevelApproved]
                                //               ,[StatusApproved]
                                //               ,[ApprovedBy]
                                //               ,[ApprovedDate]
                                //               ,[IsActive])
                                //         VALUES
                                //               ('" + _cRAkses.UserName + "'," +
                                //                               "'" + FData.HisMenuCode + "'," +
                                //                               "'" + _cRAkses.UserGroupCode + "'," +
                                //                               "'" + FData.CirculationNo + "'," +
                                //                               "'" + FData.HisLevel + "'," +
                                //                               "'" + FData.HisStatus + "'," +
                                //                              "'" + _cRAkses.UserName + "'," +
                                //                              "'" + time + "'," +
                                //                               "'" + FData.HisisActive + "')";

                                //int res = SQLHelpers.ExecQuery(QUERY2);


                                //cmd.CommandText = "INSERT INTO [T_ApproveHistory] ([UserName],[MenuCode],[DeptID],[NoTransaksi],[LevelApproved],[StatusApproved],[ApprovedBy],[ApprovedDate],[IsActive]) VALUES " +
                                //                "('" + _cRAkses.UserName + "', '" + FData.HisMenuCode + "','" + _cRAkses.UserGroupCode + "','" + FData.CirculationNo + "'," +
                                //                "'" + FData.HisLevel + "','" + FData.HisStatus + "','" + _cRAkses.UserName + "'," + time + ",'" + FData.HisisActive + "')";
                                //cmd.ExecuteNonQuery();

                                string QUERY2 = @"INSERT INTO [T_ApproveHistory]
                                               ([UserName]
                                               ,[MenuCode]
                                               ,[DeptID]
                                               ,[NoTransaksi]
                                               ,[LevelApproved]
                                               ,[StatusApproved]
                                               ,[ApprovedBy]
                                               ,[ApprovedDate]
                                               ,[IsActive])
                                         VALUES
                                               ('" + _cRAkses.UserName + "'," +
                                                              "'" + FData.HisMenuCode + "'," +
                                                              "'" + _cRAkses.UserGroupCode + "'," +
                                                              "'" + FData.CirculationNo + "'," +
                                                              "'" + FData.HisLevel + "'," +
                                                              "'" + FData.HisStatus + "'," +
                                                             "'" + _cRAkses.UserName + "'," +
                                                             " GETDATE()," +
                                                              "'" + FData.HisisActive + "')";

                                int res = SQLHelpers.ExecQuery(QUERY2);




                                for (int i = 0; i < FData.CRDetail.Count; i++)
                                {
                                    //string QUERY3 = @"Update CR_Description_Of_Cost 
                                    //       SET [Note] = '" + FData.CRDetail[i].Note + "'" +
                                    //          " where [id] = '" + FData.CRDetail[i].kode + "'";
                                    //SQLHelpers.ExecQuery(QUERY3);

                                    cmd.CommandText = "Update CR_Description_Of_Cost  SET [Note] = '" + FData.CRDetail[i].Note + "'" +
                                              " where [id] = '" + FData.CRDetail[i].kode + "'";
                                    cmd.ExecuteNonQuery();
                                }

                                Transact.Commit();
                            }
                            catch (Exception ex)
                            {
                                Transact.Rollback();
                                throw ex;
                            }
                            finally
                            {
                                cmd = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateOpinion(BO_ECirculation FData, CRAkses _cRAkses)
        {
            try
            {
                int Result = -1;

                try
                {
                    using (SqlConnection Conn = new SqlConnection(SQLHelpers.strConn))
                    {
                        Conn.Open();
                        using (SqlTransaction Transact = Conn.BeginTransaction())
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = Conn;
                            cmd.Transaction = Transact;

                            try
                            {



                                string QUERY = @"Update CR_Other_Dept 
                                               SET Date = GETDATE()" +
                                            ",Opinion = '" + FData.Other_Opinion + "'" +
                                            ",Approve = '" + FData.Other_Approve + "'" +
                                            ",DeptHead_ID = '" + FData.Other_DeptHead + "'" +
                                           " where [CirculationNo] = '" + FData.CirculationNo + "' and [dept_group]  = '" + FData.DeptGroup + "'";
                                SQLHelpers.ExecQuery(QUERY);


                                string Cek = @"SELECT
                                                CASE WHEN SUM(CAST(Approve AS INT)) < COUNT(CirculationNo) Then CAST(0 AS BIT)
                                                        ELSE CAST(1 AS BIT)
                                                END Approve
                                                FROM CR_Other_Dept
                                                WHERE[CirculationNo] = '" + FData.CirculationNo + "' GROUP BY CirculationNo ";

                                List<SqlParameter> Params = new List<SqlParameter>();
                                DataTable dt = new DataTable();

                                // dt = SQLHelpers.GetDataTableByCommand(Cek);

                                //DataTable dt = new DataTable();
                                dt = SQLHelpers.GetDataTableByParam(Cek, CommandType.Text, Params, db.connectionString);

                                var Approve = "";
                                foreach (DataRow dr in dt.Rows)
                                {
                                    Approve = Convert.ToString(dr["Approve"]);
                                }
                                if (Approve == "True")
                                {
                                    string QUERY1 = @"Update CR_Request 
                                           SET Current_Level = '" + FData.CurrentLevel + "'" +
                                               ",Status = '" + FData.Status + "'" +
                                               " where [CirculationNo] = '" + FData.CirculationNo + "'";
                                    SQLHelpers.ExecQuery(QUERY1);


                                    DateTime time = DateTime.Now;


                                    string QUERY2 = @"INSERT INTO [T_ApproveHistory]
                                               ([UserName]
                                               ,[MenuCode]
                                               ,[DeptID]
                                               ,[NoTransaksi]
                                               ,[LevelApproved]
                                               ,[StatusApproved]
                                               ,[ApprovedBy]
                                               ,[ApprovedDate]
                                               ,[IsActive])
                                         VALUES
                                               ('" + _cRAkses.UserName + "'," +
                                                                   "'" + FData.HisMenuCode + "'," +
                                                                   "'" + _cRAkses.UserGroupCode + "'," +
                                                                   "'" + FData.CirculationNo + "'," +
                                                                   "'" + FData.HisLevel + "'," +
                                                                   "'" + FData.HisStatus + "'," +
                                                                  "'" + _cRAkses.UserName + "'," +
                                                                  " GETDATE()," +
                                                                   "'" + FData.HisisActive + "')";

                                    int res = SQLHelpers.ExecQuery(QUERY2);

                                }

                                Transact.Commit();
                            }
                            catch (Exception ex)
                            {
                                Transact.Rollback();
                                throw ex;
                            }
                            finally
                            {
                                cmd = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static readonly Func<IDataReader, CRAkses> MakeAkses = reader =>
         new CRAkses
         {
             UserGroupCode = reader["UserGroupCode"].AsString(),
             UserName = reader["Username"].AsString(),
             Level = reader["Level"].AsInt(),
             Division = reader["div_Id"].AsInt(),
             Director = reader["director_Id"].AsInt(),
             DeptGroup = reader["dept_group"].AsInt(),
         };


    }
}
