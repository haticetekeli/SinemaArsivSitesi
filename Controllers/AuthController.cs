using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinemaArsivSitesi.Models;
using SinemaArsivSitesi.Services.Auth;

namespace SinemaArsivSitesi.Controllers
{

    [ApiController]
    [AllowAnonymous]

    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("Login")
            ]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("AddAuth")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.Register(model.Email, model.Password);
            if (result)
            {
                TempData["Success"] = "Kayıt başarılı! Giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Bu e-posta zaten kayıtlı.");
                return View(model);
            }
        }
    }
}