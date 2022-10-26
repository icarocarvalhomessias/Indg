using System.ComponentModel.DataAnnotations;

namespace Trader.Api.Domain.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "The Name must contain between 3 and 150 letters")]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public int PersonId { get; set; }

        public Item()
        {

        }


        public Item(string Name, int PersonId)
        {
            this.Name = Name;
            this.PersonId = PersonId;
        }
    }
}
