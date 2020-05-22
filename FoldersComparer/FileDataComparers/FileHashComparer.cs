using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace FoldersComparer.FileDataComparers
{
    public sealed class FileHashComparer : IFileDataComparer
    {
        public bool Equals([AllowNull] FileData x, [AllowNull] FileData y) => x.TrimmedName == y.TrimmedName && Enumerable.SequenceEqual(x.FileHash, y.FileHash);

        public int GetHashCode([DisallowNull] FileData obj) => obj.TrimmedName.GetHashCode() + obj.FileHash.Sum(hash => hash).GetHashCode();
    }
}