using System.Diagnostics.CodeAnalysis;
using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    public interface ISprbrkHotKeysContext : IDisposable
    {
        public IEnumerable<HotKeyEntry> Keys { get; }

        HotKeyEntry Add(ModKeys modKeys, Keys key, Func<Task> action, string description = "", Exclude exclude = Exclude.Default);

        HotKeyEntry Add(ModKeys modKeys, Keys key, Action action, string description = "", Exclude exclude = Exclude.Default);

        HotKeyEntry Add(ModKeys modKeys, Keys key, Func<HotKeyEntry, Task> action, string description = "", Exclude exclude = Exclude.Default);

        HotKeyEntry Add(ModKeys modKeys, Keys key, Action<HotKeyEntry> action, string description = "", Exclude exclude = Exclude.Default);

        [return: MaybeNull]
        HotKeyEntry Remove(ModKeys modKeys, Keys key);
    }
}