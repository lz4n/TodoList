namespace TodoList.Repository
{
    public interface IRepositoryBase<TEntity>
    { 
        List<TEntity> GetAll();
        bool Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
