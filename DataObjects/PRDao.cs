using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects.Helpers;

namespace DataObjects
{
    public class PRDao : IPRDao
    {
        static Db db = new Db();
        public List<PR> GetPR()
        {
            throw new NotImplementedException();
        }

        public List<PR> GetModelPR(PRAksesUser prakses)
        {
            try
            {
                object[] Params = { "@UserName", prakses.UserName, "@Kol", prakses.Kol };
                return db.Read("PR_GetListPR", CommandType.StoredProcedure, MakePR, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PR> GetPRById(string PRNo)
        {
            throw new NotImplementedException();
        }

        public PR GetPRModelById(string PRNo)
        {
            throw new NotImplementedException();
        }

        public int Update(PR pr)
        {
            throw new NotImplementedException();
        }

        public List<PR> GetModelPRByUser(PRAksesUser prakses)
        {
            try
            {
                object[] Params = { "@UserName", prakses.UserName, "@Kol", prakses.Kol, "@Site", prakses.Site };
                return db.Read("PR_GetListPRByUser", CommandType.StoredProcedure, MakeByUser, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PR> GetModelPRByAll(PRAksesUser prakses)
        {
            try
            {
                object[] Params = { "@UserName", prakses.UserName };
                return db.Read("PR_GetListPRByAll", CommandType.StoredProcedure, MakeByAll, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PR> GetModelPRPurchase()
        {
            try
            {
                return db.Read("PR_GetListPRPurchase", CommandType.StoredProcedure, MakePurchase, null).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PR> GetModelPRHistory(PRHistory filter)
        {
            try
            {
                string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                object[] Params = { "@DTDari", stDate, "@DTSampai", enDate, "@Cabang", filter.Cabang, "@Dept", filter.Dept, "@PurchaseType", filter.PurchaseType, "@Status", filter.Status, "@UserName", filter.UserName };
                return db.Read("PR_GetListPRHistory", CommandType.StoredProcedure, MakeByAll, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PR> GetModelPRHistoryPur(PRHistory filter)
        {
            try
            {
                string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                object[] Params = { "@DTDari", stDate, "@DTSampai", enDate, "@Cabang", filter.Cabang, "@Dept", filter.Dept, "@PurchaseType", filter.PurchaseType, "@Status", filter.Status };
                return db.Read("PR_GetListPRHistoryPur", CommandType.StoredProcedure, MakeByAll, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRDetail> GetModelPRHistoryExport(PRHistory filter)
        {
            try
            {
                string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                object[] Params = { "@DTDari", stDate, "@DTSampai", enDate, "@Cabang", filter.Cabang, "@Dept", filter.Dept, "@PurchaseType", filter.PurchaseType, "@Status", filter.Status, "@UserName", filter.UserName };
                return db.Read("PR_GetListPRHistoryExport", CommandType.StoredProcedure, MakePRDetailFull, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRDetail> GetModelPRHistoryExportPur(PRHistory filter)
        {
            try
            {
                string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                object[] Params = { "@DTDari", stDate, "@DTSampai", enDate, "@Cabang", filter.Cabang, "@Dept", filter.Dept, "@PurchaseType", filter.PurchaseType, "@Status", filter.Status };
                return db.Read("PR_GetListPRHistoryExportPur", CommandType.StoredProcedure, MakePRDetailFull, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PR> GetModelPRProgress(PRProgress filter)
        {
            try
            {
                string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                object[] Params = { "@DTDari", stDate, "@DTSampai", enDate, "@Cabang", filter.Cabang, "@Dept", filter.Dept, "@PurchaseType", filter.PurchaseType };
                return db.Read("PR_GetListPRProgress", CommandType.StoredProcedure, MakeByAll, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRDetail> GetModelPRProgressExport(PRProgress filter)
        {
            try
            {
                string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                object[] Params = { "@DTDari", stDate, "@DTSampai", enDate, "@Cabang", filter.Cabang, "@Dept", filter.Dept, "@PurchaseType", filter.PurchaseType };
                return db.Read("PR_GetListPRProgressExport", CommandType.StoredProcedure, MakePRDetailFull, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PRAksesUser GetAkses(string UserName)
        {
            try
            {
                string Query = @"SELECT  UserName AS user_id ,
                                        Jenis AS jenis ,
                                        Kol AS kol ,
                                        Site AS site ,
                                        SubAcct AS sub
                                FROM    dbo.S_AksesApproval
                                WHERE   UserName = '" + UserName + "'";
                return db.Read(Query, CommandType.Text, MakeAkses).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateFlagPR(string PRNo, string ApprovalPIC)
        {
            try
            {
                int Result = -1;
                string Query = @"UPDATE  dbo.XPRHdr
                                    SET     StatusFlag = 'P' ,
                                            ApprovalPIC = @ApprovalPIC
                                    WHERE   PRNo = @PRNo";
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                Params.Add(new SqlParameter() { ParameterName = "ApprovalPIC", Value = ApprovalPIC });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                Query = @"UPDATE  dbo.approval1
                            SET     kol1 = 0 ,
                                    kol2 = 0 ,
                                    kol3 = 0
                            WHERE   PRNo = @PRNo";
                Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateStatusBudget(string PRNo)
        {
            try
            {
                int Result = -1;
                string Query = @"UPDATE  dbo.XPRHdr
                                    SET     StatusBudget = 'O'
                                    WHERE   PRNo = @PRNo";
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateFlagPRReject(string PRNo, int Kol)
        {
            try
            {
                int Result = -1;
                string Query = @"DELETE  FROM dbo.approval
                                WHERE   PRNo = @PRNo";
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                Query = @"UPDATE  dbo.XPRHdr
                            SET     StatusFlag = 'P' ,
                                    ApprovalPIC = 'C'
                            WHERE   PRNo = @PRNo";
                Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                //if (Kol != 1)
                //{
                Query = @"DELETE  FROM dbo.approval1
                        WHERE   PRNo = @PRNo";
                Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);
                //}              

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdatePilihanPurchase(int Action, string PRNo)
        {
            try
            {
                int Result = -1;
                string Query = "";
                if (Action == 1)
                {
                    Query = @"UPDATE  dbo.XPRHdr
                            SET     ApprovalPIC = @ApprovalPIC
                            WHERE   PRNo = @PRNo";
                    List<SqlParameter> Params = new List<SqlParameter>();
                    Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                    Params.Add(new SqlParameter() { ParameterName = "ApprovalPIC", Value = "P" });
                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                } else if(Action == 2)
                {
                    Query = @"DELETE  FROM dbo.approval
                            WHERE   PRNo = @PRNo";
                    List<SqlParameter> Params = new List<SqlParameter>();
                    Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                    Query = @"UPDATE  dbo.XPRHdr
                            SET     StatusFlag = 'P' ,
                                    ApprovalPIC = 'C'
                            WHERE   PRNo = @PRNo";
                    Params = new List<SqlParameter>();
                    Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                    Query = @"DELETE  FROM dbo.approval1
                            WHERE   PRNo = @PRNo";
                    Params = new List<SqlParameter>();
                    Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);                    
                }
                else if (Action == 3)
                {
                    Query = @"UPDATE  dbo.XPRHdr
                            SET     StatusFlag = 'C'
                            WHERE   PRNo = @PRNo";
                    List<SqlParameter> Params = new List<SqlParameter>();
                    Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);
                }                

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public PR GetPRHeaderById(string PRNo)
        {
            //try
            //{
            //    string Query = @"SELECT  AVPRHdr.LUpd_User ,
            //                            SegDef.Description ,
            //                            AVPRHdr.PRDate ,
            //                            AVPRHdr.PRNo ,
            //                            AVPRHdr.OverBudget
            //                    FROM    ( ( dbo.AVPRHdr AVPRHdr
            //                                INNER JOIN dbo.XPRDtl XPRDtl ON AVPRHdr.PRNo = XPRDtl.PRNo
            //                              )
            //                              LEFT OUTER JOIN TSC22Application.dbo.SegDef SegDef ON AVPRHdr.SecId = SegDef.ID
            //                            )
            //                            LEFT OUTER JOIN TSC22Application.dbo.AVMaxPODate AVMaxPODate ON XPRDtl.InvtId = AVMaxPODate.InvtId
            //                    WHERE   SegDef.SegNumber = '3'
            //                            AND SegDef.FieldClassName = 'SUBACCOUNT'
            //                            AND AVPRHdr.PRNo = '" + PRNo + "'";
            //    return db.Read(Query, CommandType.Text, MakePRHeader).FirstOrDefault();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            try
            {
                object[] Params = { "@PRNo", PRNo};
                return db.Read("PR_GetPRHeaderById", CommandType.StoredProcedure, MakePRHeader, Params).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRApproval> GetPRApprove(string PRNo)
        {
            try
            {
                List<PRApproval> Result = new List<PRApproval>();
                string Query = @"SELECT  a.lepelx AS Kol ,
                                        CASE WHEN ISNULL(b.Username, '') = '' THEN a.user_id
                                             ELSE b.Name
                                        END AS UserName ,
                                        a.status AS Status ,
                                        CONVERT(VARCHAR, a.tanggal, 103) AS Tanggal
                                FROM    dbo.approval AS a
                                        LEFT JOIN dbo.S_User AS b ON a.user_id = b.Username
                                WHERE   a.PRNo = @PRNo";
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                DataTable dt = new DataTable();
                dt = SQLHelpers.GetDataTableByParam(Query, CommandType.Text, Params, db.connectionString);
                foreach (DataRow reader in dt.Rows)
                {
                    Result.Add(new PRApproval
                    {
                        Kol = reader["Kol"].AsInt(),
                        UserName = reader["UserName"].AsString(),
                        Status = reader["Status"].AsString(),
                        Tanggal = reader["Tanggal"].AsString()
                    });
                }
                return Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PRDetail> GetPRDetail(string PRNo, string Year)
        {
            try
            {
                object[] Params = { "@PRNo", PRNo, "@Year", Year };
                return db.Read("PR_GetDetailPR", CommandType.StoredProcedure, MakePRDetail, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRDetail> GetPRDetailWithoutBudget(string PRNo)
        {
            try
            {
                object[] Params = { "@PRNo", PRNo };
                return db.Read("PR_GetDetailPRWithoutBudget", CommandType.StoredProcedure, MakePRDetail, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRBudget> GetPRBudget(string Year, string Sub, string Kol)
        {
            try
            {
                object[] Params = { "@Year", Year, "@Sub", Sub, "@Kol", Kol };
                return db.Read("PR_GetListBudgetControl", CommandType.StoredProcedure, MakePRBudget, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRBudgetDetail> GetPRBudgetDetail(string Year, string Month, string Sub, string Account)
        {
            try
            {
                object[] Params = { "@Year", Year, "@Month", Month, "@Sub", Sub, "@Account", Account };
                return db.Read("PR_GetBudgetDetailPR", CommandType.StoredProcedure, MakePRBudgetDetail, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateApvRemark(string PRNo, string Remark)
        {
            try
            {
                int Result = -1;
                string Query = "";
                Query = @"UPDATE  dbo.XPRHdr
                        SET     ApprovalRemark = @Remark
                        WHERE   PRNo = @PRNo";
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                Params.Add(new SqlParameter() { ParameterName = "Remark", Value = Remark });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ApprovePR(string UserName, int Kol, string PRNo)
        {
            try
            {
                int Result = -1;
                string Query = "";
                Query = @"DECLARE @_User AS VARCHAR(20);
                        DECLARE @_PRNo AS VARCHAR(20);
                        DECLARE @_Kol AS INT;

                        SET @_User = @User;
                        SET @_PRNo = @PRNo;
                        SET @_Kol = @Kol;

                        --Delete Approval
                        DELETE  FROM dbo.approval
                        WHERE   user_id = @_User
                                AND PRNo = @_PRNo;

                        ----Insert Approval
                        INSERT  INTO dbo.approval
                                ( user_id, PRNo, tanggal, status, lepelx )
                        VALUES  ( @_User, @_PRNo, GETDATE(), 'Approved', @_Kol );
        
                        -- --Jika kondisi Kol <> 1  
                        IF @_Kol <> 1
                            BEGIN
                                UPDATE  dbo.XPRHdr
                                SET     StatusFlag = 'R'
                                WHERE   PRNo = @_PRNo;
                            END;	     


                        DECLARE @PurType AS VARCHAR(2);
                        SELECT  DISTINCT
                                @PurType = PurchaseType
                        FROM    dbo.XPRDtl
                        WHERE   PRNo = @_PRNo;       
    
                        IF @_Kol = 3
                            AND @PurType = 'SE'
                            BEGIN
                                UPDATE  dbo.XPRHdr
                                SET     StatusFlag = 'C'
                                WHERE   PRNo = @_PRNo;
                            END;

                        DECLARE @TotalApv AS INT;
                        SELECT  @TotalApv = COUNT(*)
                        FROM    dbo.approval1
                                INNER JOIN dbo.approval ON approval1.PRNo = approval.PRNo
                        WHERE   approval1.PRNo = @_PRNo
                                AND approval.status = 'Approved';        

                        DECLARE @NamaKolom AS VARCHAR(10);
                        DECLARE @StrQuery AS VARCHAR(500);
                        SET @NamaKolom = 'Kol' + CAST(@_Kol AS CHAR(1));

                        IF @TotalApv = 0
	                        --Jika kondisi TotalApv = 0
                            BEGIN
                                SET @StrQuery = 'INSERT  INTO dbo.approval1
								                        ( PRNo,' + @NamaKolom + ' , status )
						                        values  ( ''' + @_PRNo + ''', 1, ''Approved'' )';
                            END;	
                        ELSE
	                        --Jika kondisi TotalApv > 0
                            BEGIN 
                                SET @StrQuery = 'update  dbo.approval1
						                        set     ' + @NamaKolom + ' = 1
						                        where   PRNo = ''' + @_PRNo + '''';      
                            END;

                        --SELECT  @StrQuery;
                        EXEC (@StrQuery)";
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "User", Value = UserName });
                Params.Add(new SqlParameter() { ParameterName = "Kol", Value = Kol });
                Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateKomenPR(List<PRDetail> PRDetail)
        {
            try
            {
                int Result = -1;
                string Query = "";
                foreach (var items in PRDetail)
                {
                    Query = @"DELETE  FROM dbo.komentar_pr
                            WHERE   PRNo = @PRNo";

                    List<SqlParameter> Params = new List<SqlParameter>();
                    Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = items.PRNo });
                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);
                }

                foreach (var items in PRDetail)
                {
                    var Komen = items.Komentar;
                    if (string.IsNullOrEmpty(items.Komentar))
                        {
                        Komen = "";
                        }
                    Query = @"INSERT  INTO dbo.komentar_pr
                                    ( PRNo, komentar, no )
                            VALUES  ( @PRNo, @Komentar, @No )";

                    List<SqlParameter> Params = new List<SqlParameter>();
                    Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = items.PRNo });
                    Params.Add(new SqlParameter() { ParameterName = "No", Value = items.No });                    
                    Params.Add(new SqlParameter() { ParameterName = "Komentar", Value = Komen });
                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);
                }                

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateInfoHargaPR(List<PRDetail> PRDetail)
        {
            try
            {
                int Result = -1;                
                string Query = "";
                if (PRDetail.Count > 0)
                {
                    Query = @"DELETE  FROM dbo.komentar_pr
                    WHERE   PRNo = @PRNo";

                    List<SqlParameter> Params = new List<SqlParameter>();
                    Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRDetail[0].PRNo });
                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);
                }

                foreach (var items in PRDetail)
                {
                    var Komen = items.Komentar2;
                    var Harga = items.Harga;
                    if (string.IsNullOrEmpty(Komen))
                    {
                        Komen = "";
                    }
                    if (Harga == null)
                    {
                        Harga = 0;
                    }
                    Query = @"INSERT  INTO dbo.komentar_pr
                            ( PRNo, komentar2, no, harga )
                    VALUES  ( @PRNo, @Komentar, @No, @Harga )";

                    List<SqlParameter> Params = new List<SqlParameter>();
                    Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = items.PRNo });
                    Params.Add(new SqlParameter() { ParameterName = "No", Value = items.No });
                    Params.Add(new SqlParameter() { ParameterName = "Komentar", Value = Komen });
                    Params.Add(new SqlParameter() { ParameterName = "Harga", Value = items.Harga });
                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);
                }

                foreach (var items in PRDetail)
                {
                    var Komen = items.Komentar2;
                    var Harga = items.Harga;
                    var Qty = items.Qty;
                    if (string.IsNullOrEmpty(Komen))
                    {
                        Komen = "";
                    }
                    if (Harga == null)
                    {
                        Harga = 0;
                    }
                    if (Qty == null)
                    {
                        Qty = 0;
                    }
                    Query = @"UPDATE  dbo.XPRDtl
                            SET     Qty = @Qty
                            WHERE   PRNo = @PRNo
                                    AND XSeqNo = @No";

                    List<SqlParameter> Params = new List<SqlParameter>();
                    Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = items.PRNo });
                    Params.Add(new SqlParameter() { ParameterName = "No", Value = items.No });
                    Params.Add(new SqlParameter() { ParameterName = "Qty", Value = Qty });
                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);
                }

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateOverBudgetPR(string UserName, string PRNo)
        {
            //try
            //{
            //    int Result = -1;
            //    string Query = "";
            //    Query = @"IF EXISTS ( SELECT  *
            //                        FROM    dbo.approval1
            //                        WHERE   PRNo = @PRNo )
            //                BEGIN
            //                    UPDATE  dbo.approval1
            //                    SET     kol1 = 1 ,
            //                            kol2 = 1 ,
            //                            kol3 = 1
            //                    WHERE   PRNo = @PRNo;
            //                END;
            //            ELSE
            //                BEGIN
            //                    INSERT  INTO dbo.approval1
            //                            ( PRNo, kol1, kol2, kol3, status )
            //                    VALUES  ( @PRNo, 1, 1, 1, 'Approved' );
            //                END;";

            //    List<SqlParameter> Params = new List<SqlParameter>();
            //    Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo.ToUpper() });
            //    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

            //    Query = @"UPDATE  dbo.XPRHdr
            //            SET     StatusFlag = 'R' 
            //            WHERE   PRNo = @PRNo";

            //    return Result;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
                                string Query = "";

                                Query = @"PR_UpdateOverBudgetPR";

                                List<SqlParameter> Params = new List<SqlParameter>();
                                Params.Add(new SqlParameter() { ParameterName = "UserName", Value = UserName });
                                Params.Add(new SqlParameter() { ParameterName = "PRNo", Value = PRNo });
                                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.StoredProcedure, Params, db.connectionString);

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

        static readonly Func<IDataReader, PR> MakeAll = reader =>
           new PR
           {
               PRNo = reader["PRNo"].AsString(),
               Description = reader["Description"].AsString(),
               PRDate = reader["PRDate"].AsDateTime(),
               PurchaseType = reader["PurchaseType"].AsString(),
               ApprovalPIC = reader["ApprovalPIC"].AsString(),
               Kol = reader["Kol"].AsInt(),
               Site = reader["Site"].AsString(),
               Sub = reader["Sub"].AsString()
           };

        static readonly Func<IDataReader, PR> MakePR = reader =>
           new PR
           {
               PRNo = reader["PRNo"].AsString(),
               Description = reader["Description"].AsString(),
               PRDate = reader["PRDate"].AsDateTime(),
               PurchaseType = reader["PurchaseType"].AsString(),
               ApprovalPIC = reader["ApprovalPIC"].AsString(),
               Remark = reader["Remark"].AsString(),
               UserBuat = reader["UserBuat"].AsString(),
               DeptCreate = reader["DeptCreate"].AsString()
           };

        static readonly Func<IDataReader, PR> MakeByUser = reader =>
           new PR
           {
               PRNo = reader["PRNo"].AsString(),
               Description = reader["Description"].AsString(),
               PRDate = reader["PRDate"].AsDateTime(),
               PurchaseType = reader["PurchaseType"].AsString(),
               Kol2 = reader["Kol2"].AsInt(),
               Kol3 = reader["Kol3"].AsInt(),
               QtyPO = reader["QtyPO"].AsDouble(),
               UserBuat = reader["UserBuat"].AsString()
           };

        static readonly Func<IDataReader, PR> MakeByAll = reader =>
          new PR
          {
              PRNo = reader["PRNo"].AsString(),
              Description = reader["Description"].AsString(),
              PRDate = reader["PRDate"].AsDateTime(),
              PurchaseType = reader["PurchaseType"].AsString(),
              UserBuat = reader["UserBuat"].AsString()
          };

        static readonly Func<IDataReader, PR> MakePurchase = reader =>
           new PR
           {
               Description = reader["Description"].AsString(),
               PRDate = reader["PRDate"].AsDateTime(),
               Tanggal = reader["Tanggal"].AsString(),
               PRNo = reader["PRNo"].AsString(),
               PurchaseType = reader["PurchaseType"].AsString(),
               ApprovalPIC = reader["ApprovalPIC"].AsString(),
               Remark = reader["Remark"].AsString(),
               QtyPO = reader["QtyPO"].AsDouble(),
               UserBuat = reader["UserBuat"].AsString()
           };

        static readonly Func<IDataReader, PRAksesUser> MakeAkses = reader =>
           new PRAksesUser
           {
               UserName = reader["user_id"].AsString(),
               Kol = reader["kol"].AsInt(),
               Site = reader["site"].AsString(),
               Sub = reader["sub"].AsString()
           };

        static readonly Func<IDataReader, PR> MakePRHeader = reader =>
           new PR
           {
               PRNo = reader["PRNo"].AsString(),
               Description = reader["Description"].AsString(),
               PRDate = reader["PRDate"].AsDateTime(),
               OverBudget = reader["OverBudget"].AsString(),
               Remark = reader["Notes"].AsString()
           };

        static readonly Func<IDataReader, PRDetail> MakePRDetail = reader =>
           new PRDetail
           {
               PRNo = reader["PRNo"].AsString(),
               Cek = reader["Cek"].AsString(),
               Cek2 = reader["Cek2"].AsString(),
               Komentar = reader["Komentar"].AsString(),
               Komentar2 = reader["Komentar2"].AsString(),
               Harga = reader["Harga"].AsDouble(),
               No = reader["No"].AsInt(),
               NamaBarang = reader["NamaBarang"].AsString(),
               InvtID = reader["InvtID"].AsString(),
               InvtSpec = reader["InvtSpec"].AsString(),
               Qty = reader["Qty"].AsDouble(),
               QtyPO = reader["QtyPO"].AsDouble(),
               UnitQty = reader["UnitQty"].AsString(),
               Price = reader["Price"].AsDouble(),
               Remark = reader["Remark"].AsString(),
               Account = reader["Account"].AsString(),
               AccountDesc = reader["AccountDesc"].AsString(),
               SubAccount = reader["SubAccount"].AsString(),
               PODate = reader["PODate"].AsString(),
               SisaBudget = reader["SisaBudget"].AsDouble(),
               Budget = reader["Budget"].AsDouble(),
               DelDate = reader["DelDate"].AsString()
           };

        static readonly Func<IDataReader, PRBudget> MakePRBudget = reader =>
           new PRBudget
           {
               Sub = reader["Sub"].AsString(),
               SubDesc = reader["SubDesc"].AsString(),
               Account = reader["Acct"].AsString(),
               AccountDesc = reader["AcctDesc"].AsString(),
               Budget01 = reader["PtdBal00"].AsDouble(),
               Budget02 = reader["PtdBal01"].AsDouble(),
               Budget03 = reader["PtdBal02"].AsDouble(),
               Budget04 = reader["PtdBal03"].AsDouble(),
               Budget05 = reader["PtdBal04"].AsDouble(),
               Budget06 = reader["PtdBal05"].AsDouble(),
               Budget07 = reader["PtdBal06"].AsDouble(),
               Budget08 = reader["PtdBal07"].AsDouble(),
               Budget09 = reader["PtdBal08"].AsDouble(),
               Budget10 = reader["PtdBal09"].AsDouble(),
               Budget11 = reader["PtdBal10"].AsDouble(),
               Budget12 = reader["PtdBal11"].AsDouble(),
               Actual01 = reader["Actual01"].AsDouble(),
               Actual02 = reader["Actual02"].AsDouble(),
               Actual03 = reader["Actual03"].AsDouble(),
               Actual04 = reader["Actual04"].AsDouble(),
               Actual05 = reader["Actual05"].AsDouble(),
               Actual06 = reader["Actual06"].AsDouble(),
               Actual07 = reader["Actual07"].AsDouble(),
               Actual08 = reader["Actual08"].AsDouble(),
               Actual09 = reader["Actual09"].AsDouble(),
               Actual10 = reader["Actual10"].AsDouble(),
               Actual11 = reader["Actual11"].AsDouble(),
               Actual12 = reader["Actual12"].AsDouble(),
               Sisa01 = reader["Sisa01"].AsDouble(),
               Sisa02 = reader["Sisa02"].AsDouble(),
               Sisa03 = reader["Sisa03"].AsDouble(),
               Sisa04 = reader["Sisa04"].AsDouble(),
               Sisa05 = reader["Sisa05"].AsDouble(),
               Sisa06 = reader["Sisa06"].AsDouble(),
               Sisa07 = reader["Sisa07"].AsDouble(),
               Sisa08 = reader["Sisa08"].AsDouble(),
               Sisa09 = reader["Sisa09"].AsDouble(),
               Sisa10 = reader["Sisa10"].AsDouble(),
               Sisa11 = reader["Sisa11"].AsDouble(),
               Sisa12 = reader["Sisa12"].AsDouble()
           };

        static readonly Func<IDataReader, PRBudgetDetail> MakePRBudgetDetail = reader =>
           new PRBudgetDetail
           {
               PRNo = reader["PRNo"].AsString(),
               XSeq = reader["XSeqNo"].AsInt(),
               InvtDescr = reader["InvtDescr"].AsString(),
               InvtSpec = reader["InvtSpec"].AsString(),
               InvtId = reader["InvtId"].AsString(),
               QtyPR = reader["QtyPR"].AsDouble(),
               QtyPO = reader["QtyPO"].AsDouble(),
               QtySisa = reader["QtySisa"].AsDouble(),
               HargaPR = reader["UnitPrice"].AsDouble(),
               HargaPO = reader["POPrice"].AsDouble(),
               AmountPR = reader["AmountPR"].AsDouble(),
               AmountPO = reader["AmountPO"].AsDouble(),
               QtyRCV = reader["QtyRCV"].AsDouble(),
               Remark = reader["Remark"].AsString(),
               Account = reader["Acct"].AsString(),
               AccountDesc = reader["AccountDesc"].AsString(),
               SubAccount = reader["Sub"].AsString(),
               SubAccountDesc = reader["SubDesc"].AsString(),
               DelDate = reader["DelDate"].AsString()
           };

        static readonly Func<IDataReader, PRDetail> MakePRDetailFull = reader =>
           new PRDetail
           {
               PRNo = reader["PRNo"].AsString(),
               Tanggal = reader["Tanggal"].AsString(),
               Dept = reader["Dept"].AsString(),
               No = reader["No"].AsInt(),
               NamaBarang = reader["InvtDescr"].AsString(),
               InvtSpec = reader["InvtSpec"].AsString(),
               InvtID = reader["InvtId"].AsString(),
               Qty = reader["Qty"].AsDouble(),
               UnitQty = reader["UnitQty"].AsString(),
               Remark = reader["Remark"].AsString(),
               Account = reader["Acct"].AsString(),
               AccountDesc = reader["Descr"].AsString(),
               SubAccount = reader["Sub"].AsString(),
               DelDate = reader["DelDate"].AsString(),
               PODate = reader["PODate"].AsString(),
               PurchaseType = reader["PurchaseType"].AsString(),
               Status = reader["StatusFlag"].AsString(),
               Price = reader["UnitPrice"].AsDouble(),
               Informasi = reader["Komentar2"].AsString(),
               TanggalApprove = reader["TanggalApprove"].AsString(),
           };

    }
}
