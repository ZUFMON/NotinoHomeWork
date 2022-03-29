using Microsoft.AspNetCore.Http;
using Nutino.HomeWork.Domain.Shared;

namespace Nutino.HomeWork.Contracts.Dto.File;

public class FileTransferDto : EncodingFile
{
    public IFormFile File { get; set; }
}

public class EncodingFile
{
    public FileEcodingType Endcoding { get; set; } = FileEcodingType.UTF8;
}

public class FileDescriptionDto : EncodingFile
{
    public string FileName { get; set; }
}