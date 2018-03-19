using LenProcurementApp.Models;
using NumberText;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Rotativa;

namespace LenProcurementApp.Controllers
{
    /// <summary>
    /// Class untuk print out data SPP dan PO Impor
    /// </summary>
    //[Authorize]
    public class OutController : BaseController
    {
        const string TRANSFER_MODEL = "tempModel";
        // Log file
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(OutController));
        // Database
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Print PO Impor
        /// </summary>
        /// <param name="po">Kode PO dalam bentuk string</param>
        /// <returns></returns>
        public ActionResult PrintPOImport(string po)
        {
            try
            {
                ViewBag.po = po;
                PoImport pi = db.poImportData.FirstOrDefault(p => p.po == po);
                if (pi != null)
                {
                    PrintPOImport ppi = new PrintPOImport()
                    {
                        po = pi.po,
                        tanggal = pi.tanggal,
                        ke = pi.ke,
                        alamat = pi.alamat,
                        no_telp = pi.no_telp,
                        no_fax = pi.no_fax,
                        attn = pi.attn,
                        ref_po = pi.ref_po,
                        condition = pi.condition,
                        term_of_payment = pi.term_of_payment,
                        pay_to_bank = pi.pay_to_bank,
                        shipment = pi.shipment,
                        delivery_time = pi.delivery_time,
                        delivery_to = pi.delivery_to,
                        attn_on_delivery = pi.attn_on_delivery,
                        manager = pi.manager
                    };
                    PoTransactions pt = new PoTransactions(po);
                    var ppp = pt.GetProducts();
                    if (ppp.Count() > 0)
                    {
                        ViewBag.cur = ppp.First().cur;
                        float total = 0;
                        foreach (var item in ppp)
                        {
                            total += item.cur_amount;
                        }
                        ViewBag.total = total;
                    }

                    ViewBag.listProducts = ppp;
                    return View(ppi);
                }
                ViewBag.noData = true;
                return View();
            }
            catch (Exception e)
            {
                Log.Error(e);
                ViewBag.noData = true;
                return View();
            }
        }

