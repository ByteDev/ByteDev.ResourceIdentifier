using System;

namespace ByteDev.ResourceIdentifier.TestApp472
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var uri = new Uri("http://api.giphy.com/v1/gifs/search")
                .AddOrUpdateQueryParam("api_key", "ABC123")
                .SetFragment("#myFrag");

            Console.WriteLine(uri.AbsoluteUri);
            Console.ReadLine();
        }
    }
}
