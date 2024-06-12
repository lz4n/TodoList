namespace TodoList.Models
{
    public class Todo
    {
        public string Title {  get; set; }
        public string Description { get; set; }
        public bool IsBlocked { get; set; } = false;
        public TodoPhase Phase { get; set; } = TodoPhase.PLANNED;
    }

    public enum TodoPhase
    {
        PLANNED,
        STARTED,
        IN_PROGRESS,
        COMPLETED
    }
}
