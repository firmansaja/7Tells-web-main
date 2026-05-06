using BusinessObjects.PPAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessObjects;
using DataObjects.Helpers;

namespace DataObjects
{
    public class PPAPDao : IPPAPDao
    {
        static Db db = new Db();

        public List<PPAPHeaderModel> GetPPAP()
        {
            try
            {
                return db.Read("usp_PPAPHeaderGet", CommandType.StoredProcedure, MakePPAP, null).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PPAPHeaderModel GetPPAPById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPHeaderByIdGet", CommandType.StoredProcedure, MakePPAPHeader, Params).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPDetailModel> GetPPAPDetailById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPDetailByIdGet", CommandType.StoredProcedure, MakePPAPDetail, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPCheckItemModel> GetPPAPCheckItemById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPRptCheckItemByIdGet", CommandType.StoredProcedure, MakePPAPCheckItem, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPAksesDeptModel> GetPPAPAksesDept(string username)
        {
            try
            {
                object[] Params = { "@Username", username };
                return db.Read("usp_PPAPAksesDeptGet", CommandType.StoredProcedure, MakePPAPAkses, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPAttendanceModel> GetPPAPAttendanceById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPAttendantByIdGet", CommandType.StoredProcedure, MakePPAPAttendance, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPApprovalDeptModel> GetPPAPApprovalDeptById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPApprovalDeptByIdGet", CommandType.StoredProcedure, MakePPAPApprovalDept, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPToolingModel> GetPPAPToolingById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPToolingByIdGet", CommandType.StoredProcedure, MakePPAPTooling, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPQualityModel> GetPPAPQualityById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPQualityByIdGet", CommandType.StoredProcedure, MakePPAPQuality, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPPreparationModel> GetPPAPPreparationById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPPreparationByIdGet", CommandType.StoredProcedure, MakePPAPPreparation, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPDocumentModel> GetPPAPDocumentById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPDocumentByIdGet", CommandType.StoredProcedure, MakePPAPDocument, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPDocumentDetailModel> GetPPAPDocumentDetailByItem(string noPPAP, string idStatus, string code)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus, "@Code", code };
                return db.Read("usp_PPAPDocumentDetailByItemGet", CommandType.StoredProcedure, MakePPAPDocumentDetail, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<PPAPRptDocumentModel> GetPPAPRptDocumentById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPRptDocumentDetailByIdGet", CommandType.StoredProcedure, MakePPAPRptDocument, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<PPAPSupplierModel> GetPPAPSupplierById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPSupplierByIdGet", CommandType.StoredProcedure, MakePPAPSupplier, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPPackingModel> GetPPAPPackingById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPPackingByIdGet", CommandType.StoredProcedure, MakePPAPPacking, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPComparationModel> GetPPAPComparationById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPComparationByIdGet", CommandType.StoredProcedure, MakePPAPComparation, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPPaintingModel> GetPPAPPaintingById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPPaintingHeaderByIdGet", CommandType.StoredProcedure, MakePPAPPainting, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPPaintingDetailModel> GetPPAPPaintingDetailById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPPaintingDetailByIdGet", CommandType.StoredProcedure, MakePPAPPaintingDetail, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPPaintingDetailModel> GetPPAPPaintingDetailByItem(string noPPAP, string idStatus, string code)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus, "@Code", code };
                return db.Read("usp_PPAPPaintingDetailByItemGet", CommandType.StoredProcedure, MakePPAPPaintingDetail, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PPAPAssemblyModel> GetPPAPAssemblyById(string noPPAP, string idStatus)
        {
            try
            {
                object[] Params = { "@NoPPAP", noPPAP, "@IdStatus", idStatus };
                return db.Read("usp_PPAPAssemblyByIdGet", CommandType.StoredProcedure, MakePPAPAssembly, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateApprovalDept(PPAPApprovalDeptModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "NoPPAP", Value = model.NoPPAP });
                Params.Add(new SqlParameter() { ParameterName = "IdStatus", Value = model.IdStatus });
                Params.Add(new SqlParameter() { ParameterName = "Dept", Value = model.Dept });
                Params.Add(new SqlParameter() { ParameterName = "Username", Value = model.Username });
                Params.Add(new SqlParameter() { ParameterName = "Judgment", Value = model.Judgment });
                Params.Add(new SqlParameter() { ParameterName = "Comment", Value = model.Comment });
                Result = SQLHelpers.ExecQueryWithValue("usp_PPAPApprovalDeptUpdate", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        static readonly Func<IDataReader, PPAPHeaderModel> MakePPAP = reader =>
           new PPAPHeaderModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Status = reader["Status"].AsString(),
               Site = reader["Site"].AsString(),
               CustID = reader["CustID"].AsString(),
               ProjectName = reader["ProjectName"].AsString(),
               ConfirmationDate = reader["ConfirmationDate"].AsDateTime(),
               MassproductionDate = reader["MassproductionDate"].AsDateTime(),
               OverallJudgment = reader["OverallJudgment"].AsString(),
               IsEdit = reader["IsEdit"].AsBool()
           };

        static readonly Func<IDataReader, PPAPAksesDeptModel> MakePPAPAkses = reader =>
           new PPAPAksesDeptModel
           {
               Username = reader["Username"].AsString(),
               NIK = reader["NIK"].AsInt(),
               Nama = reader["Nama"].AsString(),
               Site = reader["Site"].AsString(),
               Dept = reader["Dept"].AsString(),
               Level = reader["Level"].AsInt(),
               Item = reader["Item"].AsString(),
               Flag = reader["Flag"].AsBool(),
           };

        static readonly Func<IDataReader, PPAPHeaderModel> MakePPAPHeader = reader =>
           new PPAPHeaderModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Status = reader["Status"].AsString(),
               Site = reader["Site"].AsString(),
               CustID = reader["CustID"].AsString(),
               ProjectName = reader["ProjectName"].AsString(),
               ConfirmationDate = reader["ConfirmationDate"].AsDateTime(),
               MassproductionDate = reader["MassproductionDate"].AsDateTime(),
               OverallJudgment = reader["OverallJudgment"].AsString(),
               OverallComment = reader["OverallComment"].AsString(),
               Item = reader["Item"].AsString()
           };

        static readonly Func<IDataReader, PPAPDetailModel> MakePPAPDetail = reader =>
           new PPAPDetailModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Code = reader["Code"].AsString(),
               CodeParent = reader["CodeParent"].AsString(),
               Id = reader["Id"].AsInt(),
               Level = reader["Level"].AsInt(),
               GroupParent = reader["GroupParent"].AsString(),
               PartNumber = reader["PartNumber"].AsString(),
               PartName = reader["PartName"].AsString(),
               Forecast = reader["Forecast"].AsDouble(),
               Remark = reader["Remark"].AsString(),
               GroupID = reader["GroupID"].AsString()
           };

        static readonly Func<IDataReader, PPAPCheckItemModel> MakePPAPCheckItem = reader =>
           new PPAPCheckItemModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               IdItem = reader["IdItem"].AsInt(),
               Item = reader["Item"].AsString(),
               IsCheck = reader["IsCheck"].AsBool(),
               Countermeasure = reader["Countermeasure"].AsString(),
               DueDate = reader["DueDate"].AsString(),
               PIC = reader["PIC"].AsString(),
               Judge = reader["Judge"].AsString(),
               Remark = reader["Remark"].AsString(),
               Problem = reader["Problem"].AsString()
           };

        static readonly Func<IDataReader, PPAPAttendanceModel> MakePPAPAttendance = reader =>
           new PPAPAttendanceModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Seq = reader["Seq"].AsInt(),
               Nama = reader["Nama"].AsString(),
               Dept = reader["Dept"].AsString(),
               Jabatan = reader["Jabatan"].AsString()
           };

        static readonly Func<IDataReader, PPAPApprovalDeptModel> MakePPAPApprovalDept = reader =>
           new PPAPApprovalDeptModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Dept = reader["Dept"].AsString(),
               Descr = reader["Descr"].AsString(),
               Site = reader["Site"].AsString(),
               Username = reader["Username"].AsString(),
               Nama = reader["Nama"].AsString(),
               NIK = reader["NIK"].AsString(),
               Jabatan = reader["Jabatan"].AsString(),
               Judgment = reader["Judgment"].AsString(),
               Comment = reader["Comment"].AsString(),
               IsCheck = reader["IsCheck"].AsBool(),
               Tanggal = reader["Tanggal"].AsNullableDateTime()
           };

        static readonly Func<IDataReader, PPAPToolingModel> MakePPAPTooling = reader =>
           new PPAPToolingModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Code = reader["Code"].AsString(),
               CodeParent = reader["CodeParent"].AsString(),
               Id = reader["Id"].AsInt(),
               Level = reader["Level"].AsInt(),
               PartNumber = reader["PartNumber"].AsString(),
               PartName = reader["PartName"].AsString(),
               Data2D = reader["Data2D"].AsString(),
               DataRNECI = reader["DataRNECI"].AsString(),
               DataGrain = reader["DataGrain"].AsString(),
               MoldFabricationLocation = reader["MoldFabricationLocation"].AsString(),
               MoldProblem = reader["MoldProblem"].AsString(),
               MoldStatus = reader["MoldStatus"].AsString(),
               MoldTargetComplete = reader["MoldTargetComplete"].AsNullableDateTime(),
               ETFabricationLocation = reader["ETFabricationLocation"].AsString(),
               ETProblem = reader["ETProblem"].AsString(),
               ETStatus = reader["ETStatus"].AsString(),
               ETTargetComplete = reader["ETTargetComplete"].AsNullableDateTime(),
               CFFabricationLocation = reader["CFFabricationLocation"].AsString(),
               CFProblem = reader["CFProblem"].AsString(),
               CFStatus = reader["CFStatus"].AsString(),
               CFTargetComplete = reader["CFTargetComplete"].AsNullableDateTime(),
           };

        static readonly Func<IDataReader, PPAPQualityModel> MakePPAPQuality = reader =>
           new PPAPQualityModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Code = reader["Code"].AsString(),
               CodeParent = reader["CodeParent"].AsString(),
               Id = reader["Id"].AsInt(),
               Level = reader["Level"].AsInt(),
               PartNumber = reader["PartNumber"].AsString(),
               PartName = reader["PartName"].AsString(),
               CapabilityInjection = reader["CapabilityInjection"].AsString(),
               CapabilityPainting = reader["CapabilityPainting"].AsString(),
               CapabilityAssembling = reader["CapabilityAssembling"].AsString(),
               DataAccuracy = reader["DataAccuracy"].AsDouble(),
               DataN1 = reader["DataN1"].AsString(),
               LimitSampleStatus = reader["LimitSampleStatus"].AsString(),
               LimitSampleKeterangan = reader["LimitSampleKeterangan"].AsString(),
               ProblemPart = reader["ProblemPart"].AsString(),
               Remark = reader["Remark"].AsString(),
               TargetComplete = reader["TargetComplete"].AsNullableDateTime()
           };

