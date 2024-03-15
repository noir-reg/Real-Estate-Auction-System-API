using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Response
{
    public class OwnerUpdateResponseDto
    {
        public Guid RealEstateOwnerId { get; set; }
        public string FullName { get; set; }
        public string CitizenId { get; set; }
        public string ContactInformation { get; set; }
    }
}
