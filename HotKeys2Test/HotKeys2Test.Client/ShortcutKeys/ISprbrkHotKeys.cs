using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    public interface ISprbrkHotKeys
    {
        public ISprbrkHotKeysContext CreateContext();

        Task BeginScopeAsync();

        Task EndScopeAsync();

        event EventHandler<HotKeyDownEventArgs> KeyDown;
    }
}