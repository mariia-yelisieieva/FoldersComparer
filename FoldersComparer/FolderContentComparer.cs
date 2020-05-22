using FoldersComparer.FileDataComparers;
using System.Collections.Generic;
using System.Linq;

namespace FoldersComparer
{
    public sealed class FolderContentComparer
    {
        public (List<FileData?>, List<FileData?>) CompareFileSets(List<FileData> directoryData1, List<FileData> directoryData2, IFileDataComparer fileDataComparer)
        {
            directoryData1 = directoryData1.OrderBy(file => file.Name).ToList();
            directoryData2 = directoryData2.OrderBy(file => file.Name).ToList();
            List<FileData> allData = directoryData1.Union(directoryData2, fileDataComparer).OrderBy(file => file.TrimmedName).ThenBy(file => file.Size).ToList();

            int fileIndex1 = 0, fileIndex2 = 0;
            var result1 = new List<FileData?>();
            var result2 = new List<FileData?>();
            foreach (FileData file in allData)
            {
                if (fileIndex1 < directoryData1.Count)
                {
                    if (fileDataComparer.Equals(directoryData1[fileIndex1], file))
                    {
                        result1.Add(file);
                        fileIndex1++;
                    }
                    else
                        result1.Add(null);
                }
                else
                    result1.Add(null);

                if (fileIndex2 < directoryData2.Count)
                {
                    if (fileDataComparer.Equals(directoryData2[fileIndex2], file))
                    {
                        result2.Add(file);
                        fileIndex2++;
                    }
                    else
                        result2.Add(null);
                }
                else
                    result2.Add(null);
            }
            return (result1, result2);
        }
    }
}