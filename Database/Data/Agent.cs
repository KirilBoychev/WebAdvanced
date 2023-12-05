    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constants.GlobalConstants;
using Microsoft.AspNetCore.Identity;

namespace Database.Data
{
    public class Agent
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhonenumberMaxLengthAgent)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<House> OwnedHouses { get; set; } = new HashSet<House>();
    }

//    • Id – a unique integer, Primary Key
//• PhoneNumber – a string with min length 7 and max length 15 (required)
//• UserId – a string (required)
//• User – an IdentityUser object
}
