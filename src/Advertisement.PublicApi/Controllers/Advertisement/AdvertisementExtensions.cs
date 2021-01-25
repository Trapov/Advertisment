namespace Advertisement.PublicApi.Controllers.Advertisement
{
    public static class AdvertisementExtenstions
    {
        public static AdvertisementDto ToDto(this Advertisement advertisement)
        {
            if (advertisement == null)
                return null;

            return new AdvertisementDto
            {
                Id = advertisement.Id,
                Name = advertisement.Name,
                Price = advertisement.Price,
                UserId = advertisement.User.Id
            };
        }
    }
}