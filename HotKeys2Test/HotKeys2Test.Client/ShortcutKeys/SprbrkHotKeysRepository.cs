using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    internal class SprbrkHotKeysRepository
    {
        private readonly Dictionary<SprbrkHotKeysRepositoryRegistration, Stack<HotKeyEntry>> _hotkeysRepository = new();

        public IEnumerable<HotKeyEntry> ActiveEntries()
        {
            foreach (var stack in _hotkeysRepository.Values)
            {
                if (stack.Count == 0)
                {
                    continue;
                }

                yield return stack.Peek();
            }
        }

        public HotKeyEntry Deregister((ModCode modCode, Code code) key)
        {
            var repositoryKey = (SprbrkHotKeysRepositoryRegistration)key;

            if (!_hotkeysRepository.TryGetValue(repositoryKey, out Stack<HotKeyEntry> value) || value.Count == 0)
            {
                return null;
            }

            return value.Pop();
        }

        public void Register((ModCode modCode, Code code) key, HotKeyEntry entry)
        {
            var repositoryKey = (SprbrkHotKeysRepositoryRegistration)key;

            if (!_hotkeysRepository.TryGetValue(repositoryKey, out Stack<HotKeyEntry> value))
            {
                value = new Stack<HotKeyEntry>();
                _hotkeysRepository[repositoryKey] = value;
            }

            value.Push(entry);
        }

        public void Clear() => _hotkeysRepository.Clear();
    }
}