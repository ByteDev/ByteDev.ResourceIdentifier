using System;

namespace ByteDev.ResourceIdentifier.TestApp472
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var uri1 = new Uri("http://api.giphy.com/v1/gifs/search")
                .AddOrUpdateQueryParam("api_key", "ABC123")
                .SetFragment("#myFrag");

            Console.WriteLine(uri1.AbsoluteUri);
            Console.WriteLine();

            var uri2 = new Uri("http://example.com/")
                .SetPath("/path2");

            Console.WriteLine(uri2.AbsoluteUri);
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
