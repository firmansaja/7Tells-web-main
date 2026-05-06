using ActionServices;
using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using BusinessObjects;
using iTells.Helper;
using DataObjects.Helpers;
using System.Globalization;
using ClosedXML.Excel;
using System.IO;
using DevExpress.XtraReports.Web;
using System.Data;

namespace iTells.Areas.User.Controllers
{
    public class ECirculationController : Controller
    {
        // GET: User/ECirculation
        int Division = 0;
        int Director = 0;
        int DeptGroup = 0;


        GeneralHelper dHelper = new GeneralHelper();
        CRAkses _CRAkses { get; set; }
        ICirculationService service { get; set; }


        public ECirculationController() : this(new CRAkses(), new CirculationServices(), new ComboListService()) { }
        public ECirculationController(CRAkses CRAkses,ICirculationService service, IComboListService comboListService)
        {
            this._CRAkses = CRAkses;
            this.service = service;
            //this.comboListService = comboListService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApproveCirculation()
        {
            CRUserAkses();

            if (_CRAkses == null)
            {
                return Redirect("/account/login");
            }

          
            GetDataApprove(_CRAkses.UserGroupCode, _CRAkses.Level, _CRAkses.Division, _CRAkses.Director, _CRAkses.UserName);
            GetDataCRApproveDetail("0");
            GetDataCRApproveHeadDetail("0", 0);
            return View();
        }

        public void CRUserAkses()
        {
            _CRAkses = service.GetAkses(dHelper.CurrentLoginUserName());
            ViewData["AksesUser"] = _CRAkses;
        }

        public ActionResult GetDataApprove(string _dept, int level, int div, int dir,string _user)

        {
            try

            {
                List<BO_ECirculation> ListApproval = new List<BO_ECirculation>();
                ListApproval = service.GetCRApproveHead(_dept, level, div, dir,_user);
                ViewData["ListCRHead"] = ListApproval;

                //CirculationServices M = new CirculationServices();
                //ViewData["ListCRHead"] = M.GetCRApproveHead(_dept, level, div, dir);
                return PartialView("Partial/CRHead");


            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult GetCRSearch(DateTime TanggalAwal, DateTime TanggalAhir)

        {
            try

            {
                CRUserAkses();

                List<BO_ECirculation> ListApproval = new List<BO_ECirculation>();
                ListApproval = service.GetCRApproveHeadSearch(_CRAkses, TanggalAwal, TanggalAhir);
                ViewData["ListCRHead"] = ListApproval;

                return PartialView("Partial/CRHead");

            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult GetDataCRApproveDetail(string Id)

        {
            try

            {

                List<BO_ECirculation_Detail> ListData = new List<BO_ECirculation_Detail>();
                ListData = service.GetCRApproveDetail(Id);
                ViewData["ListCRDetail"] = ListData;
                return PartialView("Partial/CRGridDetail");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult GetDataCRApproveHeadDetail(string Id, int DeptGroup)

        {
            try

            {
                CRUserAkses();
                List<BO_ECirculation> ListData = new List<BO_ECirculation>();
                List<BO_ECirculation> ListDataOther = new List<BO_ECirculation>();

                ListData = service.GetCRHeadDetail(Id,DeptGroup);
                ListDataOther = service.GetOtherDeptList(Id);

                ViewData["ListCRHeadDetail"] = ListData;
                ViewData["ListOtherDept"] = ListDataOther;

                //ViewData["ListOtherDept"] = M.GetOtherDeptList(Id);
                return PartialView("Partial/CRHeadDetail");
            }
            catch (Exception)
            {

                throw;
            }

        }


        public ActionResult UpdateCR(BO_ECirculation Fdata)
        {
            CRUserAkses();

            string hari = 1.ToString();
            string bulan = (DateTime.Now.Month).ToString();
            string tahun = ((DateTime.Now.Year) - 1).ToString();


            service.Update(Fdata,_CRAkses);
            //string _dept, int level, int div, int dir,string _user
            return this.GetDataApprove(_CRAkses.UserGroupCode, _CRAkses.Level, _CRAkses.Division, _CRAkses.Director,_CRAkses.UserName);
        }

        public ActionResult RejectCR(BO_ECirculation Fdata)
        {
            CRUserAkses();

            string hari = 1.ToString();
            string bulan = (DateTime.Now.Month).ToString();
            string tahun = ((DateTime.Now.Year) - 1).ToString();


            service.Reject(Fdata, _CRAkses);
            return this.GetDataApprove(_CRAkses.UserGroupCode, _CRAkses.Level, _CRAkses.Division, _CRAkses.Director, _CRAkses.UserName);
        }




        // OPINI

        public ActionResult CROpinion()
        {
            CRUserAkses();

            if (_CRAkses == null)
            {
                return Redirect("/account/login");
            }

            CirculationServices M = new CirculationServices();

            string hari = 1.ToString();
            string bulan = (DateTime.Now.Month).ToString();
            string tahun = ((DateTime.Now.Year) - 1).ToString();


           // GetDataApprove((Convert.ToDateTime(tahun + "-" + bulan + "-" + hari)), DateTime.Now, 1, 2);
            GetDataOpinion(_CRAkses);
            GetDataCRApproveDetailOpinion("");
            //GetDataCRApproveHeadDetail("0", DeptGroup);

            return View();
        }

        public ActionResult GetDataOpinion(CRAkses _cRAkses)

        {
            try

            {
                List<BO_ECirculation> ListData = new List<BO_ECirculation>();
                ListData = service.GetCRApproveHeadOpinion(_cRAkses);
                ViewData["ListCRHeadOpinion"] = ListData;
                //CirculationServices M = new CirculationServices();
                //ViewData["ListCRHeadOpinion"] = M.GetCRApproveHeadOpinion(_User, _dept, DeptGroup);
                return PartialView("Partial/CRHeadOpinion");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult GetDataOpinionSearch(DateTime TanggalAwal, DateTime TanggalAhir)

        {
            try

            {
                CRUserAkses();

                List<BO_ECirculation> ListApproval = new List<BO_ECirculation>();
                ListApproval = service.GetDataOpinionSearch(_CRAkses, TanggalAwal, TanggalAhir);
                ViewData["ListCRHead"] = ListApproval;

                return PartialView("Partial/CRHead");

            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult GetDataCRApproveDetailOpinion(string Id)
        {

            try

            {

                List<BO_ECirculation_Detail> ListData = new List<BO_ECirculation_Detail>();
                ListData = service.GetDataCRApproveDetailOpinion(Id);
                ViewData["ListCRDetailOpinion"] = ListData;
                return PartialView("Partial/CRGridDetailOpinion");
            }
            catch (Exception)
            {

                throw;
            }
            //CirculationServices M = new CirculationServices();
            //ViewData["ListCRDetailOpinion"] = M.GetCRApproveDetail(Id);
        }

        public ActionResult UpdateCROpinion(BO_ECirculation Fdata)
        {
            //string hari = 1.ToString();
            //string bulan = (DateTime.Now.Month).ToString();
            //string tahun = ((DateTime.Now.Year) - 1).ToString();

            //CirculationServices M = new CirculationServices();
            //M.UpdateOpinion(Fdata);

            //return this.GetDataApprove(UserHelper.UserGroupCode, UserHelper.LevelHistory, UserHelper.Division, UserHelper.Director);

            CRUserAkses();

            string hari = 1.ToString();
            string bulan = (DateTime.Now.Month).ToString();
            string tahun = ((DateTime.Now.Year) - 1).ToString();


            service.UpdateOpinion(Fdata, _CRAkses);
            //string _dept, int level, int div, int dir,string _user
            return this.GetDataOpinion(_CRAkses);

        }


        }
}