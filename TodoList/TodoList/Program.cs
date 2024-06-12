using Microsoft.EntityFrameworkCore;
using System;
using TodoList.Client.Pages;
using TodoList.Components;
using TodoList.Infraestructure;
using TodoList.Models;
using TodoList.Repository;
using TodoList.UnitOfWork;

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

            string? database = builder.Configuration.GetConnectionString("TodoDatabase");
            builder.Services.AddDbContext<TodoDbContext>(options =>
            {
                options.UseSqlServer(database);
            }
);

            builder.Services.AddScoped<IRepositoryBase<Todo>, TodoRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            var app = builder.Build();

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
