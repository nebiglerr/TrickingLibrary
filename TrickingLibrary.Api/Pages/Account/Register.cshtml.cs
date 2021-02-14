using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrickingLibrary.Pages.Account
{
    public class Register : PageModel
    {
       
        [BindProperty] public RegisterForm Form { get; set; } = new RegisterForm();
        public void OnGet(string returnUrl)
        {
            Form = new RegisterForm
            {
                ReturnUrl = returnUrl
            };

        }

        public async Task<IActionResult> OnPostAsync([FromServices] UserManager<IdentityUser> userManager,[FromServices] SignInManager<IdentityUser> signInManager)
        {
            if(ModelState.IsValid)

                return Page();
            var user = new IdentityUser(Form.Email);
         var createResult =   await userManager.CreateAsync(user,Form.Password);
            if (createResult.Succeeded)
            {
                await signInManager.SignInAsync(user,true);
                
                return Redirect(Form.ReturnUrl);
            }

            return Page();
        }
        public class RegisterForm
        {
            [Required] public string ReturnUrl { get; set; }            
            [Required] public string Email { get; set; }            
            [Required] 
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Required] 
            [DataType(DataType.Password)]
            [Compare(nameof(Password))]
            public string ConfirmPassword { get; set; }            
        }
    }
}