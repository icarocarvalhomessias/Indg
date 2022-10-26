using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Api.Domain.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "The Name must contain between 3 and 150 letters")]
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual List<Item> Items { get; set; }


        public Person()
        {

        }

        public Person(string Name)
        {
            this.Name = Name;
        }
    }
}
