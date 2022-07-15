namespace ByteDev.ResourceIdentifier
{
    internal static class Tcp
    {
        public const int MinPort = 0;
        public const int MaxPort = 65535;

        public static bool IsPortValid(int port)
        {
            return port >= MinPort && port <= MaxPort;
        }
    }
}