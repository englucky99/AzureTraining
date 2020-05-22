using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tableStorageApp.Model;

namespace tableStorageApp
{
    class CloudTableOperation
    {
        public async Task RunSamples()
        {
            Console.WriteLine("Azure Cosmos DB Table - Basic Samples\n");
            Console.WriteLine();

            string tableName = "demo";
           
            CloudTable table = await Common.CreateTableAsync(tableName);

            try
            {                
                await BasicDataOperationsAsync(table);
            }
            finally
            {
                // Delete the table
                // await table.DeleteIfExistsAsync();
            }
        }

        private static async Task BasicDataOperationsAsync(CloudTable table)
        {
           
            CustomerEntity customer = new CustomerEntity("Harp", "Walter")
            {
                Email = "test@storage.com",
                PhoneNumber = "425-555-0101"
            };

           
            Console.WriteLine("Insert an Entity.");
            customer = await Utility.InsertOrMergeEntityAsync(table, customer);

           
            Console.WriteLine("Update an existing Entity ");
            customer.PhoneNumber = "425-555-0105";
            await Utility.InsertOrMergeEntityAsync(table, customer);
            Console.WriteLine();

           
            Console.WriteLine("Reading the updated Entity.");
            customer = await Utility.RetrieveEntityUsingPointQueryAsync(table, "Harp", "Walter");
            Console.WriteLine();

            //Delete an entity
            //Console.WriteLine("Delete the entity. ");
            //await SamplesUtils.DeleteEntityAsync(table, customer);
            //Console.WriteLine();
        }

    }
}
