using System.Diagnostics.CodeAnalysis;
using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    /// <summary>
    /// Holds and manages global set of hotkeys, creates contexts and disposes them properly
    /// </summary>
    public class SprbrkHotKeysRootContext : IAsyncDisposable
    {
        /// <summary>
        /// Holds (modkey, key) => hotkey stack.
        /// Only top value on the stack works at the same time
        /// </summary>
        private readonly Stack<SprbrkHotKeysRepository> _hotkeysRepositoryScopes = new();

        private readonly SprbrkHotKeysRepository _rootSprbrkHotKeysRepository = new();
        private HotKeysContext _currentContext;
        private bool _disposed;

        public SprbrkHotKeysRootContext(HotKeys hotKeys)
        {
            HotKeys = hotKeys ?? throw new ArgumentNullException(nameof(hotKeys));
            _currentContext = hotKeys.CreateContext();
        }

        public HotKeys HotKeys { get; }

        public IEnumerable<HotKeyEntry> Keys => _currentContext.HotKeyEntries;

        private SprbrkHotKeysRepository CurrentHotkeysRepository =>
            _hotkeysRepositoryScopes.Count > 0 ? _hotkeysRepositoryScopes.Peek() : _rootSprbrkHotKeysRepository;

        public Task<HotKeyEntryByCode> AddAsync(ModKeys modKey, Keys key, Func<Task> action, string description = "", Exclude exclude = Exclude.Default) =>
            RegisterAsync(modKey, key, context => context.Add(modKey.ToModCode(), key.ToCode(), action, description, exclude.ToExclude()));

        public Task<HotKeyEntryByCode> AddAsync(ModKeys modKey, Keys key, Action action, string description = "", Exclude exclude = Exclude.Default) =>
            RegisterAsync(modKey, key, context => context.Add(modKey.ToModCode(), key.ToCode(), action, description, exclude.ToExclude()));

        public Task<HotKeyEntryByCode> AddAsync(ModKeys modKey, Keys key, Func<HotKeyEntry, Task> action, string description = "", Exclude exclude = Exclude.Default) =>
            RegisterAsync(modKey, key, context => context.Add(modKey.ToModCode(), key.ToCode(), action, description, exclude.ToExclude()));

        public Task<HotKeyEntryByCode> AddAsync(ModKeys modKey, Keys key, Action<HotKeyEntry> action, string description = "", Exclude exclude = Exclude.Default) =>
            RegisterAsync(modKey, key, context => context.Add(modKey.ToModCode(), key.ToCode(), action, description, exclude.ToExclude()));

        public async ValueTask DisposeAsync()
        {
            if (_disposed)
            {
                return;
            }

            while (_hotkeysRepositoryScopes.Count > 0)
            {
                _hotkeysRepositoryScopes.Pop().Clear();
            }

            _rootSprbrkHotKeysRepository.Clear();
            await _currentContext.DisposeAsync();

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        public async Task<HotKeyEntry>? RemoveAsync(ModCode modKey, Code key)
        {
            HotKeyEntry? removedEntry = CurrentHotkeysRepository.Deregister((modKey, key))!;

            if (removedEntry == null)
            {
                return null;
            }

            //recreates current context with refreshed set of hotkeys
            await RecreateCurrentContextAsync();

            return removedEntry;
        }

        internal async Task BeginScopeAsync()
        {
            _hotkeysRepositoryScopes.Push(new SprbrkHotKeysRepository());
            await RecreateCurrentContextAsync();
        }

        internal async Task EndScopeAsync()
        {
            if (_hotkeysRepositoryScopes.Count > 0)
            {
                _hotkeysRepositoryScopes.Pop().Clear();
            }

            await RecreateCurrentContextAsync();
        }

        /// <summary>
        /// properly disposes HotkeyContext and its values.
        /// Then fills them using hotkey values from the top stack
        /// </summary>
        private async Task RecreateCurrentContextAsync()
        {
            await _currentContext.DisposeAsync();
            _currentContext = HotKeys.CreateContext();

            //re-register remained hotkeys
            foreach (var hotKeyEntry in CurrentHotkeysRepository.ActiveEntries().OfType<HotKeyEntryByCode>())
            {
                _currentContext.Add(hotKeyEntry.Modifiers,
                    hotKeyEntry.Code,
                    hotKeyEntry.InvokeAction,
                    hotKeyEntry.Description ?? string.Empty,
                    hotKeyEntry.Exclude);
            }
        }

        private async Task<HotKeyEntryByCode> RegisterAsync(ModKeys modKeys, Keys keys, Action<HotKeysContext> registerAction)
        {
            var newRegistration = (modKeys.ToModCode(), keys.ToCode());
            registerAction(_currentContext);

            //take just created hotkey and store it into stack
            var newHotKey = (HotKeyEntryByCode)_currentContext.HotKeyEntries.Last();
            CurrentHotkeysRepository.Register(newRegistration, newHotKey);

            //need to recreate stack with the top values from
            //hotkey stacks to clean up possible previous hotkey registrations
            await RecreateCurrentContextAsync();

            return newHotKey;
        }
    }
}