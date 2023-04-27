namespace Server.Repository.Contracts;

public interface IGeneralRepository<TEntity, TKey> where TEntity : class
{
    Task <IEnumerable<TEntity>> GetAllAsync();
    Task <TEntity?> GetByIdAsync(TKey key);
    Task InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey key);
}
