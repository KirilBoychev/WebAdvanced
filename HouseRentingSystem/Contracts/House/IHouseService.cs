using HouseRentingSystem.Models.Houses;
using HouseRentingSystem.Services.House.Models;
using System.Collections.Generic;

namespace HouseRentingSystem.Contracts.House
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();

        Task<IEnumerable<HouseCategoryServiceModel>> AllCategories();
    }
}
