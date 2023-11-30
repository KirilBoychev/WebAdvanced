using Database;
using HouseRentingSystem.Contracts.House;
using HouseRentingSystem.Models.Houses;

namespace HouseRentingSystem.Services.House
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext _data;
        public HouseService(HouseRentingDbContext data)
        {
            _data = data;
        }

        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses()
        {
            return _data
                .Houses
                .OrderByDescending(x => x.Id)
                .Select(c => new HouseIndexServiceModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    ImageUrl = c.ImageUrl
                })
                .Take(3);
        }
    }
}
