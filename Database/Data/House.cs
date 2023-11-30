using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Constants.GlobalConstants;

namespace Database.Data
{
    public class House
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLengthHouse)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLengthHouse)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(DescrptionMaxLengthHouse)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Range((double)PricePerMonthMin, (double)PricePerMonthMax)]
        public decimal PricePerMonth { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        [Required]
        public Guid AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        public virtual Agent Agent { get; set; } = null!;

        public Guid? RenterId { get; set; }

        public virtual ApplicationUser? Renter { get; set; }
    }

//    • Id – a unique integer, Primary Key
//• Title – a string with min length 10 and max length 50 (required)
//• Address – a string with min length 30 and max length 150 (required)
//• Description – a string with min length 50 and max length 500 (required)
//• ImageUrl – a string (required)
//• PricePerMonth – a decimal with min value 0 and max value 2000 (required)
//• CategoryId – an integer(required)
//• Category – a Category object
//• AgentId – an integer(required)
//• Agent – an Agent object
//• RenterId – a string
}
