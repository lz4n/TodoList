namespace Infraestructure.Repository
{
    public interface IRepositoryBase<TEntity>
    { 
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByPage(int pageIndex, int entriesByPage);
        void Create(TEntity todoDto);
        void AutoCreate(int count);
        void Update(TEntity todoDto);
        void Delete(TEntity todoDto);
        void DeleteRange(IEnumerable<TEntity> todoList);
        int GetCount();
    }
}
