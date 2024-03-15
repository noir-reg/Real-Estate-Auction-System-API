﻿namespace BusinessObjects.Dtos.Response;

public class AuctionPostDetailResponseDto
{
    public Guid AuctionId { get; set; }
    public string? Title { get; set; }
    public Guid RealEstateId { get; set; }
    public string? RealEstateName { get; set; }
    public string? RealEstateOwnerName { get; set; }
    
    public decimal InitialPrice { get; set; }
    public decimal IncrementalPrice { get; set; }
    public string? Address { get; set; }
    public string Thumbnail { get; set; } = "https://data.lvo.vn/media/upload/1004603/online-auction-sales_istock.jpg";
    public string? Description { get; set; }
    public DateTime RegistrationStartPeriod { get; set; }
    public DateTime RegistrationEndPeriod { get; set; }
    public DateTime AuctionPeriodStart { get; set; }
    public DateTime AuctionPeriodEnd { get; set; }
    public IEnumerable<GetLegalDocumentsResponseDto>? LegalDocuments { get; set; }
}