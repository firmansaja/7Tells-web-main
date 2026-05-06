using BusinessObjects;
using DataObjects.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataObjects
{
    public class UserDao : IUserDao
    {
        private static Db db = new Db();

        public User GetUser(string Username, string Password)
        {
            try
            {
                string Query = @"SELECT
                                    ROW_NUMBER() OVER (
                                 ORDER BY Username
                                    ) No,
                                    Username, UserGroupCode, Site, UserId, RoleID, Email
                                FROM S_User
                                WHERE Username = @Username AND Password = HASHBYTES('SHA1', CAST(@Password AS VARCHAR(100)))";
                object[] Params = { "@Username", Username, "@Password", Password };
                //List<SqlParameter> Params = new List<SqlParameter>();
                //Params.Add(new SqlParameter() { ParameterName = "Username", Value = Username });
                //Params.Add(new SqlParameter() { ParameterName = "Password", Value = Password });
                return db.Read(Query, CommandType.Text, MakeLogin, Params).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                return db.Read("S_User_GetUser", CommandType.StoredProcedure, Make).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetUsers1()
        {
            try
            {
                List<User> Result = new List<User>();
                string Query = @"SELECT
                                    ROW_NUMBER() OVER (
	                                ORDER BY Username
                                    ) No,
                                    Username, UserGroupCode, Site, Email
                                FROM S_User";
                DataTable dt = new DataTable();
                dt = SQLHelpers.GetDataTableByParam(Query, CommandType.Text, null, db.connectionString);
                foreach (DataRow dr in dt.Rows)
                {
                    Result.Add(new User
                    {
                        No = dr["No"].AsInt(),
                        Username = dr["Username"].AsString(),
                        UserGroupCode = dr["UserGroupCode"].AsString(),
                        Site = dr["Site"].AsString(),
                        Email = dr["Email"].AsString()
                    });
                }
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetUserById(string Username)
        {
            try
            {
                List<User> Result = new List<User>();
                string Query = @"SELECT
                                    ROW_NUMBER() OVER (
	                                ORDER BY Username
                                    ) No,
                                    Username, UserGroupCode, Site, UserId, RoleID, Email
                                FROM S_User
                                WHere Username = @Username";
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "Username", Value = Username }
                };
                DataTable dt = new DataTable();
                dt = SQLHelpers.GetDataTableByParam(Query, CommandType.Text, Params, db.connectionString);
                foreach (DataRow dr in dt.Rows)
                {
                    Result.Add(new User
                    {
                        No = dr["No"].AsInt(),
                        Username = dr["Username"].AsString(),
                        UserGroupCode = dr["UserGroupCode"].AsString(),
                        Site = dr["Site"].AsString(),
                        UserId = dr["UserId"].AsGuid(),
                        RoleID = dr["RoleID"].AsGuid(),
                        Email = dr["Email"].AsString()
                    });
                }
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(User user)
        {
            try
            {
                int Result = -1;
                string Query = @"Update S_User
                                 Set RoleID=@RoleID
                                 WHere Username = @Username";
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "Username", Value = user.Username });
                Params.Add(new SqlParameter() { ParameterName = "RoleID", Value = user.RoleID });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static readonly Func<IDataReader, User> MakeLogin = reader =>
              new User
              {
                  No = reader["No"].AsInt(),
                  UserId = reader["UserId"].AsGuid(),
                  RoleID = reader["RoleID"].AsGuid(),
                  Username = reader["Username"].AsString(),
                  Email = reader["Email"].AsString(),
                  Site = reader["Site"].AsString(),
                  UserGroupCode = reader["UserGroupCode"].AsString()
              };

        private static readonly Func<IDataReader, User> Make = reader =>
              new User
              {
                  No = reader["No"].AsInt(),
               //UserId = reader["UserId"].AsGuid(),
               //RoleID = reader["RoleID"].AsGuid(),
               Username = reader["Username"].AsString(),
                  Email = reader["Email"].AsString(),
                  Site = reader["Site"].AsString(),
                  UserGroupCode = reader["UserGroupCode"].AsString()
              };
    }
}