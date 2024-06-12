using System;
using TodoList.Infraestructure;
using TodoList.Models;
using TodoList.Repository;

namespace TodoList.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoDbContext _context;
        public IRepositoryBase<Todo> _todoRepository { get; }

        public UnitOfWork(TodoDbContext context, IRepositoryBase<Todo> todoRepository)
        {
            _context = context;
            _todoRepository = todoRepository;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }


        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
