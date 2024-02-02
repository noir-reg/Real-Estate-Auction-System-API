using BusinessObjects.Entities;

namespace Repositories;

public interface ITransactionRepository
{
    Task<Transaction?> GetTransaction(Guid transactionId);
    Task DeleteTransactionAsync(Transaction transaction);
    Task UpdateTransactionAsync(Transaction transaction);
    Task<List<Transaction>> GetTransactions();
    Task AddTransactionAsync(Transaction transaction);
}