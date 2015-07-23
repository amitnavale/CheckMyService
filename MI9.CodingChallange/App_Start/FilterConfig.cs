using System.Web;
using System.Web.Mvc;

namespace MI9.CodingChallange
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}