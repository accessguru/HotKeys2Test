using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    /// <summary>
    /// Keeps context-specific hotkeys that will be disposed by a component via simple Dispose method call
    /// </summary>
    public class SprbrkHotKeysChildContext : ISprbrkHotKeysContext
    {
        private readonly List<HotKeyEntryByCode> _registeredHotKeys = [];
        private readonly SprbrkHotKeysRootContext _rootContext;
        private bool _disposed;

        public SprbrkHotKeysChildContext(SprbrkHotKeysRootContext rootContext)
        {
            _rootContext = rootContext ?? throw new ArgumentNullException(nameof(rootContext));
        }

        public IEnumerable<HotKeyEntry> Keys => _rootContext.Keys;

        public async Task<HotKeyEntry> AddAsync(ModKeys modKeys, Keys key, Func<Task> action, string description = "", Exclude exclude = Exclude.Default)
        {
            var hotKeyEntry = await _rootContext.AddAsync(modKeys, key, action, description, exclude);
            _registeredHotKeys.Add(hotKeyEntry);
            return hotKeyEntry;
        }

        public async Task<HotKeyEntry> AddAsync(ModKeys modKeys, Keys key, Action action, string description = "", Exclude exclude = Exclude.Default)
        {
            var hotKeyEntry = await _rootContext.AddAsync(modKeys, key, action, description, exclude);
            _registeredHotKeys.Add(hotKeyEntry);
            return hotKeyEntry;
        }

        public async Task<HotKeyEntry> AddAsync(ModKeys modKeys, Keys key, Func<HotKeyEntry, Task> action, string description = "", Exclude exclude = Exclude.Default)
        {
            var hotKeyEntry = await _rootContext.AddAsync(modKeys, key, action, description, exclude);
            _registeredHotKeys.Add(hotKeyEntry);
            return hotKeyEntry;
        }

        public async Task<HotKeyEntry> AddAsync(ModKeys modKeys, Keys key, Action<HotKeyEntry> action, string description = "", Exclude exclude = Exclude.Default)
        {
            var hotKeyEntry = await _rootContext.AddAsync(modKeys, key, action, description, exclude);
            _registeredHotKeys.Add(hotKeyEntry);
            return hotKeyEntry;
        }

        public async Task<HotKeyEntry> RemoveAsync(ModKeys modKey, Keys key) =>
            await _rootContext.RemoveAsync(modKey.ToModCode(), key.ToCode())!;

        public async ValueTask DisposeAsync()
        {
            if (_disposed)
            {
                return;
            }

            foreach (var registeredHotKey in _registeredHotKeys)
            {
                await _rootContext.RemoveAsync(registeredHotKey.Modifiers, registeredHotKey.Code)!;
            }

            _registeredHotKeys.Clear();
            _disposed = true;
        }
    }
}