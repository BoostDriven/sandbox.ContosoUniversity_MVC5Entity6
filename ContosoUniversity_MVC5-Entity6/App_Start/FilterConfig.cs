using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity_MVC5_Entity6
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