        /// <summary>
        /// Print SPP
        /// </summary>
        /// <returns>SPP Print model</returns>
        public ActionResult PrintSPP()
        {
            try
            {
                PrintSPPNew ps = new PrintSPPNew();
                if (TempData[TRANSFER_MODEL] != null)
                {
                    SppDashboard sppd = TempData[TRANSFER_MODEL] as SppDashboard;
                    ps.address = sppd.address;
                    ps.another = sppd.another;
                    ps.attachment = sppd.attachment;
                    ps.bank_name = sppd.bank_name;
                    ps.bill_number = sppd.bill_number;
                    ps.bill_owner = sppd.bill_owner;
                    ps.document_number = sppd.document_number;

                    ps.invoice_number = sppd.invoice_number;

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    InvoiceFormat[] invoices = js.Deserialize<InvoiceFormat[]>(ps.invoice_number);
                    if (invoices.Count() > 0)
                    {
                        foreach (var invoice in invoices)
                        {
                            ps.invoice_num = invoice.number;
                            ps.invoice_date = DateTime.ParseExact(invoice.date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            ps.invoice_value = invoice.total;

                            ViewBag.intWord = ps.invoice_value.ToText();
                        }
                    }

                    //ps.note = sppd.note;
                    ps.npwp = sppd.npwp;
                    ps.payment_for = sppd.payment_for;
                    ps.po = sppd.po;
                    //ps.spp_date = sppd.spp_date;
                    ps.spp_number = sppd.spp_number;
                    ps.kabag_from = sppd.kabag_from;
                    ps.kabag_from_name = sppd.kabag_from_name;
                    ps.kabag_from_nik = sppd.kabag_from_nik;
                    ps.kabag_accounting_name = sppd.kabag_accounting_name;

                    var getDpb =
                            from basis in db.dbSpph
                            join dpb in db.dbDpb on basis.dpb equals dpb.dpb
                            where basis.po == ps.po
                            select new { jobcode = dpb.job_code, jobcodet = dpb.job_code_t };
                    var firstData = getDpb.FirstOrDefault();
                    ps.job_code_x = firstData.jobcode + " - " + firstData.jobcodet;

                    ps.supplier = sppd.supplier;
                }
                return View(ps);
            }
            catch (Exception e)
            {
                Log.Error(e);
                PrintSPPNew ps = new PrintSPPNew();
                return View(ps);
            }
        }

        /// <summary>
        /// Fungsi untuk menghandle beberapa PO
        /// </summary>
        /// <returns></returns>
        public ActionResult MultiplePrint()
        {
            return View();
        }

        /// <summary>
        /// Fungsi untuk menghandle beberapa PO
        /// </summary>
        /// <param name="model">Model multiple print</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MultiplePrint(MultiplePrint model)
        {
            try
            {
                model.invoice_number = new List<InvoiceFormat>();
                model.payment_for = new List<string>();
                model.another = new List<string>();
                model.job_code_x = new List<string>();
                string[] attachs = new string[] { "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false" };
                double price = 0;
                string firstPo = null;
                if(model.multiple_po == null)
                {
                    return View();
                }

                // START NEW
                List<string> listIdSpp = new List<string>();
                foreach (string item in model.multiple_po)
                {
                    string[] splitString = item.Split(' ');
                    string getSppId = splitString[3];
                    listIdSpp.Add(getSppId);
                }
                string idString = string.Join(",",listIdSpp);
                string query = "SELECT DISTINCT s.spp_id, ( SELECT job_code FROM len_enq_dpb WHERE dpb = z.dpb LIMIT 1 ) AS job_code, ( SELECT job_code_t FROM len_enq_dpb WHERE dpb = z.dpb LIMIT 1 ) AS job_code_t, s.po, s.supplier, s.address, s.npwp, s.invoice_number, s.bank_name, s.bill_number, s.bill_owner, s.payment_for, s.another, s.attachment, s.kabag_from, s.kabag_from_name, s.kabag_from_nik, s.kabag_accounting_name FROM len_spp s JOIN len_enq_spph z ON z.po = s.po WHERE s.spp_id IN (" + idString +");";
                List<NewMultiPrint> newMulti = new List<NewMultiPrint>();
                var newModel = db.Database.SqlQuery<NewMultiPrint>(query);
                if(newModel != null)
                {
                    newMulti = newModel.ToList();
                }

                if (newMulti.Count() > 0)
                {
                    foreach (var item in newMulti)
                    {
                        // invoice list
                        if (item.invoice_number != null)
                        {
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            InvoiceFormat[] invoices = js.Deserialize<InvoiceFormat[]>(item.invoice_number);
                            if (invoices.Count() > 0)
                            {
                                foreach (var invoice in invoices)
                                {
                                    model.invoice_number.Add(invoice);
                                    price = price + invoice.total;
                                }
                            }
                        }
                        // untuk pembayaran
                        if (item.payment_for != null)
                        {
                            model.payment_for.Add(item.payment_for);
                        }
                        // attachment
                        if (item.attachment != null)
                        {
                            //attachs.Add(sd.attachment);
                            bool[] boolString = Array.ConvertAll(item.attachment.Split(','), bool.Parse);
                            for (int i = 0; i < boolString.Count(); i++)
                            {
                                if (boolString[i])
                                {
                                    attachs[i] = "true";
                                }
                            }
                            model.attachment = string.Join(",", attachs);
                        }
                        // lain lain
                        if (item.another != null)
                        {

                            model.another.Add(item.another);
                        }
                        // job code dan job code t
                        if (item.job_code != null && item.job_code_t != null)
                        {
                            model.job_code_x.Add(item.job_code + " - " + item.job_code_t);
                        }
                    }
                }

                // END NEW

                // START OLD

                //foreach (string item in model.multiple_po)
                //{
                //    string[] splitString = item.Split(' ');
                //    string getPo = splitString[0];
                //    int getSppId = Int32.Parse(splitString[3]);
                //    if(firstPo == null)
                //    {
                //        firstPo = getPo;
                //    }
                //    SppDashboard sd = db.sppData.FirstOrDefault(f => f.spp_id == getSppId);
                //    if (sd != null)
                //    {
                //        // invoice list
                //        if (sd.invoice_number != null)
                //        {
                //            JavaScriptSerializer js = new JavaScriptSerializer();
                //            InvoiceFormat[] invoices = js.Deserialize<InvoiceFormat[]>(sd.invoice_number);
                //            if (invoices.Count() > 0)
                //            {
                //                foreach (var invoice in invoices)
                //                {
                //                    model.invoice_number.Add(invoice);
                //                    price = price + invoice.total;
                //                }
                //            }
                //        }
                //        // untuk pembayaran
                //        if (sd.payment_for != null)
                //        {
                //            model.payment_for.Add(sd.payment_for);
                //        }
                //        // attachment
                //        if (sd.attachment != null)
                //        {
                //            //attachs.Add(sd.attachment);
                //            bool[] boolString = Array.ConvertAll(sd.attachment.Split(','), bool.Parse);
                //            for (int i = 0; i < boolString.Count(); i++)
                //            {
                //                if (boolString[i])
                //                {
                //                    attachs[i] = "true";
                //                }
                //            }
                //            model.attachment = string.Join(",", attachs);
                //        }
                //        // lain lain
                //        if (sd.another != null)
                //        {

                //            model.another.Add(sd.another);
                //        }
                //        // job code dan job code t
                //        var getDpb =
                //            from basis in db.dbSpph
                //            join dpb in db.dbDpb on basis.dpb equals dpb.dpb
                //            where basis.po == getPo
                //            select new { jobcode = dpb.job_code, jobcodet = dpb.job_code_t };
                //        var firstData = getDpb.FirstOrDefault();
                //        if (firstData != null)
                //        {
                //            if (firstData.jobcode != null && firstData.jobcodet != null)
                //            {
                //                model.job_code_x.Add(firstData.jobcode + " - " + firstData.jobcodet);
                //            }
                //        }
                //    }

                //}

                // END OLD

                SavePo(model);
                ViewBag.total = price.ConvertToDot();
                ViewBag.intWord = price.ToText();

                //Session["multimodel"] = model;
                //Session["total"] = price;
                //Session["intWord"] = price.ToText();

                model.total = price;
                model.intWord = price.ToText();

                var modelx = new JavaScriptSerializer().Serialize(model);

                if (model.state == 2)
                {
                    return RedirectToAction("PDFMultiPO", new { modelx = modelx });
                }
                else if (model.state == 3)
                {
                    string redPo = firstPo;
                    return RedirectToAction("SPPBoard", "Po", new { po = redPo });
                }
                else 
                {
                    Session["modelxx"] = modelx;
                    //return RedirectToAction("NewMultiplePrint", new { modelx = modelx });
                    return RedirectToAction("NewMultiplePrint");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return View();
            }
            //return View(model);
        }
        
        /// <summary>
        /// Simpan model ke SPP dengan PO yang ada di multiple PO
        /// </summary>
        /// <param name="model"></param>
        private void SavePo(MultiplePrint model)
        {
            try
            {
                foreach (string item in model.multiple_po)
                {
                    string[] splitString = item.Split(' ');
                    string getPo = splitString[0];
                    int getSppId = Int32.Parse(splitString[3]);
                    SppDashboard sd = db.sppData.FirstOrDefault(f => f.spp_id == getSppId);
                    if (sd != null)
                    {
                        //SppDashboard nsd = new SppDashboard();
                        sd.spp_number = model.spp_number;
                        sd.spp_date = model.spp_date;
                        sd.address = model.address;
                        sd.npwp = model.npwp;
                        sd.bank_name = model.bank_name;
                        sd.bill_number = model.bill_number;
                        sd.bill_owner = model.bill_owner;
                        sd.kabag_from = model.kabag_from;
                        sd.kabag_from_name = model.kabag_from_name;
                        sd.kabag_from_nik = model.kabag_from_nik;
                        sd.kabag_accounting_name = model.kabag_accounting_name;
                        sd.updated_at = DateTime.Now;
                        //db.Entry(sd).CurrentValues.SetValues(nsd);
                        db.Entry(sd).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// cetak pdf po impor
        /// </summary>
        /// <param name="po"></param>
        /// <returns></returns>
        public ActionResult PDFPoImport(string po)
        {
            //return RedirectToAction("PrintPOImport", new { po = id });
            try
            {
                return new ActionAsPdf("PrintPOImport", new { po = po })
                {
                    FileName = "AMP - PO Impor - " + DateTime.Now.ToShortDateString() + ".pdf"
                };
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        /// <summary>
        /// Cetak PDF multi PO
        /// </summary>
        /// <param name="modelx"></param>
        /// <returns></returns>
        public ActionResult PDFMultiPO(string modelx)
        {
            try
            {
                return new ActionAsPdf("NewMultiplePrint", new { modelx = modelx })
                {
                    FileName = "AMP - SPP - " + DateTime.Now.ToShortDateString() + ".pdf"
                };
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        /// <summary>
        /// Multiple print SPP
        /// </summary>
        /// <returns></returns>
        public ActionResult NewMultiplePrint()
        {
            try
            {
                string modelx = Session["modelxx"].ToString();
                MultiplePrint model = new JavaScriptSerializer().Deserialize<MultiplePrint>(modelx);
                if (model != null)
                {

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
    }
}