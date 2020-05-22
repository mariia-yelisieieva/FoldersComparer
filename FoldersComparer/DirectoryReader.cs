using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FoldersComparer
{
    public sealed class DirectoryReader
    {
        private readonly string initialDirectory;
        private readonly bool trimPath;

        public List<FileData> ReadDirectiryData(string initialDirectory = null, List<FileData> existingData = null)
        {
            if (initialDirectory == null)
                initialDirectory = this.initialDirectory;
            if (existingData == null)
                existingData = new List<FileData>();

            if (!this.DirectoryExists())
                return existingData;

            string[] files = Directory.GetFiles(initialDirectory);
            existingData.AddRange(files.Select(file => new FileData(file, new FileInfo(file).Length, this.initialDirectory, trimPath)));

            string[] subDirectories = Directory.GetDirectories(initialDirectory);
            foreach (string subDirectory in subDirectories)
                this.ReadDirectiryData(subDirectory, existingData);

            return existingData;
        }

        public DirectoryReader(string initialDirectory, bool trimPath)
        {
            this.initialDirectory = initialDirectory;
            this.trimPath = trimPath;
        }

        public string GetRootFolderName()
        {
            if (!this.DirectoryExists())
                return null;
            return Directory.GetParent(this.initialDirectory).Name;
        }

        public bool DirectoryExists() => Directory.Exists(initialDirectory);
    }
}