using System.ComponentModel;

namespace Nutino.HomeWork.Domain.Shared
{
    public enum FileEcodingType
    {
        [Description("utf-8")]
        UTF8,
        [Description("utf-16")]
        UTF16,
        [Description("utf-32")]
        UTF32,
    }
}