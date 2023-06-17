using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DTO.UserDto;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenManager _tokenManager;

        public AuthController(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher, ITokenManager tokenManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenManager = tokenManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] ImportUserDto registerUser)
        {
            if (await _userRepository.CheckIfUserExistsByUsername(registerUser.Username))
            {
                return BadRequest("Such user already exists!");
            }

            string passwordHash = await _passwordHasher.CreatePasswordHash(registerUser.Password);

            var user = await _userRepository.RegisterUser(registerUser.Username, passwordHash);

            string token = await _tokenManager.CreateToken(user);

            return Ok(token);

        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] ImportUserDto loginUser)
        {
            if (!await _userRepository.CheckIfUserExistsByUsername(loginUser.Username))
            {
                return BadRequest("There is no user with the given username!");
            }

            var user = await _userRepository.LoginUser(loginUser.Username);

            if (!await _passwordHasher.CheckIfPasswordsAreEqual(loginUser.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password!");
            }

            string token = await _tokenManager.CreateToken(user);
            return Ok(token);
        }

    }
}
