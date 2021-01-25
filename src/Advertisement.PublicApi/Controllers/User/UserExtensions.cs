using System.Linq;
using System.Security.Claims;

namespace Advertisement.PublicApi.Controllers.User
{
    public static class UserExtensions
    {
        public static UserController.UserDto ToDto(this UserController.User user)
        {
            if (user == null)
                return null;

            return new UserController.UserDto
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public static UserController.UserDto ToDto(this ClaimsPrincipal principal)
        {
            return new UserController.UserDto
            {
                Id = int.Parse(principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value),
                Name = principal.Claims.First(c => c.Type == ClaimTypes.Name).Value
            };
        }
    }
}