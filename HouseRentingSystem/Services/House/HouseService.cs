using Database;
using Database.Data;
using HouseRentingSystem.Contracts.House;
using HouseRentingSystem.Infrastructure;
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

        public HouseQueryServiceModel All(string category = null, string searchTerm = null, HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housesPerPage = 1)
        {
            var housesQuery = _data.Houses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                housesQuery = _data.Houses.Where(h => h.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                housesQuery = _data.Houses.Where(h =>
                    h.Title.ToLower().Contains(searchTerm.ToLower()) ||
                    h.Address.ToLower().Contains(searchTerm.ToLower()) ||
                    h.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            housesQuery = sorting switch
            {
                HouseSorting.Price => housesQuery.OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesQuery.OrderBy(h => h.RenterId != null)
                                                .ThenByDescending(h => h.Id),
                _ => housesQuery.OrderByDescending(h => h.Id)
            };

            var houses = housesQuery
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(h => new HouseServiceModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth
                })
                .ToList();

            var totalHouses = housesQuery.Count();

            return new HouseQueryServiceModel()
            {
                TotalHousesCount = totalHouses,
                Houses = houses
            };
        }

        public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategories()
        {
            return await _data.Categories
                .Select(c => new HouseCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoriesNames()
        {
            return await _data
                            .Categories
                            .Select(c => c.Name)
                            .Distinct()
                            .ToListAsync();
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(Guid agentId)
        {
            var houses =  await _data
                            .Houses
                            .Where(h => h.AgentId == agentId)
                            .Distinct()
                            .ToListAsync();

            return ProjectToModel(houses);
        }

        private IEnumerable<HouseServiceModel> ProjectToModel(List<Database.Data.House> houses)
        {
            IEnumerable<HouseServiceModel> housesNew = houses
                                                            .Select(h => new HouseServiceModel
                                                            {
                                                                Id = h.Id,
                                                                Title = h.Title,
                                                                Address = h.Address,
                                                                ImageUrl = h.ImageUrl,
                                                                PricePerMonth = h.PricePerMonth,
                                                                IsRented = h.RenterId != null
                                                            })
                                                            .ToList();
            return housesNew;
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(Guid userId)
        {
            var houses = await _data
                                    .Houses
                                    .Where(h => h.RenterId == userId)
                                    .Distinct()
                                    .ToListAsync();

            return ProjectToModel(houses);
        }

        public async Task<bool> CategoryExist(int categoryId)
        {
            return await _data
                .Categories
                .AnyAsync(c => c.Id == categoryId);
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
            return await _data
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
