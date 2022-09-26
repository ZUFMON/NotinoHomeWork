using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notino.HomeWork.Common;

public static class Extensions
{
    public static string GetDescription(this Enum enumValue)
    {
        var enumDescription = enumValue.GetAttribute<DescriptionAttribute>()?.Description;
        if (enumDescription == null) throw new InvalidEnumArgumentException(enumValue.ToString());
        return enumDescription;
    }

    public static TAttribute? GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()?
            .GetCustomAttribute<TAttribute>();
    }

    public static byte[] GetBytes(this IFormFile formFile)
    {
        using var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}

