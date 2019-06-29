using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookProjectTest.Models
{
    public class Reviewer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "FirstName can't be more than 50 character")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Last Name can't be more than 50 character")]
        public string LastName { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
