using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDataController : ControllerBase
    {

        private readonly IUserRepository _userRepo;
        public UserDataController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }


        [HttpGet]
        [Authorize("AdminOnly")]
        public async Task<IEnumerable<User>> Get()
        {
            var users = await _userRepo.All();
            return users;
        }

        [HttpGet("{userLogin}")]
        public async Task<ActionResult> GetUser([FromRoute] string userLogin)
        {
            User? user = await _userRepo.GetByLogin(userLogin);
            return user != null ? Ok(user): StatusCode(StatusCodes.Status404NotFound);
        }


    }
}
