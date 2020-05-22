using FoldersComparer.FileDataComparers;

namespace FoldersComparer.Configuration
{
    public sealed class Configuration
    {
        private string[] directories;
        public string[] Directories
        {
            get => this.directories;
            set
            {
                if (value == null)
                    return;

                for (int i = 0; i < value.Length; i++)
                {
                    if (!value[i].EndsWith("\\"))
                        value[i] += "\\";
                }
                this.directories = value;
            }
        }

        public bool TrimPaths { get; set; }

        public bool ShowEqualLines { get; set; }

        public IFileDataComparer DataComparer { get; set; }
    }
}