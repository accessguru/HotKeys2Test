using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    public interface ISprbrkHotKeys
    {
        public ISprbrkHotKeysContext CreateContext();

        void BeginScope();

        void EndScope();

        event EventHandler<HotKeyDownEventArgs> KeyDown;
    }
}