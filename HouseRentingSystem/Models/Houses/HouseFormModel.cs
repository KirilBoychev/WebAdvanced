using HouseRentingSystem.Services.House.Models;
using System.ComponentModel.DataAnnotations;
using static Constants.GlobalConstants;

namespace HouseRentingSystem.Models.Houses
{
    public class HouseFormModel
    {
        [Required]
        [StringLength(TitleMaxLengthHouse, MinimumLength = TitleMinLengthHouse)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLengthHouse, MinimumLength = AddressMinLengthHouse)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(DescrptionMaxLengthHouse, MinimumLength = DescrptionMinLengthHouse)]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Range((double)PricePerMonthMin, (double)PricePerMonthMin, ErrorMessage = "Price must be a positive number and less than {2} leva")]
        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [Display(Name = "Catgory")]
        public int CategoryId { get; set; }
        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; } = new HashSet<HouseCategoryServiceModel>();
    }
}
