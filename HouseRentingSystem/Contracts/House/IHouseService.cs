using HouseRentingSystem.Models.Houses;
using System.Collections.Generic;

namespace HouseRentingSystem.Contracts.House
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();
    }
}
