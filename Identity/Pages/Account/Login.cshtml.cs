using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        public Crudential Crudential { get; set; }
        public void OnGet()
        {
        }
    }

    public class Crudential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
