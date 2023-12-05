using Database;
using HouseRentingSystem.Contracts.House;
using HouseRentingSystem.Models.Houses;
using HouseRentingSystem.Services.House.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.House
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext _data;
        public HouseService(HouseRentingDbContext data)
        {
            _data = data;
        }

        public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategories()
        {
            return await _data.Categories.OrderBy(c => c.Name).Select(c => new HouseCategoryServiceModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
        }

        public async Task<bool> CategoryExist(int categoryId)
        {
            return await _data
                .Houses
                .AnyAsync(h => h.CategoryId == categoryId);
        }

        public async Task<Guid> Create(string title, string address, string description, string imageUrl, decimal price, int categoryId, Guid agentId)
        {
            var house = new Database.Data.House()
            {
                Title = title,
                Address = address,
                Description = description,
                ImageUrl = imageUrl,
                PricePerMonth = price,
                CategoryId = categoryId,
                AgentId = agentId
            };

            await _data.Houses.AddAsync(house);
            await _data.SaveChangesAsync();

            return house.Id;
        }

        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses()
        {
            return  await _data
                .Houses
                .OrderByDescending(x => x.Id)
                .Select(c => new HouseIndexServiceModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    ImageUrl = c.ImageUrl
                })
                .Take(3)
                .ToListAsync();
        }
    }
}
