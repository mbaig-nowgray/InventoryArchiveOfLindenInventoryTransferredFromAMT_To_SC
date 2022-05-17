using System.Web;
using System.Web.Mvc;

namespace InventoryArchiveOfLindenInventoryTransferredFromAMT_To_SC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
