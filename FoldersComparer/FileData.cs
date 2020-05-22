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

        private readonly FileHashProvider hashProvider;
        private byte[] fileHash;
        public byte[] FileHash => this.fileHash ?? (this.fileHash = hashProvider.GetFileHash(this.fileName));

        public FileData(string fileName, long size, string trimmedDirectory, bool trimPath)
        {
            this.trimmedDirectory = trimmedDirectory ?? string.Empty;
            this.trimPath = trimPath;
            this.fileName = fileName;
            this.Size = size;

            this.fileHash = null;
            this.hashProvider = new FileHashProvider();
        }

        public override string ToString() => $"{this.Name} (size: {this.Size} B)";
    }
}