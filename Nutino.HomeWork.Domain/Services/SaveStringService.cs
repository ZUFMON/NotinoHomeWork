using System.Text;
using Microsoft.Extensions.Logging;
using Notino.HomeWork.Contracts.Dto.File;
using Notino.HomeWork.Contracts.Interfaces;
using Notino.HomeWork.Domain.Shared;
using Notino.HomeWork.Common;

namespace Notino.HomeWork.Domain.Services;

/// <summary> Service - care about how to and where save stream data </summary>
public class SaveStringService : ISaveStringService
{
    private readonly ILogger<SaveStringService> _logger;

    public SaveStringService(ILogger<SaveStringService> logger)
    {
        _logger = logger;
    }

    //TODO OT: ukladani na serveru do konkretni slozky, dle zadani to ma sahat vsude po disku (security issue, garbage issue)
    /// <summary> Save file into server specifig folder </summary>
    /// <param name="fileTransfer">full path where is file saved</param>
    /// <param name="data"><see cref="RawData"/></param>
    /// <param name="cancellationToken">cancel processing</param>
    /// <returns></returns>
    public async Task SaveToFileAsync(string path, FileTransferDto fileTransfer, CancellationToken cancellationToken = default)
    {
        var encoding = Encoding.GetEncoding(fileTransfer.Encoding.GetDescription());
        var contentFile = encoding.GetString(fileTransfer.File.GetBytes());
        var fullPathFilename = Path.Combine(path, fileTransfer.File.FileName);

        await File.WriteAllTextAsync(fullPathFilename, contentFile, encoding, cancellationToken);
        _logger.LogInformation("Save file into folder was sucessfully. File {f}", fullPathFilename);
    }

    public async Task SaveToFileAsync(string fullPathFilename, string data, FileEcodingType ecodingType = FileEcodingType.UTF8, CancellationToken cancellationToken = default)
    {
        var encoding = Encoding.GetEncoding(ecodingType.GetDescription());
        
        await File.WriteAllTextAsync(fullPathFilename, data, encoding, cancellationToken);
        _logger.LogInformation("Save file into folder was sucessfully. File {f}", fullPathFilename);
    }
}