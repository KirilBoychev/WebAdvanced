using Database;
using HouseRentingSystem.Contracts.Agent;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Agent
{
    public class AgentService : IAgentService
    {
        private readonly HouseRentingDbContext _data;
        public AgentService(HouseRentingDbContext data)
        {
            this._data = data;
        }

        public async Task Create(Guid userId, string phoneNumber)
        {
            var agent = new Database.Data.Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            await _data.Agents.AddAsync(agent);
            await _data.SaveChangesAsync();
        }

        public async Task<bool> ExistById(Guid userId)
        {
            return await _data
                        .Agents
                        .AnyAsync(a => a.UserId == userId);
        }

        public async Task<Guid> GetAgentId(Guid userId)
        {
            int agentId = _data.Agents.FirstOrDefaultAsync(a => a.UserId == userId).Id;
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(agentId).CopyTo(bytes, 0);
            Guid g = new Guid(bytes);

            return g;
        }

        public async Task<bool> UserHasRent(Guid userId)
        {
            return await _data.Houses.AnyAsync(h => h.RenterId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
        {
            return await _data.Agents.AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
