using TodoList.Models;
using TodoList.Repository;

namespace TodoList.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}
