using PersonalFinanceManagement.Domain.Commons;

namespace PersonalFinanceManagement.Data.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    public Task<bool> InsertAsync(TEntity entity); 
    public Task<bool> UpdateAsync(TEntity entity);
    public Task<bool> DeleteByIdAsync(int id);
    public Task<List<TEntity>> SelectAllAsync();
    public Task<TEntity> SelectByIdAsync(int id); 
}
