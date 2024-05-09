using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    public static class SprbrkHotKeysExtensions
    {
        public static Toolbelt.Blazor.HotKeys2.Exclude ToExclude(this Exclude exclude)
        {
            return exclude switch
            {
                Exclude.ContentEditable => Toolbelt.Blazor.HotKeys2.Exclude.ContentEditable,
                Exclude.Default => Toolbelt.Blazor.HotKeys2.Exclude.Default,
                Exclude.InputNonText => Toolbelt.Blazor.HotKeys2.Exclude.InputNonText,
                Exclude.None => Toolbelt.Blazor.HotKeys2.Exclude.None,
                Exclude.TextArea => Toolbelt.Blazor.HotKeys2.Exclude.TextArea,
                Exclude.InputText => Toolbelt.Blazor.HotKeys2.Exclude.InputText,
                _ => throw new ArgumentOutOfRangeException(nameof(exclude), exclude, null),
            };
        }

        public static Code ToCode(this Keys keys) =>
            keys switch
            {
                Keys.A => Code.A,
                Keys.B => Code.B,
                Keys.C => Code.C,
                Keys.D => Code.D,
                Keys.E => Code.E,
                Keys.F => Code.F,
                Keys.G => Code.G,
                Keys.H => Code.H,
                Keys.I => Code.I,
                Keys.J => Code.J,
                Keys.K => Code.K,
                Keys.L => Code.L,
                Keys.M => Code.M,
                Keys.N => Code.N,
                Keys.O => Code.O,
                Keys.P => Code.P,
                Keys.Q => Code.Q,
                Keys.R => Code.R,
                Keys.S => Code.S,
                Keys.T => Code.T,
                Keys.U => Code.U,
                Keys.V => Code.V,
                Keys.W => Code.W,
                Keys.X => Code.X,
                Keys.Y => Code.Y,
                Keys.Z => Code.Z,
                Keys.Add => Code.Insert,
                Keys.BackQuote => Code.Backquote,
                Keys.Delete => Code.Delete,
                Keys.Insert => Code.Insert,
                Keys.BackSlash => Code.Backslash,
                Keys.Backspace => Code.Backspace,
                Keys.CapsLock => Code.CapsLock,
                Keys.Comma => Code.Comma,
                Keys.ContextMenu => Code.ContextMenu,
                Keys.Down => Code.ArrowDown,
                Keys.ESC => Code.Escape,
                Keys.End => Code.End,
                Keys.Enter => Code.Enter,
                Keys.Equal => Code.Equal,
                Keys.F1 => Code.F1,
                Keys.F2 => Code.F2,
                Keys.F3 => Code.F3,
                Keys.F4 => Code.F4,
                Keys.F5 => Code.F5,
                Keys.F6 => Code.F6,
                Keys.F7 => Code.F7,
                Keys.F8 => Code.F8,
                Keys.F9 => Code.F9,
                Keys.F10 => Code.F10,
                Keys.F11 => Code.F11,
                Keys.F12 => Code.F12,
                Keys.Home => Code.Home,
                Keys.Left => Code.ArrowLeft,
                Keys.Num0 => Code.Num0,
                Keys.Num1 => Code.Num1,
                Keys.Num2 => Code.Num2,
                Keys.Num3 => Code.Num3,
                Keys.Num4 => Code.Num4,
                Keys.Num5 => Code.Num5,
                Keys.Num6 => Code.Num6,
                Keys.Num7 => Code.Num7,
                Keys.Num8 => Code.Num8,
                Keys.Num9 => Code.Num9,
                Keys.NumLock => Code.NumLock,
                Keys.Period => Code.Period,
                Keys.PgDown => Code.PageDown,
                Keys.PgUp => Code.PageUp,
                Keys.Right => Code.ArrowRight,
                Keys.ScrollLock => Code.ScrollLock,
                Keys.SemiColon => Code.SemiColon,
                Keys.Slash => Code.Slash,
                Keys.Space => Code.Space,
                Keys.Subtract => Code.Minus,
                Keys.Tab => Code.Tab,
                Keys.Up => Code.ArrowUp,
                Keys.SingleQuote => Code.Quote,
                _ => throw new ArgumentOutOfRangeException(nameof(keys), keys, null)
            };

        public static ModCode ToModCode(this ModKeys modKeys) =>
            modKeys switch
            {
                ModKeys.Alt => ModCode.Alt,
                ModKeys.Ctrl => ModCode.Ctrl,
                ModKeys.Meta => ModCode.Meta,
                ModKeys.None => ModCode.None,
                ModKeys.Shift => ModCode.Shift,
                _ => throw new ArgumentOutOfRangeException(nameof(modKeys), modKeys, null)
            };
    }
}