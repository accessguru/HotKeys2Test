namespace HotKeys2Test.Client.ShortcutKeys
{
    [Flags]
    public enum Exclude
    {
        None = 0,
        InputText = 1,
        InputNonText = 2,
        TextArea = 4,
        ContentEditable = 8,
        Default = TextArea | InputNonText | InputText, // 0x00000007
    }
}