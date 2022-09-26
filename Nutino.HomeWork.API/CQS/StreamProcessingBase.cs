namespace Notino.HomeWork.API.CQS;

public class StreamProcessingBase 
{
    protected readonly string RootPath;
    public const string MineTextPLain = "text/plain";

    public StreamProcessingBase(IWebHostEnvironment environment)
    {
        RootPath = environment.ContentRootPath + @"Upload\";
        Directory.CreateDirectory(RootPath);
    }

    //TODO OT: je treba dat do configu nebo DB a dle potreby pripsat Mine
    private Dictionary<string, string> _typeContensDic = new Dictionary<string, string>()
    {
        {".txt", MineTextPLain},
        {".pdf", "application/pdf"},
        {".doc", "application/vnd.ms-word"},
        {".docx", "application/vnd.ms-word"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsx", "application/vnd.ms-excel"},
        {".json", "application/json"},
        {".png", "image/png"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".gif", "image/gif"},
        {".csv", "text/csv"},
        {".xml", "text/xml"},
        {".zip", "application/zip"}
    };

    /// <summary> Load type content from file extension </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    protected string GetContentTypeFromExtensionFile(string extension)
    {
        if (_typeContensDic.ContainsKey(extension))
        {
            return _typeContensDic[extension];
        }

        return _typeContensDic[".txt"];
    }

    protected FileInfo CheckFile(string fileName)
    {
        string fullPathFilename;

        FileInfo fi = new FileInfo(fileName);
        if (fi.Exists) return fi;
        
        fullPathFilename = Path.Combine(RootPath, fileName);
        fi = new FileInfo(fullPathFilename);
        if (!fi.Exists) throw new FileNotFoundException($"File not found! File: {fileName}.");
        
        return fi;
    }
}