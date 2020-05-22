using System.Diagnostics.CodeAnalysis;

namespace FoldersComparer.FileDataComparers
{
    public sealed class FileDataComparer : IFileDataComparer
    {
        public bool Equals([AllowNull] FileData x, [AllowNull] FileData y) => x.TrimmedName == y.TrimmedName && x.Size == y.Size;

        public int GetHashCode([DisallowNull] FileData obj) => obj.TrimmedName.GetHashCode() + obj.Size.GetHashCode();
    }
}