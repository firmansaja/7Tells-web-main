using BusinessObjects;
using DataObjects.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataObjects
{
    internal class ComboListDao : IComboList
    {
        private static Db db = new Db();

        public List<ComboList> GetRole()
        {
            try
            {
                string Query = @"SELECT
                                    ID [Value], Name [Text]
                                FROM TblRole
                                Where IsDeleted=0
                                ORDER By Name";
                return db.Read(Query, CommandType.Text, Make).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComboList> GetModuleParent()
        {
            try
            {
                string Query = @"SELECT
                                    ID [Value], ModuleName [Text]
                                FROM TblModule
                                Where IsDeleted = 0 AND ParentModuleID = 0
                                ORDER By DisplayOrder";
                return db.Read(Query, CommandType.Text, Make).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComboList> GetDeptPR()
        {
            try
            {
                string Query = @"SELECT DISTINCT
                                        SUBSTRING(PRNo, 2, 3) AS Value ,
                                        SUBSTRING(PRNo, 2, 3) AS [Text]
                                FROM    TSC22Application.dbo.XPRHdr
                                WHERE   YEAR(PRDate) >= 2021";
                return db.Read(Query, CommandType.Text, Make).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ComboList> GetSubAcct(string UserName)
        {
            try
            {
                string Query = @"SELECT DISTINCT
                                        SubAcct AS Value ,
                                        --SubAcct AS [Text]
                                        SubAcct + ' - ' + ISNULL(sa.Descr, SubAcct) AS [Text]
                                FROM    dbo.S_AksesApproval AS saa
                                        LEFT JOIN TSC22Application.dbo.SubAcct AS sa ON sa.Sub = saa.SubAcct
                                WHERE   ISNULL(SubAcct, '') <> ''
                                        AND UserName = '" + UserName + "'";
                return db.Read(Query, CommandType.Text, Make).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ComboList> GetSupplier()
        {
            try
            {
                string Query = @"select distinct
                                vendid AS Value  ,
                                name AS [Text] 
                                from 
                                TSC22Application.dbo.vendor order by name";
                return db.Read(Query, CommandType.Text, Make).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ComboList> GetSubMenuPR(string Akses)
        {
            try
            {
                string Query = @"SELECT  Code AS Value ,
                                        [Desc] AS [Text]
                                FROM    dbo.S_PRSubMenu
                                WHERE   Akses IN ( '*', '" + Akses + "' ) ORDER BY Code";

                return db.Read(Query, CommandType.Text, Make).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ComboList> GetKategori()
        {
            try
            {
                string Query = @"SELECT
                                    IdKategori AS [Value], Kategori AS [Text]
                                FROM e_KategoriSurat
                                ORDER By Id";
                return db.Read(Query, CommandType.Text, Make).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ComboList> GetDept()
        {
            try
            {
                string Query = @"SELECT  IdDept AS Value ,
                                        NamaDept AS [Text]
                                FROM    dbo.mDept;";
                return db.Read(Query, CommandType.Text, Make).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        static readonly Func<IDataReader, ComboList> Make = reader =>
           new ComboList
           {
               Value = reader["Value"].AsString(),
               Text = reader["Text"].AsString()
           };
    }
}