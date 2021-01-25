using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        [HttpGet]
        [AllowAnonymous]
        public IList<AdvertisementDto> GetAll([FromQuery]GetAllRequest request)
        {
            return Advertisements.Select(advertisement => advertisement.ToDto())
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToList();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public AdvertisementDto GetById(int id)
        {
            return Advertisements
                .First(advertisement => advertisement.Id == id).ToDto();
        }

        public class GetAllRequest
        {
            /// <summary>
            /// Количество возвращаемых объявлений
            /// </summary>
            public int Limit { get; set; } = 20;
            
            /// <summary>
            /// Смещение начиная с котрого возвращаются объявления
            /// </summary>
            public int Offset { get; set; } = 0;
        }
    }
}