using Shop.Application.DTOs.Auth;


namespace Shop.Application.Interfaces.Services
{
    public interface IAuthService
    {
        // Register a new user
        Task<bool> RegisterUserAsync(RegisterDto registerDto);

        // Login user
        Task<bool> LoginUserAsync(LoginDto loginDto);

        //Logout user
        Task LogoutAsync();

        // check if email exist
        Task<bool> CheckExistsAsync(string email);

    }
}
