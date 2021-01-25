using System.Collections.Generic;
using Advertisement.PublicApi.Controllers.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers.Advertisement
{
    [Route("api/v1/advertisements")]
    [ApiController]
    [Authorize]
    public partial class AdvertisementController : ControllerBase
    {
        public static readonly List<Advertisement> Advertisements = new List<Advertisement>();
    }

    public sealed class Advertisement
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public UserController.User User { get; set; }
    }
    
    public sealed class AdvertisementDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public int UserId { get; set; }
    }
}