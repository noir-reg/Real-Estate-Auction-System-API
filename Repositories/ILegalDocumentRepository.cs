using BusinessObjects.Entities;

namespace Repositories;

public interface ILegalDocumentRepository
{
    IQueryable<LegalDocument> GetLegalDocumentQuery();
}