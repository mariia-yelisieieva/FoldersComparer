using System;

namespace FoldersComparer
{
    public struct FileData
    {
        private readonly string trimmedDirectory;
        private readonly bool trimPath;

        private readonly string fileName;

        public string TrimmedName => this.fileName.Substring(this.trimmedDirectory.Length);
        public string Name => this.trimPath ? this.TrimmedName : this.fileName;

        public long Size { get; }

        public FileData(string fileName, long size, string trimmedDirectory, bool trimPath)
        {
            this.trimmedDirectory = trimmedDirectory ?? string.Empty;
            this.trimPath = trimPath;
            this.fileName = fileName;
            this.Size = size;
        }

        public override string ToString() => $"{this.Name} (size: {this.Size} bytes)";
    }
}