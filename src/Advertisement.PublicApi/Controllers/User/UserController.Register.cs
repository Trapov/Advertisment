using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers.User
{
    public partial class UserController
    {
        public class UserRegisterRequest
        {
            [Required]
            [MaxLength(30, ErrorMessage = "Максимальная длина имени не должна превышать 30 символов")]
            public string Name { get; set; }
            
            [Required]
            public string Password { get; set; }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Register(UserRegisterRequest request)
        {
            if (Users.Exists(u => u.Name == request.Name))
            {
                return Conflict("Пользователь с таким именем уже существует");
            }
            
            var user = new User
            {
                Id = Users.Count + 1,
                Name = request.Name,
                Password = request.Password
            };

            Users.Add(user);
            
            return Created($"api/v1/users/{user.Id}", user.ToDto());
        }
    }
}