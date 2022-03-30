using Nutino.HomeWork.Contracts.Interfaces;
using Nutino.HomeWork.Domain.Shared;

namespace Nutino.HomeWork.Domain.Services;

/// <summary> Description important values and raw DATA to work</summary>
public class RawData : IData
{
    /// <summary>Encoding of type data, or which type are saved, load </summary>
    public FileEcodingType FileEncoding { get; set; }
    /// <summary>string data from stream as (data, url, ...) </summary>
    public string Data { get; set; }
}