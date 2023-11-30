using System.ComponentModel.DataAnnotations;
using static Constants.GlobalConstants;

namespace Database.Data
{
    public class Category
    {
        public Category()
        {
            this.Houses = new HashSet<House>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLengthCategory)]
        public string Name { get; set; } = null!;

        public ICollection<House> Houses { get; set; }
    }

//    Id – a unique integer, Primary Key
//• Name – a string with max length 50 (required)
//• Houses – a collection of House

}
