using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryArchiveOfLindenInventoryTransferredFromAMT_To_SC.Models
{
    public class MultiData
    {
        public List<Eli_InventoryArchiveForLindenWHSE> EliInventoryArchiveForLindenWHSE_Data { get; set; }
        public List<InventoryArchiveForLindenWHSE> InventoryArchiveForLindenWHSE_Data { get; set; }
        public List<InventoryArchiveForLindenWHSE_SelectData> InventoryArchiveForLindenWHSE_SelectData_List { get; set; }
        
    }

    
}