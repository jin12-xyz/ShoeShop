using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Auth;
using Shop.Application.Interfaces.Services;
using Shop.Web.ViewModels.Auth;

namespace Shop.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController (IAuthService authService, IMapper mapper)
        {
            this._authService = authService;
            this._mapper = mapper;
        }

        // ------- REGISTER ----------
        [HttpGet]
        public IActionResult Register()
        {
            // Redirect if already login
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // check if email already exist
            if (await _authService.CheckExistsAsync(model.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(model);
            }
            // Map ViewModel -> DTO
            var registerDto = _mapper.Map<RegisterDto>(model);

            // register user
            var result = await _authService.RegisterUserAsync(registerDto);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Registration failed. Please try again later.");
                return View(model);
            }
            // Redirect to login after successful registration.
            TempData["Success"] = "Registration successful! Please Login.";
            return RedirectToAction(nameof(Login));
        }
        // Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity!.IsAuthenticated)
                return View("Index", "Home");

            ViewData["ResultUrl"] = returnUrl;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Map Viewmodel -> dto
            var loginDto = _mapper.Map<LoginDto>(model);
            var result = await _authService.LoginUserAsync(loginDto);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }
            // Redirect to returnURL or Home
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        //  ------ Access Denied -------
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
