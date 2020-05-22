using FoldersComparer.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace FoldersComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var configProvider = new ConfigurationProvider();
            Configuration.Configuration config = configProvider.GetConfiguration();

            int directoriesCount = config.Directories.Length;

            var directoryReaders = new DirectoryReader[directoriesCount];
            for (int i = 0; i < directoriesCount; i++)
            {
                string directory = config.Directories[i];
                directoryReaders[i] = new DirectoryReader(directory, config.TrimPaths);
                if (!directoryReaders[i].DirectoryExists())
                {
                    Console.WriteLine("The first suggested directory doesn't exist.");
                    return;
                }
            }

            var directoryDataAll = new List<FileData>[directoriesCount];
            for (int i = 0; i < directoriesCount; i++)
                directoryDataAll[i] = directoryReaders[i].ReadDirectiryData();

            var comparer = new FolderContentComparer(config.DataComparer);
            List<FileData?>[] results = comparer.CompareFileSets(config.ShowEqualLines, directoryDataAll);

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