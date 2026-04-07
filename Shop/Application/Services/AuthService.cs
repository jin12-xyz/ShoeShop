using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Shop.Application.DTOs.Auth;
using Shop.Application.Interfaces.Services;
using Shop.Models.Domain;

namespace Shop.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }
        public async Task<bool> RegisterUserAsync(RegisterDto registerDto)
        {
            // map dto to application user
            var user = _mapper.Map<ApplicationUser>(registerDto);

            // create new user identity
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return false;

            // Assign default role 
            await _userManager.AddToRoleAsync(user, "Customer");

            return true;
        }

        public async Task<bool> LoginUserAsync(LoginDto loginDto)
        {
            // find user email
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return false;

            // Signin with password
            var result = await _signInManager.PasswordSignInAsync(
                user,
                loginDto.Password,
                loginDto.RememberMe,
                lockoutOnFailure: true); // lock account on failure attempts

            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> CheckExistsAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);

            return result != null;
        }
    }
}
