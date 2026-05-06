using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BusinessObjects;
using DataObjects.Helpers;
using System.Net;
//using System.Net.Mail;
//using System.Text;
using System.Text;

// Add EASendMail namespace


namespace DataObjects
{
    public class PODao : IPODao
    {
        static Db db = new Db();
        public List<BO_PO> GetModelPO(POAksesUser poakses)
        {
            try
            {
                //object[] Params = { "@UserName", poakses.user_id, "@Kol", poakses.Kol };
                object[] Params = { "@UserName", poakses.user_id, "@Kol", poakses.Kol };
                return db.Read("PO_W_GetPO", CommandType.StoredProcedure, MakePO, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BO_PO GetPOHeaderById(string PONo)
        {
            try
            {
                object[] Params = { "@PONumber", PONo};
                //return db.Read("PO_W_GetPOByID", CommandType.StoredProcedure, MakePOHeader,Params).FirstOrDefault();
                return db.Read("PO_W_GetPOByID", CommandType.StoredProcedure, MakePOHeader, Params).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_PODetail> GetPODetail(string PONo)
        {
            try
            {
                object[] Params = { "@PONumber", PONo };
                //return db.Read("PO_W_GetPOByID", CommandType.StoredProcedure, MakePOHeader,Params).FirstOrDefault();
                return db.Read("PO_W_GetPODetail", CommandType.StoredProcedure, MakePODetail, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BO_PO GetPOTotalAmount(string PONo)
        {
            try
            {
                object[] Params = { "@PONumber", PONo };
                //return db.Read("PO_W_GetPOByID", CommandType.StoredProcedure, MakePOHeader,Params).FirstOrDefault();
                return db.Read("PO_W_GetTotal", CommandType.StoredProcedure, MakePOtotalAmount, Params).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_POApproval> GetPOApprove(string PONo)
        {
            try
            {
                List<BO_POApproval> Result = new List<BO_POApproval>();
                string Query = @"SELECT  a.[level] AS Kol ,
                                        CASE WHEN ISNULL(b.Username, '') = '' THEN a.user_id
                                             ELSE b.Name
                                        END AS UserName ,
                                        a.status AS Status ,
                                        CONVERT(VARCHAR, a.tanggal, 6) AS Tanggal
                                FROM    dbo.approvalpo AS a
                                        LEFT JOIN dbo.S_User AS b ON a.user_id = b.Username
                                WHERE   a.PONbr = @PONo";
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "PONo", Value = PONo });
                DataTable dt = new DataTable();
                dt = SQLHelpers.GetDataTableByParam(Query, CommandType.Text, Params, db.connectionString);
                foreach (DataRow reader in dt.Rows)
                {
                    Result.Add(new BO_POApproval
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
        public POAksesUser GetAkses(string UserName)
        {
            try
            {
                string Query = @"SELECT  user_id ,
                                        Jenis AS jenis ,
                                        Kol AS kol ,
                                        VendId,
                                        email
                                FROM    dbo.aksespo_approval
                                WHERE   user_id = '" + UserName + "'";
                return db.Read(Query, CommandType.Text, MakeAkses).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_PO> GetModelPOByUser(POAksesUser poakses,DateTime dtdari,DateTime dtsampai)
        {
            try
            {
                //object[] Params = { "@UserName", poakses.user_id, "@Kol", poakses.Kol };
                object[] Params = { "@UserName", poakses.user_id, "@Kol", poakses.Kol, "@DTAwal",dtdari, "@DTAkhir", dtsampai };
                return db.Read("[PO_W_GetPOByUser]", CommandType.StoredProcedure, MakePOByUser, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_PO> GetModelPOApproveSM(POAksesUser poakses)
        {
            //object[] Params = { "@UserName", poakses.user_id, "@Kol", poakses.Kol };
            object[] Params = { "@UserName", poakses.user_id, "@Kol", poakses.Kol };
            return db.Read("[PO_W_GetPOApproveSM]", CommandType.StoredProcedure, MakePOApproveSM, Params).ToList();
        }

        //public int ApprovePO(string UserName, int Kol, string PONo)
        //{
        //    try
        //    {
        //        int Result = -1;
                
        //        List<SqlParameter> Params = new List<SqlParameter>();
        //        Params.Add(new SqlParameter() { ParameterName = "UserName", Value = UserName });
        //        Params.Add(new SqlParameter() { ParameterName = "Kol", Value = Kol });
        //        Params.Add(new SqlParameter() { ParameterName = "PONo", Value = PONo });
        //        Result = SQLHelpers.ExecQueryWithValue("PO_W_ApprovePO", CommandType.StoredProcedure, Params, db.connectionString);

        //        return Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public int ApprovePO(string UserName, int Kol, string PONo)
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
                                List<SqlParameter> Params = new List<SqlParameter>();
                                Params.Add(new SqlParameter() { ParameterName = "UserName", Value = UserName });
                                Params.Add(new SqlParameter() { ParameterName = "Kol", Value = Kol });
                                Params.Add(new SqlParameter() { ParameterName = "PONo", Value = PONo });
                                Result = SQLHelpers.ExecQueryWithValue("PO_W_ApprovePO", CommandType.StoredProcedure, Params, db.connectionString);

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


        public int RejectPO(string UserName, int Kol, string PONo)
        {
            try
            {
                int Result = -1;

                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "UserName", Value = UserName });
                Params.Add(new SqlParameter() { ParameterName = "Kol", Value = Kol });
                Params.Add(new SqlParameter() { ParameterName = "PONo", Value = PONo });
                Result = SQLHelpers.ExecQueryWithValue("PO_W_RejectPO", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateUrgent(string PONo)
        {
            int Result = -1;

            try

            {
                string Query = @"UPDATE purchord SET User6='U' where PONbr = @PONo ";
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "PONo", Value = PONo });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int PrintPO(string PONo)
        {
            int Result = -1;

            try

            {
                string Query = @"UPDATE approvalpo1 SET email = 'SEND' where PONbr = @PONo ";
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "PONo", Value = PONo });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_PO> GetModelPONonConfirm(POAksesUser poakses)
        {
            //object[] Params = { "@UserName", poakses.user_id, "@Kol", poakses.Kol };
            return db.Read("[PO_W_GetPONonConfirm]", CommandType.StoredProcedure, MakePONonConfirm, null).ToList();
        }

        public List<BO_POExport> GetModelPOHistoryExport(POHistory filter)
        {
            try
            {
                string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                object[] Params = { "@DTDari", stDate, "@DTSampai", enDate, "@PONumber", filter.PONumber, "@PRNumber", filter.PRNumber, "@Supplier", filter.Supplier, "@Level", filter.Level };
                return db.Read("PO_W_GetListPRHistoryExport", CommandType.StoredProcedure, MakePOExport, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_POExport> GetModelPOHistory(POHistory filter)
        {
            try
            {
                string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                object[] Params = { "@DTDari", stDate, "@DTSampai", enDate, "@PONumber", filter.PONumber, "@PRNumber", filter.PRNumber, "@Supplier", filter.Supplier, "@Level", filter.Level };
                return db.Read("PO_W_GetListPOHistory", CommandType.StoredProcedure, MakePOHistoryNew, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateApproval(List<BO_PODetail> PODetail, string UserName, int Kol)
        {
            //try
            //{
            //    int Result = -1;
            //string Query = "";
            //foreach (var items in PODetail)
            //{
            //    var Komen = items.Komentar;
            //    //var Harga = items.Harga;
            //    if (string.IsNullOrEmpty(Komen))
            //    {
            //        Komen = "";
            //    }
            //    Query = @"Update PurOrdDet set Komentar =  @Komentar
            //                     where PONbr = @PONbr and LineID = @Line ";
            //    List<SqlParameter> Params = new List<SqlParameter>();
            //    Params.Add(new SqlParameter() { ParameterName = "PONbr", Value = items.PONumber });
            //    Params.Add(new SqlParameter() { ParameterName = "Line", Value = items.LineID });
            //    Params.Add(new SqlParameter() { ParameterName = "Komentar", Value = Komen });
            //    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);
                //    }
                //    return Result;
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}

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
                                var PoNbr = "";
                                foreach (var items in PODetail)
                                {
                                    PoNbr = items.PONumber;
                                }


                                List<SqlParameter> Params = new List<SqlParameter>();
                                Params.Add(new SqlParameter() { ParameterName = "UserName", Value = UserName });
                                Params.Add(new SqlParameter() { ParameterName = "Kol", Value = Kol });
                                Params.Add(new SqlParameter() { ParameterName = "PONo", Value = PoNbr });
                                SQLHelpers.ExecQueryWithValue("PO_W_ApprovePO", CommandType.StoredProcedure, Params, db.connectionString);

                                string Query = "";
                                foreach (var items in PODetail)
                                {
                                    var Komen = items.Komentar;
                                    //var Harga = items.Harga;
                                    if (string.IsNullOrEmpty(Komen))
                                    {
                                        Komen = "";
                                    }
                                    Query = @"Update PurOrdDet set Komentar =  @Komentar
                                                     where PONbr = @PONbr and LineID = @Line ";
                                    List<SqlParameter> Params1 = new List<SqlParameter>();
                                    Params1.Add(new SqlParameter() { ParameterName = "PONbr", Value = items.PONumber });
                                    Params1.Add(new SqlParameter() { ParameterName = "Line", Value = items.LineID });
                                    Params1.Add(new SqlParameter() { ParameterName = "Komentar", Value = Komen });
                                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params1, db.connectionString);
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

        public int RejectApproval(List<BO_PODetail> PODetail, string UserName, int Kol)
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
                                var PoNbr = "";
                                foreach (var items in PODetail)
                                {
                                    PoNbr = items.PONumber;
                                }


                                List<SqlParameter> Params = new List<SqlParameter>();
                                Params.Add(new SqlParameter() { ParameterName = "UserName", Value = UserName });
                                Params.Add(new SqlParameter() { ParameterName = "Kol", Value = Kol });
                                Params.Add(new SqlParameter() { ParameterName = "PONo", Value = PoNbr });
                                SQLHelpers.ExecQueryWithValue("PO_W_RejectPO", CommandType.StoredProcedure, Params, db.connectionString);

                                string Query = "";
                                foreach (var items in PODetail)
                                {
                                    var Komen = items.Komentar;
                                    //var Harga = items.Harga;
                                    if (string.IsNullOrEmpty(Komen))
                                    {
                                        Komen = "";
                                    }
                                    Query = @"Update PurOrdDet set Komentar =  @Komentar
                                                     where PONbr = @PONbr and LineID = @Line ";
                                    List<SqlParameter> Params1 = new List<SqlParameter>();
                                    Params1.Add(new SqlParameter() { ParameterName = "PONbr", Value = items.PONumber });
                                    Params1.Add(new SqlParameter() { ParameterName = "Line", Value = items.LineID });
                                    Params1.Add(new SqlParameter() { ParameterName = "Komentar", Value = Komen });
                                    Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params1, db.connectionString);
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

        public List<BO_PO> GetModelPOSupplierApprove(POAksesUser poakses)
        {
            return db.Read("[PO_W_GetSupplierApprove]", CommandType.StoredProcedure, MakePONonConfirm, null).ToList();
        }

        public int SendEmail(string PONo)
        {
            int Result = -1;




            //        //    //string Query = @"UPDATE approvalpo1 SET email = 'Print PO',[tgl_mail] = '" + DateTime.Now + "' where PONbr = @PONo ";
            //        //    //List<SqlParameter> Params = new List<SqlParameter>();
            //        //    //Params.Add(new SqlParameter() { ParameterName = "PONo", Value = PONo });
            //        //    //Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);




            //MailMessage message = new MailMessage();
            //message.To.Add("tsm.miftahudin@gmail.com");
            //message.From = new MailAddress("circulation@tsmu.co.id");
            //message.Subject = "PO";
            //message.SubjectEncoding = System.Text.Encoding.UTF8;
            //message.Body = "PO Email";
            //message.BodyEncoding = System.Text.Encoding.UTF8;
            //message.IsBodyHtml = true;
            //message.Priority = MailPriority.High;
            //message.CC.Add("log@tsmu.co.id");

            //using (var client = new SmtpClient())
            //{
            //    client.Credentials = new System.Net.NetworkCredential("circulation@tsmu.co.id", "BP41DLc{fPO4");
            //    client.Port = 465;
            //    client.Host = "mail.tsmu.co.id";
            //    client.EnableSsl = true;

            //    client.Send(message);
            //}


            //using (MailMessage mail = new MailMessage())
            //{
            //    string displayName = "Pemberitahuan";
            //    string email = "tsm.miftahdin@gmail.com";
            //    string password = "";
            //    string tujuan = "miftah-mis@tsmu.co.id";

            //    mail.From = new MailAddress(email, displayName, System.Text.Encoding.UTF8);
            //    mail.To.Add(new MailAddress(tujuan));
            //    mail.SubjectEncoding = System.Text.Encoding.UTF8;
            //    mail.BodyEncoding = System.Text.Encoding.UTF8;
            //    mail.IsBodyHtml = true;
            //    mail.Priority = MailPriority.High;
            //    mail.Subject = "Subjek email";
            //    mail.Body = "Isi email";
            //    mail.CC.Add(new MailAddress("log@tsmu.co.id"));

            //    using (var smtpClient = new SmtpClient("smtp.gmail.com", 465))
            //    {
            //        smtpClient.Credentials = new NetworkCredential(email, password);
            //        smtpClient.EnableSsl = true;
            //        smtpClient.Send(mail);
            //    }
            //}

            try
            {
                //SmtpMail oMail = new SmtpMail("TryIt");

                //// Set sender email address, please change it to yours
                //oMail.From = "circulation@tsmu.co.id";

                //// Set recipient email address, please change it to yours
                //oMail.To = "tsm.miftahudin@gmail.com";
                //oMail.Cc = "log@tsmu.co.id";

                //// Set email subject
                //oMail.Subject = "test email from c#, ssl, 465 port";

                //// Set email body
                //oMail.TextBody = "this is a test email sent from c# project, do not reply";

                //// Your SMTP server address
                //SmtpServer oServer = new SmtpServer("mail.tsmu.co.id");

                //// User and password for ESMTP authentication, if your server doesn't require
                //// User authentication, please remove the following codes.
                //oServer.User = "circulation@tsmu.co.id";
                //oServer.Password = "BP41DLc{fPO4";

                //// Set 465 SMTP port
                //oServer.Port = 465;

                //// Enable SSL connection
                //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                //Console.WriteLine("start to send email ...");

                //SmtpClient oSmtp = new SmtpClient();
                //oSmtp.SendMail(oServer, oMail);

            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }



            return 1;
        }

        static readonly Func<IDataReader, BO_PO> MakePO = reader =>
            new BO_PO
            {
                PONumber = reader["PONbr"].AsString(),
                CreateUser = reader["crtd_user"].AsString(),
                PODate = reader["PODate"].AsString(),
                ClassID = reader["ClassID"].AsString(),
                VendorName = reader["VendName"].AsString(),
                User6 = reader["User6"].AsString()
            };

        static readonly Func<IDataReader, BO_PO> MakePOHistory = reader =>
            new BO_PO
            {
                PONumber = reader["PONbr"].AsString(),
                CreateUser = reader["crtd_user"].AsString(),
                PODate = reader["PODate"].AsString(),
                ClassID = reader["ClassID"].AsString(),
                VendorName = reader["VendName"].AsString(),
                User6 = reader["User6"].AsString(),
                Email = reader["Email"].AsString()

            };

        static readonly Func<IDataReader, BO_POExport> MakePOHistoryNew = reader =>
            new BO_POExport
            {
                PONbr = reader["PONbr"].AsString(),
                PromDate = reader["PromDate"].AsDateTime(),
                PODate = reader["PODate"].AsDateTime(),
                LineID = reader["LineID"].AsString(),
                PRNo = reader["PRNo"].AsString(),
                VendId = reader["VendId"].AsString(),
                Name = reader["Name"].AsString(),
                InvtID = reader["InvtID"].AsString(),
                AlternateID = reader["AlternateID"].AsString(),
                TranDesc = reader["TranDesc"].AsString(),
                PurchUnit = reader["PurchUnit"].AsString(),
                QtyOrd = reader["QtyOrd"].AsDouble(),
                QtyRcvd = reader["QtyRcvd"].AsDouble(),
                CuryID = reader["CuryID"].AsString(),
                CuryUnitCost = reader["CuryUnitCost"].AsDouble(),
                Level = reader["Level"].AsString(),
                Note = reader["Note"].AsString(),
                CirculationNo = reader["CirculationNo"].AsString(),
                SiteId = reader["SiteID"].AsString(),
                Terms = reader["Terms"].AsString()

            };

        static readonly Func<IDataReader, BO_PO> MakePOByUser = reader =>
           new BO_PO
           {
               PONumber = reader["PONbr"].AsString(),
               CreateUser = reader["crtd_user"].AsString(),
               PODate = reader["PODate"].AsString(),
               ClassID = reader["ClassID"].AsString(),
               VendorName = reader["VendName"].AsString(),
               User6 = reader["User6"].AsString(),
               Level1 = reader["Level1"].AsString(),
               Level2 = reader["Level2"].AsString(),
               Level3 = reader["Level3"].AsString(),
               Level4 = reader["Level4"].AsString(),

           };

        static readonly Func<IDataReader, BO_PO> MakePOApproveSM = reader =>
          new BO_PO
          {
              PONumber = reader["PONbr"].AsString(),
              CreateUser = reader["crtd_user"].AsString(),
              PODate = reader["PODate"].AsString(),
              ClassID = reader["ClassID"].AsString(),
              VendorName = reader["VendName"].AsString(),
              User6 = reader["User6"].AsString(),
              Level1 = reader["Level1"].AsString()
          };

        static readonly Func<IDataReader, BO_PO> MakePONonConfirm = reader =>
        new BO_PO
        {
            PONumber = reader["PONbr"].AsString(),
            CreateUser = reader["crtd_user"].AsString(),
            PODate = reader["PODate"].AsString(),
            ClassID = reader["ClassID"].AsString(),
            VendorName = reader["VendName"].AsString(),
            User6 = reader["User6"].AsString(),
            Email = reader["email"].AsString(),
            TglEmail = reader["tgl_mail"].AsString()

        };

        static readonly Func<IDataReader, BO_PO> MakePOHeader = reader =>
           new BO_PO

           {

               PONumber = reader["PONbr"].AsString(),
               //CreateUser = reader["PODate"].AsString(),
               PODate = reader["PODate"].AsString(),
               //ClassID = reader["ClassID"].AsString(),
               VendorName = reader["VendName"].AsString(),
               VendAttn = reader["VendAttn"].AsString(),
               VendAddr1 = reader["VendAddr1"].AsString(),
               VendAddr2 = reader["VendAddr2"].AsString(),
               VendCity = reader["VendCity"].AsString(),
               VendPhone = reader["VendPhone"].AsString(),
               VendFax = reader["VendFax"].AsString(),
               ShipName = reader["ShipName"].AsString(),
               ShipAddr1 = reader["ShipAddr1"].AsString(),
               ShipAddr2 = reader["ShipAddr2"].AsString(),
               ShipCity = reader["ShipCity"].AsString(),
               ShipState = reader["ShipState"].AsString(),
               ShipZip = reader["ShipZip"].AsString(),
               ShipCountry = reader["ShipCountry"].AsString(),
               Negara = reader["Negara"].AsString(),
               Terms = reader["Terms"].AsString(),
               BillName = reader["BillName"].AsString(),
               BillAttn = reader["BillAttn"].AsString(),
               BillAddr1 = reader["BillAddr1"].AsString(),
               BillAddr2 = reader["BillAddr2"].AsString(),
               BillCity = reader["BillCity"].AsString(),
               BillState = reader["BillState"].AsString(),
               BillZip = reader["BillZip"].AsString(),
               BillPhone = reader["BillPhone"].AsString(),
               BillFax = reader["BillFax"].AsString(),
               BillCountry = reader["BillCountry"].AsString(),
               CuryID = reader["CuryID"].AsString(),
               Negara2 = reader["Negara2"].AsString(),
               User5 = reader["User5"].AsString(),
               Tax = reader["Tax"].AsString(),
               TaxF = reader["Taxf"].AsString(),
               SNote = reader["Note"].AsString()

           };

        static readonly Func<IDataReader, BO_PO> MakePOtotalAmount = reader =>
          new BO_PO

          {
              TotalAmount = reader["TotalAmount"].AsDouble()
          };

        static readonly Func<IDataReader, POAksesUser> MakeAkses = reader =>
          new POAksesUser
          {
              user_id = reader["user_id"].AsString(),
              Kol = reader["kol"].AsInt(),
              VendId = reader["VendId"].AsString(),
              Email = reader["email"].AsString(),
              Jenis = reader["jenis"].AsString(),
          };

        static readonly Func<IDataReader, BO_PODetail> MakePODetail = reader =>
          new BO_PODetail

          {

              PONumber = reader["PONbr"].AsString(),
              LineID = reader["LineID"].AsString(),
              InvtID = reader["InvtID"].AsString(),
              Description = reader["Description"].AsString(),
              PRNo = reader["PRNo"].AsString(),
              PromDate = reader["PromDate"].AsString(),
              Account = reader["Account"].AsString(),
              ReqNbr = reader["ReqNbr"].AsString(),
              Unit = reader["Unit"].AsString(),
              QtyOrd = reader["QtyOrd"].AsDouble(),
              CuryUnitCost = reader["CuryUnitCost"].AsDouble(),
              total = reader["total"].AsDouble(),
              No = reader["No"].AsString(),
              Komentar = reader["Komentar"].AsString(),
              cek = "checked",
              User5 = reader["User5"].AsString(),

          };

        static readonly Func<IDataReader, BO_POExport> MakePOExport = reader =>
         new BO_POExport

         {

             //PONumber = reader["PONbr"].AsString(),
             PONbr = reader["PONbr"].AsString(),
             PODate = reader["PODate"].AsDateTime(),
             Name = reader["Name"].AsString(),
             ShipName = reader["ShipName"].AsString(),
             ShipAddr1 = reader["ShipAddr1"].AsString(),
             ShipAddr2 = reader["ShipAddr2"].AsString(),
             ShipState = reader["ShipState"].AsString(),
             ShipZip = reader["ShipZip"].AsString(),
             ShipCountry = reader["ShipCountry"].AsString(),
             Negara = reader["Negara"].AsString(),
             Terms = reader["Terms"].AsString(),
             BillName = reader["BillName"].AsString(),
             BillAttn = reader["BillAttn"].AsString(),
             BillAddr1 = reader["BillAddr1"].AsString(),
             BillAddr2 = reader["BillAddr2"].AsString(),
             BillCity = reader["BillCity"].AsString(),
             BillZip = reader["BillZip"].AsString(),
             BillState = reader["BillState"].AsString(),
             BillPhone = reader["BillPhone"].AsString(),
             BillFax = reader["BillFax"].AsString(),
             BillCountry = reader["BillCountry"].AsString(),
             CuryID = reader["CuryID"].AsString(),
             User5 = reader["User5"].AsString(),
             Negara2 = reader["Negara2"].AsString(),
             LineID = reader["LineID"].AsString(),
             InvtID = reader["InvtID"].AsString(),
             TranDesc = reader["TranDesc"].AsString(),
             User1 = reader["User1"].AsString(),
             PromDate = reader["PromDate"].AsDateTime(),
             QtyOrd = reader["QtyOrd"].AsDouble(),
             PurchUnit = reader["PurchUnit"].AsString(),
             CuryUnitCost = reader["CuryUnitCost"].AsDouble(),
             totb = reader["totb"].AsString(),
             ReqNbr = reader["ReqNbr"].AsString(),
             VendId = reader["VendId"].AsString(),

         };
    }
}
