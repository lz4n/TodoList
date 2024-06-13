using Domain.Enums;
using Domain.Models;

namespace Domain.Dto
{
    public class TodoDto
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string? Title { get; set; }
		public string? Description { get; set; }
		public bool IsBlocked { get; set; }
		public bool IsSelected { get; set; }

		private TodoPhase _phase;
		public TodoPhase Phase { get => _phase; }

		public void NextPhase()
		{
			if (_phase == TodoPhase.COMPLETED)
			{
				return;
			}

			_phase++;
		}


		public static Todo? MapToEntity(TodoDto todoDto, Todo? entity = null)
		{
			if (todoDto == null)
			{
				return null;
			}

			if (entity == null)
			{
				entity = new Todo();
			}

            entity.Id = todoDto.Id;
            entity.Title = todoDto.Title;
            entity.Description = todoDto.Description;
            entity.IsBlocked = todoDto.IsBlocked;
            entity.Phase = todoDto.Phase;

			return entity;
		}

		public static TodoDto? MapFromEntity(Todo todo)
		{
			if (todo == null)
			{
				return null;
			}

			return new TodoDto()
			{
				Id = todo.Id,
				Title = todo.Title,
				Description = todo.Description,
				IsBlocked = todo.IsBlocked,
				_phase = todo.Phase
			};
		}
	}
}
