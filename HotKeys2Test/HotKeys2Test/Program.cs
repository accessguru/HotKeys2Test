using HotKeys2Test.Client.Pages;
using HotKeys2Test.Client.ShortcutKeys;
using HotKeys2Test.Components;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace HotKeys2Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddHotKeys2();
            builder.Services.AddScoped<SprbrkHotKeysRootContext>();
            builder.Services.AddScoped<ISprbrkHotKeys, SprbrkHotKeys>();

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
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}