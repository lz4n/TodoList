
using Domain.Dto;
using Infraestructure.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TodoList.Client.Pages
{
    public partial class Home
    {
        private const int EntriesPerPage = 10;

        [Inject]
        private IRepositoryBase<TodoDto> todoRepository { get; set; }

        private TodoDto todoModel = new TodoDto();
        private List<TodoDto> todoList = null;

        private string errorMessage = string.Empty;

        private int page = 10;

        protected override void OnInitialized()
        {
            FillTodos();

            if (todoRepository.GetCount() == 0)
            {
                page = 0;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeVoidAsync("infiniteScroll.init", DotNetObjectReference.Create(this));
            }
        }

        private void FillTodos()
        {
            todoList = todoRepository.GetByPage(0, EntriesPerPage *page).ToList();
   
            StateHasChanged();
        }

		private void AddTodo()
		{
            if (string.IsNullOrWhiteSpace(todoModel.Title) || string.IsNullOrWhiteSpace(todoModel.Description))
            {
                errorMessage = "El título y la descripción no pueden estar vacíos.";
                return;
            }

            todoRepository.Create(todoModel);

            FillTodos();

            errorMessage = string.Empty;
            todoModel = new TodoDto();
		}

        private void AutoGenerate()
        {
            page = 10;

            todoRepository.AutoCreate(100000);

            FillTodos();
        }

        private void RemoveTodo(TodoDto todo)
        {
            todoRepository.Delete(todo);

            FillTodos();
        }

        private void UpdateTodo(TodoDto todo) 
        {
            todoRepository.Update(todo);
        }

        private void RemoveSelectedTodos()
        {
            todoRepository.DeleteRange(todoList.Where(x => x.IsSelected));

            FillTodos();      
        }

        private void NextPhase(TodoDto todo)
        {
            todo.NextPhase();

            UpdateTodo(todo);
        }

        [JSInvokable]
        public void LoadMoreItems()
        {
            todoList.AddRange(todoRepository.GetByPage(page++, EntriesPerPage));

            StateHasChanged();
        }


        private string GetDisplayName(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Name ?? enumValue.ToString();
        }
    }
}
