using FoldersComparer.FileDataComparers;
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
            //Console.WriteLine("Please, enter the first directory:");
            //string directory1 = Console.ReadLine();
            //Console.WriteLine();

            //Console.WriteLine("Please, enter the second directory:");
            //string directory2 = Console.ReadLine();
            //Console.WriteLine();

            //Console.WriteLine("Type YES if you want to get a full directory name or just press enter:");
            //bool trimPath = Console.ReadLine() != "YES";

            string directory1 = @"D:\Data\Udemy\";
            string directory2 = @"D:\Data\Udemy1\";
            bool trimPath = true;
            bool showEqual = false;

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