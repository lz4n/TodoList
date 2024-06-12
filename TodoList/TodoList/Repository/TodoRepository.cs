using System;
using TodoList.Infraestructure;
using TodoList.Models;

namespace TodoList.Repository
{
    public class TodoRepository : IRepositoryBase<Todo>
    {
        private TodoDbContext _context { get; set; }

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public bool Create(Todo entity)
        {
            try
            {
                _context.Todos.Add(entity);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(Todo entity)
        {
            try
            {
                _context.Todos.Remove(entity);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<Todo> GetAll()
        {
            return _context.Todos.ToList();
        }

        public bool Update(Todo entity)
        {
            try
            {
                _context.Todos.Update(entity);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
