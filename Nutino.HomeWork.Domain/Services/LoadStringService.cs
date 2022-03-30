using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nutino.HomeWork.Common;
using Nutino.HomeWork.Contracts.Interfaces;
using Nutino.HomeWork.Domain.Shared;

namespace Nutino.HomeWork.Domain.Services;

/// <summary> Service - care of load data from source</summary>
public class LoadStringService : ILoadStringService
{
    /// <summary> Upload local or network file on server </summary>
    /// <param name="filename"> path filename (it mus by full path) <example> C:\Folder\myFile.xml</example></param>
    /// <param name="fileEcodingType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IData> LoadFromFileAsync(string filename, FileEcodingType fileEcodingType = FileEcodingType.UTF8, CancellationToken cancellationToken = default)
    {
        var data = new RawData();
        var encodingType = fileEcodingType.GetDescription();
        var encoding = Encoding.GetEncoding(encodingType);

        data.Data = await File.ReadAllTextAsync(filename, encoding, cancellationToken);
        data.FileEncoding = fileEcodingType;
        return data;
    }

    /// <summary> Load content from url</summary>
    /// <param name="url">load url for example: <example>http://google.com</example></param>
    /// <param name="urlEncoding"> encode url from web</param>
    /// <returns> return structure as string in memory</returns>
    public async Task<IData> LoadFromUrlAsync(string url, FileEcodingType urlEncoding = FileEcodingType.UTF8)
    {
        var client = new HttpClient();
        var response = await client.GetByteArrayAsync(url);
        var encoding = Encoding.GetEncoding(urlEncoding.GetDescription());

        var responseString = encoding.GetString(response, 0, response.Length - 1);
        return new RawData
        {
            Data = responseString,
            FileEncoding = urlEncoding
        };
    }
}