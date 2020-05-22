using System;
using System.Collections.Generic;

namespace FoldersComparer
{
    public sealed class ResultFileNameProvider
    {
        private readonly HashSet<string> cachedFileNames = new HashSet<string>();
        private readonly int directoriesCount;

        public string GetUniqueRootName(string directoryRootName)
        {
            for (var index = 0; index < this.directoriesCount; index++)
            {
                string fileName = (index == 0) ? directoryRootName : $"{directoryRootName} ({index})";
                if (this.cachedFileNames.Contains(fileName))
                    continue;

                this.cachedFileNames.Add(fileName);
                return fileName;
            }
            return DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        public ResultFileNameProvider(int directoriesCount)
        {
            this.directoriesCount = directoriesCount;
        }
    }
}