using System.ComponentModel.DataAnnotations;
using BusinessObjects.Entities;

namespace BusinessObjects.Dtos.Request;

public class CreateAuctionRequestDto
{
    [Required(ErrorMessage = "Admin Id is required")]
    public Guid AdminId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; } = AuctionStatus.ToBeSold;


    [Required(ErrorMessage = "RegistrationPeriodEnd is required")]
    [DataType(DataType.Date)]
    public DateTime RegistrationPeriodEnd { get; set; }

    [Required(ErrorMessage = "RegistrationPeriodStart is required")]
    [DataType(DataType.Date)]
    public DateTime RegistrationPeriodStart { get; set; }

    [Required(ErrorMessage = "InitialPrice is required")]
    public decimal InitialPrice { get; set; }

    [Required(ErrorMessage = "AuctionPeriodStart is required")]
    [DataType(DataType.Date)]
    public DateTime AuctionPeriodStart { get; set; }

    [Required(ErrorMessage = "AuctionPeriodEnd is required")]
    [DataType(DataType.Date)]
    public DateTime AuctionPeriodEnd { get; set; }

    [Required(ErrorMessage = "IncrementalPrice is required")]
    public decimal IncrementalPrice { get; set; }

    [Required(ErrorMessage = "RealEstateCode is required")]
    public string RealEstateCode { get; set; }

    public Guid OwnerId { get; set; }
}