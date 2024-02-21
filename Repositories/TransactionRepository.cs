using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly RealEstateDbContext _context;

    public TransactionRepository()
    {
        _context = new RealEstateDbContext();
    }

    public Task AddTransactionAsync(Transaction transaction)
    {
        try
        {
            _context.Transactions.Add(transaction);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<Transaction>> GetTransactions()
    {
        try
        {
            var result = _context.Transactions.ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task UpdateTransactionAsync(Transaction transaction)
    {
        try
        {
            _context.Transactions.Update(transaction);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task DeleteTransactionAsync(Transaction transaction)
    {
        try
        {
            _context.Transactions.Remove(transaction);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Transaction?> GetTransaction(Guid transactionId)
    {
        try
        {
            var result = _context.Transactions.Where(x => x.TransactionId == transactionId).SingleOrDefaultAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public IQueryable<Transaction> GetTransactionQuery()
    {
        return _context.Transactions.AsQueryable();
    }
}