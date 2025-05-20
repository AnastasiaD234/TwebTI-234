using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sarkis.Models
{
     public class ProductModel
     {
          public int Id { get; set; }

          [Required]
          [Display(Name = "Nume produs")]
          public string Name { get; set; }

          [Required]
          [Display(Name = "Preț")]
          public decimal Price { get; set; }

          [Display(Name = "Descriere")]
          public string Description { get; set; }

          [Display(Name = "Imagine (URL)")]
          public string ImageUrl { get; set; }
     }
}