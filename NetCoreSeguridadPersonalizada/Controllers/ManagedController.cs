using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NetCoreSeguridadPersonalizada.Controllers
{
    public class ManagedController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string username, string password)
        {
            if(username.ToLower() == "admin" && password == "12345")
            {
                //POR MEDIDAS DE SEGURIDAD, SE GENERA UNA COOKIE
                //CIFRADA QUE ES PARA SABER SI EL USER SE HA VALIDADO EN ESTE EXPLORADOR O NO.
                ClaimsIdentity identity =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role
                        );
                Claim claimUserName =
                    new Claim(ClaimTypes.Name, username);
                Claim claimRole =
                    new Claim(ClaimTypes.Role, "USUARIO");
                identity.AddClaim(claimUserName);
                identity.AddClaim(claimRole);
                //CREAMOS UN USUARIO PRINCIPAL CON ESTA IDENTIDAD
                ClaimsPrincipal userPrincipal =
                    new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(10)
                    });
                RedirectToAction("Perfil", "Usuarios");
            }
            ViewData["MENSAJE"] = "Credenciales incorrectas";
            return View();
        }
    }
}
