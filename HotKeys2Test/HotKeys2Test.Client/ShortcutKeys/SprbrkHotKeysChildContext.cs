using System.Diagnostics.CodeAnalysis;
using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    /// <summary>
    /// Keeps context-specific hotkeys that will be disposed by a component via simple Dispose method call
    /// </summary>
    public class SprbrkHotKeysChildContext : ISprbrkHotKeysContext
    {
        private readonly List<HotKeyEntryByCode> _registeredHotKeys = new();
        private readonly SprbrkHotKeysRootContext _rootContext;
        private bool _disposed;

        public SprbrkHotKeysChildContext(SprbrkHotKeysRootContext rootContext)
        {
            _rootContext = rootContext ?? throw new ArgumentNullException(nameof(rootContext));
        }

        public IEnumerable<HotKeyEntry> Keys => _rootContext.Keys;

        public HotKeyEntry Add(ModKeys modKeys, Keys key, Func<Task> action, string description = "", Exclude exclude = Exclude.Default)
        {
            var hotKeyEntry = _rootContext.Add(modKeys, key, action, description, exclude);
            _registeredHotKeys.Add(hotKeyEntry);
            return hotKeyEntry;
        }

        public HotKeyEntry Add(ModKeys modKeys, Keys key, Action action, string description = "", Exclude exclude = Exclude.Default)
        {
            var hotKeyEntry = _rootContext.Add(modKeys, key, action, description, exclude);
            _registeredHotKeys.Add(hotKeyEntry);
            return hotKeyEntry;
        }

        public HotKeyEntry Add(ModKeys modKeys, Keys key, Func<HotKeyEntry, Task> action, string description = "", Exclude exclude = Exclude.Default)
        {
            var hotKeyEntry = _rootContext.Add(modKeys, key, action, description, exclude);
            _registeredHotKeys.Add(hotKeyEntry);
            return hotKeyEntry;
        }

        public HotKeyEntry Add(ModKeys modKeys, Keys key, Action<HotKeyEntry> action, string description = "", Exclude exclude = Exclude.Default)
        {
            var hotKeyEntry = _rootContext.Add(modKeys, key, action, description, exclude);
            _registeredHotKeys.Add(hotKeyEntry);
            return hotKeyEntry;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //public async ValueTask DisposeAsync()
        //{
        //    await DisposeAsyncCore().ConfigureAwait(false);
        //    Dispose(false);
        //    GC.SuppressFinalize(this);
        //}

        [return: MaybeNull]
        public HotKeyEntry Remove(ModKeys modKey, Keys key) =>
            _rootContext.Remove(modKey.ToModCode(), key.ToCode());

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var registeredHotKey in _registeredHotKeys)
                {
                    _rootContext.Remove(registeredHotKey.Modifiers, registeredHotKey.Code);
                }

                _registeredHotKeys.Clear();
            }

            _disposed = true;
        }

        //protected virtual async ValueTask DisposeAsyncCore() =>
        //    await _rootContext.HotKeys.DetachAsync();
    }
}