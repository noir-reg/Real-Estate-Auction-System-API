using System.Linq.Expressions;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Repositories;

public interface ILegalDocumentRepository
{
    IQueryable<LegalDocument> GetLegalDocumentQuery();
    Task<LegalDocument> Add(LegalDocument legalDocument);
    // Task<LegalDocument?> GetLegalDocument(Expression<Func<LegalDocument, bool>> predicate);
    // Task<ListResponseBaseDto<LegalDocument>> GetLegalDocuments(Expression<Func<LegalDocument, bool>> predicate);
    // Task Update(LegalDocument legalDocument);
    // Task Delete(LegalDocument legalDocument);

    Task<int> GetCount(Expression<Func<LegalDocument, bool>> predicate);
}