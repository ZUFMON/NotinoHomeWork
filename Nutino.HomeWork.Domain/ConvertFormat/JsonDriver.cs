using Newtonsoft.Json;

namespace Notino.HomeWork.Domain.ConvertFormat;

public class JsonDriver
{
    public string ObjToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}