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
    public class MRPDao : IMRPDao
    {
        static Db db = new Db();

        public List<MRPForecastCust> GetListForecatCust(string Year)
        {
            try
            {
                object[] Params = { "@Year", Year };
                return db.Read("MRP_GetListForecastCust", CommandType.StoredProcedure, MakeForecaseCust, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MRPForecastModel> GetListForecatModel(string Year, string CustID)
        {
            try
            {
                object[] Params = { "@Year", Year, "@CustID", CustID };
                return db.Read("MRP_GetListForecastModel", CommandType.StoredProcedure, MakeForecaseModel, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MRPForecastDetail> GetListForecatDetail(string Year, string CustID, string Model)
        {
            try
            {
                object[] Params = { "@Year", Year, "@CustID", CustID, "@Model", Model };
                return db.Read("MRP_GetListForecastDetail", CommandType.StoredProcedure, MakeForecaseDetail, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MRPBoMCalculate> GetListBoMCalculate(string Year, string Site)
        {
            try
            {
                object[] Params = { "@Year", Year, "@Site", Site };
                return db.Read("MRP_GetListBoMCalculate", CommandType.StoredProcedure, MakeBoMCalculate, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MRPBoMCalculateDetail> GetListBoMCalculateDetail(string Year, string Site, string MaterialID)
        {
            try
            {
                object[] Params = { "@Year", Year, "@Site", Site, "@MaterialID", MaterialID };
                return db.Read("MRP_GetListBoMCalculateDetail", CommandType.StoredProcedure, MakeBoMCalculateDetail, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static readonly Func<IDataReader, MRPForecastCust> MakeForecaseCust = reader =>
          new MRPForecastCust
          {
              CustID = reader["CustID"].AsString(),
              Customer = reader["Customer"].AsString(),
              FCJan = reader["FCJan"].AsInt(),
              FCFeb = reader["FCFeb"].AsInt(),
              FCMar = reader["FCMar"].AsInt(),
              FCApr = reader["FCApr"].AsInt(),
              FCMei = reader["FCMei"].AsInt(),
              FCJun = reader["FCJun"].AsInt(),
              FCJul = reader["FCJul"].AsInt(),
              FCAgt = reader["FCAgt"].AsInt(),
              FCSep = reader["FCSep"].AsInt(),
              FCOkt = reader["FCOkt"].AsInt(),
              FCNov = reader["FCNov"].AsInt(),
              FCDes = reader["FCDes"].AsInt()
          };

        static readonly Func<IDataReader, MRPForecastModel> MakeForecaseModel = reader =>
          new MRPForecastModel
          {
              CustID = reader["CustID"].AsString(),
              Customer = reader["Customer"].AsString(),
              Model = reader["Model"].AsString(),
              FCJan = reader["FCJan"].AsInt(),
              FCFeb = reader["FCFeb"].AsInt(),
              FCMar = reader["FCMar"].AsInt(),
              FCApr = reader["FCApr"].AsInt(),
              FCMei = reader["FCMei"].AsInt(),
              FCJun = reader["FCJun"].AsInt(),
              FCJul = reader["FCJul"].AsInt(),
              FCAgt = reader["FCAgt"].AsInt(),
              FCSep = reader["FCSep"].AsInt(),
              FCOkt = reader["FCOkt"].AsInt(),
              FCNov = reader["FCNov"].AsInt(),
              FCDes = reader["FCDes"].AsInt()
          };

        static readonly Func<IDataReader, MRPForecastDetail> MakeForecaseDetail = reader =>
          new MRPForecastDetail
          {
              CustID = reader["CustID"].AsString(),
              Customer = reader["Customer"].AsString(),
              Model = reader["Model"].AsString(),
              InvtID = reader["InvtID"].AsString(),
              Nama = reader["Nama"].AsString(),
              FCJan = reader["FCJan"].AsInt(),
              FCFeb = reader["FCFeb"].AsInt(),
              FCMar = reader["FCMar"].AsInt(),
              FCApr = reader["FCApr"].AsInt(),
              FCMei = reader["FCMei"].AsInt(),
              FCJun = reader["FCJun"].AsInt(),
              FCJul = reader["FCJul"].AsInt(),
              FCAgt = reader["FCAgt"].AsInt(),
              FCSep = reader["FCSep"].AsInt(),
              FCOkt = reader["FCOkt"].AsInt(),
              FCNov = reader["FCNov"].AsInt(),
              FCDes = reader["FCDes"].AsInt()
          };

        static readonly Func<IDataReader, MRPBoMCalculate> MakeBoMCalculate = reader =>
          new MRPBoMCalculate
          {
              Account = reader["Account"].AsString(),
              Site = reader["Site"].AsString(),
              InvtID = reader["InvtID"].AsString(),
              Nama = reader["Nama"].AsString(),
              Unit = reader["Unit"].AsString(),
              FCJan = reader["FCJan"].AsDouble(),
              FCFeb = reader["FCFeb"].AsDouble(),
              FCMar = reader["FCMar"].AsDouble(),
              FCApr = reader["FCApr"].AsDouble(),
              FCMei = reader["FCMei"].AsDouble(),
              FCJun = reader["FCJun"].AsDouble(),
              FCJul = reader["FCJul"].AsDouble(),
              FCAgt = reader["FCAgt"].AsDouble(),
              FCSep = reader["FCSep"].AsDouble(),
              FCOkt = reader["FCOkt"].AsDouble(),
              FCNov = reader["FCNov"].AsDouble(),
              FCDes = reader["FCDes"].AsDouble()
          };

        static readonly Func<IDataReader, MRPBoMCalculateDetail> MakeBoMCalculateDetail = reader =>
          new MRPBoMCalculateDetail
          {
              ParentID = reader["ParentID"].AsString(),
              ParentIDLvl0 = reader["ParentID0"].AsString(),
              Desc = reader["Desc"].AsString(),
              FCJan = reader["FCJan"].AsDouble(),
              FCFeb = reader["FCFeb"].AsDouble(),
              FCMar = reader["FCMar"].AsDouble(),
              FCApr = reader["FCApr"].AsDouble(),
              FCMei = reader["FCMei"].AsDouble(),
              FCJun = reader["FCJun"].AsDouble(),
              FCJul = reader["FCJul"].AsDouble(),
              FCAgt = reader["FCAgt"].AsDouble(),
              FCSep = reader["FCSep"].AsDouble(),
              FCOkt = reader["FCOkt"].AsDouble(),
              FCNov = reader["FCNov"].AsDouble(),
              FCDes = reader["FCDes"].AsDouble()
          };
    }
}
