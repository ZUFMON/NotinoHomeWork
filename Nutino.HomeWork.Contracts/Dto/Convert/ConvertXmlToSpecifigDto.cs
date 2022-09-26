using System.ComponentModel.DataAnnotations;
using Notino.HomeWork.Contracts.Dto.File;
using Notino.HomeWork.Domain.Shared;

namespace Notino.HomeWork.Contracts.Dto.Convert;

/// <summary> Convert from Xml to specifig format </summary>
public class ConvertXmlToSpecifigDto : FileDescriptionDto
{
    public FileEcodingType FileSaveAsFormat { get; set; } = FileEcodingType.UTF8;
    /// <summary>Type of Format to convert</summary>
    public ConvertToFormat ConvertToFormat { get; set; } = ConvertToFormat.Json;
}

public class FileFormatReturnDto : FileDescriptionDto
{
    
    /// <summary>Type of Format to convert</summary>
    public ConvertToFormat ConvertedFormat { get; set; } = ConvertToFormat.Json;
}