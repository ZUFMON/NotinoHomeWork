using System.Text;
using Nutino.HomeWork.Common;
using Nutino.HomeWork.Domain.Shared;

namespace Nutino.HomeWork.Contracts.Interfaces;

public interface IData
{
    public FileEcodingType FileEncoding { get; set; }
    public string Data { get; set; }

    public byte[] DataAsByte
    {
        get
        {
            Encoding encoding = Encoding.GetEncoding(FileEncoding.GetDescription());
            return encoding.GetBytes(Data);
        }

        set
        {
            Encoding encoding = Encoding.GetEncoding(FileEncoding.GetDescription());
            Data = encoding.GetString(value);
        }
    }
}