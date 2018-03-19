using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LenProcurementApp.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

namespace LenProcurementApp.Controllers
{
    /// <summary>
    /// controller untuk dashboard
    /// </summary>
    [Authorize]
    public class DashboardController : BaseController
    {

        // Log file
        //private static log4net.ILog Log { get; set; }
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(DashboardController));
        // Database
        private ApplicationDbContext db = new ApplicationDbContext();
        private string[] month = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        /// <summary>
        /// Main dashboard
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter">filter string</param>
        /// <param name="searchString"></param>
        /// <param name="page">Halaman yang akan ditampilkan</param>
        /// <returns>Model main dashboard</returns>
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.useDataTable = true;

                ViewBag.useAjax = true;
                ViewBag.useDateRangePicker = true;

                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                ViewBag.topMenuId = "none";
                ViewBag.addPlk = false;
                ViewBag.addSpp = false;

                ViewBag.initial = null;
                ViewBag.dangerTask = 0;

                ViewBag.isStruktur = 0;
                
                ViewBag.alertLokal = 0;
                ViewBag.alertImport = 0;

                if (User.IsInRole(STRUKTURALLOKAL))
                {
                    ViewBag.isStruktur = 1;
                } else if (User.IsInRole(STRUKTURALIMPOR))
                {
                    ViewBag.isStruktur = 2;
                }

                if (User.IsInRole(ADMIN) || User.IsInRole(DEVELOPER))
                {
                    ViewBag.addPlk = true;
                }

                if (User.IsInRole(BAPBSPP) || User.IsInRole(BAPB) || User.IsInRole(DEVELOPER))
                {
                    ViewBag.addSpp = true;
                }

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                //int pageSize = 10;
                int pageNumber = (page ?? 1);

                Session["pageNumber"] = pageNumber;

                List<NewMainDashboard> model = new List<NewMainDashboard>();
                MainTransaction mt = new MainTransaction();

                // untuk pelaksana
                


                if (User.IsInRole(USERDBP))
                {
                    string userId = User.Identity.GetUserName();
                    model = mt.GetMainUserDpb(userId);
                }
                else if (User.IsInRole(PELAKSANA))
                {
                    string uid = User.Identity.GetUserId();
                    string initial = null;
                    var init = db.profiles.FirstOrDefault(f => f.user_id == uid);
                    if (init != null)
                    {
                        initial = init.initials;
                    }

                    ViewBag.initial = initial;

                    model = mt.GetMainExecutor(initial);

                    SummaryTransactionPLK st = new SummaryTransactionPLK(ViewBag.initial);
                    int d8 = int.Parse(st.GetSummary8().data1);
                    int d9 = int.Parse(st.GetSummary9().data1);
                    int d10 = int.Parse(st.GetSummary10().data1);
                    int d11 = int.Parse(st.GetSummary11().data1);
                    int d24 = int.Parse(st.GetSummary24().data1);

                    //int d8 = int.Parse("0");
                    //int d9 = int.Parse("0");
                    //int d10 = int.Parse("0");
                    //int d11 = int.Parse("0");
                    //int d24 = int.Parse("0");
                    ViewBag.alertLokal = d8 + d9 + d10 + d11;
                    ViewBag.alertImport = d24;

                    SummaryModel sm = mt.GetDangerTask(initial);
                    int dangerTask = 0;
                    if (sm.data1 != null)
                    {
                        dangerTask = int.Parse(sm.data1);
                    }
                    ViewBag.dangerTask = dangerTask;
                    ViewBag.dangerLink = sm.link1;
                }
                else
                {
                    model = mt.getMainTableContents();
                    if (User.IsInRole(STRUKTURALLOKAL))
                    {
                        if(model != null) { 
                            model = model.Where(w => w.is_import == 0).ToList();
                        }
                    }
                    else if (User.IsInRole(STRUKTURALIMPOR))
                    {
                        if (model != null) { 
                            model = model.Where(w => w.is_import > 0).ToList();
                        }
                    }
                }

                // role untuk menu detail
                ViewBag.role = 0;
                ViewBag.developer = false;
                if (User.IsInRole(DEVELOPER))
                {
                    ViewBag.developer = true;
                }
                if(User.IsInRole(STRUKTURAL) || User.IsInRole(STRUKTURALLOKAL) || User.IsInRole(STRUKTURALIMPOR))
                {
                    ViewBag.role = 1;
                } else if (User.IsInRole(PELAKSANA))
                {
                    ViewBag.role = 2;
                } else if(User.IsInRole(ADMIN))
                {
                    ViewBag.role = 3;
                } else if (User.IsInRole(PENERIMABARANG))
                {
                    ViewBag.role = 4;
                } else if(User.IsInRole(BAPB) || User.IsInRole(BAPBSPP))
                {
                    ViewBag.role = 5;
                } else if (User.IsInRole(USERDBP))
                {
                    ViewBag.role = 6;
                }

