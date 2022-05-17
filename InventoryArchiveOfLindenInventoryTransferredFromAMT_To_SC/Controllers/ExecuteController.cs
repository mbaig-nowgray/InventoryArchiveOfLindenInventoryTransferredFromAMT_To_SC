using InventoryArchiveOfLindenInventoryTransferredFromAMT_To_SC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using ClosedXML.Excel;

namespace InventoryArchiveOfLindenInventoryTransferredFromAMT_To_SC.Controllers
{
    public class ExecuteController : Controller
    {
        DBHelper objDB = new DBHelper();
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //InventoryArchiveForLindenWHSE ObjInventoryArchiveForLindenWHSE = new InventoryArchiveForLindenWHSE();   

            ViewBag.ComapanyData = new SelectList(GetCompanies(), "Company", "Company");
            return View();
        }

        public List<Customer> GetCompanies()
        {
            List<Customer> listCompany = new List<Customer>();
            DataTable dtCompany = new DataTable();
            using (SqlConnection con = new SqlConnection(objDB.ConnString))
            {

                con.Open();
                SqlDataAdapter sqlDACompany = new SqlDataAdapter("select Company from Eli_InventoryArchiveForLindenWHSE group by Company", con);
                sqlDACompany.Fill(dtCompany);
                con.Close();

                for (int i = 0; i < dtCompany.Rows.Count; i++)
                {
                    Customer ObjCompany = new Customer();
                    ObjCompany.Company = dtCompany.Rows[i]["Company"].ToString();
                    listCompany.Add(ObjCompany);
                }
                return listCompany;
            }
        }

        public ActionResult GetTicketNo(string Company)
        {
            List<Customer> listTicketNo = new List<Customer>();
            DataTable dtTicketNo = new DataTable();
            using (SqlConnection con = new SqlConnection(objDB.ConnString))
            {

                con.Open();
                SqlDataAdapter sqlDATicketNo = new SqlDataAdapter("select * from Eli_InventoryArchiveForLindenWHSE where Company ='" + Company + "'", con);
                sqlDATicketNo.Fill(dtTicketNo);
                con.Close();

                for (int i = 0; i < dtTicketNo.Rows.Count; i++)
                {
                    Customer ObjDivision = new Customer();
                    ObjDivision.Ticket_Num = int.Parse(dtTicketNo.Rows[i]["Ticket_Num"].ToString());
                    listTicketNo.Add(ObjDivision);
                }

                ViewBag.TicketNo = new SelectList(listTicketNo, "Ticket_Num", "Ticket_Num");
                return PartialView("TicketNoPartial");

                // return listTicketNo;

                //var division = new SelectList(listTicketNo, "Ticket_Num", "Ticket_Num");
                //return Json(Ticket_Num, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Execute(InventoryArchiveForLindenWHSE IAFL)
        {
            try
            {
                if (IAFL.Company == null)
                {
                    ViewBag.ComapanySelect = "Select Comapany";
                }
                if (IAFL.Ticket_Num == null)
                {
                    ViewBag.TicketNoSelect = "Select Ticket Number";
                }
                using (SqlConnection con = new SqlConnection(objDB.ConnString))
                {
                    DataTable dtDownload = new DataTable();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SP_Get_Eli_InventoryArchiveForLindenWHSE_SelectData", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CompanyCode", SqlDbType.Char).Value = IAFL.Company;
                    cmd.Parameters.Add("@TicketNo", SqlDbType.Char).Value = IAFL.Ticket_Num;

                    SqlDataAdapter sqlDADownload = new SqlDataAdapter(cmd);
                    sqlDADownload.Fill(dtDownload);
                    cmd.ExecuteNonQuery();

                    con.Close();

                    using (XLWorkbook wb = new XLWorkbook())
                    {

                        var wsCompany = wb.Worksheets.Add(dtDownload, "Inventory Archive");
                        wsCompany.Columns().AdjustToContents();


                        wb.SaveAs(Server.MapPath("~/temp/") + "excel.xlsx");

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=Report.xlsx");
                        WebClient req = new WebClient();
                        byte[] data = req.DownloadData(Server.MapPath("~/temp/") + "excel.xlsx");
                        Response.BinaryWrite(data);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
            }

            return RedirectToAction("Execute/Index");
            //return View("Index");

        }
        
        public ActionResult Create(InventoryArchiveForLindenWHSE IAFL)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(objDB.ConnString))
                {
                    DataTable dtDownload = new DataTable();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SP_Get_Eli_InventoryArchiveForLindenWHSE_SelectData", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CompanyCode", SqlDbType.Char).Value = IAFL.Company;
                    cmd.Parameters.Add("@TicketNo", SqlDbType.Char).Value = IAFL.Ticket_Num;

                    SqlDataAdapter sqlDADownload = new SqlDataAdapter(cmd);
                    sqlDADownload.Fill(dtDownload);
                    cmd.ExecuteNonQuery();

                    con.Close();

                    using (XLWorkbook wb = new XLWorkbook())
                    {

                        var wsCompany = wb.Worksheets.Add(dtDownload, "Inventory Archive");
                        wsCompany.Columns().AdjustToContents();


                        wb.SaveAs(Server.MapPath("~/temp/") + "excel.xlsx");

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=Report.xlsx");
                        WebClient req = new WebClient();
                        byte[] data = req.DownloadData(Server.MapPath("~/temp/") + "excel.xlsx");
                        Response.BinaryWrite(data);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
            }

            return RedirectToAction("Execute/Index");
            //return View("Index");

        }


        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //protected ActionResult DownloadInventoryArchive(string Company, string Ticket_Num)
        protected ActionResult DownloadInventoryArchive()
        {
            using (SqlConnection con = new SqlConnection(objDB.ConnString))
            {
                DataTable dtDownload = new DataTable();
                con.Open();

                SqlCommand cmd = new SqlCommand("SP_Get_Eli_InventoryArchiveForLindenWHSE_SelectData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CompanyCode", SqlDbType.Char).Value = "05";//Company;
                cmd.Parameters.Add("@TicketNo", SqlDbType.Char).Value = "144";  //Ticket_Num;

                SqlDataAdapter sqlDADownload = new SqlDataAdapter(cmd);
                sqlDADownload.Fill(dtDownload);
                cmd.ExecuteNonQuery();

                con.Close();

                using (XLWorkbook wb = new XLWorkbook())
                {

                    var wsCompany = wb.Worksheets.Add(dtDownload, "Inventory Archive");
                    wsCompany.Columns().AdjustToContents();


                    wb.SaveAs(Server.MapPath("~/temp/") + "excel.xlsx");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Report.xlsx");
                    WebClient req = new WebClient();
                    byte[] data = req.DownloadData(Server.MapPath("~/temp/") + "excel.xlsx");
                    Response.BinaryWrite(data);
                    Response.End();
                }
            }        
           
            return RedirectToAction("Execute/Index");

            // return RedirectToAction("Index");
        }
       
        [HttpPost]
        public ActionResult DownloadInventoryArchive(FormCollection collection)
        {
            try
            {
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}