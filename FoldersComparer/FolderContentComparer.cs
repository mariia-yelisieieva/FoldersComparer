using FoldersComparer.FileDataComparers;
using System.Collections.Generic;
using System.Linq;

namespace FoldersComparer
{
    public sealed class FolderContentComparer
    {
        private readonly IFileDataComparer fileDataComparer;

        public List<FileData?>[] CompareFileSets(bool showEqual, params List<FileData>[] directoriesData)
        {
            IEnumerable<FileData> allData = new List<FileData>();
            for (int i = 0; i < directoriesData.Length; i++)
            {
                directoriesData[i] = directoriesData[i].OrderBy(file => file.Name).ToList();
                allData = allData.Union(directoriesData[i], this.fileDataComparer);
            }
            allData = allData.OrderBy(file => file.TrimmedName).ThenBy(file => file.Size).ToList();

            var fileIndices = new int[directoriesData.Length];
            for (int i = 0; i < directoriesData.Length; i++)
                fileIndices[i] = 0;

            var results = new List<FileData?>[directoriesData.Length];
            for (int i = 0; i < directoriesData.Length; i++)
                results[i] = new List<FileData?>();

            foreach (FileData file in allData)
            {
                if (!showEqual && this.AreAllLinesEqual(directoriesData, fileIndices, file))
                    continue;

                for (int i = 0; i < directoriesData.Length; i++)
                {
                    if (fileIndices[i] >= directoriesData[i].Count)
                    {
                        results[i].Add(null);
                        continue;
                    }

                    if (this.fileDataComparer.Equals(directoriesData[i][fileIndices[i]], file))
                    {
                        results[i].Add(file);
                        fileIndices[i]++;
                    }
                    else
                        results[i].Add(null);
                }
            }
            return results;
        }

        private bool AreAllLinesEqual(List<FileData>[] directoriesData, int[] fileIndices, FileData file)
        {
            bool allEqual = true;
            for (int i = 0; i < directoriesData.Length; i++)
                allEqual &= fileIndices[i] < directoriesData[i].Count && this.fileDataComparer.Equals(directoriesData[i][fileIndices[i]], file);

            if (allEqual)
            {
                for (int i = 0; i < directoriesData.Length; i++)
                    fileIndices[i]++;
                return true;
            }
            return false;
        }

        public FolderContentComparer(IFileDataComparer fileDataComparer)
        {
            this.fileDataComparer = fileDataComparer;
        }
    }
}