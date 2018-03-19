using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Transaksi PO
    /// </summary>
    public class PoTransactions
    {
        const string AMANDEMEN = "amendment";
        const string POIMPOR = "po_import";
        const string NEGO = "negotiation";
        const string JAMINANUM = "jaminan_um";
        const string JAMINANPELAKSANAAN = "jaminan_pelaksanaan";
        const string JAMINANPEMELIHARAAN = "jaminan_pemeliharaan";
        const string ISDONE = "is_done";
        const string TGLUM = "tgl_um";
        const string TGLPELAKSANAAN = "tgl_pelaksanaan";
        const string TGLPEMELIHARAAN = "tgl_pemeliharaan";

        // Log file
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(PoTransactions));
        // database
        private ApplicationDbContext db = new ApplicationDbContext();
        string _po;

        /// <summary>
        /// construct
        /// </summary>
        /// <param name="po"></param>
        public PoTransactions(string po)
        {
            _po = po;
        }

        /// <summary>
        /// produk untuk po ketika print
        /// </summary>
        /// <returns></returns>
        public List<PrintPoProduct> GetProducts()
        {
            try
            {
                var query = "SELECT DISTINCT "
                        + " pd.product, "
                        + " p.product_t, "
                        + " pd.qty, "
                        + " p.unit, "
                        + " pd.cur, "
                        + " pd.unit_price, "
                        + " pd.cur_amount "
                        + " FROM len_enq_po_product pd "
                        + " JOIN len_enq_product p ON p.product = pd.product "
                        + " WHERE pd.po = '" + _po + "' ";
                var newModel = db.Database.SqlQuery<PrintPoProduct>(query).ToList();
                return newModel;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new List<PrintPoProduct>();
            }
        }

        /// <summary>
        /// menampilkan produk untuk po board
        /// </summary>
        /// <returns></returns>
        public List<PoDashboard> getProducts()
        {
            try
            {
                var query = "SELECT DISTINCT "
                        + " pd.product, "
                        + " p.product_t, "
                        + " pd.qty, "
                        + " pd.cur, "
                        + " p.unit, "
                        + " pd.unit_price, "
                        + " pd.cur_amount, "
                        + " pd.total_idr "
                        + " FROM len_enq_po_product pd "
                        + " JOIN len_enq_product p ON p.product = pd.product "
                        + " WHERE pd.po = '" + _po + "' ";
                var newModel = db.Database.SqlQuery<PoDashboard>(query).ToList();
                return newModel;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new List<PoDashboard>();
            }
        }

        /// <summary>
        /// mendapatkan tanggal po
        /// </summary>
        /// <returns></returns>
        public string getDatePublish()
        {
            try
            {
                var pd = db.dbPo.FirstOrDefault(p => p.po == _po);
                if (pd != null)
                {
                    return pd.tgl_po.ToShortDateString();
                }
                return null;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        /// <summary>
        /// mendapatkan tanggal habis kontrak
        /// </summary>
        /// <returns></returns>
        public string getDateEnd()
        {
            try
            {
                var pd = db.dbPo.FirstOrDefault(p => p.po == _po);
                if (pd != null)
                {
                    return pd.tgl_habis_kontrak.ToShortDateString();
                }
                return null;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        /// <summary>
        /// mendapatkan dpb untuk po
        /// </summary>
        /// <returns></returns>
        public string getDpb()
        {
            try
            {
                return db.dbSpph.FirstOrDefault(p => p.po == _po).dpb;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }

        }

        /// <summary>
        /// mendapatkan data options po
        /// </summary>
        /// <returns></returns>
        public PoOption getOption()
        {
            try
            {
                PoOption po = db.poOptionData.FirstOrDefault(p => p.po == _po);
                if (po != null)
                {
                    return po;
                }
                else
                {
                    return new PoOption()
                    {
                        amendment = false,
                        negotiation = false,
                        po_import = false
                    };
                }

            }
            catch (Exception e)
            {
                Log.Error(e);
                return new PoOption()
                {
                    amendment = false,
                    negotiation = false,
                    po_import = false
                };
            }
        }

        /// <summary>
        /// data untuk po board
        /// </summary>
        /// <returns></returns>
        public List<PoDashboard> getProductsAndNotes()
        {
            try
            {
                var products = getProducts();
                List<PoDashboard> listPoNote = new List<PoDashboard>();
                foreach (var item in products)
                {
                    PoDashboard newItem = new PoDashboard()
                    {
                        product = item.product,
                        product_t = item.product_t,
                        qty = item.qty,
                        unit = item.unit,
                        unit_price = item.unit_price,
                        cur = item.cur,
                        cur_amount = item.cur_amount,
                        total_idr = item.total_idr,
                        tgl_po = item.tgl_po,
                        tgl_habis_kontrak = item.tgl_habis_kontrak
                    };
                    newItem.note = getProductNote(item.product);
                    listPoNote.Add(newItem);
                }
                return listPoNote;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new List<PoDashboard>();
            }
        }

        /// <summary>
        /// catatan tiap produk
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string getProductNote(string code)
        {
            try
            {
                var data1 = db.noteData.FirstOrDefault(c => c.code == code && c.po == _po);
                string note = null;
                if(data1 != null)
                {
                    note = data1.note;
                }
                return note;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        /// <summary>
        /// perubahan data options po
        /// </summary>
        /// <param name="name">nama jenis options</param>
        /// <param name="state">di ceklis atau tidak</param>
        /// <returns></returns>
        public bool setOption(string name, bool state)
        {
            try
            {
                PoOption po = db.poOptionData.FirstOrDefault(p => p.po == _po);
                if (po == null)
                {
                    po = new PoOption()
                    {
                        po = _po
                    };
                    switch (name)
                    {
                        case AMANDEMEN:
                            po.amendment = state;
                            break;
                        case POIMPOR:
                            po.po_import = state;
                            break;
                        case NEGO:
                            po.negotiation = state;
                            break;
                        case JAMINANUM:
                            po.jaminan_um = state;
                            po.tgl_um = DateTime.Now;
                            break;
                        case JAMINANPELAKSANAAN:
                            po.jaminan_pelaksanaan = state;
                            po.tgl_pelaksanaan = DateTime.Now;
                            break;
                        case JAMINANPEMELIHARAAN:
                            po.jaminan_pemeliharaan = state;
                            po.tgl_pemeliharaan = DateTime.Now;
                            break;
                        case ISDONE:
                            po.is_done = state;
                            break;
                        default:
                            break;
                    }
                    db.poOptionData.Add(po);
                }
                else
                {
                    switch (name)
                    {
                        case AMANDEMEN:
                            po.amendment = state;
                            break;
                        case POIMPOR:
                            po.po_import = state;
                            break;
                        case NEGO:
                            po.negotiation = state;
                            break;
                        case JAMINANUM:
                            po.jaminan_um = state;
                            break;
                        case JAMINANPELAKSANAAN:
                            po.jaminan_pelaksanaan = state;
                            break;
                        case JAMINANPEMELIHARAAN:
                            po.jaminan_pemeliharaan = state;
                            break;
                        case ISDONE:
                            po.is_done = state;
                            break;
                        default:
                            break;
                    }
                }
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
        /// perubahan tanggal jaminan
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool setDateOption(string name, DateTime value)
        {
            try
            {
                PoOption po = db.poOptionData.FirstOrDefault(p => p.po == _po);
                if (po == null)
                {
                    po = new PoOption()
                    {
                        po = _po
                    };
                    switch (name)
                    {
                        case TGLUM:
                            po.tgl_um = value;
                            break;
                        case TGLPELAKSANAAN:
                            po.tgl_pelaksanaan = value;
                            break;
                        case TGLPEMELIHARAAN:
                            po.tgl_pemeliharaan = value;
                            break;
                        default:
                            break;
                    }
                    db.poOptionData.Add(po);
                }
                else
                {
                    switch (name)
                    {
                        case TGLUM:
                            po.tgl_um = value;
                            break;
                        case TGLPELAKSANAAN:
                            po.tgl_pelaksanaan = value;
                            break;
                        case TGLPEMELIHARAAN:
                            po.tgl_pemeliharaan = value;
                            break;
                        default:
                            break;
                    }
                }
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
        /// mendapatkan info po impor
        /// </summary>
        /// <returns></returns>
        public PoImport getImport()
        {
            try
            {
                PoImport pi = db.poImportData.FirstOrDefault(p => p.po == _po);
                if (pi == null)
                {
                    Po pd = db.dbPo.FirstOrDefault(p => p.po == _po);
                    pi = new PoImport()
                    {
                        po = pd.po,
                        tanggal = pd.tgl_po.Date,
                        ke = pd.supplier_t
                    };
                }
                return pi;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new PoImport();
            }
        }

        /// <summary>
        /// list pembayaran untuk po
        /// </summary>
        /// <returns></returns>
        public List<Payment> getPayments()
        {
            try
            {
                return db.paymentData.Where(p => p.po == _po).ToList();
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new List<Payment>();
            }
        }

        /// <summary>
        /// menampilkan data produk yang tiba
        /// </summary>
        /// <returns></returns>
        public List<PoDelivered> getProductDelivered()
        {
            var product = getProducts();
            if (product.Count() > 0)
            {
                try
                {
                    List<PoDelivered> lpod = new List<PoDelivered>();
                    foreach (var item in product)
                    {
                        int qty = 0;
                        ProductDelivered pdd = db.productDeliveredData.FirstOrDefault(p => p.po == _po && p.product == item.product);
                        if (pdd != null)
                        {
                            qty = pdd.qty_delivered;
                        }
                        PoDelivered pd = new PoDelivered()
                        {
                            product = item.product,
                            product_t = item.product_t,
                            qty = item.qty,
                            unit = item.unit,
                            qty_delivered = qty
                        };
                        lpod.Add(pd);
                    }
                    return lpod.ToList();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    return new List<PoDelivered>();
                }
            }
            else
            {
                return new List<PoDelivered>();
            }
        }

        /// <summary>
        /// menampilkan status produk yang tiba
        /// </summary>
        /// <returns></returns>
        public DeliveredInfo getDeliveredInfo()
        {
            try
            {
                DeliveredInfo di = db.deliveredInfoData.FirstOrDefault(p => p.po == _po);
                if (di == null)
                {
                    di = new DeliveredInfo();
                }
                return di;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new DeliveredInfo();
            }
        }

        /// <summary>
        /// perubahan status barang tiba
        /// </summary>
        /// <param name="po"></param>
        /// <param name="noteModel"></param>
        /// <param name="statusModel"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool setPoDelivered(string po, string noteModel, string statusModel, IEnumerable<ProductDelivered> model)
        {
            try
            {
                DeliveredInfo di = db.deliveredInfoData.FirstOrDefault(p => p.po == po);
                if (di == null)
                {
                    di = new DeliveredInfo()
                    {
                        po = po,
                        note = noteModel,
                        status = statusModel,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };
                    db.deliveredInfoData.Add(di);
                }
                else
                {
                    di.note = noteModel;
                    di.status = statusModel;
                    di.updated_at = DateTime.Now;
                }
                db.SaveChanges();

                foreach (var item in model)
                {
                    ProductDelivered pd = db.productDeliveredData.FirstOrDefault(p => p.po == po && p.product == item.product);
                    if (pd == null)
                    {
                        pd = new ProductDelivered()
                        {
                            po = po,
                            product = item.product,
                            qty_delivered = item.qty_delivered,
                            created_at = DateTime.Now,
                            updated_at = DateTime.Now
                        };
                        db.productDeliveredData.Add(pd);
                    }
                    else
                    {
                        //ProductDelivered newPd = new ProductDelivered() {
                        //    qty_delivered = item.qty_delivered,
                        //    updated_at = item.updated_at
                        //};
                        pd.qty_delivered = item.qty_delivered;
                        pd.updated_at = DateTime.Now;
                        //db.Entry(pd).CurrentValues.SetValues(newPd);
                        //db.Entry(pd).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        /// <summary>
        /// medapatkan list spp
        /// </summary>
        /// <returns></returns>
        public List<SppDashboard> getSppLists()
        {
            try
            {
                return db.sppData.Where(p => p.po == _po).ToList();
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new List<SppDashboard>();
            }
        }

        /// <summary>
        /// info spp tertentu
        /// </summary>
        /// <param name="spp">id spp</param>
        /// <returns></returns>
        public SppDashboard getSpp(int? spp)
        {
            try
            {
                SppDashboard sd = db.sppData.Find(spp);
                if (sd != null)
                {
                    var sppData = db.sppData.Where(m => m.po == _po).ToList();
                    if(sppData.Count() > 0)
                    {
                        foreach (var item in sppData)
                        {
                            if(sd.spp_number == null && item.spp_number != null)
                            {
                                sd.spp_number = item.spp_number;
                            }
                            if(sd.spp_date == null && sd.spp_date == DateTime.MinValue && item.spp_date != null && item.spp_date > DateTime.MinValue)
                            {
                                sd.spp_date = item.spp_date;
                            }
                            else
                            {
                                sd.spp_date = DateTime.Now;
                            }
                            if(sd.address == null && item.address != null)
                            {
                                sd.address = item.address;
                            }
                            if(sd.npwp == null && item.npwp != null)
                            {
                                sd.npwp = item.npwp;
                            }
                            if(sd.bank_name == null && item.bank_name != null)
                            {
                                sd.bank_name = item.bank_name;
                            }
                            if (sd.bill_number == null && item.bill_number != null)
                            {
                                sd.bill_number = item.bill_number;
                            }
                            if (sd.bill_owner == null && item.bill_owner != null)
                            {
                                sd.bill_owner = item.bill_owner;
                            }
                            if (sd.kabag_from == null && item.kabag_from != null)
                            {
                                sd.kabag_from = item.kabag_from;
                            }
                            if (sd.kabag_from_name == null && item.kabag_from_name != null)
                            {
                                sd.kabag_from_name = item.kabag_from_name;
                            }
                            if (sd.kabag_from_nik == null && item.kabag_from_nik != null)
                            {
                                sd.kabag_from_nik = item.kabag_from_nik;
                            }
                            if (sd.kabag_accounting_name == null && item.kabag_accounting_name != null)
                            {
                                sd.kabag_accounting_name = item.kabag_accounting_name;
                            }
                        }
                    }
                    return sd;
                }
                else
                {
                    return new SppDashboard();
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new SppDashboard();
            }
        }

        /// <summary>
        /// total tagihan untuk po
        /// </summary>
        /// <returns></returns>
        public double getTotalBilling()
        {
            //var pd = getProducts();
            //if (pd.Count() > 0)
            //{
            //    double total = 0;
            //    foreach (var item in pd)
            //    {
            //        total += item.total_idr;
            //    }
            //    return total;
            //}
            //return 0;

            // diedit oleh Okyza, alternatif kode yang atas
            var query = "SELECT SUM(pd.total_idr) FROM len_enq_po_product pd"
                        + " WHERE pd.po = '" + _po + "' ";
            var result = db.Database.SqlQuery<double>(query).ToList();
            return result[0];
        }

        /// <summary>
        /// tagihan yang sudah dibayar
        /// </summary>
        /// <returns></returns>
        public double getHasPayment()
        {
            double price = 0;
            try
            {
                var payment = db.paymentData.Where(w => w.po == _po && w.status == true).ToList();
                if (payment != null && payment.Count() > 0)
                {
                    foreach (var item in payment)
                    {
                        price += item.claim_value;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
            return price;
        }

        /// <summary>
        /// total pembayaran invoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public double getTotalInvoice(double id)
        {
            double price = -1;
            try
            {
                price = 0;
                SppDashboard sd = db.sppData.FirstOrDefault(f => f.spp_id == id);
                if (sd != null)
                {
                    // invoice list
                    if (sd.invoice_number != null)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        InvoiceFormat[] invoices = js.Deserialize<InvoiceFormat[]>(sd.invoice_number);
                        if (invoices.Count() > 0)
                        {
                            foreach (var invoice in invoices)
                            {
                                price = price + invoice.total;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return price;
        }

        /// <summary>
        /// mendapatkan kode jobcode
        /// </summary>
        /// <returns></returns>
        public string getJobCode()
        {
            try
            {
                return db.basisData.FirstOrDefault(p => p.po == _po).job_code;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// mendapatkan data supplier
        /// </summary>
        /// <returns></returns>
        public string getSupplier()
        {
            try
            {
                return db.dbPo.FirstOrDefault(p => p.po == _po).supplier_t;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        /// <summary>
        /// mendapatkan data supplier info (data terbaru)
        /// </summary>
        /// <returns></returns>
        public string[] getSupplierInfo()
        {
            try
            {
                string[] info = new string[4];
                string supplier = db.dbPo.FirstOrDefault(p => p.po == _po).supplier_t;
                if(supplier != null)
                {
                    info[0] = supplier;
                    Supplier sup = db.dbSupplier.FirstOrDefault(s => s.supp_id_t == supplier);
                    if(sup != null)
                    {
                        info[1] = sup.address;
                        info[2] = sup.npwp;
                        info[3] = sup.bill_number;
                    }
                }
                return info;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        //public string getBapb()
        //{
        //    try
        //    {
        //        return db.bapbData.FirstOrDefault(p => p.po == _po).bapb;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// mendapatkan tanggal jaminan uang muka
        /// </summary>
        /// <returns></returns>
        public string getUmDate()
        {
            try
            {
                var date1 = db.poOptionData.FirstOrDefault(p => p.po == _po);
                DateTime date = new DateTime();
                if(date1 != null)
                {
                    date = date1.tgl_um;
                }
                if (date == null || date == DateTime.MinValue)
                {
                    date = DateTime.Now;
                }
                return date.ToShortDateString();
            }
            catch (Exception e)
            {
                Log.Error(e);
                return DateTime.Now.ToShortDateString();
            }
        }

        /// <summary>
        /// mendapatkan data jaminan pelaksanaan
        /// </summary>
        /// <returns></returns>
        public string getPelaksanaanDate()
        {
            try
            {
                var date1 = db.poOptionData.FirstOrDefault(p => p.po == _po);
                DateTime date = new DateTime();
                if (date1 != null)
                {
                    date = date1.tgl_pelaksanaan;
                }
                if (date == null || date == DateTime.MinValue)
                {
                    date = DateTime.Now;
                }
                return date.ToShortDateString();
            }
            catch (Exception e)
            {
                Log.Error(e);
                return DateTime.Now.ToShortDateString();
            }
        }

        /// <summary>
        /// mendapatkan data jaminan pemeliharaan
        /// </summary>
        /// <returns></returns>
        public string getPemeliharaanDate()
        {
            try
            {
                var date1 = db.poOptionData.FirstOrDefault(p => p.po == _po);
                DateTime date = new DateTime();
                if (date1 != null)
                {
                    date = date1.tgl_pemeliharaan;
                }
                if (date == null || date == DateTime.MinValue)
                {
                    date = DateTime.Now;
                }
                return date.ToShortDateString();
            }
            catch (Exception e)
            {
                Log.Error(e);
                return DateTime.Now.ToShortDateString();
            }
        }
        /// <summary>
        /// mendapatkan PO sesuai nama supplier
        /// </summary>
        /// <returns></returns>
        public List<string> getMultiplePo(string supplier)
        {
            List<string> multiple = new List<string>();
            try
            {
                var pos = (from dp in db.sppData
                           join bd in db.dbPo
                           on dp.po equals bd.po
                           where bd.supplier_t == supplier
                           select new { dp.po, dp.spp_id }
                         )
                            .Distinct()
                            .ToList();
                if (pos != null && pos.Count() > 0)
                {
                    foreach (var item in pos)
                    {
                        multiple.Add(item.po + " ID SPP " + item.spp_id);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return multiple;
        }

        /// <summary>
        /// Cek apakah PO itu milik user atau bukan
        /// </summary>
        /// <param name="dpb"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool isMyPo(string dpb, string userId)
        {
            try
            {

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }
    }
}