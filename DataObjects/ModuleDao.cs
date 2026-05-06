using BusinessObjects;
using DataObjects.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataObjects
{
    public class ModuleDao : IModuleDao
    {
        private static Db db = new Db();

        public int Delete(Module module)
        {
            try
            {
                int Result = -1;
                string Query = @"Delete From TblModule
                                 WHere ID = @ID";
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "ID", Value = module.ID });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Module GetModuleById(int Id)
        {
            string sql =
            @" SELECT *
                 FROM TblModule
                WHERE ID = @Id";
            object[] parms = { "@Id", Id };
            return db.Read(sql, CommandType.Text, MakeAll, parms).FirstOrDefault();
        }

        public List<Module> GetModuleById(string Username)
        {
            throw new NotImplementedException();
        }

        public List<Module> GetModules()
        {
            try
            {
                object[] Params = { "@ID", null };
                return db.Read("ModuleList", CommandType.StoredProcedure, Make, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(Module module)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "DisplayOrder", Value = module.DisplayOrder },
                    new SqlParameter() { ParameterName = "ModuleName", Value = module.ModuleName },
                    new SqlParameter() { ParameterName = "PageIcon", Value = module.PageIcon },
                    new SqlParameter() { ParameterName = "PageUrl", Value = module.PageUrl },
                    new SqlParameter() { ParameterName = "PageSlug", Value = module.PageSlug },
                    new SqlParameter() { ParameterName = "IsDeleted", Value = false },
                    new SqlParameter() { ParameterName = "IsActive", Value = true },
                    new SqlParameter() { ParameterName = "IsDefault", Value = false },
                    new SqlParameter() { ParameterName = "ParentModuleID", Value = module.ParentModuleID == null ? 0 : module.ParentModuleID }
                };
                Result = SQLHelpers.ExecQueryWithValue("TblModule_Insert", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Module Module)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static readonly Func<IDataReader, Module> MakeAll = reader =>
              new Module
              {
                  ID = reader["id"].AsInt(),
                  DisplayOrder = reader["DisplayOrder"].AsInt(),
                  ModuleName = reader["ModuleName"].AsString(),
                  PageIcon = reader["PageIcon"].AsString(),
                  PageUrl = reader["PageUrl"].AsString(),
                  PageSlug = reader["PageSlug"].AsString(),
                  IsDeleted = reader["IsDeleted"].AsBool(),
                  IsActive = reader["IsActive"].AsBool(),
                  IsDefault = reader["IsDefault"].AsBool(),
                  ParentModuleID = reader["ParentModuleID"].AsInt()
              };

        private static readonly Func<IDataReader, Module> Make = reader =>
              new Module
              {
                  ID = reader["id"].AsInt(),
                  ModuleName = reader["ModuleName"].AsString(),
                  FullName = reader["FullName"].AsString(),
                  IsActive = reader["IsActive"].AsBool(),
                  DisplayOrder = reader["DisplayOrder"].AsInt()
              };
    }
}