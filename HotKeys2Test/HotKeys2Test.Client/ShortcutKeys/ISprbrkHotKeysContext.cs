using System.Diagnostics.CodeAnalysis;
using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    public interface ISprbrkHotKeysContext : IAsyncDisposable
    {
        public IEnumerable<HotKeyEntry> Keys { get; }

        Task<HotKeyEntry> AddAsync(ModKeys modKeys, Keys key, Func<Task> action, string description = "", Exclude exclude = Exclude.Default);

        Task<HotKeyEntry> AddAsync(ModKeys modKeys, Keys key, Action action, string description = "", Exclude exclude = Exclude.Default);

        Task<HotKeyEntry> AddAsync(ModKeys modKeys, Keys key, Func<HotKeyEntry, Task> action, string description = "", Exclude exclude = Exclude.Default);

        Task<HotKeyEntry> AddAsync(ModKeys modKeys, Keys key, Action<HotKeyEntry> action, string description = "", Exclude exclude = Exclude.Default);

        Task<HotKeyEntry>? RemoveAsync(ModKeys modKeys, Keys key);
    }
}