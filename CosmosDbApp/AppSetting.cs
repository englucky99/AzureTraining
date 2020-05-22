using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace tableStorageApp
{
    public class AppSetting
    {
        public string StorageConnectionString { get; set; }
        public static AppSetting LoadAppSettings()
        {
            var configRoot = new ConfigurationBuilder();
           
            configRoot.SetBasePath(Directory.GetCurrentDirectory());
            configRoot.AddJsonFile("Settings.json");            
            IConfiguration configuration = configRoot.Build();
            AppSetting appSettings = configuration.Get<AppSetting>();
            return appSettings;
        }
    }
}
