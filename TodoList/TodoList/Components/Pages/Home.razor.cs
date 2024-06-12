using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TodoList.Dto;
using TodoList.Models;
using TodoList.Repository;

namespace TodoList.Components.Pages
{
    public partial class Home
    {
        private const int EntriesPerPage = 10;

        [Inject]
        private IRepositoryBase<TodoDto> todoRepository { get; set; }

        private TodoDto todoModel = new TodoDto();
        private IEnumerable<TodoDto> todoList = null;

        private int page = 1, totalPages;
        private bool IsPrevPageAvailable { get => page > 1; }
        private bool IsNextPageAvailabe { get => page < totalPages; }

        protected override void OnInitialized()
        {

            /*if (todoRepository.GetCount() == 0)
            {
                for (int index = 0; index < 100000; index++)
                {
                    todoRepository.Create(new TodoDto() { Title = $"Tarea número {index}", Description = $"Descripción de la tarea {index}" });
                }
            }*/

            FillTodos();

            if (todoRepository.GetCount() == 0)
            {
                page = 0;
            }
        }

        private void FillTodos()
        {
            todoList = todoRepository.GetByPage(page -1, EntriesPerPage);

            totalPages = (int) Math.Ceiling((double) todoRepository.GetCount() / EntriesPerPage);

            StateHasChanged();
        }

		private void AddTodo()
		{
            todoRepository.Create(todoModel);

            FillTodos();
			todoModel = new TodoDto();
		}

        private void RemoveSelectedTodos()
        {
            todoRepository.DeleteRange(todoList.Where(x => x.IsSelected));

            FillTodos();      
        }

        private void NextPhase(TodoDto todo)
        {
            todo.NextPhase();

            todoRepository.Update(todo);

            StateHasChanged();
        }

        private void changePage(int offset)
        {
            if (page +offset > totalPages || page +offset < 1)
            {
                return;
            }

            page += offset;

            FillTodos();
        }
    }
}
