using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Pages.HumanResources
{
    [Authorize(Policy = "MustBelongToHRDepartment")]
    public class HumanResourcesModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
