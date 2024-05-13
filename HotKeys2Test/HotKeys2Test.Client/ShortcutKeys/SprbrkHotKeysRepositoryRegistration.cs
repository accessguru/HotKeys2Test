using Toolbelt.Blazor.HotKeys2;

namespace HotKeys2Test.Client.ShortcutKeys
{
    internal class SprbrkHotKeysRepositoryRegistration : IEquatable<SprbrkHotKeysRepositoryRegistration>, IEquatable<(ModCode ModCode, Code Code)>
    {
        public static implicit operator ValueTuple<ModCode, Code>(SprbrkHotKeysRepositoryRegistration registration) =>
            new(registration.ModCode, registration.Code);

        public static explicit operator SprbrkHotKeysRepositoryRegistration((ModCode ModCode, Code Code) registration) =>
            new(registration.ModCode, registration.Code);

        public ModCode ModCode { get; }

        public Code Code { get; }

        public SprbrkHotKeysRepositoryRegistration(ModCode modCode, Code code)
        {
            ModCode = modCode;
            Code = code;
        }

        public override bool Equals(object obj)
        {
            if (obj is SprbrkHotKeysRepositoryRegistration registration)
            {
                return Equals(registration);
            }

            if (obj is ValueTuple<ModCode, Code> tuple)
            {
                return Equals(tuple);
            }

            return false;
        }

        public bool Equals(SprbrkHotKeysRepositoryRegistration other) =>
            other is not null &&
            ModCode == other.ModCode &&
            Code == other.Code;

        public bool Equals((ModCode ModCode, Code Code) other) =>
            ModCode == other.ModCode &&
            Code == other.Code;

        public override int GetHashCode() => HashCode.Combine(ModCode, Code);
    }
}