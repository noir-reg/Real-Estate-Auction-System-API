using System.Linq.Expressions;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<LegalDocument> Add(LegalDocument legalDocument)
    {
        try
        {
            _context.LegalDocuments.Add(legalDocument);
            await _context.SaveChangesAsync();

            return legalDocument;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<int> GetCount(Expression<Func<LegalDocument, bool>> predicate)
    {
        var query = _context.LegalDocuments.AsQueryable();
        if (predicate != null) query = query.Where(predicate);
        return query.CountAsync();
    }
    
}