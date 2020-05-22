using FoldersComparer.FileDataComparers;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FoldersComparer.Configuration
{
    public sealed class ConfigurationProvider
    {
        public Configuration GetConfiguration()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName).AddJsonFile("appsettings.json", false).Build();

            return new Configuration
            {
                Directories = configuration.GetSection("directories").Get<string[]>(),
                TrimPaths = Convert.ToBoolean(configuration["trimPaths"]),
                ShowEqualLines = Convert.ToBoolean(configuration["showEqualLines"]),
                DataComparer = new FileComparerFactory().GetFileComparerByKindName(configuration["fileComparer"]),
            };
        }
    }
}