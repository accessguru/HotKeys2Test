using System.Diagnostics.CodeAnalysis;
using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    /// <summary>
    /// Holds and manages global set of hotkeys, creates contexts and disposes them properly
    /// </summary>
    public class SprbrkHotKeysRootContext : IDisposable
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

        public IEnumerable<HotKeyEntry> Keys => _currentContext.Keys;

        private SprbrkHotKeysRepository CurrentHotkeysRepository =>
            _hotkeysRepositoryScopes.Any() ? _hotkeysRepositoryScopes.Peek() : _rootSprbrkHotKeysRepository;

        public HotKeyEntryByCode Add(ModKeys modKey, Keys key, Func<Task> action, string description = "", Exclude exclude = Exclude.Default) =>
            Register(modKey, key, context => context.Add(modKey.ToModCode(), key.ToCode(), action, description, exclude.ToExclude()));

        public HotKeyEntryByCode Add(ModKeys modKey, Keys key, Action action, string description = "", Exclude exclude = Exclude.Default) =>
            Register(modKey, key, context => context.Add(modKey.ToModCode(), key.ToCode(), action, description, exclude.ToExclude()));

        public HotKeyEntryByCode Add(ModKeys modKey, Keys key, Func<HotKeyEntry, Task> action, string description = "", Exclude exclude = Exclude.Default) =>
            Register(modKey, key, context => context.Add(modKey.ToModCode(), key.ToCode(), action, description, exclude.ToExclude()));

        public HotKeyEntryByCode Add(ModKeys modKey, Keys key, Action<HotKeyEntry> action, string description = "", Exclude exclude = Exclude.Default) =>
            Register(modKey, key, context => context.Add(modKey.ToModCode(), key.ToCode(), action, description, exclude.ToExclude()));

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                return;
            }

            if (disposing)
            {
                while (_hotkeysRepositoryScopes.Any())
                {
                    _hotkeysRepositoryScopes.Pop().Clear();
                }

                _rootSprbrkHotKeysRepository.Clear();
                _currentContext.Dispose();
            }

            _disposed = true;
        }

        [return: MaybeNull]
        public HotKeyEntry Remove(ModCode modKey, Code key)
        {
            HotKeyEntry removedEntry = CurrentHotkeysRepository.Deregister((modKey, key));

            if (removedEntry == null)
            {
                return null;
            }

            //recreates current context with refreshed set of hotkeys
            RecreateCurrentContext();

            return removedEntry;
        }

        internal void BeginScope()
        {
            _hotkeysRepositoryScopes.Push(new SprbrkHotKeysRepository());
            RecreateCurrentContext();
        }

        internal void EndScope()
        {
            if (_hotkeysRepositoryScopes.Any())
            {
                _hotkeysRepositoryScopes.Pop().Clear();
            }

            RecreateCurrentContext();
        }

        /// <summary>
        /// properly disposes HotkeyContext and its values.
        /// Then fills them using hotkey values from the top stack
        /// </summary>
        private void RecreateCurrentContext()
        {
            var keyEntriesToDispose = new List<HotKeyEntry>(_currentContext.Keys);

            //_currentContext.Remove(entries => entries);
            _currentContext.Dispose();

            keyEntriesToDispose.ForEach(k => k.Dispose());
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

        private HotKeyEntryByCode Register(ModKeys modKeys, Keys keys, Action<HotKeysContext> registerAction)
        {
            var newRegistration = (modKeys.ToModCode(), keys.ToCode());
            registerAction(_currentContext);

            //take just created hotkey and store it into stack
            var newHotKey = (HotKeyEntryByCode)_currentContext.Keys.Last();
            CurrentHotkeysRepository.Register(newRegistration, newHotKey);

            //need to recreate stack with the top values from
            //hotkey stacks to clean up possible previous hotkey registrations
            RecreateCurrentContext();

            return newHotKey;
        }
    }
}