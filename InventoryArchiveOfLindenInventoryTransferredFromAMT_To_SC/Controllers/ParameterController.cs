using InventoryArchiveOfLindenInventoryTransferredFromAMT_To_SC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryArchiveOfLindenInventoryTransferredFromAMT_To_SC.Controllers
{
    public class ParameterController : Controller
    {
        DBHelper objDB = new DBHelper();
        // GET: Parameter
        public ActionResult Index()
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            DataTable dtInventoryArchive = new DataTable();
            MultiData multiData = new MultiData();

            using (SqlConnection con = new SqlConnection(objDB.ConnString))
            {
                con.Open();
                SqlDataAdapter sqlDAInventoryArchive = new SqlDataAdapter("select * from Eli_InventoryArchiveForLindenWHSE", con);
                sqlDAInventoryArchive.Fill(dtInventoryArchive);
                con.Close();


                List<Eli_InventoryArchiveForLindenWHSE> InventoryArchiveList = new List<Eli_InventoryArchiveForLindenWHSE>();

               for (int i = 0; i < dtInventoryArchive.Rows.Count; i++)
                {
                    Eli_InventoryArchiveForLindenWHSE ObjInventoryArchive = new Eli_InventoryArchiveForLindenWHSE();

                    ObjInventoryArchive.ID = Convert.ToInt32(dtInventoryArchive.Rows[i]["ID"]);
                    ObjInventoryArchive.Company = dtInventoryArchive.Rows[i]["Company"].ToString();
                    ObjInventoryArchive.Ticket_Num = Convert.ToInt32(dtInventoryArchive.Rows[i]["Ticket_Num"]);
                    InventoryArchiveList.Add(ObjInventoryArchive);
                }
                //ViewBag.InventoryArchiveList = new SelectList(InventoryArchiveList,"ID","Company", "Ticket_Num");

                multiData.EliInventoryArchiveForLindenWHSE_Data = InventoryArchiveList;

                return View(multiData);
            }
        }

        [HttpPost]
        public ActionResult AddInventoryArchive(MultiData multiData)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(objDB.ConnString))
                {
                    con.Open();
                    string query = "insert into Eli_InventoryArchiveForLindenWHSE values(@Company,@Ticket_Num)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    for (int i = 0; i < multiData.EliInventoryArchiveForLindenWHSE_Data.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@Company", multiData.EliInventoryArchiveForLindenWHSE_Data[i].Company);
                        cmd.Parameters.AddWithValue("@Ticket_Num", multiData.EliInventoryArchiveForLindenWHSE_Data[i].Ticket_Num);
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Session["Parameter"] = "Parameter Added Successfully.";
                }
            }
            catch (Exception ex)
            {
                Session["Parameter"] = "This Parameter is Already in the List."; ;
            }

            ModelState.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteInventoryArchive(int id)
        {
            bool result = false;
            try
            {
                using (SqlConnection con = new SqlConnection(objDB.ConnString))
                {

                    con.Open();
                    string query = "delete from Eli_InventoryArchiveForLindenWHSE where ID = @ID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    result = true;
                    Session["DelInventoryArchive"] = "Inventory Archive Deleteed Successfully.";
                }
            }
            catch (Exception ex)
            {
                Session["DelInventoryArchive"] = ex.Message.ToString();
            }
            ModelState.Clear();
            return RedirectToAction("Index");
        }
    }
}