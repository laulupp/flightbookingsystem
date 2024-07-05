using AutoMapper;
using Backend.Api.Models;
using Backend.Persistence.Models;
using Backend.Persistence.Repositories.Interfaces;
using Backend.Services.Interfaces;
using static Backend.Exceptions.CustomExceptions;

namespace Backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordEncryptionService _passwordEncryptionService;
    private readonly ILogger _logger;

    public UserService(IUserRepository userRepository, IMapper mapper, IPasswordEncryptionService passwordEncryptionService, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordEncryptionService = passwordEncryptionService;
        _logger = logger;
    }

    public async Task<UserDTO> GetByIdAsync(int userId)
    {
        _logger.LogInformation("Retrieving user with ID: {UserId}", userId);
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> GetByUsernameAsync(string username)
    {
        _logger.LogInformation("Retrieving user with username: {Username}", username);
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        return _mapper.Map<UserDTO>(user);
    }

    public async Task ChangePasswordAsync(string username, string oldPassword, string newPassword, bool ommitOldPasswordCheck = false)
    {
        _logger.LogInformation("Changing password for username: {Username}", username);
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        if (!ommitOldPasswordCheck && !_passwordEncryptionService.VerifyPassword(oldPassword, user.Password!))
        {
            throw new InvalidPasswordException();
        }
        user.Password = _passwordEncryptionService.EncryptPassword(newPassword);
        await _userRepository.UpdateAsync(user);
    }

    public async Task UpdateInfoAsync(UserDTO userDto, string contextUsername, bool ommitUsernameCheck = false)
    {
        _logger.LogInformation("Updating user info for username: {Username}", userDto.Username);
        if (!ommitUsernameCheck && userDto.Username != contextUsername)
        {
            throw new UserUnauthorizedException();
        }

        var user = await _userRepository.GetByUsernameAsync(userDto.Username);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        //Check if email was changed. If changed, check if such an email is already registered

        var userByEmail = await _userRepository.GetByEmailAsync(userDto.Email);
        if (userByEmail != null && userByEmail.Username != userDto.Username)
        {
            throw new EmailAlreadyRegisteredException();
        }

        if (!ommitUsernameCheck && (userDto.Password == null || !_passwordEncryptionService.VerifyPassword(userDto.Password, user.Password!)))
        {
            throw new InvalidPasswordException();
        }

        _mapper.Map(userDto, user);

        await _userRepository.UpdateAsync(user);
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        _logger.LogInformation("Retrieving all users");
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task<IEnumerable<UserDTO>> GetAllCompaniesAsync()
    {
        _logger.LogInformation("Retrieving all company representatives");
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDTO>>(users.Where(u => u.Role == Role.CompanyRepresentative));
    }

    public async Task<IEnumerable<UserDTO>> GetAllCustomersAsync()
    {
        _logger.LogInformation("Retrieving all customers");
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDTO>>(users.Where(u => u.Role == Role.Traveler));
    }

    public async Task DeleteByUsernameAsync(string username)
    {
        _logger.LogInformation("Deleting user with username: {Username}", username);
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        await _userRepository.DeleteUserAndLinkagesAsync(username);
    }

    public async Task DeleteUserAndLinkagesAsync(string? username)
    {
        _logger.LogInformation("Deleting user and linkages with username: {Username}", username);
        await _userRepository.DeleteUserAndLinkagesAsync(username);
    }
}
