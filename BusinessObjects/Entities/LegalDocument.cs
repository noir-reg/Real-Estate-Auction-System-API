﻿namespace BusinessObjects.Entities;

public class LegalDocument
{
    public Guid DocumentId { get; set; }
    public string FileName { get; set; }
    public string DocumentUrl { get; set; }
    public string Description { get; set; }
    public string DocumentType { get; set; }
    public Guid AuctionId { get; set; }
    public Auction Auction { get; set; }
}