using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Ad.Contracts;
using Advertisement.PublicApi.Controllers.User;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _adService.Delete(new Delete.Request
            {
                Id = id
            }, cancellationToken);
            
            // var userDto = HttpContext.User.ToDto();
            // var user = UserController.Users.FirstOrDefault(u => u.Id == userDto.Id);
            // if (user == null)
            // {
            //     return BadRequest($"Не существует пользователя с Id: {userDto.Id}");
            // }
            //
            // var advertisement = Advertisements.FirstOrDefault(adv => adv.Id == id);
            // if (advertisement == null)
            // {
            //     return NotFound($"Не существует объявления с Id:{id}");
            // }
            //
            // if (advertisement.User.Id != user.Id)
            // {
            //     return Forbid("Нет прав на удаление данного объявления");
            // }
            //
            // Advertisements.Remove(advertisement);

            return NoContent();
        }
    }
}