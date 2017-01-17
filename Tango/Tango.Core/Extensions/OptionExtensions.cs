namespace Tango.Core.Extensions
{
    public static class OptionExtensions
    {
        public static Option<T> MapToOption<T>(this T value)
            => Option<T>.Map(value);
    }
}
