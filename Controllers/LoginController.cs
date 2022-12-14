using Assignment_4_SI.Mail;
using Assignment_4_SI.Models.API.RequestObjects;
using Assignment_4_SI.Models.API.ResponseObjects;
using Assignment_4_SI.Models.Database.UserRepository;
using Assignment_4_SI.Models.Entities;
using Assignment_4_SI.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_4_SI.Controllers;

[Produces("application/json")]
[Route("api/login")]
public class LoginController : Controller
{
    private readonly IUserRepository _repo;
    private readonly IEmailService _emailService;

    public LoginController(IUserRepository repo, IEmailService emailService)
    {
        _repo = repo;
        _emailService = emailService;
    }

    [HttpPost]
    [ProducesResponseType(201,Type = typeof(UserResponseObject))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult Login([FromBody]LoginRequestObject requestObj)
    {
        if(requestObj == null)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        User user = _repo.GetByEmail(requestObj.Email);
        if (user != null)
        {
            if (!user.EmailVerified)
                return BadRequest("Please activate your account using the email we sent you");
            if (PasswordGenerator.ValidatePassword(requestObj.Password,user.Password,user.Salt) && user.IsActive)
                return Ok(user.ToResponseObject());
        }
        return Unauthorized();
    }
    //set the EmailVerified field to true
    //this can't be accomplished using the UserController's PUT action
    [HttpGet]
    [Route("~/api/login/activate")]
    [ProducesResponseType(200,Type = typeof(UserResponseObject))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult Activate([FromQuery]ActivationRequestObject requestObj)
    {
        if (requestObj == null)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        User user = _repo.GetByEmail(requestObj.Email);
        if(user != null)
        {
            if(user.Password == requestObj.Pass && user.IsActive)
            {
                user.EmailVerified = true;
                _repo.Update(user);
                return Ok(user.ToResponseObject());
            }
        }
        return Unauthorized();
    }
    //TODO forgot password flow
    [HttpPost]
    [Route("~/api/login/reset")]
    [ProducesResponseType(201,Type = typeof(UserResponseObject))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult ResetPassword([FromBody]ResetPasswordRequestObject requestObj)
    {
        if (requestObj == null)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        User u = _repo.GetByEmail(requestObj.Email);
        if(u != null && u.IsActive && u.EmailVerified)
        {
            //update user password
            var password = PasswordGenerator.CreateRandomPassword(8);
            var hashedPassword = PasswordGenerator.EncryptPassword(password);
            u.Password = hashedPassword.HashedPwd;
            u.Salt = hashedPassword.Salt;
            _repo.Update(u);

            //send password reset mail
            _emailService.SendPasswordResetMail(new Tuple<string, User>(password, u));

            return Ok();
        }
        return Unauthorized();
    }
    
}