using BusinessObjects.Entities;

namespace Repositories;

public class LegalDocumentRepository : ILegalDocumentRepository
{
    private readonly RealEstateDbContext _context;

    public LegalDocumentRepository()
    {
        _context = new RealEstateDbContext();
    }

    public IQueryable<LegalDocument> GetLegalDocumentQuery()
    {
        return _context.LegalDocuments.AsQueryable();
    }
}