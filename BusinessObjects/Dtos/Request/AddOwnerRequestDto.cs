using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Request
{
    public class AddOwnerRequestDto
    {
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }
        
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid citizenId")]
        [Required(ErrorMessage = "CitizenId is required")]
        public string CitizenId { get; set; }
        [Required(ErrorMessage = "ContactInformation is required")]
        public string ContactInformation { get; set; }

    }
}
