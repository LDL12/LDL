using DB.Context;
using DB.Dao.Admin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Security.Principal;

namespace Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AdminDBContext _adminDBContext;

        public LoginModel(AdminDBContext adminDBContext)
        {
            _adminDBContext = adminDBContext;
        }

        [BindProperty]
        public string? UserName { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [BindProperty]
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "���������û���/����";
                return Page();
            }

            var user = await new UserDao(_adminDBContext).LoadUserByUserName(UserName);
            if (user == null)
            {
                ErrorMessage = "�û���/�������";
                return Page();
            }

            if (!BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                ErrorMessage = "�û���/�������";
                return Page();
            }

            // ǩ����� Cookie
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, UserName),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToPage("/Index");
        }
    }
}
