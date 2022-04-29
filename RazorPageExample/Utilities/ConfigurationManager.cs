using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace RazorPageExample.Utilities
{
    public static class ConfigurationManager
    {
        public static IConfiguration AppSettings { get; }
        static ConfigurationManager()
        {
            AppSettings = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("AppSettings.json").Build();
        }
    }
}
