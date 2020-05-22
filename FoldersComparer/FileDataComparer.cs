using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FoldersComparer
{
    public sealed class FileDataComparer : IEqualityComparer<FileData>
    {
        public bool Equals([AllowNull] FileData x, [AllowNull] FileData y) => x.TrimmedName == y.TrimmedName && x.Size == y.Size;

        public int GetHashCode([DisallowNull] FileData obj) => obj.TrimmedName.GetHashCode() + obj.Size.GetHashCode();
    }
}