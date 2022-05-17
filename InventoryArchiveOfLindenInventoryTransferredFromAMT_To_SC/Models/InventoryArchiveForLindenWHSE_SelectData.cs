using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryArchiveOfLindenInventoryTransferredFromAMT_To_SC.Models
{
    public class InventoryArchiveForLindenWHSE_SelectData
    {
        public string Company { get; set; }
        public string Division_Code { get; set; }
        public string Item_Number { get; set; }
        public string Color_Code { get; set; }
        public string Warehouse_Code { get; set; }
        public string Quantity { get; set; }
        public string Remarks { get; set; }
        public string Adjust { get; set; }
        public string Date { get; set; }
        public string MaimanScriptDate { get; set; }
        public int Ticket_Num { get; set; }
        public string Standard_Price_Per_Piece { get; set; }
        public string Standard_Duty_Amount { get; set; }
        public string Standard_Import_Freight { get; set; }
        public string Standard_Total { get; set; }
        public string Actual_Price_Per_Piece { get; set; }
        public string Actual_Duty_Amount { get; set; }
        public string Actual_Import_Freight { get; set; }
        public string Actual_Total_Price_Per_Piece { get; set; }
    }
}