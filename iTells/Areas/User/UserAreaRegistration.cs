using System.Web.Mvc;

namespace iTells.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("", "modules", new { area = "User", controller = "Module", action = "Index" });
            context.MapRoute("", "getmodules", new { area = "User", controller = "Module", action = "GetModules" });
            context.MapRoute("", "getmodule/{id}", new { area = "User", controller = "Module", action = "GetModuleById" });
            context.MapRoute("", "postmodule", new { area = "User", controller = "Module", action = "Create" });
            context.MapRoute("", "budget", new { area = "User", controller = "EBudget", action = "Index" });
            context.MapRoute("", "lihatbudget/{id}", new { area = "User", controller = "EBudget", action = "LihatBudget" });
            context.MapRoute("", "lihatbudget_isi/{id}", new { area = "User", controller = "EBudget", action = "LihatBudget_isi" });
            context.MapRoute("", "loadbudget", new { area = "User", controller = "EBudget", action = "LoadBudget" });
            context.MapRoute("", "loadbudgetpresentasi/{id}", new { area = "User", controller = "EBudget", action = "LoadBudgetPresentasi" });
            context.MapRoute("", "loadbudgetisi", new { area = "User", controller = "EBudget", action = "LoadBudget_Isi" });
            context.MapRoute("", "getbudget", new { area = "User", controller = "EBudget", action = "GetBudget" });
            context.MapRoute("", "lihatBudgetDetail/{data}", new { area = "User", controller = "EBudget", action = "LihatBudgetDetail" });
            context.MapRoute("", "showBudgetDetail/{AcctId}/Tahun/{Tahun}/Dept/{DeptID}", new { area = "User", controller = "EBudget", action = "ShowLihatBudgetDetail" });
            context.MapRoute("", "showBudgetDetail_pl/{AcctId}/Tahun/{Tahun}/Dept/{DeptID}", new { area = "User", controller = "EBudget", action = "ShowLihatBudgetDetail_Pl" });
            context.MapRoute("", "showBudgetDetail_isi/{AcctId}/Tahun/{Tahun}/Dept/{DeptID}", new { area = "User", controller = "EBudget", action = "ShowLihatBudgetDetail_isi" });
            //context.MapRoute("", "ShowEditData/{data}", new { area = "User", controller = "EBudget", action = "ShowEditData" });
            context.MapRoute("", "ShowEditData/{data}/tahun/{tahun}/dept/{deptid}/acctid/{acctid}/semester/{semester}", new { area = "User", controller = "EBudget", action = "ShowEditData" });
            context.MapRoute("", "ShowEditDataFAC/{data}/tahun/{tahun}/dept/{deptid}/acctid/{acctid}/semester/{semester}", new { area = "User", controller = "EBudget", action = "ShowEditDataFAC" });
            context.MapRoute("", "getbudgetDetail", new { area = "User", controller = "EBudget", action = "GetBudgetDetail" });
            context.MapRoute("", "getbudgetDetail2", new { area = "User", controller = "EBudget", action = "GetBudgetDetail2" });
            context.MapRoute("", "simpanbudget", new { area = "User", controller = "EBudget", action = "SimpanBudget" });
            context.MapRoute("", "tambahbudget/{AcctId}/AcctName/{AcctName}/Tahun/{Tahun}/DeptId/{DeptId}/SideID/{SideID}", new { area = "User", controller = "EBudget", action = "NewBudget" });
            context.MapRoute("", "simpanupload/{data}", new { area = "User", controller = "EBudget", action = "SimpanBudget" });
            context.MapRoute("", "uploadexcel/{data}", new { area = "User", controller = "EBudget", action = "UploadExcel" });
            context.MapRoute("", "ebudget_import/{tahun}/Semester/{Semester}", new { area = "User", controller = "EBudget", action = "Import" });
            context.MapRoute("", "budgetdept", new { area = "User", controller = "EBudget", action = "BudgetDept" });
            context.MapRoute("", "loadBudgetDeptSummary", new { area = "User", controller = "EBudget", action = "loadBudgetDeptSummary" });
            context.MapRoute("", "budgetdeptsummary/{id}", new { area = "User", controller = "EBudget", action = "Budgetdeptsummary" });
            context.MapRoute("", "getDeptSummary", new { area = "User", controller = "EBudget", action = "GetDeptSummary" });
            context.MapRoute("", "lihatbudgetpresentasi/{Tahun}/DeptId/{DeptId}", new { area = "User", controller = "EBudget", action = "LihatBudgetPresentasi" });
            context.MapRoute("", "getbudgetpresentasi", new { area = "User", controller = "EBudget", action = "GetBudgetPresentasi" });
            context.MapRoute("", "budgetvsactualdept", new { area = "User", controller = "EBudget", action = "BudgetvsActualDept" });
            context.MapRoute("", "loadBudgetvsactualDeptSummary", new { area = "User", controller = "EBudget", action = "loadBudgetvsActualDeptSummary" });
            context.MapRoute("", "budgetvsactualdeptsummary/{id}", new { area = "User", controller = "EBudget", action = "BudgetvsActualdeptsummary" });
            context.MapRoute("", "getActualDeptSummary", new { area = "User", controller = "EBudget", action = "GetActualDeptSummary" });
            context.MapRoute("", "lihatbudgetvsactualpresentasi/{Tahun}/DeptId/{DeptId}", new { area = "User", controller = "EBudget", action = "LihatBudgetvsActualPresentasi" });
            context.MapRoute("", "getbudgetvsactualpresentasi", new { area = "User", controller = "EBudget", action = "GetBudgetvsActualPresentasi" });
            context.MapRoute("", "showBudgetvsActualDetail/{AcctId}/Tahun/{Tahun}/Dept/{DeptID}", new { area = "User", controller = "EBudget", action = "ShowLihatBudgetvsActualDetail" });


            context.MapRoute("", "budgetpl", new { area = "User", controller = "EBudgetPL", action = "Index" });
            context.MapRoute("", "budgetPL_Header", new { area = "User", controller = "EBudgetPL", action = "budgetPL_Header" });
            context.MapRoute("", "budgetProfitLost/{tahun}", new { area = "User", controller = "EBudgetPL", action = "ProfitLost" });
            context.MapRoute("", "ShowProfitLostByAccount/{acctid}/tahun/{tahun}", new { area = "User", controller = "EBudgetPL", action = "ShowProfitLostByAccount" });

            context.MapRoute("", "PO", new { area = "User", controller = "EPO", action = "Index" });
            context.MapRoute("", "getmodelpo", new { area = "User", controller = "EPO", action = "GetModelPO" });
            context.MapRoute("", "getmodelpObyuser", new { area = "User", controller = "EPO", action = "GetModelPOByUser" });
            context.MapRoute("", "getmodelpOApproveSM", new { area = "User", controller = "EPO", action = "GetModelPOApproveSM" });
            context.MapRoute("", "getmodelpONonConfirm", new { area = "User", controller = "EPO", action = "GetModelPONonConfirm" });
            context.MapRoute("", "getmodelpOhistory", new { area = "User", controller = "EPO", action = "GetModelPOHistory" });
            context.MapRoute("", "getmodelpOSupplierApprove", new { area = "User", controller = "EPO", action = "GetModelPOSupplierApprove" });

            context.MapRoute("", "viewpouser/{PONo}", new { area = "User", controller = "EPO", action = "ViewPOUser" });
            context.MapRoute("", "viewpoApproveUser/{PONo}", new { area = "User", controller = "EPO", action = "ViewPOApproveUser" });
                       
            //context.MapRoute("", "postmodule/{model}", new { area = "User", controller = "Module", action = "Create" });
            context.MapRoute("", "pr", new { area = "User", controller = "PR", action = "Index" });
            context.MapRoute("", "getmodelpr", new { area = "User", controller = "PR", action = "GetModelPR" });
            context.MapRoute("", "getmodelprbyuser", new { area = "User", controller = "PR", action = "GetModelPRByUser" });
            context.MapRoute("", "getmodelprbyall", new { area = "User", controller = "PR", action = "GetModelPRByAll" });
            context.MapRoute("", "getmodelprpur", new { area = "User", controller = "PR", action = "GetModelPRPurchase" });
            context.MapRoute("", "getmodelprhistory", new { area = "User", controller = "PR", action = "GetModelPRHistory" });
            context.MapRoute("", "viewpr/{PRNo}/Year/{Year}", new { area = "User", controller = "PR", action = "ViewPR" });
            context.MapRoute("", "viewpruser/{PRNo}", new { area = "User", controller = "PR", action = "ViewPRUser" });
            context.MapRoute("", "viewprall/{PRNo}", new { area = "User", controller = "PR", action = "ViewPRAll" });
            context.MapRoute("", "viewprpurchase/{PRNo}", new { area = "User", controller = "PR", action = "ViewPRPurchase" });
            context.MapRoute("", "viewbudget/{Dept}/Year/{Year}", new { area = "User", controller = "PR", action = "ViewBudget" });
            context.MapRoute("", "getmodelprprogress", new { area = "User", controller = "PR", action = "GetModelPRProgress" });

            //Modul Material Stok
            context.MapRoute("", "materialkontrol", new { area = "User", controller = "EMaterialKontrol", action = "Index" });
            context.MapRoute("", "loadStokMaterial", new { area = "User", controller = "EMaterialKontrol", action = "loadStokMaterial" });
            context.MapRoute("", "StokMaterial/{id}/account/{account}", new { area = "User", controller = "EMaterialKontrol", action = "StokMaterial" });
            context.MapRoute("", "loadStokMaterialByAccount", new { area = "User", controller = "EMaterialKontrol", action = "loadStokMaterialByAccount" });
            context.MapRoute("", "StokMaterialByAccount/{id}", new { area = "User", controller = "EMaterialKontrol", action = "StokMaterialByAccount" });
            context.MapRoute("", "StokMaterialByAccount1/{id}", new { area = "User", controller = "EMaterialKontrol", action = "StokMaterialByAccount1" });
            context.MapRoute("", "fifo", new { area = "User", controller = "EMaterialKontrol", action = "FIFO" });
            context.MapRoute("", "GetFifo", new { area = "User", controller = "EMaterialKontrol", action = "GetFifo" });
            context.MapRoute("", "LoadStokFifo", new { area = "User", controller = "EMaterialKontrol", action = "LoadStokFifo" });
            //context.MapRoute("", "StokFifo/{id}", new { area = "User", controller = "EMaterialKontrol", action = "LoadStokFifo" });
            context.MapRoute("", "LPH", new { area = "User", controller = "EMaterialKontrol", action = "LPH" });

            //Display Laporan Hasil Produksi
            context.MapRoute("", "hasilproduksi", new { area = "User", controller = "EMaterialKontrol", action = "LoadHasilProduksi" });

            context.MapRoute("", "Circulation", new { area = "User", controller = "ECirculation", action = "ApproveCirculation" });
            context.MapRoute("", "Opini", new { area = "User", controller = "ECirculation", action = "CROpinion" });
            context.MapRoute("", "test", new { area = "User", controller = "EBudget", action = "testRollBack" });
            context.MapRoute("", "lphmesin", new { area = "User", controller = "EMaterialKontrol", action = "LPHMesin" });
            context.MapRoute("", "viewlphmesin/{NoMesin}", new { area = "User", controller = "EMaterialKontrol", action = "LPHMesinDetail" });

            //Modul SO
            context.MapRoute("", "SO", new { area = "User", controller = "SO", action = "Index" });
            context.MapRoute("", "Load_Os_SalesOrder", new { area = "User", controller = "SO", action = "Load_Os_SalesOrder" });
            context.MapRoute("", "ViewOsSalesOrder/{id}", new { area = "User", controller = "SO", action = "ViewOsSalesOrder" });
            context.MapRoute("", "GetOutstanding", new { area = "User", controller = "SO", action = "GetOutstanding" });
            context.MapRoute("", "viewbyordnbr/{id}", new { area = "User", controller = "SO", action = "ViewGetdataByOrdNbr" });
            context.MapRoute("", "loaddataByOrdNbr", new { area = "User", controller = "SO", action = "loaddataByOrdNbr" });

            //Modul kendaraan
            context.MapRoute("", "reqKendaraan", new { area = "User", controller = "EMobil", action = "Index" });
            context.MapRoute("", "getReqMobil", new { area = "User", controller = "EMobil", action = "GetReqMobil" });
            context.MapRoute("", "viewReqMobil/{NoTrans}", new { area = "User", controller = "EMobil", action = "ViewReqMobil" });
            context.MapRoute("", "cekKendaraan", new { area = "User", controller = "EMobil", action = "CekMobil" });
            context.MapRoute("", "getCekMobil", new { area = "User", controller = "EMobil", action = "GetCekMobil" });
            context.MapRoute("", "viewCekMobil/{NoSuratJalan}", new { area = "User", controller = "EMobil", action = "ViewCekMobil" });

            //Modul Klinik
            context.MapRoute("", "getTrxPeriksa", new { area = "User", controller = "Klinik", action = "GetTrxPeriksa" });

            //Modul MRP
            context.MapRoute("", "mrp", new { area = "User", controller = "MRP", action = "Index" });
            context.MapRoute("", "forecastcust", new { area = "User", controller = "MRP", action = "ForecastCust" });
            context.MapRoute("", "getForecastCust", new { area = "User", controller = "MRP", action = "GetForecastCust" });
            context.MapRoute("", "viewForecastModel/{CustID}/Year/{Year}", new { area = "User", controller = "MRP", action = "ViewForecastModel" });
            context.MapRoute("", "viewForecastDetail/{CustID}/Model/{Model}/Year/{Year}", new { area = "User", controller = "MRP", action = "ViewForecastDetail" });
            context.MapRoute("", "bomCalculate", new { area = "User", controller = "MRP", action = "BoMCalculate" });
            context.MapRoute("", "getBoMCalculate", new { area = "User", controller = "MRP", action = "GetBoMCalculate" });
            context.MapRoute("", "viewBoMCalculateDetail/{MaterialID}/Site/{Site}/Year/{Year}", new { area = "User", controller = "MRP", action = "ViewBoMCalculateDetail" });

            //Modul Trolley
            context.MapRoute("", "trolleyin", new { area = "User", controller = "Trolley", action = "TrolleyIn" });
            context.MapRoute("", "trolleyout", new { area = "User", controller = "Trolley", action = "TrolleyOut" });

            //Modul Trolly TSC
            context.MapRoute("", "trollyinout", new { area = "User", controller = "Trolly", action = "Index" });
            context.MapRoute("", "trollyintsc", new { area = "User", controller = "Trolly", action = "TrollyInTSC" });
            context.MapRoute("", "trollyouttsc", new { area = "User", controller = "Trolly", action = "TrollyOutTSC" });

            //Modul Trolly Depo
            context.MapRoute("", "trollyinoutdepo", new { area = "User", controller = "TrollyDepo", action = "Index" });
            context.MapRoute("", "trollyindepo", new { area = "User", controller = "TrollyDepo", action = "TrollyInDepo" });
            context.MapRoute("", "trollyoutdepo", new { area = "User", controller = "TrollyDepo", action = "TrollyOutDepo" });
            context.MapRoute("", "trollyoutdepotoadm", new { area = "User", controller = "TrollyDepo", action = "TrollyOutDepoToADM" });
            context.MapRoute("", "laptrollyoutdepo", new { area = "User", controller = "TrollyDepo", action = "LapTrollyOutDepo" });
            context.MapRoute("", "gettrollyoutdepo", new { area = "User", controller = "TrollyDepo", action = "GetLapTrollyOutDepo" });
            context.MapRoute("", "trollystokdepo", new { area = "User", controller = "TrollyDepo", action = "TrollyStokDepo" });

            //Modul Line Store
            context.MapRoute("", "linestore", new { area = "User", controller = "LineStore", action = "Index" });
            context.MapRoute("", "scanpart", new { area = "User", controller = "LineStore", action = "ScanPart" });
            context.MapRoute("", "scanpartpunch", new { area = "User", controller = "LineStore", action = "ScanPartPunch" });

            //Modul Line Store
            //context.MapRoute("", "linestore", new { area = "User", controller = "LineStore", action = "Index" });
            context.MapRoute("", "scanpartout", new { area = "User", controller = "PPIC", action = "ScanPartOut" });
                       
            //Surat

            //Surat Kategori
            context.MapRoute("", "kategorisurat", new { area = "User", controller = "KategoriSurat", action = "Index" });
            context.MapRoute("", "getkategori", new { area = "User", controller = "KategoriSurat", action = "Get" });
            context.MapRoute("", "addkategori", new { area = "User", controller = "KategoriSurat", action = "Add" });
            context.MapRoute("", "postkategorisurat", new { area = "User", controller = "KategoriSurat", action = "Create" });
            context.MapRoute("", "putkategorisurat", new { area = "User", controller = "KategoriSurat", action = "Update" });
            context.MapRoute("", "deletekategorisurat", new { area = "User", controller = "KategoriSurat", action = "Delete" });
            context.MapRoute("", "getkategorisurat", new { area = "User", controller = "KategoriSurat", action = "Get" });

            //SuratMasuk
            context.MapRoute("", "suratmasuk", new { area = "User", controller = "SuratMasuk", action = "Index" });
            context.MapRoute("", "suratmasuksecurity", new { area = "User", controller = "SuratMasuk", action = "SuratMasukSecurity" });
            context.MapRoute("", "getlistsuratmasuk", new { area = "User", controller = "SuratMasuk", action = "Get" });
            context.MapRoute("", "postsuratmasuk", new { area = "User", controller = "SuratMasuk", action = "Create" });
            context.MapRoute("", "putsuratmasuk", new { area = "User", controller = "SuratMasuk", action = "Update" });
            context.MapRoute("", "deletesuratmasuk", new { area = "User", controller = "SuratMasuk", action = "Delete" });
            context.MapRoute("", "getnourutsuratmasuk", new { area = "User", controller = "SuratMasuk", action = "GetNoUrut" });
            context.MapRoute("", "getsuratmasuk", new { area = "User", controller = "SuratMasuk", action = "Get" });

            //Surat Keluar
            context.MapRoute("", "suratkeluar", new { area = "User", controller = "SuratKeluar", action = "Index" });
            context.MapRoute("", "getlistsuratkeluar", new { area = "User", controller = "SuratKeluar", action = "Get" });
            context.MapRoute("", "postsuratkeluar", new { area = "User", controller = "SuratKeluar", action = "Create" });
            context.MapRoute("", "putsuratkeluar", new { area = "User", controller = "SuratKeluar", action = "Update" });
            context.MapRoute("", "deletesuratkeluar", new { area = "User", controller = "SuratKeluar", action = "Delete" });
            context.MapRoute("", "getsuratkeluar", new { area = "User", controller = "SuratKeluar", action = "Get" });
            context.MapRoute("", "getnosuratkeluar", new { area = "User", controller = "SuratKeluar", action = "GetNoSurat" });
            context.MapRoute("", "getkategorikode", new { area = "User", controller = "SuratKeluar", action = "GetKategori" });
            
            //Surat Test
            context.MapRoute("", "test1", new { area = "User", controller = "test1", action = "Index" });

            //PPAP
            context.MapRoute("", "ppap", new { area = "User", controller = "PPAP", action = "Index" });
            context.MapRoute("", "getlistppap", new { area = "User", controller = "PPAP", action = "GetPPAP" });
            context.MapRoute("", "viewppap/{noppap}/{idstatus}", new { area = "User", controller = "PPAP", action = "ViewPPAP" });
            context.MapRoute("", "getlisttoolingstatus", new { area = "User", controller = "PPAP", action = "GetToolingStatus" });

            context.MapRoute("", "get", new { area = "User", controller = "SuratMasuk", action = "Get" });
            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "iTells.Areas.User.Controllers" }
            );


            
        }
    }
}