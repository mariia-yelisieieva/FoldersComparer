using FoldersComparer.FileDataComparers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace FoldersComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName).AddJsonFile("appsettings.json", false).Build();

            string directory1 = configuration["directory1"];
            string directory2 = configuration["directory2"];
            bool trimPath = Convert.ToBoolean(configuration["trimPath"]);
            bool showEqual = Convert.ToBoolean(configuration["showEqual"]);

            string[] directories = new[] { directory1, directory2 };
            int directoriesCount = directories.Length;

            var directoryReaders = new DirectoryReader[directoriesCount];
            for (int i = 0; i < directoriesCount; i++)
            {
                string directory = directories[i];
                directoryReaders[i] = new DirectoryReader(directory, trimPath);
                if (!directoryReaders[i].DirectoryExists())
                {
                    Console.WriteLine("The first suggested directory doesn't exist.");
                    return;
                }
            }

            var directoryDataAll = new List<FileData>[directoriesCount];
            for (int i = 0; i < directoriesCount; i++)
            {
                directoryDataAll[i] = directoryReaders[i].ReadDirectiryData();
            }

            var fileHashComparer = new FileHashComparer();
            var comparer = new FolderContentComparer(fileHashComparer);
            List<FileData?>[] results = comparer.CompareFileSets(showEqual, directoryDataAll);

            var fileNameProvider = new ResultFileNameProvider(directoriesCount);
            for (int i = 0; i < directoriesCount; i++)
            {
                string uniqueFileName = fileNameProvider.GetUniqueRootName(directoryReaders[i].GetRootFolderName());
                using var writer = new StreamWriter(uniqueFileName, false);
                results[i].ForEach(result => writer.WriteLine(result));
            }
        }
    }
}