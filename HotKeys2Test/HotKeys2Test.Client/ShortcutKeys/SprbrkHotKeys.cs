using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    public class SprbrkHotKeys : ISprbrkHotKeys
    {
        private readonly HotKeys _hotkeys;
        private readonly SprbrkHotKeysRootContext _rootContext;

        public SprbrkHotKeys(HotKeys rootHotkeys, SprbrkHotKeysRootContext rootContext)
        {
            _hotkeys = rootHotkeys;
            _rootContext = rootContext;
        }

        public ISprbrkHotKeysContext CreateContext()
        {
            return new SprbrkHotKeysChildContext(_rootContext);
        }

        public void BeginScope() => _rootContext.BeginScope();

        public void EndScope() => _rootContext.EndScope();

        public event EventHandler<HotKeyDownEventArgs> KeyDown
        {
            add => _hotkeys.KeyDown += value;
            remove => _hotkeys.KeyDown -= value;
        }
    }
}