namespace TodoList.Infraestructure
{
    public class TodoDbContextSeed
    {
        public static void Init(IServiceProvider provider)
        {
            using (var dbContext = provider.GetRequiredService<TodoDbContext>())
            {
                dbContext.Database.EnsureCreated();
                dbContext.SaveChanges();
            }
        }
    }
}
