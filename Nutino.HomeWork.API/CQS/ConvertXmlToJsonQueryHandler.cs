using CommandQuery;
using Nutino.HomeWork.Contracts.Dto.Convert;
using Nutino.HomeWork.Domain.XmlTemplates;

namespace Nutino.HomeWork.API.CQS;

public class FileFormatReturnCqs
{
    public FileFormatReturnDto Dto { get; init; }

    public FileFormatReturnCqs(FileFormatReturnDto dto)
    {
        Dto = dto;
    }
}

public class ConvertXmlToSpecifigCqs : IQuery<FileFormatReturnCqs>
{
    public readonly ConvertXmlToSpecifigDto Dto;

    public ConvertXmlToSpecifigCqs(ConvertXmlToSpecifigDto dto)
    {
        Dto = dto;
    }
}

public class ConvertXmlToJsonQueryHandler : StreamProcessingBase, IQueryHandler<ConvertXmlToSpecifigCqs, FileFormatReturnCqs>
{
    private readonly ILoadStringService _loadStringService;
    private readonly ILogger<ConvertXmlToJsonQueryHandler> _logger;
    private readonly IConvertorContentFormatFile _convertorContentFormatFile;
    private readonly ISaveStringService _saveStringService;

    public ConvertXmlToJsonQueryHandler(ILogger<ConvertXmlToJsonQueryHandler> logger,
        IConvertorContentFormatFile convertorContentFormatFile,
        ILoadStringService loadStringService,
        ISaveStringService saveStringService, IWebHostEnvironment environment) : base(environment)
    {
        _loadStringService = loadStringService;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _convertorContentFormatFile = convertorContentFormatFile;
        _saveStringService = saveStringService;
    }

    public async Task<FileFormatReturnCqs> HandleAsync(ConvertXmlToSpecifigCqs query, CancellationToken cancellationToken)
    {
        var data = query.Dto;

        _logger.LogInformation("Convert File: {file} to type: {convertType}", data.FileName, data.ConvertToFormat.ToString());
        var fi = CheckFile(data.FileName);

        switch (data.ConvertToFormat)
        {
            case ConvertToFormat.Json:
                var xml = await _loadStringService.LoadFromFileAsync(fi.FullName, data.Endcoding, cancellationToken);
                var json = _convertorContentFormatFile.ConvertXmlToJson<DocumentXML>(xml.Data);
                var filenameToSave = RootPath + Path.GetFileNameWithoutExtension(fi.Name) + "." + data.ConvertToFormat;

                await _saveStringService.SaveToFileAsync(filenameToSave, json, data.FileSaveAsFormat, cancellationToken);

                _logger.LogInformation("Convert file: {sourcefile} to {convertfile} was successfully.", data.FileName, filenameToSave);
                return new FileFormatReturnCqs(
                    new FileFormatReturnDto
                    {
                        FileName = filenameToSave,
                        Endcoding = data.FileSaveAsFormat,
                        ConvertedFormat = data.ConvertToFormat
                    });

            default:
                throw new Exception("Unknow format converting! Convert from xml to specify format is not set in request correct!");
        }
    }
}