        static readonly Func<IDataReader, PPAPPreparationModel> MakePPAPPreparation = reader =>
           new PPAPPreparationModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Code = reader["Code"].AsString(),
               CodeParent = reader["CodeParent"].AsString(),
               Id = reader["Id"].AsInt(),
               Level = reader["Level"].AsInt(),
               PartNumber = reader["PartNumber"].AsString(),
               PartName = reader["PartName"].AsString(),
               Material = reader["Material"].AsString(),
               ProsesPlanInjection = reader["ProsesPlanInjection"].AsString(),
               ProsesPlanPainting = reader["ProsesPlanPainting"].AsString(),
               ProsesPlanAssembling = reader["ProsesPlanAssembling"].AsString(),
               Machine = reader["Machine"].AsString(),
               ChuckRobot = reader["ChuckRobot"].AsString(),
               Tools = reader["Tools"].AsString(),
               JigPainting = reader["JigPainting"].AsString(),
               JigAssy = reader["JigAssy"].AsString(),
               MejaKerja = reader["MejaKerja"].AsString(),
               MappingSkillInjection = reader["MappingSkillInjection"].AsString(),
               MappingSkillPainting = reader["MappingSkillPainting"].AsString(),
               MappingSkillAssembling = reader["MappingSkillAssembling"].AsString(),
               Remark = reader["Remark"].AsString(),
               TargetComplete = reader["TargetComplete"].AsNullableDateTime()
           };

