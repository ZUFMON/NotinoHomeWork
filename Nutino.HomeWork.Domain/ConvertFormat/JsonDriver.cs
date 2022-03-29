using Newtonsoft.Json;

namespace Nutino.HomeWork.Domain.ConvertFormat;

public class JsonDriver
{
    public string ObjToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}