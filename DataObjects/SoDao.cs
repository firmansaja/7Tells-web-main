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
    public class SoDao : ISoDao
    {
        static Db db = new Db();
        public List<BO_So> GetOs_SO(string CustOrdNbr)
        {
            try
            {

                object[] Params = { "@CustOrdNbr", CustOrdNbr };
                return db.Read("[SO_GetOutstanding_ById]", CommandType.StoredProcedure, MakeOS_So, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_So> GetByOrdeNbr(string OrdNbr)
        {
            try
            {

                object[] Params = { "@OrdNbr", OrdNbr };
                return db.Read("[SO_GetByOrdeNbr]", CommandType.StoredProcedure, MakeSO_GetByOrdeNbr, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static readonly Func<IDataReader, BO_So> MakeOS_So = reader =>
      new BO_So
      {

          CustOrdNbr = reader["CustOrdNbr"].AsString(),
          OrdNbr = reader["OrdNbr"].AsString(),
          ShipName = reader["ShipName"].AsString(),
          InvtID = reader["InvtID"].AsString(),
          Descr = reader["Descr"].AsString(),
          AlternateID = reader["AlternateID"].AsString(),
          ReqDate = reader["ReqDate"].AsDateTime(),
          QtyOrd = reader["QtyOrd"].AsDouble(),
          QtyOpenShip = reader["QtyOpenShip"].AsDouble(),
          QtyCloseShip = reader["QtyCloseShip"].AsDouble(),
          QtyShip = reader["QtyShip"].AsDouble(),
          QtyBal = reader["QtyBal"].AsDouble(),
          SiteID = reader["SiteID"].AsString(),
          NoSPAB = reader["NoSPAB"].AsString(),
          Tujuan = reader["Tujuan"].AsString(),
          No = reader["No"].AsInt()

      };


        static readonly Func<IDataReader, BO_So> MakeSO_GetByOrdeNbr = reader =>
        new BO_So
        {

            CustOrdNbr = reader["CustOrdNbr"].AsString(),
            OrdNbr = reader["OrdNbr"].AsString(),
            No = reader["No"].AsInt()

        };

    }
}
