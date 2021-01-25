using System.ComponentModel.DataAnnotations;
using System.Linq;
using Advertisement.PublicApi.Controllers.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create(AdvertisementCreateRequest request)
        {
            var userDto = HttpContext.User.ToDto();
            
            var user = UserController.Users.FirstOrDefault(u => u.Id == userDto.Id);
            if (user == null)
            {
                return BadRequest($"Не существует пользователя с Id: {userDto.Id}");
            }

            var advertisement = new Advertisement
            {
                Id = Advertisements.Count + 1,
                User = user,
                Name = request.Name,
                Price = request.Price
            };
            
            Advertisements.Add(advertisement);

            return Created($"api/v1/advertisements/{advertisement.Id}", advertisement.ToDto());
        }

        public sealed class AdvertisementCreateRequest
        {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }
            
            [Required]
            [Range(0, 100_000_000_000)]
            public decimal Price { get; set; }
        }
    }
}