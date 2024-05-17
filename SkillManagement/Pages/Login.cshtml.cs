using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.ViewModels;
using Services.Internfaces;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace SkillManagement.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IMainService _mainService;
        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public LoginModel(LoginViewModel loginViewModel, IMainService mainService)
        {
            LoginViewModel = loginViewModel;
            _mainService = mainService;
        }

        public void OnGet()
        {
            // Any GET specific logic can be placed here
        }

        public async Task OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                RedirectToPage("Index");
            }

            if (ModelState.IsValid)
            {
                // checking the user credentials and if the user is authorized then set the cookie and redirect to the home page
                var res = await _mainService.Login(LoginViewModel.UserName, LoginViewModel.Password);
                var tokenCookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = res.Expiration
                };
                var userDtoCookieOptions = new CookieOptions
                {
                    Expires = res.Expiration
                };

                Response.Cookies.Append("jwt", res.Token, tokenCookieOptions);
                var identityUser = await _mainService.GetUserById(res.User.AspNetUserId);
                Response.Cookies.Append("user", JsonConvert.SerializeObject(identityUser), userDtoCookieOptions);



                //making user autorized and set the cookie
                if (res.IsAuthorized)
                {
                    Response.Cookies.Append("user", JsonConvert.SerializeObject(res.User), userDtoCookieOptions);
                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name, LoginViewModel.UserName)
                    };

                    var claimIdentities = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimPrincipal = new ClaimsPrincipal(claimIdentities);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, authProperties);

                    var IsUserLoggedIn = User.Identity.IsAuthenticated;
                    RedirectToPage("Index");
                }
                else
                {
                    RedirectToPage("Index");
                }
            }


            // Perform login logic here (e.g., check credentials)
            // For demonstration, let's assume login is successful:
            bool loginSuccessful = true; // replace with actual login logic

            if (loginSuccessful)
            {
                // Redirect to a different page after successful login
                RedirectToPage("Index");
            }

            // If login fails, add a model error
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            RedirectToPage("Index");
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
