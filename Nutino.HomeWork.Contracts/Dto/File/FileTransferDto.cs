using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Notino.HomeWork.Domain.Shared;

namespace Notino.HomeWork.Contracts.Dto.File;

public class FileTransferDto : EncodingFile
{
    [Required]
    public IFormFile File { get; set; } = default!;
}

public class EncodingFile
{
    public FileEcodingType Encoding { get; set; } = FileEcodingType.UTF8;
}

public class FileDescriptionDto : EncodingFile
{
    [Required]
    public string FileName { get; set; } = default!;
}