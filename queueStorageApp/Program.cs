using System; 
using System.Configuration; 
using System.Threading.Tasks; 
using Azure.Storage.Queues; 
using Azure.Storage.Queues.Models; 

namespace queueStorageApp
{
    class Program
    {

        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.AppSettings["storageConnectionString"];
            QueueClient queueClient = new QueueClient(connectionString, "myqueue");
            queueClient.CreateIfNotExists();
            if (queueClient.Exists())
            {
              
               // queueClient.SendMessage("test message");

                PeekedMessage[] peekedMessage = queueClient.PeekMessages();

                // Display the message
                Console.WriteLine($"Peeked message: '{peekedMessage[0].MessageText}'");
                Console.ReadLine();
            }
        }
    }
}
