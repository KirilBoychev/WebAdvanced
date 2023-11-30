using System.ComponentModel.DataAnnotations;
using static Constants.GlobalConstants;

namespace HouseRentingSystem.Models.Agents
{
    public class BecomeAgentFormModel
    {
        [Required]
        [StringLength(PhonenumberMaxLengthAgent, MinimumLength = PhonenumberMinLengthAgent)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
