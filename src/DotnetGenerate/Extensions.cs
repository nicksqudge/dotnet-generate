using System;

namespace DotnetGenerate
{
    static class Extensions
    {
        public static bool HasValue(this string input)
            => !string.IsNullOrEmpty(input);
    }
}
