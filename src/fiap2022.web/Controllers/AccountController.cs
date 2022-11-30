using fiap2022.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace fiap2022.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Time");
            }

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.UserName == "leonardo" && model.Password == "l30n4rd")
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, model.UserName));

                    var id = new ClaimsIdentity(claims, "password");
                    var principal = new ClaimsPrincipal(id);

                    await HttpContext.SignInAsync("app", principal,
                        new AuthenticationProperties() { IsPersistent = model.IsPersistent }
                        );

                    return Redirect("/time/create");

                }
            }

            return View(model);
        }



        public async Task<IActionResult> Logoff()
        {

            await HttpContext.SignOutAsync("app");

            return Redirect("/");


        }
    }
}
