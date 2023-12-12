namespace HouseRentingSystem.Contracts.Agent
{
    public interface IAgentService
    {
        Task<bool> ExistById(Guid userId);

        Task<bool> UserWithPhoneNumberExists(string phoneNumber);

        Task<bool> UserHasRent(Guid userId);

        Task Create(Guid userId, string phoneNumber);

        Task<Guid> GetAgentId(Guid userId);
    }
}
