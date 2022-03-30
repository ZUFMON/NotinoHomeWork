using Nutino.HomeWork.Domain.Shared;

namespace Nutino.HomeWork.Contracts.Interfaces;

public interface ILoadStringService 
{
    /// <summary> Upload local or network file on server </summary>
    /// <param name="filename"> path filename (it mus by full path) <example> C:\Folder\myFile.xml</example></param>
    /// <param name="fileEcodingType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IData> LoadFromFileAsync(string filename, FileEcodingType fileEcodingType = FileEcodingType.UTF8, CancellationToken cancellationToken = default);

    /// <summary> Load content from url</summary>
    /// <param name="url">load url for example: <example>http://google.com</example></param>
    /// <param name="urlEncoding"> encode url from web</param>
    /// <returns> return structure as string in memory</returns>
    Task<IData> LoadFromUrlAsync(string url, FileEcodingType urlEncoding = FileEcodingType.UTF8);
}