                if (model != null)
                {
                    ViewBag.totalRow = model.Count();
                    //return View(model.ToPagedList(pageNumber, pageSize));
                    return View(model);
                }
                // jika data tidak tersedia
                ViewBag.error = "Data masih kosong";
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return View();
            }
        }

        /// <summary>
        /// DPB view controller
        /// </summary>
        /// <param name="dpb">kode DPB</param>
        /// <returns>List Produk</returns>
        public ActionResult DPBBoard(string dpb)
        {
            try
            {
                ViewBag.useAjax = true;
                ViewBag.useDataTable = true;
                ViewBag.dpb = dpb;
                ViewBag.addNote = false;

                if (User.IsInRole(PELAKSANA) || User.IsInRole(DEVELOPER))
                {
                    ViewBag.addNote = true;
                }

                if (dpb != null && dpb != "")
                {
                    DpbTransactions dt = new DpbTransactions(dpb);
                    var model = dt.getDashboardData();
                    if (model.Count > 0)
                    {
                        DateTime created = model.First().tgl_pengajuan;
                        DateTime needed = model.First().tgl_dibutuhkan;
                        ViewBag.created = created.ToShortDateString();
                        ViewBag.needed = needed.ToShortDateString();
                        ViewBag.note = dt.getNote();
                        return View(model);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            ViewBag.noData = true;
            return View();
        }

        /// <summary>
        /// BAPB board view controller
        /// </summary>
        /// <param name="bapb">Kode BAPB</param>
        /// <returns>Model BAPBDashboard</returns>
        public ActionResult BAPBBoard(string bapb)
        {
            try
            {
                ViewBag.useAjax = true;
                ViewBag.bapb = bapb;
                ViewBag.useDataTable = true;
                ViewBag.addNote = false;

                if (User.IsInRole(BAPB) || User.IsInRole(BAPBSPP) || User.IsInRole(DEVELOPER))
                {
                    ViewBag.addNote = true;
                }

                if (bapb != null && bapb != "")
                {
                    BapbTransactions bt = new BapbTransactions(bapb);
                    var model = bt.getProducts();
                    if (model.Count > 0)
                    {
                        string po = model.First().po;
                        string dpb = bt.getDpb(po);
                        DateTime created = model.First().tgl_print;
                        ViewBag.po = po;
                        ViewBag.dpb = dpb;
                        ViewBag.created = created.ToShortDateString();
                        ViewBag.note = bt.getNote();
                        return View(model);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            
            ViewBag.noData = true;
            return View();
        }

        /// <summary>
        /// Penyimpanan Keterangan
        /// </summary>
        /// <param name="Model">Model Note</param>
        /// <returns>Partial Note Model</returns>
        [HttpPost]
        public ActionResult SubmitNote(Note Model)
        {
            try
            {
                Note noteTable = new Note();
                if(Model.po != null)
                {
                    noteTable = db.noteData.FirstOrDefault(c => c.code == Model.code && c.po == Model.po);
                }
                else
                {
                    noteTable = db.noteData.FirstOrDefault(c => c.code == Model.code);
                }
                if (noteTable == null)
                {
                    noteTable = new Note()
                    {
                        code = Model.code,
                        note = Model.note,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };
                    if(Model.po != null)
                    {
                        noteTable.po = Model.po;
                    }
                    db.noteData.Add(noteTable);
                    db.SaveChanges();
                }
                else
                {
                    noteTable.note = Model.note;
                    noteTable.updated_at = DateTime.Now;
                    db.SaveChanges();
                }
                return PartialView("_Note", noteTable);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return PartialView("_Note", new Note() { });
            }
        }

        /// <summary>
        /// Dashboard pelaksana
        /// </summary>
        /// <param name="dpb"></param>
        /// <returns></returns>
        [Authorize(Roles = ADMINDEVELOPER)]
        public ActionResult Executor(string dpb)
        {
            try
            {
                ViewBag.useDataTable = true;
                ViewBag.dpb = dpb;
                if (dpb != null && dpb != "")
                {
                    DpbTransactions dt = new DpbTransactions(dpb);
                    //var users = db.profiles.ToList();
                    var users = dt.executorList();
                    var currentModel = dt.getExecutor();
                    ViewBag.currentModel = currentModel;
                    List<Profile> newModel = new List<Models.Profile>();
                    foreach (var item in users)
                    {
                        bool newP = true;
                        foreach (var itemC in currentModel)
                        {
                            if (item.user_id == itemC.user_id)
                            {
                                newP = false;
                            }
                        }
                        if (newP)
                        {
                            newModel.Add(item);
                        }
                    }
                    return View(newModel);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            
            ViewBag.noData = true;
            return View();
        }

        /// <summary>
        /// perubahan data pelaksana
        /// </summary>
        /// <param name="dpb">kode dpb</param>
        /// <param name="id">id executor table, jika act adalah add maka akan null</param>
        /// <param name="act">tambah atau hapus</param>
        /// <returns></returns>
        [Authorize(Roles = ADMINDEVELOPER)]
        public ActionResult ExecutorAction(string dpb, string id, string act)
        {
            try
            {
                ViewBag.dpb = dpb;
                if (dpb != null && dpb != "")
                {
                    DpbTransactions dt = new DpbTransactions(dpb);
                    if (act != null)
                    {
                        if (act == "add")
                        {
                            Executor executor = new Executor() { dpb = dpb, executor = id, created_at = DateTime.Now, updated_at = DateTime.Now };
                            db.executorData.Add(executor);
                        }
                        else if (act == "delete")
                        {
                            Executor executor = db.executorData.FirstOrDefault(i => i.executor == id && i.dpb == dpb);
                            if (executor != null)
                            {
                                db.executorData.Remove(executor);
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            
            return RedirectToAction("Executor", new { dpb = dpb });
        }

        /// <summary>
        /// untuk summary
        /// </summary>
        /// <param name="id">terdiri dari DPb, PO, dll</param>
        /// <returns></returns>
        public ActionResult TopMenuDetail(string id)
        {
            ViewBag.useChart = true;
            id = id != null ? id.ToLower() : id;
            ViewBag.id = id;
            ViewBag.isGrafik = false;
                        
            List<SummaryModel> model = new List<SummaryModel>();
            try
            {
                if (id == "dpb")
                {
                    if (User.IsInRole(STRUKTURALLOKAL))
                    {
                        ViewBag.name = "DPB";
                        SummaryTransactionDPB st = new SummaryTransactionDPB();
                        model.Add(st.GetSummary6());
                        model.Add(st.GetSummary7());
                        model.Add(st.GetSummary8());
                        model.Add(st.GetSummary9());
                        model.Add(st.GetSummarym1());
                    }
                    else if (User.IsInRole(STRUKTURALIMPOR))
                    {
                        ViewBag.name = "DPB";
                        SummaryTransactionDPB st = new SummaryTransactionDPB();
                        model.Add(st.GetSummary10());
                        model.Add(st.GetSummary11());
                        model.Add(st.GetSummary12());
                        model.Add(st.GetSummary13());
                        model.Add(st.GetSummarym2());
                    }

                    else
                    {
                        //GetChart("len_enq_dpb", "tgl_pengajuan", "dpb", "F", "N", "C");
                        ViewBag.name = "DPB";
                        SummaryTransactionDPB st = new SummaryTransactionDPB();
                        model.Add(st.GetSummary1());
                        model.Add(st.GetSummary2());
                        model.Add(st.GetSummary3());
                        model.Add(st.GetSummary4());
                        model.Add(st.GetSummary5());
                    }
                }
                else if (id == "po")
                {
                    if (User.IsInRole(STRUKTURALLOKAL))
                    {
                        ViewBag.name = "PO";
                        SummaryTransactionPO st = new SummaryTransactionPO();
                        model.Add(st.GetSummary11());
                        model.Add(st.GetSummary12());
                        model.Add(st.GetSummary13());
                        model.Add(st.GetSummary14());
                        model.Add(st.GetSummary15());
                        model.Add(st.GetSummary16());
                        model.Add(st.GetSummary17());
                    }
                    else if (User.IsInRole(STRUKTURALIMPOR))
                    {
                        ViewBag.name = "PO";
                        SummaryTransactionPO st = new SummaryTransactionPO();
                        model.Add(st.GetSummary18());
                        model.Add(st.GetSummary19());
                        model.Add(st.GetSummary20());
                        model.Add(st.GetSummary21());
                        model.Add(st.GetSummary22());
                        model.Add(st.GetSummary23());
                        model.Add(st.GetSummary24());
                        model.Add(st.GetSummary8());
                        model.Add(st.GetSummary9());
                        model.Add(st.GetSummary10());
                    }

                    else
                    {
                        //GetChart("len_enq_po", "tgl_po", "po", "F", "O", "C");
                        ViewBag.name = "PO";
                        SummaryTransactionPO st = new SummaryTransactionPO();
                        model.Add(st.GetSummary1());
                        model.Add(st.GetSummary2());
                        model.Add(st.GetSummary3());
                        model.Add(st.GetSummary4());
                        model.Add(st.GetSummary5());
                        model.Add(st.GetSummary6());
                        model.Add(st.GetSummary7());
                        model.Add(st.GetSummary8());
                        model.Add(st.GetSummary9());
                        model.Add(st.GetSummary10());
                    }


                }
                else if (id == "kb")
                {
                    if (User.IsInRole(STRUKTURALLOKAL))
                    {
                        ViewBag.name = "Kedatangan Barang";
                        structuralChart(3, 4);
                        SummaryTransactionKB st = new SummaryTransactionKB();
                        model.Add(st.GetSummary12());
                        model.Add(st.GetSummary13());
                        model.Add(st.GetSummary14());
                        model.Add(st.GetSummary15());
                        model.Add(st.GetSummary18());
                        model.Add(st.GetSummary19());
                        model.Add(st.GetSummary20());
                        model.Add(st.GetSummary21());
                        model.Add(st.GetSummary22());
                    }
                    else if (User.IsInRole(STRUKTURALIMPOR))
                    {
                        ViewBag.name = "Kedatangan Barang";
                        structuralChart(5, 6);
                        SummaryTransactionKB st = new SummaryTransactionKB();
                        model.Add(st.GetSummary23());
                        model.Add(st.GetSummary24());
                        model.Add(st.GetSummary25());
                        model.Add(st.GetSummary26());
                        model.Add(st.GetSummary31());
                        model.Add(st.GetSummary27());
                        model.Add(st.GetSummary28());
                        
                    }
                    else
                    {
                        ViewBag.name = "Kedatangan Barang";
                        newChart();
                        SummaryTransactionKB st = new SummaryTransactionKB();
                        model.Add(st.GetSummary1());
                        model.Add(st.GetSummary2());
                        model.Add(st.GetSummary3());
                        model.Add(st.GetSummary4());
                        model.Add(st.GetSummary7());
                        model.Add(st.GetSummary8());
                        model.Add(st.GetSummary9());
                        model.Add(st.GetSummary10());
                        model.Add(st.GetSummary11());
                    }
                }
                else if (id == "pj")
                {
                    if (User.IsInRole(STRUKTURALLOKAL))
                    {
                        ViewBag.name = "Pertanggungjawaban";
                        SummaryTransactionPJ st = new SummaryTransactionPJ();
                        model.Add(st.GetSummary13());
                        model.Add(st.GetSummary14());
                        model.Add(st.GetSummary15());
                        model.Add(st.GetSummary19());
                        model.Add(st.GetSummary20());
                        model.Add(st.GetSummary21());

                    }
                    else if (User.IsInRole(STRUKTURALIMPOR))
                    {
                        ViewBag.name = "Pertanggungjawaban";
                        SummaryTransactionPJ st = new SummaryTransactionPJ();
                        model.Add(st.GetSummary1());
                        model.Add(st.GetSummary2());
                        model.Add(st.GetSummary3());
                        model.Add(st.GetSummary4());
                        model.Add(st.GetSummary5());
                        model.Add(st.GetSummary6());
                        model.Add(st.GetSummary7());
                        model.Add(st.GetSummary8());
                        model.Add(st.GetSummary9());
                        model.Add(st.GetSummary10());
                        model.Add(st.GetSummary11());
                        //model.Add(st.GetSummary13());
                        //model.Add(st.GetSummary22());
                        //model.Add(st.GetSummary23());
                        //model.Add(st.GetSummary24());
                    }

                    else
                    {
                        ViewBag.name = "Pertanggungjawaban";
                        SummaryTransactionPJ st = new SummaryTransactionPJ();
                        model.Add(st.GetSummary1());
                        model.Add(st.GetSummary2());
                        model.Add(st.GetSummary3());
                        model.Add(st.GetSummary4());
                        model.Add(st.GetSummary5());
                        model.Add(st.GetSummary6());
                        model.Add(st.GetSummary7());
                        model.Add(st.GetSummary8());
                        model.Add(st.GetSummary9());
                        model.Add(st.GetSummary10());
                        model.Add(st.GetSummary11());
                        model.Add(st.GetSummary13());
                        model.Add(st.GetSummary14());
                        model.Add(st.GetSummary15());
                        model.Add(st.GetSummary16());
                        model.Add(st.GetSummary17());
                        model.Add(st.GetSummary18());
                    }
                }
                else if (id == "plokal")
                {
                    string initial = ViewBag.initial;
                    SummaryTransactionPLK st = new SummaryTransactionPLK(initial);
                    model.Add(st.GetSummary1());
                    model.Add(st.GetSummary2());
                    model.Add(st.GetSummary3());
                    model.Add(st.GetSummary4());
                    model.Add(st.GetSummary5());

                    model.Add(st.GetSummary8());
                    model.Add(st.GetSummary9());
                    model.Add(st.GetSummary10());
                    model.Add(st.GetSummary11());
                    model.Add(st.GetSummary12());

                    model.Add(st.GetSummary13());
                    model.Add(st.GetSummary14());
                    model.Add(st.GetSummary15());
                    model.Add(st.GetSummary16());
                }
                else if (id == "pimpor")
                {
                    string initial = ViewBag.initial;
                    SummaryTransactionPLK st = new SummaryTransactionPLK(initial);
                    //model.Add(st.GetSummary1());
                    //model.Add(st.GetSummary2());
                    //model.Add(st.GetSummary3());
                    //model.Add(st.GetSummary4());
                    //model.Add(st.GetSummary5());

                    model.Add(st.GetSummary17());
                    model.Add(st.GetSummary18());
                    model.Add(st.GetSummary19());
                    model.Add(st.GetSummary22());
                    model.Add(st.GetSummary23());
                    model.Add(st.GetSummary24());
                    model.Add(st.GetSummary25());
                    model.Add(st.GetSummary26());
                    model.Add(st.GetSummary27());


                    //model.Add(st.GetSummary13());
                    //model.Add(st.GetSummary14());
                    //model.Add(st.GetSummary15());
                    //model.Add(st.GetSummary16());
                }
                else if (id == "admin")
                {
                    SummaryTransactionDBC st = new SummaryTransactionDBC();
                    model.Add(st.GetSummary1());
                    model.Add(st.GetSummary2());
                    model.Add(st.GetSummary3());
                    model.Add(st.GetSummary4());
                    model.Add(st.GetSummary5());
                    model.Add(st.GetSummary6());
                    model.Add(st.GetSummary7());
                    model.Add(st.GetSummary8());
                    model.Add(st.GetSummary9());
                    model.Add(st.GetSummary10());
                    model.Add(st.GetSummary11());
                    model.Add(st.GetSummary12());
                    model.Add(st.GetSummary13());
                    model.Add(st.GetSummary14());
                }
                else if (id == "pbarang")
                {
                    ViewBag.name = "Penerima Barang";
                    newChart();
                    SummaryTransactionPB st = new SummaryTransactionPB();
                    model.Add(st.GetSummary1());
                    model.Add(st.GetSummary2());
                    model.Add(st.GetSummary3());
                    model.Add(st.GetSummary4());
                }
                else if (id == "tagihan")
                {
                    ViewBag.name = "BAPB dan SPP";
                    SummaryTransactionBST st = new SummaryTransactionBST();
                    model.Add(st.GetSummary1());
                    model.Add(st.GetSummary2());
                    model.Add(st.GetSummary3());
                    model.Add(st.GetSummary4());
                    model.Add(st.GetSummary5());
                }
                else if (id == "uprogress")
                {
                    ViewBag.name = "User DPB";
                    string username = User.Identity.GetUserName();
                    userChart(username);
                    SummaryTransactionUDPB st = new SummaryTransactionUDPB(username);
                    model.Add(st.GetSummary1());
                    model.Add(st.GetSummary2());
                    model.Add(st.GetSummary3());
                    model.Add(st.GetSummary4());
                    model.Add(st.GetSummary5());
                    model.Add(st.GetSummary6());
                }
                else if (id == "ubarang")
                {

                }
                else
                {
                    ViewBag.noData = true;
                }

            }
            catch (Exception e)
            {
                Log.Error(e);
                ViewBag.noData = true;
            }
            return View(model);
        }

        /// <summary>
        /// membuat Chart versi summary terbaru
        /// </summary>
        private void newChart()
        {
            // grafik
            ViewBag.isGrafik = true;
            MainTransaction mt = new MainTransaction();
            List<SummaryModel> sm1 = mt.GetChart(1);
            List<SummaryModel> sm2 = mt.GetChart(2);

            List<string> x1 = new List<string>();
            List<string> y1 = new List<string>();

            if (sm1.Count() > 0)
            {
                foreach (var item in sm1)
                {
                    x1.Add(item.data2);
                    y1.Add(item.data1);
                }
            }

            ViewBag.x1 = x1;
            ViewBag.y1 = y1;

            List<string> x2 = new List<string>();
            List<string> y2 = new List<string>();

            if (sm2.Count() > 0)
            {
                foreach (var item in sm2)
                {
                    x2.Add(item.data2);
                    y2.Add(item.data1);
                }
            }

            ViewBag.x2 = x2;
            ViewBag.y2 = y2;
        }

        /// <summary>
        /// membuat Chart versi summary terbaru
        /// <param name="q1">id query struktural</param>
        /// <param name="q2">id query struktural</param>
        /// </summary>
        private void structuralChart(int q1, int q2)
        {
            // grafik
            ViewBag.isGrafik = true;
            MainTransaction mt = new MainTransaction();
            List<SummaryModel> sm1 = mt.GetChart(q1);
            List<SummaryModel> sm2 = mt.GetChart(q2);

            List<string> x1 = new List<string>();
            List<string> y1 = new List<string>();

            if (sm1.Count() > 0)
            {
                foreach (var item in sm1)
                {
                    x1.Add(item.data2);
                    y1.Add(item.data1);
                }
            }

            ViewBag.x1 = x1;
            ViewBag.y1 = y1;

            List<string> x2 = new List<string>();
            List<string> y2 = new List<string>();

            if (sm2.Count() > 0)
            {
                foreach (var item in sm2)
                {
                    x2.Add(item.data2);
                    y2.Add(item.data1);
                }
            }

            ViewBag.x2 = x2;
            ViewBag.y2 = y2;
        }

        /// <summary>
        /// Chart untuk User DPB
        /// </summary>
        private void userChart(string username)
        {
            // grafik
            ViewBag.isGrafik = true;
            MainTransaction mt = new MainTransaction();
            List<SummaryModel> sm1 = mt.GetPosUserChart(username);
            List<SummaryModel> sm2 = mt.GetNegUserChart(username);

            List<string> x1 = new List<string>();
            List<string> y1 = new List<string>();

            if (sm1.Count() > 0)
            {
                foreach (var item in sm1)
                {
                    x1.Add(item.data2);
                    y1.Add(item.data1);
                }
            }

            ViewBag.x1 = x1;
            ViewBag.y1 = y1;

            List<string> x2 = new List<string>();
            List<string> y2 = new List<string>();

            if (sm2.Count() > 0)
            {
                foreach (var item in sm2)
                {
                    x2.Add(item.data2);
                    y2.Add(item.data1);
                }
            }

            ViewBag.x2 = x2;
            ViewBag.y2 = y2;
        }

        /// <summary>
        /// handle pencarian data
        /// </summary>
        /// <param name="options">model options</param>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter"></param>
        /// <param name="searchString"></param>
        /// <param name="page">halaman yang akan ditampilkan</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Search(Search options, string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                List<NewMainDashboard> model = new List<NewMainDashboard>();
                ViewBag.useAjax = true;
                ViewBag.useDateRangePicker = true;

                ViewBag.options = options;

                // sort data
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                ViewBag.keyword = options.keyword;
                ViewBag.category = options.category;

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);

                // role data
                ViewBag.addPlk = false;
                ViewBag.addSpp = false;

                if (User.IsInRole(ADMIN) || User.IsInRole(DEVELOPER))
                {
                    ViewBag.addPlk = true;
                }

                if (User.IsInRole(BAPBSPP) || User.IsInRole(BAPB) || User.IsInRole(DEVELOPER))
                {
                    ViewBag.addSpp = true;
                }
                string table = null;
                switch (options.category)
                {
                    case "dpb":
                        table = "d.dpb";
                        break;
                    case "po":
                        table = "b.po";
                        break;
                    case "poimpor":
                        table = "b.spph";
                        break;
                    case "spph":
                        table = "b.spph";
                        break;
                    case "plk":
                        table = "pr.initials";
                        break;
                    case "udpb":
                        table = "d.user_dpb";
                        break;
                    case "jcode":
                        table = "d.job_code";
                        break;
                    default:
                        break;
                }
                options.table = table;
                model = SearchNow(options);
                if (model != null)
                {
                    ViewBag.totalRow = model.Count();
                    return View(model.ToPagedList(pageNumber, pageSize));
                }
                // jika data tidak tersedia
                ViewBag.error = "Data tidak tersedia";
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e);
                List<NewMainDashboard> model = new List<NewMainDashboard>();
                ViewBag.error = "Data tidak tersedia";
                return View(model);
            }
        }
        
        /// <summary>
        /// Search Detail
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchDetail(int? id, string query)
        {
            try
            {
                ViewBag.useDataTable = true;
                List<SearchDetail> model = new List<SearchDetail>();
                ViewBag.type = id;
                model = db.Database.SqlQuery<SearchDetail>(query).ToList();
                if(id > 0 && id == 1)
                {
                    ViewBag.typeString = "DPB";
                } else if(id > 0 && id == 2)
                {
                    ViewBag.typeString = "PO";
                }
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e);
                List<SearchDetail> model = new List<SearchDetail>();
                ViewBag.noData = true;
                return View(model);
            }
        }

        /// <summary>
        /// pengambilan data dari database ketika proses pencarian data
        /// </summary>
        /// <param name="options">model options</param>
        /// <returns></returns>
        private List<NewMainDashboard> SearchNow(Search options)
        {
            try
            {
                var query = "SELECT DISTINCT "
                            + " d.user_dpb "
                            + " ,d.tgl_pengajuan "
                            + " ,(SELECT DISTINCT count(dp.product) FROM len_enq_dpb_product dp WHERE d.dpb = dp.dpb LIMIT 1) AS jml_item "
                            + " ,d.divisi_t AS divisi "
                            + " ,d.job_code "
                            + " ,d.job_code_t "
                            + " ,d.dpb "
                            + " ,b.spph "
                            + " ,b.po "
                            + " ,(SELECT DISTINCT COUNT(op.po) FROM len_option_po op WHERE op.po_import = TRUE AND op.po = b.po) AS is_import "
                            + " ,(SELECT DISTINCT p.supplier_t FROM len_enq_po p WHERE b.po = p.po LIMIT 1) AS supplier_t "
                            + " , bap.bapb "
                            + " ,(SELECT DISTINCT st.`status` FROM len_delivered st WHERE b.po = st.po LIMIT 1) AS barang_tiba "
                            + " ,(SELECT DISTINCT sp.spp_number FROM len_spp sp WHERE b.po = sp.po LIMIT 1) AS spp "
                            + " ,d.status "
                            + " ,(SELECT DISTINCT p.`status` FROM len_enq_po p WHERE b.po = p.po LIMIT 1) AS status_po "
                            + " FROM len_enq_dpb d ";
                if(options.table == "d.dpb" || options.table == "d.user_dpb")
                {
                    query += " LEFT JOIN len_enq_spph b ON b.dpb = d.dpb ";
                    query += " LEFT JOIN len_enq_bapb bap ON bap.po = b.po ";
                }
                else if(options.table == "pr.initials")
                {
                    query += " JOIN len_executor ex ON ex.dpb = d.dpb";
                    query += " JOIN len_profile pr ON pr.user_id = ex.executor ";
                    query += " LEFT JOIN len_enq_spph b ON b.dpb = d.dpb ";
                    query += " LEFT JOIN len_enq_bapb bap ON bap.po = b.po ";
                }
                else if(options.table == "d.job_code")
                {
                    query += " LEFT JOIN len_enq_spph b ON b.dpb = d.dpb ";
                    query += " LEFT JOIN len_enq_bapb bap ON bap.po = b.po ";
                }
                else
                {
                    query += " JOIN len_enq_spph b ON b.dpb = d.dpb ";
                    query += " LEFT JOIN len_enq_bapb bap ON bap.po = b.po ";
                }
                if(options.keyword != null && options.from == null && options.to == null)
                {
                    query += " WHERE " + options.table + " LIKE '%" + options.keyword + "%'";
                }
                else if(options.keyword != null && options.from != null && options.to != null)
                {
                    query += " WHERE " + options.table + " LIKE '%" + options.keyword + "%' ";
                    query += " AND LEFT(d.tgl_pengajuan, 10) BETWEEN '" + options.from + "' AND '" +options.to + "' ";
                }
                else if(options.keyword == null && options.from != null && options.to != null)
                {
                    query += " WHERE LEFT(d.tgl_pengajuan, 10) BETWEEN '" + options.from + "' AND '" + options.to + "' ";
                }
                query += " ORDER BY d.tgl_pengajuan DESC, d.dpb DESC ";

                db.Database.CommandTimeout = 600;
                var newModel = db.Database.SqlQuery<NewMainDashboard>(query).ToList();
                foreach (var item in newModel)
                {
                    var plk = (from dp in db.executorData
                               join bd in db.profiles
                               on dp.executor equals bd.user_id
                               where dp.dpb == item.dpb
                               select bd.initials
                         )
                            .Distinct()
                            .ToList();
                    if (plk != null && plk.Count() > 0)
                    {
                        item.plk = String.Join(", ", plk);
                    }
                }

                // revisi filter user dpb dan plk
                if (User.IsInRole(USERDBP))
                {
                    string username = User.Identity.GetUserName();
                    if (username != null)
                    {
                        var forUserDpb = newModel.Where(w => w.user_dpb == username).ToList();
                        newModel = forUserDpb;
                    }
                }
                if (User.IsInRole(PELAKSANA))
                {
                    string initial = ViewBag.initial;
                    if (initial != null)
                    {
                        try
                        {
                            var forPlk = newModel.Where(w => w.plk.Contains(initial));
                            if (forPlk != null)
                            {
                                newModel = forPlk.ToList();
                            }
                        }
                        catch (Exception)
                        {
                            newModel.Clear();
                        }
                    }
                }
                // end revisi

                return newModel;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
        
        /// <summary>
        /// untuk mengetahui delta tanggal
        /// dapat dilihat di referensi
        /// </summary>
        // http://stackoverflow.com/questions/4638993/difference-in-months-between-two-dates
        public struct DateTimeSpan
        {
            private readonly int years;
            private readonly int months;
            private readonly int days;
            private readonly int hours;
            private readonly int minutes;
            private readonly int seconds;
            private readonly int milliseconds;

            /// <summary>
            /// Untuk pengurangan date
            /// </summary>
            /// <param name="years">years</param>
            /// <param name="months">months</param>
            /// <param name="days">days</param>
            /// <param name="hours">hours</param>
            /// <param name="minutes">minutes</param>
            /// <param name="seconds">seconds</param>
            /// <param name="milliseconds">milliseconds</param>
            public DateTimeSpan(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
            {
                this.years = years;
                this.months = months;
                this.days = days;
                this.hours = hours;
                this.minutes = minutes;
                this.seconds = seconds;
                this.milliseconds = milliseconds;
            }
            /// <summary>
            /// Years
            /// </summary>
            public int Years { get { return years; } }
            /// <summary>
            /// Months
            /// </summary>
            public int Months { get { return months; } }
            /// <summary>
            /// Days
            /// </summary>
            public int Days { get { return days; } }
            /// <summary>
            /// Hours
            /// </summary>
            public int Hours { get { return hours; } }
            /// <summary>
            /// Minutes
            /// </summary>
            public int Minutes { get { return minutes; } }
            /// <summary>
            /// Seconds
            /// </summary>
            public int Seconds { get { return seconds; } }
            /// <summary>
            /// Milliseconds
            /// </summary>
            public int Milliseconds { get { return milliseconds; } }

            enum Phase { Years, Months, Days, Done }
            /// <summary>
            /// Compare Dates
            /// </summary>
            /// <param name="date1">date1</param>
            /// <param name="date2">date2</param>
            /// <returns>DateTimeSpan</returns>
            public static DateTimeSpan CompareDates(DateTime date1, DateTime date2)
            {
                try
                {
                    if (date2 < date1)
                    {
                        var sub = date1;
                        date1 = date2;
                        date2 = sub;
                    }

                    DateTime current = date1;
                    int years = 0;
                    int months = 0;
                    int days = 0;

                    Phase phase = Phase.Years;
                    DateTimeSpan span = new DateTimeSpan();
                    int officialDay = current.Day;
                    while (phase != Phase.Done)
                    {
                        switch (phase)
                        {
                            case Phase.Years:
                                if (current.AddYears(years + 1) > date2)
                                {
                                    phase = Phase.Months;
                                    current = current.AddYears(years);
                                }
                                else
                                {
                                    years++;
                                }
                                break;
                            case Phase.Months:
                                if (current.AddMonths(months + 1) > date2)
                                {
                                    phase = Phase.Days;
                                    current = current.AddMonths(months);
                                    if (current.Day < officialDay && officialDay <= DateTime.DaysInMonth(current.Year, current.Month))
                                        current = current.AddDays(officialDay - current.Day);
                                }
                                else
                                {
                                    months++;
                                }
                                break;
                            case Phase.Days:
                                if (current.AddDays(days + 1) > date2)
                                {
                                    current = current.AddDays(days);
                                    var timespan = date2 - current;
                                    span = new DateTimeSpan(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                                    phase = Phase.Done;
                                }
                                else
                                {
                                    days++;
                                }
                                break;
                        }
                    }
                    return span;
                }
                catch (Exception e)
                {
                    log4net.ILog Log = log4net.LogManager.GetLogger(typeof(DateTimeSpan));
                    Log.Error(e);
                    return new DateTimeSpan();
                }
            }
        }

        /// <summary>
        /// Excel
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelActive()
        {
            ViewBag.useDatePicker = true;
            ViewBag.from = "";
            ViewBag.to = "";
            return View();
        }

        /// <summary>
        /// Excel dengan paramater
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExcelActive(string from, string to)
        {
            try
            {
                ViewBag.useDatePicker = true;
                ViewBag.from = from;
                ViewBag.to = to;
                ViewBag.info = "";
                if (from != null && to != null)
                {
                    string c = "-";
                    string[] sFrom = from.Split('/');
                    string[] sTo = to.Split('/');
                string[] colData = { "No", "Pengguna", "PLK", "DPB", "Tgl Masuk", "Divisi", "Kode Proyek", "Product", "Nama Proyek", "Description", "Account", "Unit", "Cur", "Unit price", "Order", "Curr. amount", "Total (Rupiah)", "Tgl Pengajuan", "Tgl Kebutuhan", "SPPH", "PO", "Jenis Order", "Supplier", "Order", "Cur", "Harga", "Total", "Tanggal PO", "Tanggal Kirim", "Amandemen", "Barang Tiba", "Negosiasi", "Efisiensi", "BA", "Jml Brg", "delivery time Amd", "Status", "Keterangan" };
                from = sFrom[2] + c + sFrom[1] + c + sFrom[0];
                    to = sTo[2] + c + sTo[1] + c + sTo[0];

                    MainTransaction mt = new MainTransaction();
                    List<ExcelActive> model = mt.GetExcelActive(from, to);
                    string dataCsv = null;
                    int it = 0;
                    string delimiter = "|";
                    foreach (var item in colData)
                    {
                        it++;
                        if(it < colData.Count()) {
                            dataCsv += item + delimiter;
                        }
                        else
                        {
                            dataCsv += item + Environment.NewLine;
                        }
                    }
                    it = 0;
                    foreach (var item in model)
                    {
                        it++;
                        //DateTime dateData;
                        dataCsv += it.ToString() + delimiter;
                        dataCsv += item.pengguna + delimiter;
                        //dataCsv += item.jml_item + delimiter;
                        dataCsv += item.plk + delimiter;
                        dataCsv += item.dpb + delimiter;
                        dataCsv += item.tgl_masuk + delimiter;
                        //if (item.tgl_masuk != null)
                        //{
                        //    dateData = DateTime.Parse(item.tgl_masuk);
                        //    if (item.tgl_masuk != null && dateData > DateTime.MinValue)
                        //    {
                        //        dataCsv += dateData.ToLongDateString() + delimiter;
                        //    }
                        //    else
                        //    {
                        //        dataCsv += "" + delimiter;
                        //    }
                        //}
                        //else
                        //{
                        //    dataCsv += "" + delimiter;
                        //}
                        dataCsv += item.divisi + delimiter;
                        dataCsv += item.kode_proyek + delimiter;
                        dataCsv += item.product + delimiter;
                        dataCsv += item.nama_proyek + delimiter;
                        string descriptionX = Regex.Replace(item.description, @"\r\n?|\n", "");
                        dataCsv += descriptionX + delimiter;
                        dataCsv += item.account + delimiter;
                        dataCsv += item.unit + delimiter;
                        dataCsv += item.cur + delimiter;
                        dataCsv += item.unit_price + delimiter;
                        dataCsv += item.order + delimiter;
                        dataCsv += item.cur_amount + delimiter;
                        dataCsv += item.total_idr + delimiter;
                        dataCsv += item.tgl_pengajuan + delimiter;
                        //if (item.tgl_pengajuan != null)
                        //{
                        //    dateData = DateTime.Parse(item.tgl_pengajuan);
                        //    if (item.tgl_masuk != null && dateData > DateTime.MinValue)
                        //    {
                        //        dataCsv += dateData.ToLongDateString() + delimiter;
                        //    }
                        //    else
                        //    {
                        //        dataCsv += "" + delimiter;
                        //    }
                        //}
                        //else
                        //{
                        //    dataCsv += "" + delimiter;
                        //}
                        dataCsv += item.tgl_dibutuhkan + delimiter;
                        //if (item.tgl_dibutuhkan != null)
                        //{
                        //    dateData = DateTime.Parse(item.tgl_dibutuhkan);
                        //    if (item.tgl_masuk != null && dateData > DateTime.MinValue)
                        //    {
                        //        dataCsv += dateData.ToLongDateString() + delimiter;
                        //    }
                        //    else
                        //    {
                        //        dataCsv += "" + delimiter;
                        //    }
                        //}
                        //else
                        //{
                        //    dataCsv += "" + delimiter;
                        //}
                        dataCsv += item.spph + delimiter;
                        dataCsv += item.po + delimiter;
                        dataCsv += item.jenis_order + delimiter;
                        dataCsv += item.supplier + delimiter;
                        dataCsv += item.order2 + delimiter;
                        dataCsv += item.cur2 + delimiter;
                        dataCsv += item.harga + delimiter;
                        dataCsv += item.total + delimiter;
                        dataCsv += item.tanggal_po + delimiter;
                        //if (item.tanggal_po != null)
                        //{
                        //    dateData = DateTime.Parse(item.tanggal_po);
                        //    if (item.tgl_masuk != null && dateData > DateTime.MinValue)
                        //    {
                        //        dataCsv += dateData.ToLongDateString() + delimiter;
                        //    }
                        //    else
                        //    {
                        //        dataCsv += "" + delimiter;
                        //    }
                        //}
                        //else
                        //{
                        //    dataCsv += "" + delimiter;
                        //}
                        dataCsv += item.tanggal_kirim + delimiter;
                        //if (item.tanggal_kirim != null)
                        //{
                        //    dateData = DateTime.Parse(item.tanggal_kirim);
                        //    if (item.tgl_masuk != null && dateData > DateTime.MinValue)
                        //    {
                        //        dataCsv += dateData.ToLongDateString() + delimiter;
                        //    }
                        //    else
                        //    {
                        //        dataCsv += "" + delimiter;
                        //    }
                        //}
                        //else
                        //{
                        //    dataCsv += "" + delimiter;
                        //}
                        dataCsv += item.eta + delimiter;
                        dataCsv += item.barang_tiba + delimiter;
                        dataCsv += item.negosiasi + delimiter;
                        dataCsv += item.efisiensi + delimiter;
                        dataCsv += item.bapb + delimiter;
                        dataCsv += item.jml_brg + delimiter;
                        dataCsv += item.delivery_time_amd + delimiter;
                        dataCsv += item.status + delimiter;
                        dataCsv += item.keterangan + Environment.NewLine;
                    }

                    // CSV file

                    string FileBankPhysicalFolder = Server.MapPath("~/Downloads/");

                    string dateAccess = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                    string fileName = "AMPCSV" + dateAccess + ".csv";
                    string FileBankPath = FileBankPhysicalFolder + fileName;

                    System.IO.File.WriteAllText(FileBankPath, dataCsv);

                    //Response.ContentType = "application/force-download";
                    Response.AppendHeader("Content-Type", "application/force-download;");
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.WriteFile(FileBankPath);
                    Response.End();

                    //Response.Redirect("~/Downloads/" + fileName);

                    ViewBag.info = "Berhasil diekspor";

                    // dummy
                    //ViewBag.kolom = colData;
                    //return View(model);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                ViewBag.useDatePicker = true;
                ViewBag.from = from;
                ViewBag.to = to;
                ViewBag.info = "";
                ViewBag.info = "Gagal diekspor";
            }

            return View();
        }
    }
}