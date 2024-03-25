using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Models.Houses;
using HouseRentingSystem.Services.House.Models;
using System.Collections.Generic;

namespace HouseRentingSystem.Contracts.House
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();

        Task<IEnumerable<HouseCategoryServiceModel>> AllCategories();

        Task<bool> CategoryExist(int categoryId);

        Task<Guid> Create(string title, string address, string description,
            string imageUrl, decimal price, int categoryId, Guid agentId);

        HouseQueryServiceModel All(string category = null,
            string searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1);

        Task<IEnumerable<string>> AllCategoriesNames();

        Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(Guid agentId);
        Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(Guid userId);
    }
}
