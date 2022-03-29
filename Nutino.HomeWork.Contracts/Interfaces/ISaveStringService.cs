using Nutino.HomeWork.Contracts.Dto.File;
using Nutino.HomeWork.Domain.Shared;

namespace Nutino.HomeWork.Contracts.Interfaces;

public interface ISaveStringService
{
    /// <summary> Save file into server specifig folder </summary>
    /// <param name="path">Path on the server <example>C:\Temp\</example></param>
    /// <param name="fileTransfer">full file data object</param>
    /// <param name="data"><see cref="RawData"/></param>
    /// <param name="cancellationToken">cancel processing</param>
    /// <returns></returns>
    Task SaveToFileAsync(string path, FileTransferDto fileTransfer, CancellationToken cancellationToken = default);

    Task SaveToFileAsync(string fullPathFilename, string data, FileEcodingType ecodingType = FileEcodingType.UTF8, CancellationToken cancellationToken = default);
}