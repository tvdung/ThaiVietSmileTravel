using System.Web;
using System.Web.Mvc;

namespace ThaiViet_Smile_Travel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
