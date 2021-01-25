using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers.User
{
    [Route("api/v1/users")]
    [ApiController]
    [AllowAnonymous]
    public partial class UserController : ControllerBase
    {
        public static readonly List<User> Users = new();

        public sealed class User
        {
            public int Id { get; set; }
            
            public string Name { get; set; }
            
            public string Password { get; set; }
        }
        
        public sealed class UserDto
        {
            public int Id { get; set; }
            
            public string Name { get; set; }
        }
    }
}