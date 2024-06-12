using System;
using TodoList.Dto;
using TodoList.Infraestructure;
using TodoList.Models;

namespace TodoList.Repository
{
    public class TodoRepository : IRepositoryBase<TodoDto>
    {
        private TodoDbContext _context { get; set; }

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public bool Create(TodoDto todoDto)
        {
            try
            {
                _context.Todos.Add(TodoDto.MapToEntity(todoDto)!);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(TodoDto todoDto)
        {
            try
            {
                _context.Todos.Remove(GetById(todoDto.Id));
				_context.SaveChanges();
			}
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteRange(IEnumerable<TodoDto> todosDtoList)
        {
            try
            {
                _context.Todos.RemoveRange(todosDtoList.Select(x => GetById(x.Id)));
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<TodoDto> GetAll()
        {
            try
            {
                return _context.Todos.Select(x => TodoDto.MapFromEntity(x)!).ToList();
            }
            catch (Exception)
            {
                return new List<TodoDto>();
            }
        }

        public IEnumerable<TodoDto> GetByPage(int pageIndex, int entriesByPage)
        {
            try
            {
                return _context.Todos.Skip(pageIndex *entriesByPage).Take(entriesByPage).Select(x => TodoDto.MapFromEntity(x)!).ToList();
            }
            catch (Exception)
            {
                return new List<TodoDto>();
            }
        }

        public bool Update(TodoDto todoDto)
        {
            try
            {
                Todo todo = _context.Todos.Find(todoDto.Id);
                if (todo == null)
                {
                    return false;
                }

                TodoDto.MapToEntity(todoDto, todo);
				_context.SaveChanges();
			}
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private Todo GetById(Guid id)
        {
            return _context.Todos.Find(id)!; 
        }

        public int GetCount()
        {
            return _context.Todos.Count();
        }
    }
}
