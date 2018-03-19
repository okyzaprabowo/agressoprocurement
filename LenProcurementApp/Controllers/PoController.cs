using LenProcurementApp.Models;
using Microsoft.AspNet.Identity;
using NumberText;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LenProcurementApp.Controllers
{
    /// <summary>
    /// Controller untuk menghandle perubahan yang terkait PO
    /// </summary>
    [Authorize]
    public class PoController : BaseController
    {
        // Log file
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(PoController));
        // Database
        private ApplicationDbContext db = new ApplicationDbContext();

        const string LenAddress = "PT. Len Industri (Persero) \nJl. Soekarno-Hatta No.442 Regol Bandung 40254 - Indonesia";

        /// <summary>
        /// PO view controller
        /// </summary>
        /// <param name="po">kode PO</param>
        /// <returns>List produk</returns>
        public ActionResult POBoard(string po)
        {
            try
            {
                ViewBag.useAjax = true;
                ViewBag.useIcheck = true;
                ViewBag.useDataTable = true;
                ViewBag.useDatePicker = true;
                ViewBag.po = po;
                ViewBag.editPoBoard = false;

                if (po != null && po != "")
                {
                    PoTransactions pt = new PoTransactions(po);
                    ViewData["poOption"] = pt.getOption();
                    ViewData["poPayment"] = pt.getPayments();
                    var model = pt.getProductsAndNotes();
                    if (model.Count() > 0)
                    {
                        ViewBag.created = pt.getDatePublish();
                        ViewBag.limited = pt.getDateEnd();
                    }
                    ViewBag.umDate = pt.getUmDate();
                    ViewBag.pelDate = pt.getPelaksanaanDate();
                    ViewBag.pemDate = pt.getPemeliharaanDate();
                    ViewBag.dpb = pt.getDpb();
                    double totalBilling = pt.getTotalBilling();
                    double hasPayment = pt.getHasPayment();
                    double theTotal = totalBilling - hasPayment;
                    ViewBag.balance = theTotal.ConvertToDot();

                    if (User.IsInRole(PELAKSANA) || User.IsInRole(DEVELOPER))
                    {
                        string nDpb = pt.getDpb();
                        bool isMine = false;
                        string uid = User.Identity.GetUserId();
                        if(nDpb != null && uid != null)
                        {
                            isMine = pt.isMyPo(nDpb, uid);
                        }
                        if (isMine)
                        {
                            ViewBag.editPoBoard = true;
                        }
                    }

                    return View(model);
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
        /// PO import view controller
        /// menampilkan form
        /// </summary>
        /// <param name="po">Kode PO</param>
        /// <returns>Model POImport</returns>
        public ActionResult POImport(string po)
        {
            try
            {
                ViewBag.po = po;
                ViewBag.saved = 0;
                ViewBag.addPoImport = false;

                if (User.IsInRole(PELAKSANA) || User.IsInRole(DEVELOPER))
                {
                    ViewBag.useDatePicker = true;
                    ViewBag.addPoImport = true;
                }

                if (po != null && po != "")
                {
                    PoTransactions pt = new PoTransactions(po);
                    var model = pt.getImport();
                    if (model != null)
                    {
                        var ppp = pt.GetProducts();
                        if (ppp.Count() > 0)
                        {
                            ViewBag.cur = ppp.First().cur;
                        }
                        ViewBag.listProducts = ppp;
                        model.delivery_to = LenAddress;
                        return View(model);
                    }
                }
                ViewBag.noData = true;
                return View();
            }
            catch (Exception e)
            {
                Log.Error(e);
                ViewBag.po = po;
                ViewBag.saved = 0;
                ViewBag.addPoImport = false;
                ViewBag.noData = true;
                return View();
            }
        }

        /// <summary>
        /// Post PO Import controller
        /// </summary>
        /// <param name="model">PO Import model</param>
        /// <returns>PO Import model</returns>
        [HttpPost]
        public ActionResult POImport(PoImport model)
        {
            ViewBag.po = model.po;
            ViewBag.addPoImport = false;

            if (User.IsInRole(PELAKSANA) || User.IsInRole(DEVELOPER))
            {
                ViewBag.useDatePicker = true;
                ViewBag.addPoImport = true;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    PoImport pi = db.poImportData.Find(model.po_import_id);
                    if (pi == null)
                    {
                        model.created_at = DateTime.Now;
                        model.updated_at = DateTime.Now;
                        db.poImportData.Add(model);
                        db.SaveChanges();
                    }
                    else
                    {
                        //pi = model;
                        model.updated_at = DateTime.Now;
                        db.Entry(pi).CurrentValues.SetValues(model);
                        db.Entry(pi).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    if (model.state == 0)
                    {
                        PoTransactions pt = new PoTransactions(model.po);
                        var ppp = pt.GetProducts();
                        if (ppp.Count() > 0)
                        {
                            ViewBag.cur = ppp.First().cur;
                        }
                        ViewBag.listProducts = ppp;
                        ViewBag.saved = 1;
                        return View(model);
                    }
                    else if (model.state == 1)
                    {
                        return RedirectToAction("PrintPOImport", "Out", new { po = model.po });
                    }
                    else if (model.state == 2)
                    {
                        return RedirectToAction("PDFPoImport", "Out", new { po = model.po });
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    throw;
                }
            }
            ViewBag.saved = 2;
            return View(model);
        }

        /// <summary>
        /// Post Delete controller
        /// </summary>
        /// <param name="po">Kode PO</param>
        /// <returns>true jika sukses</returns>
        [HttpPost]
        public bool POImportDelete(string po)
        {
            try
            {
                PoImport pi = db.poImportData.FirstOrDefault(p => p.po == po);
                if (pi != null)
                {
                    db.poImportData.Remove(pi);
                }
                PoOption pop = db.poOptionData.FirstOrDefault(p => p.po == po);
                pop.po_import = false;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        /// <summary>
        /// View Controller untuk barang tiba
        /// </summary>
        /// <param name="po">Kode PO</param>
        /// <returns>Model PO Delivered</returns>
        public ActionResult DeliveredBoard(string po)
        {
            try
            {
                ViewBag.useIcheck = true;
                ViewBag.useDataTable = true;
                ViewBag.po = po;
                ViewBag.addDelivered = false;

                // status kedatangan barang
                // F = full, P = partial, N = none
                ViewBag.full = "F";
                ViewBag.partial = "P";
                ViewBag.none = "N";

                if (User.IsInRole(PENERIMABARANG) || User.IsInRole(DEVELOPER))
                {
                    ViewBag.addDelivered = true;
                }

                if (po != null && po != "")
                {
                    PoTransactions pt = new PoTransactions(po);
                    ViewBag.created = pt.getDatePublish();
                    var model = pt.getProductDelivered();
                    if (model != null)
                    {
                        ViewBag.dpb = pt.getDpb();
                        DeliveredInfo di = pt.getDeliveredInfo();
                        ViewBag.note = di.note;
                        ViewBag.status = di.status;
                        return View(model);
                    }
                }
                ViewBag.noData = true;
                return View();
            }
            catch (Exception e)
            {
                Log.Error(e);
                ViewBag.useIcheck = true;
                ViewBag.useDataTable = true;
                ViewBag.po = po;
                ViewBag.addDelivered = false;

                // status kedatangan barang
                // F = full, P = partial, N = none
                ViewBag.full = "F";
                ViewBag.partial = "P";
                ViewBag.none = "N";
                ViewBag.noData = true;
                return View();
            }
        }

        /// <summary>
        /// Post untuk menyimpan barang tiba
        /// </summary>
        /// <param name="po">Kode PO</param>
        /// <param name="noteModel">Keterangan</param>
        /// <param name="statusModel">Status apakah barang sudah sampai semua atau belum</param>
        /// <param name="model">Model Product delivered</param>
        /// <returns>True jika sukses</returns>
        [HttpPost]
        public bool DeliveredBoard(string po, string noteModel, string statusModel, IEnumerable<ProductDelivered> model)
        {
            try
            {
                PoTransactions pt = new PoTransactions(po);
                return pt.setPoDelivered(po, noteModel, statusModel, model);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        /// <summary>
        /// SPP board view controller
        /// </summary>
        /// <param name="po">Kode PO</param>
        /// <returns>Model SPP dashboard</returns>
        public ActionResult SPPBoard(string po)
        {
            try
            {
                ViewBag.useDataTable = true;
                ViewBag.po = po;
                ViewBag.addSpp = false;

                if (User.IsInRole(BAPBSPP) || User.IsInRole(BAPB) || User.IsInRole(DEVELOPER))
                {
                    ViewBag.addSpp = true;
                }

                if (po != null && po != "")
                {
                    PoTransactions pt = new PoTransactions(po);
                    var model = pt.getSppLists();
                    //DateTime sppDate = DateTime.MinValue;
                    //string bankName = null;
                    //string billNumber = null;
                    //string billOwner = null;
                    List<SppBoard> sbModel = new List<SppBoard>();
                    if (model.Count() > 0)
                    {
                        foreach (var item in model)
                        {
                            SppBoard newSb = new SppBoard()
                            {
                                spp_id = item.spp_id,
                                spp_number = item.spp_number,
                                spp_date = item.spp_date,
                                payment_for = item.payment_for,
                                bank_name = item.bank_name,
                                bill_number = item.bill_number,
                                bill_owner = item.bill_owner,
                                claim_value = pt.getTotalInvoice(item.spp_id),
                                note = item.note
                            };

                            //if (item.spp_date != null && item.spp_date > DateTime.MinValue)
                            //{
                            //    sppDate = item.spp_date;
                            //}
                            //if (bankName == null)
                            //{
                            //    bankName = item.bank_name;
                            //}
                            //if (billNumber == null)
                            //{
                            //    billNumber = item.bill_number;
                            //}
                            //if (billOwner == null)
                            //{
                            //    billOwner = item.bill_owner;
                            //}

                            sbModel.Add(newSb);
                        }
                    }
                    //ViewBag.sppDate = sppDate;
                    //ViewBag.bankName = bankName;
                    //ViewBag.billNumber = billNumber;
                    //ViewBag.billOwner = billOwner;
                    return View(sbModel);
                }
                ViewBag.noData = true;
                return View();
            }
            catch (Exception e)
            {
                Log.Error(e);
                ViewBag.useDataTable = true;
                ViewBag.po = po;
                ViewBag.addSpp = false;
                ViewBag.noData = true;
                return View();
            }
        }

        /// <summary>
        /// Form pembuatan SPP
        /// </summary>
        /// <param name="po">Kode PO</param>
        /// <param name="spp">ID SPP</param>
        /// <returns>Model SPPH Dashboard</returns>
        [Authorize(Roles = BAPBSPPDEVELOPER)]
        public ActionResult SPPForm(string po, int? spp)
        {
            try
            {
                ViewBag.useAjax = true;
                ViewBag.useDatePicker = true;
                ViewBag.useIcheck = true;
                ViewBag.po = po;
                ViewBag.supplier = "";
                ViewBag.address = "";
                ViewBag.npwp = "";
                ViewBag.billNumber = "";
                ViewBag.cur = "";
                if (po != null && po != "")
                {
                    PoTransactions pt = new PoTransactions(po);
                    //var poOptions = db.sppData.Select(s => s.po).ToList();
                    generatePaymentDropDown(false);
                    SppDashboard model = new SppDashboard();
                    string supplier = pt.getSupplier();
                    string[] supplierInfo = pt.getSupplierInfo();
                    if(supplier != null)
                    {
                        ViewBag.supplier = supplier;
                    }
                    model.spp_date = DateTime.Now;
                    var cur = db.dbPoProduct.FirstOrDefault(f => f.po == po);
                    if (cur != null)
                    {
                        try
                        {
                            ViewBag.cur = cur.cur;
                        }
                        catch(ArgumentNullException)
                        {
                            ViewBag.cur = "0";
                        }
                        
                    }

                    List<string> poOptions = pt.getMultiplePo(ViewBag.supplier);
                    ViewBag.options = poOptions;

                    if (spp > 0)
                    {
                        model = pt.getSpp(spp);
                        if (model != null)
                        {
                            if (model.spp_date == null || model.spp_date == DateTime.MinValue)
                            {
                                model.spp_date = DateTime.Now;
                            }
                            if (supplierInfo != null && supplierInfo.Count() > 0)
                            {
                                model.address = model.address != null ? model.address : supplierInfo[1];
                                model.npwp = model.npwp != null ? model.npwp : supplierInfo[2];
                                model.bill_number = model.bill_number != null ? model.bill_number : supplierInfo[3];
                            }
                            ViewBag.attachmentOptions = model.attachment;

                            JavaScriptSerializer js = new JavaScriptSerializer();
                            InvoiceFormat[] invoices = js.Deserialize<InvoiceFormat[]>(model.invoice_number);
                            if (invoices.Count() > 0)
                            {
                                    foreach (var invoice in invoices)
                                    {
                                        model.invoice_num = invoice.number;
                                        model.invoice_date = invoice.date;
                                        model.invoice_value = invoice.total;
                                    }
                            }

                            return View(model);
                        }
                        ViewBag.noData = true;
                        return View();
                    }
                    else
                    {
                        if (supplierInfo != null && supplierInfo.Count() > 0)
                        {
                            model.address = model.address != null ? model.address : supplierInfo[1];
                            model.npwp = model.npwp != null ? model.npwp : supplierInfo[2];
                            model.bill_number = model.bill_number != null ? model.bill_number : supplierInfo[3];
                        }
                    }
                    return View(model);
                }
                ViewBag.noData = true;
                return View();
            }
            catch (Exception e)
            {
                Log.Error(e);
                ViewBag.useAjax = true;
                ViewBag.useDatePicker = true;
                ViewBag.useIcheck = true;
                ViewBag.po = po;
                ViewBag.cur = "";
                ViewBag.supplier = "";
                ViewBag.address = "";
                ViewBag.npwp = "";
                ViewBag.billNumber = "";
                ViewBag.noData = true;
                return View();
            }
        }

        /// <summary>
        /// Post penyimpanan SPP
        /// </summary>
        /// <param name="model">SPP Dashboard model</param>
        /// <returns>True jika sukses</returns>
        [HttpPost]
        public bool SPPForm(SppDashboard model)
        {
            try
            {
                SppDashboard sd = db.sppData.Find(model.spp_id);
                if (sd == null)
                {
                    if(model.spp_number == null)
                    {
                        model.spp_number = "Belum ada No SPP";
                    }
                    model.spp_date = DateTime.Now;
                    model.created_at = DateTime.Now;
                    model.updated_at = DateTime.Now;
                    db.sppData.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    model.updated_at = DateTime.Now;
                    db.Entry(sd).CurrentValues.SetValues(model);
                    db.Entry(sd).State = EntityState.Modified;
                    db.SaveChanges();
                }
                TempData["tempModel"] = model;
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        /// <summary>
        /// Menghapus SPP
        /// </summary>
        /// <param name="po">Kode PO</param>
        /// <param name="id">ID SPP</param>
        /// <returns></returns>
        public ActionResult SPPDelete(string po, int? id)
        {
            if (id > 0)
            {
                try
                {
                    SppDashboard sd = db.sppData.Find(id);
                    db.sppData.Remove(sd);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    throw;
                }
            }
            return RedirectToAction("SPPBoard", new { po = po });
        }

        /// <summary>
        /// Payment Board controller
        /// </summary>
        /// <param name="po">Kode PO</param>
        /// <returns>Model Payment</returns>
        public ActionResult PaymentBoard(string po)
        {
            ViewBag.useIcheck = true;
            ViewBag.useDataTable = true;
            ViewBag.po = po;
            ViewBag.addPayment = false;
            try
            {
                if (User.IsInRole(ADMIN) || User.IsInRole(DEVELOPER))
                {
                    ViewBag.addPayment = true;
                }

                if (po != null && po != "")
                {
                    PoTransactions pt = new PoTransactions(po);
                    var model = pt.getPayments();
                    //if (model.Count > 0)
                    //{
                    //string dpb = pt.getDpb();
                    //ViewBag.dpb = dpb;
                    return View(model);
                    //}
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
        /// Form pembuatan Payment (Pembayaran)
        /// </summary>
        /// <param name="po">Kode PO</param>
        /// <param name="pay">ID payment</param>
        /// <returns>Model Payment</returns>
        public ActionResult PaymentForm(string po, int? pay)
        {
            ViewBag.useDateRangePicker = true;
            ViewBag.useDatePicker = true;
            ViewBag.useIcheck = true;
            ViewBag.po = po;
            ViewBag.isImport = false;
            try
            {
                if (po != null && po != "")
                {
                    var poImport = db.poOptionData.FirstOrDefault(f => f.po == po);
                    if (poImport != null)
                    {
                        ViewBag.isImport = poImport.po_import;
                    }
                    generatePaymentDropDown(ViewBag.isImport);
                    Payment model = new Payment()
                    {
                        payment_schedule = DateTime.Now,
                        claim_date = DateTime.Now
                    };
                    if (pay > 0)
                    {
                        try
                        {
                            model = db.paymentData.Find(pay);
                            if (model != null)
                            {
                                return View(model);
                            }
                            ViewBag.noData = true;
                            return View();
                        }
                        catch (Exception e)
                        {
                            Log.Error(e);
                            return View(model);
                        }
                    }
                    return View(model);
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
        /// Post penyimpanan Payment
        /// </summary>
        /// <param name="model">Model Payment</param>
        /// <returns>Model Payment</returns>
        [HttpPost]
        public ActionResult paymentForm(Payment model)
        {
            ViewBag.useDatePicker = true;
            ViewBag.po = model.po;
            ViewBag.isImport = false;
            try
            {
                var poImport = db.poOptionData.FirstOrDefault(f => f.po == model.po);
                if (poImport != null)
                {
                    ViewBag.isImport = poImport.po_import;
                }
                generatePaymentDropDown(ViewBag.isImport);
                if (ModelState.IsValid)
                {
                    try
                    {
                        Payment pm = db.paymentData.Find(model.payment_id);
                        if (pm == null)
                        {
                            model.created_at = DateTime.Now;
                            model.updated_at = DateTime.Now;
                            db.paymentData.Add(model);
                        }
                        else
                        {
                            model.updated_at = DateTime.Now;
                            db.Entry(pm).CurrentValues.SetValues(model);
                            db.Entry(pm).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                        ViewBag.success = true;
                        return View(model);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            ViewBag.failed = true;
            return View(model);
        }

        /// <summary>
        /// Hapus pembayaran
        /// </summary>
        /// <param name="po">Kode PO</param>
        /// <param name="id">ID Payment</param>
        /// <returns>Kode PO</returns>
        public ActionResult PaymentDelete(string po, int? id)
        {
            if (id > 0)
            {
                try
                {
                    Payment pm = db.paymentData.Find(id);
                    db.paymentData.Remove(pm);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    throw;
                }
            }
            return RedirectToAction("PaymentBoard", new { po = po });
        }

        /// <summary>
        /// Generate untuk dropdown list pendukung payment
        /// </summary>
        private void generatePaymentDropDown(bool isImport)
        {
            try
            {
                PaymentDropDown pd = new PaymentDropDown();
                ViewBag.arrayPayment = pd.payment().Select(s => new SelectListItem { Value = s.name, Text = s.name });
                ViewBag.arrayMethod = pd.method(isImport).Select(s => new SelectListItem { Value = s.name, Text = s.name });
                ViewBag.arrayStatus = pd.status().Select(s => new SelectListItem { Value = s.id.ToString(), Text = s.name });
                ViewBag.arrayImportTax = pd.importTax().Select(s => new SelectListItem { Value = s.id.ToString(), Text = s.name });
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// Class dropdown untuk pembayaran
        /// </summary>
        class PaymentDropDown
        {
            public bool id;
            public string name;
            // Log file
            log4net.ILog Log = log4net.LogManager.GetLogger(typeof(PaymentDropDown));
            public List<PaymentDropDown> payment()
            {
                try
                {
                    string[] array = new string[7] { "Uang Muka", "Termin 1", "Termin 2", "Termin 3", "Termin 4", "Termin 5", "Termin Lainnya" };
                    List<PaymentDropDown> list = new List<PaymentDropDown>();
                    foreach (var item in array)
                    {
                        list.Add(new PaymentDropDown() { name = item });
                    }
                    return list;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    return null;
                }
            }

            public List<PaymentDropDown> method(bool isImport)
            {
                try
                {
                    string[] array = new string[2];
                    if (isImport)
                    {
                        array[0] = "T/T";
                        array[1] = "L/C";
                    }
                    else
                    {
                        array[0] = "SKBDN";
                        array[1] = "Cash";
                    }
                    List<PaymentDropDown> list = new List<PaymentDropDown>();
                    foreach (var item in array)
                    {
                        list.Add(new PaymentDropDown() { name = item });
                    }
                    return list;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    return null;
                }
                
            }

            public List<PaymentDropDown> status()
            {
                try
                {
                    List<PaymentDropDown> list = new List<PaymentDropDown>();
                    list.Add(new PaymentDropDown() { id = false, name = "Belum Selesai" });
                    list.Add(new PaymentDropDown() { id = true, name = "Sudah Selesai" });
                    return list;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    return null;
                }
            }

            public List<PaymentDropDown> importTax()
            {
                try
                {
                    List<PaymentDropDown> list = new List<PaymentDropDown>();
                    list.Add(new PaymentDropDown() { id = false, name = "Belum Dibayar" });
                    list.Add(new PaymentDropDown() { id = true, name = "Sudah Dibayar" });
                    return list;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    return null;
                }
            }
        }

        /// <summary>
        /// Option untuk PO (amandemen, impor dan negosiasi)
        /// </summary>
        /// <param name="po">Kode PO</param>
        /// <param name="name">Option PO</param>
        /// <param name="state">true jika di checklist</param>
        /// <returns>True jika sukses</returns>
        [HttpPost]
        public bool SubmitOption(string po, string name, bool state)
        {
            try
            {
                PoTransactions pt = new PoTransactions(po);
                bool result = pt.setOption(name, state);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        /// <summary>
        /// fungsi untuk tanggal jaminan
        /// </summary>
        /// <param name="po">kode PO</param>
        /// <param name="name">jaminan pelaksanaan atau uang muka</param>
        /// <param name="value">tanggal jaminan</param>
        /// <returns></returns>
        [HttpPost]
        public bool SubmitDateOption(string po, string name, DateTime value)
        {
            try
            {
                PoTransactions pt = new PoTransactions(po);
                //DateTime dtime = DateTime.Parse(value);
                bool result = pt.setDateOption(name, value);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }
    }
}