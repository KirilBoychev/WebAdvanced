using Microsoft.AspNetCore.Identity;

namespace Database.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.RentedHouses = new HashSet<House>();
        }
        public virtual ICollection<House> RentedHouses { get; set; }
    }
}
