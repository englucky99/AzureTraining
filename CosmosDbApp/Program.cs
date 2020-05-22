using System;

namespace tableStorageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Azure Table Samples");
            CloudTableOperation cloudOperation = new CloudTableOperation();
            cloudOperation.RunSamples().Wait();
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.Read();
        }
    }
}
