using Microsoft.EntityFrameworkCore;
using System;
using TodoList.Client.Pages;
using TodoList.Components;
using TodoList.Dto;
using TodoList.Infraestructure;
using TodoList.Models;
using TodoList.Repository;

namespace TodoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            string database = builder.Configuration.GetConnectionString("TodoDatabase")!;
            builder.Services.AddDbContext<TodoDbContext>(options =>
            {
                options.UseSqlServer(database);
            });

            builder.Services.AddScoped<IRepositoryBase<TodoDto>, TodoRepository>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
                dbContext.Database.EnsureCreated();
                dbContext.SaveChanges();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Counter).Assembly);

            app.Run();

            

        }
    }
}
