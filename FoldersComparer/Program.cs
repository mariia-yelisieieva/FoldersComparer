using FoldersComparer.FileDataComparers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            var directoryReader1 = new DirectoryReader(directory1, trimPath);
            if (!directoryReader1.DirectoryExists())
            {
                Console.WriteLine("The first suggested directory doesn't exist.");
                return;
            }

            var directoryReader2 = new DirectoryReader(directory2, trimPath);
            if (!directoryReader2.DirectoryExists())
            {
                Console.WriteLine("The first suggested directory doesn't exist.");
                return;
            }

            List<FileData> directoryData1 = directoryReader1.ReadDirectiryData().ToList();
            List<FileData> directoryData2 = directoryReader2.ReadDirectiryData().ToList();

            var fileHashComparer = new FileHashComparer();
            var comparer = new FolderContentComparer();
            (List<FileData?> result1, List<FileData?> result2) = comparer.CompareFileSets(directoryData1, directoryData2, fileHashComparer, showEqual);

            string directoryRootName1 = directoryReader1.GetRootFolderName();
            using (var writer = new StreamWriter(directoryRootName1, false))
                result1.ForEach(result => writer.WriteLine(result));

            string directoryRootName2 = directoryReader2.GetRootFolderName();
            if (directoryRootName1 == directoryRootName2)
                directoryRootName2 += " (1)";
            using (var writer = new StreamWriter(directoryRootName2, false))
                result2.ForEach(result => writer.WriteLine(result));
        }
    }
}