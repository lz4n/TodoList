namespace Infraestructure.Repository
{
    public interface IRepositoryBase<TEntity>
    { 
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByPage(int pageIndex, int entriesByPage);
        bool Create(TEntity todoDto);
        bool Update(TEntity todoDto);
        bool Delete(TEntity todoDto);
        bool DeleteRange(IEnumerable<TEntity> todoList);
        int GetCount();
    }
}
