using Applications.InterfaceServices;
using Applications.ViewModels.UserViewModels;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidator<LoginRequestViewModel> _loginValidator;
        private readonly IValidator<RegisterRequestViewModel> _registerValidator;
        private readonly IValidator<UpdateRequestViewModel> _updateValidator;
        public UserController(IUserService userService,
            IMapper mapper,
            IValidator<LoginRequestViewModel> loginValidator,
            IValidator<RegisterRequestViewModel> registerValidator,
            IValidator<UpdateRequestViewModel> updateValidator)
        {
            _userService = userService;
            _mapper = mapper;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
            _updateValidator = updateValidator;
        }

        //api: api/User/Register
        #region Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestViewModel registerUser)
        {
            var result = _registerValidator.Validate(registerUser);
            if (result.IsValid)
            {
                var message = await _userService.RegisterAsync(registerUser);

                return Ok(message);
            }

            return BadRequest("Register Fail");
        }
        #endregion

        //api: api/User/Login
        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestViewModel loginUser)
        {
            var result = _loginValidator.Validate(loginUser);
            if (result.IsValid)
            {
                var token = await _userService.LoginAsync(loginUser);

                return Ok(token);
            }

            return BadRequest("Login Fail");
        }
        #endregion

        //api: api/User/GetAllUser
        #region GetAllUser
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAllUser(int pageIndex = 0, int pageSize = 10)
        {
            var users = await _userService.GetUsersAsync(pageIndex, pageSize);

            //  _mapper.Map<IEnumerable<UserViewModel>>(users);
            return Ok(users);
        }
        #endregion
    }
}
