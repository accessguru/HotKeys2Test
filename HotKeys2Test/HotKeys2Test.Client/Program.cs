using HotKeys2Test.Client.ShortcutKeys;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace HotKeys2Test.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddHotKeys2();
            builder.Services.AddScoped<SprbrkHotKeysRootContext>();
            builder.Services.AddScoped<ISprbrkHotKeys, SprbrkHotKeys>();

            await builder.Build().RunAsync();
        }
    }
}