        static readonly Func<IDataReader, PPAPDocumentModel> MakePPAPDocument = reader =>
           new PPAPDocumentModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Code = reader["Code"].AsString(),
               CodeParent = reader["CodeParent"].AsString(),
               Id = reader["Id"].AsInt(),
               Level = reader["Level"].AsInt(),
               PartNumber = reader["PartNumber"].AsString(),
               PartName = reader["PartName"].AsString(),
               PIS = reader["PIS"].AsString(),
               CPFP = reader["CPFP"].AsString(),
               PFMEA = reader["PFMEA"].AsString(),
               IRDCheckSheet = reader["IRDCheckSheet"].AsString(),
               SCPInjection = reader["SCPInjection"].AsString(),
               SCPPainting = reader["SCPPainting"].AsString(),
               SCPAssembling = reader["SCPAssembling"].AsString(),
               InvtID = reader["InvtID"].AsString(),
               Remark = reader["Remark"].AsString(),
               TargetComplete = reader["TargetComplete"].AsNullableDateTime()
           };

        static readonly Func<IDataReader, PPAPDocumentDetailModel> MakePPAPDocumentDetail = reader =>
           new PPAPDocumentDetailModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Code = reader["Code"].AsString(),
               CodeItem = reader["CodeItem"].AsString(),
               Item = reader["Item"].AsString(),
               PIC = reader["PIC"].AsString(),
               Status = reader["Status"].AsString(),
               Pending = reader["Pending"].AsString(),
               DueDate = reader["DueDate"].AsNullableDateTime(),
               FileName = reader["FileName"].AsString(),
               No = reader["No"].AsNullableInt(),
               IsEdit = reader["IsEdit"].AsBool()
           };

        static readonly Func<IDataReader, PPAPRptDocumentModel> MakePPAPRptDocument = reader =>
           new PPAPRptDocumentModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Numbering = reader["Numbering"].AsString(),
               Code = reader["Code"].AsString(),
               PartNumber = reader["PartNumber"].AsString(),
               PartName = reader["PartName"].AsString(),
               InvtID = reader["InvtID"].AsString(),
               No = reader["No"].AsNullableInt(),
               CodeItem = reader["CodeItem"].AsString(),
               Item = reader["Item"].AsString(),
               PIC = reader["PIC"].AsString(),
               IsEdit = reader["IsEdit"].AsBool(),
               Status = reader["Status"].AsString(),
               Pending = reader["Pending"].AsString(),
               DueDate = reader["DueDate"].AsNullableDateTime()
           };

        static readonly Func<IDataReader, PPAPSupplierModel> MakePPAPSupplier = reader =>
           new PPAPSupplierModel
           {
               NoPPAP = reader["NoPPAP"].AsString(),
               IdStatus = reader["IdStatus"].AsInt(),
               Seq = reader["Seq"].AsInt(),
               PartNumber = reader["PartNumber"].AsString(),
               PartName = reader["PartName"].AsString(),
               PIS = reader["PIS"].AsString(),
               CPFP = reader["CPFP"].AsString(),
               IRDCheckSheet = reader["IRDCheckSheet"].AsString(),
               N1Data = reader["N1Data"].AsString(),
               LimitSample = reader["LimitSample"].AsString(),
               PackingSpec = reader["PackingSpec"].AsString(),
               PackingQty = reader["PackingQty"].AsInt(),
               Remark = reader["Remark"].AsString(),
               TargetComplete = reader["TargetComplete"].AsNullableDateTime()
           };

        static readonly Func<IDataReader, PPAPPackingModel> MakePPAPPacking = reader =>
          new PPAPPackingModel
          {
              NoPPAP = reader["NoPPAP"].AsString(),
              IdStatus = reader["IdStatus"].AsInt(),
              Code = reader["Code"].AsString(),
              CodeParent = reader["CodeParent"].AsString(),
              Id = reader["Id"].AsInt(),
              Level = reader["Level"].AsInt(),
              PartNumber = reader["PartNumber"].AsString(),
              PartName = reader["PartName"].AsString(),
              SPPS = reader["SPPS"].AsString(),
              PackingSpec = reader["PackingSpec"].AsString(),
              TypePacking = reader["TypePacking"].AsString(),
              Qty = reader["Qty"].AsInt(),
              PackingStandard = reader["PackingStandard"].AsString(),
              OrderPackingPlan = reader["OrderPackingPlan"].AsInt(),
              OrderPackingActual = reader["OrderPackingActual"].AsInt(),
              Remark = reader["Remark"].AsString(),
              TargetComplete = reader["TargetComplete"].AsNullableDateTime()
          };

        static readonly Func<IDataReader, PPAPComparationModel> MakePPAPComparation = reader =>
          new PPAPComparationModel
          {
              NoPPAP = reader["NoPPAP"].AsString(),
              IdStatus = reader["IdStatus"].AsInt(),
              Code = reader["Code"].AsString(),
              CodeParent = reader["CodeParent"].AsString(),
              Id = reader["Id"].AsInt(),
              Level = reader["Level"].AsInt(),
              PartNumber = reader["PartNumber"].AsString(),
              PartName = reader["PartName"].AsString(),
              TonaseMCMoldSpec = reader["TonaseMCMoldSpec"].AsString(),
              TonaseMCActual = reader["TonaseMCActual"].AsString(),
              TonaseMCNo = reader["TonaseMCNo"].AsString(),
              CycleTimeSpec = reader["CycleTimeSpec"].AsString(),
              CycleTimePlus = reader["CycleTimePlus"].AsString(),
              CycleTimeActual = reader["CycleTimeActual"].AsString(),
              PartWeightMinus = reader["PartWeightMinus"].AsString(),
              PartWeight2DData = reader["PartWeight2DData"].AsString(),
              PartWeightPlus = reader["PartWeightPlus"].AsString(),
              PartWeightActual = reader["PartWeightActual"].AsString()
          };

        static readonly Func<IDataReader, PPAPPaintingModel> MakePPAPPainting = reader =>
          new PPAPPaintingModel
          {
              NoPPAP = reader["NoPPAP"].AsString(),
              IdStatus = reader["IdStatus"].AsInt(),
              Code = reader["Code"].AsString(),
              PartNumber = reader["PartNumber"].AsString(),
              PartName = reader["PartName"].AsString(),
              ColorCode = reader["ColorCode"].AsString(),
              PaintMaker = reader["PaintMaker"].AsString(),
              ToolsEquipments = reader["ToolsEquipments"].AsString(),
              FollowDocumentStandard = reader["FollowDocumentStandard"].AsString(),
              CTMaskingSpec = reader["CTMaskingSpec"].AsNullableDouble(),
              CTMaskingActual = reader["CTMaskingActual"].AsNullableDouble(),
              Judgement = reader["Judgement"].AsString()
          };

        static readonly Func<IDataReader, PPAPPaintingDetailModel> MakePPAPPaintingDetail = reader =>
          new PPAPPaintingDetailModel
          {
              NoPPAP = reader["NoPPAP"].AsString(),
              IdStatus = reader["IdStatus"].AsInt(),
              Code = reader["Code"].AsString(),
              CodeItem = reader["CodeItem"].AsString(),
              NoUrut = reader["NoUrut"].AsInt(),
              Item = reader["Item"].AsString(),
              SubItem = reader["SubItem"].AsString(),
              ProductName = reader["ProductName"].AsString(),
              Spec = reader["Spec"].AsNullableDouble(),
              Actual = reader["Actual"].AsNullableDouble()
          };

        static readonly Func<IDataReader, PPAPAssemblyModel> MakePPAPAssembly = reader =>
          new PPAPAssemblyModel
          {
              NoPPAP = reader["NoPPAP"].AsString(),
              IdStatus = reader["IdStatus"].AsInt(),
              Code = reader["Code"].AsString(),
              Seq = reader["Seq"].AsInt(),
              PartNumber = reader["PartNumber"].AsString(),
              PartName = reader["PartName"].AsString(),
              ComponentPart = reader["ComponentPart"].AsString(),
              Qty = reader["Qty"].AsNullableInt(),
              ProcessName = reader["ProcessName"].AsString(),
              ManPower = reader["ManPower"].AsNullableInt(),
              Actual = reader["Actual"].AsNullableDouble(),
              Spec = reader["Spec"].AsNullableDouble()
          };
    }
}
