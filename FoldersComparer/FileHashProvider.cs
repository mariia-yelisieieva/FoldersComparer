using System.IO;
using System.Security.Cryptography;

namespace FoldersComparer
{
    public sealed class FileHashProvider
    {
        private readonly SHA256 Sha256 = SHA256.Create();

        public byte[] GetFileHash(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            using FileStream stream = File.OpenRead(fileName);
            return this.Sha256.ComputeHash(stream);
        }
    }
}