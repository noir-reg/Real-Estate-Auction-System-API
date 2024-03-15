using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Request
{
    public class OwnerUpdateRequestDto
    {
        
        public string? FullName { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid citizenId")]
        public string? CitizenId { get; set; }
        public string? ContactInformation { get; set; }
    }
}
