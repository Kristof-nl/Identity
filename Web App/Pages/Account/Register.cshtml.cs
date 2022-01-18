using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Web_App.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            //Validating Email address (optional)

            // Create the user
            var user = new IdentityUser
            {
                Email = RegisterViewModel.Email,
                UserName = RegisterViewModel.Email
            };
        }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [DataType(dataType: DataType.Password)]
        public string Password { get; set; }
    }


}
