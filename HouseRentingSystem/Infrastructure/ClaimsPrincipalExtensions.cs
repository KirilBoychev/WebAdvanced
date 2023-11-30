using System.Security.Claims;

namespace HouseRentingSystem.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid Id(this ClaimsPrincipal user)
        {
            Guid guid = new Guid(user.FindFirst(ClaimTypes.NameIdentifier).Value);
            return guid;
        }
    }
}
