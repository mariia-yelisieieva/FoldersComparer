using System;

namespace FoldersComparer.FileDataComparers
{
    public sealed class FileComparerFactory
    {
        public IFileDataComparer GetFileComparerByKindName(string comparerKindName)
        {
            FileComparerKind comparerKind = (FileComparerKind)Enum.Parse(typeof(FileComparerKind), comparerKindName);

            return comparerKind switch
            {
                FileComparerKind.FileSize => new FileDataComparer(),
                FileComparerKind.FileHash => new FileHashComparer(),
                _ => throw new Exception(),
            };
        }
    }
}