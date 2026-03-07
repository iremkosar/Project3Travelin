using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.Controllers
{
    public class LanguageController : Controller
    {
        [HttpPost]
        public IActionResult Change(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl ?? "/");
        }
    }
}
