using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
     public class UDbTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

     
          [Required]
          [Display(Name ="Username")]
          [StringLength(30, MinimumLength =5, ErrorMessage ="Username cannot be longer than 30 characters.")]

          public string Username { get; set; }

          [Required]
          [Display(Name = "Password")]
          [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 characters.")]

          public string Password { get; set; }

          [Required]
          [Display(Name = "Email")]
          [StringLength(30)]

          public string Email { get; set; }

          public URole Level { get; set; }
     }
}
