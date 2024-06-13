using Domain.Dto;
using Domain.Models;
using Infraestructure.Infraestructure;


namespace Infraestructure.Repository
{
    public class TodoRepository : IRepositoryBase<TodoDto>
    {
        private TodoDbContext _context { get; set; }

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public void Create(TodoDto todoDto)
        {
            try
            {
                _context.Todos.Add(TodoDto.MapToEntity(todoDto)!);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error añadiendo Todo.");
            }
        }

        public void AutoCreate(int count)
        {
            try
            {
                _context.Todos.RemoveRange(_context.Todos);
              
                for (int index = 0; index < count; index++)
                {
                    _context.Todos.Add(new Todo() { Title = $"Tarea número {index}", Description = $"Descripción de la tarea {index}" });
                }
                    
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error añadiendo Todo.");
            }
        }

        public void Delete(TodoDto todoDto)
        {
            try
            {
                _context.Todos.Remove(GetById(todoDto.Id));
				_context.SaveChanges();
			}
            catch (Exception)
            {
                throw new Exception("Error eliminando Todo.");
            }
        }

        public void DeleteRange(IEnumerable<TodoDto> todosDtoList)
        {
            try
            {
                _context.Todos.RemoveRange(todosDtoList.Select(x => GetById(x.Id)));
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error eliminando una lista de Todos.");
            }           
        }

        public IEnumerable<TodoDto> GetAll()
        {
            try
            {
                return _context.Todos.Select(x => TodoDto.MapFromEntity(x)!);
            }
            catch (Exception)
            {
                throw new Exception("Error obteniendo todos los Todos.");
            }
        }

        public IEnumerable<TodoDto> GetByPage(int pageIndex, int entriesByPage)
        {
            try
            {
                return _context.Todos.Skip(pageIndex *entriesByPage).Take(entriesByPage).Select(x => TodoDto.MapFromEntity(x)!);
            }
            catch (Exception)
            {
                throw new Exception("Error obteniendo Todos por página.");
            }
        }

        public void Update(TodoDto todoDto)
        {
            try
            {
                Todo todo = _context.Todos.Find(todoDto.Id);
                if (todo == null)
                {
                    return;
                }

                TodoDto.MapToEntity(todoDto, todo);
				_context.SaveChanges();
			}
            catch (Exception)
            {
                throw new Exception("Error actualizando Todo.");
            }
        }

        private Todo GetById(Guid id)
        {
            return _context.Todos.Find(id)!;
        }
    }
}
