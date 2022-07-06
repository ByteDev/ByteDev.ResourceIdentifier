using System;

namespace ByteDev.ResourceIdentifier
{
    internal static class RandomExtensions
    {
        public static string GetBase64String(this Random source, int length)
        {
            var array = new byte[length];
            source.NextBytes(array);
            return Convert.ToBase64String(array);
        }
    }
}