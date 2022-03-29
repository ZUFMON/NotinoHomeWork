using Microsoft.AspNetCore.Mvc;
using Nutino.HomeWork.Contracts.Dto;
using Nutino.HomeWork.Contracts.Dto.Convert;
using Nutino.HomeWork.Domain.Services;
using Nutino.HomeWork.Domain.Shared;
using Nutino.HomeWork.Domain.XmlTemplates;

namespace Nutino.HomeWork.API.Controllers;

/// <summary> Controler for Converting source raw to specifig format </summary>
[ApiController]
[Route("api/[controller]")]
public class ConvertXmlToDocumentJsonController : StreamProcessingControllerBase
{
    private readonly ILoadStringService _loadStringService;
    private readonly ILogger<ConvertXmlToDocumentJsonController> _logger;
    private readonly IConvertorContentFormatFile _convertorContentFormatFile;
    private readonly ISaveStringService _saveStringService;

    public ConvertXmlToDocumentJsonController(
        ILogger<ConvertXmlToDocumentJsonController> logger,
        IConvertorContentFormatFile convertorContentFormatFile,
        ILoadStringService loadStringService,
        ISaveStringService saveStringService,
        IWebHostEnvironment environment) : base(environment)
    {
        _loadStringService = loadStringService;
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
        _convertorContentFormatFile = convertorContentFormatFile;
        _saveStringService = saveStringService;
    }

    [HttpPost("XmlToDocumentJson")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FileFormatReturnDto>> ConvertXmlTo(ConvertXmlToSpecifigDto data)
    {
        _logger.LogInformation("Convert File: {file} to type: {convertType}", data.FileName, data.ConvertToFormat.ToString());
        var fi = CheckFile(data.FileName);

        switch (data.ConvertToFormat)
        {
            case ConvertToFormat.Json:
                var xml = await _loadStringService.LoadFromFileAsync(fi.FullName, data.Endcoding);
                var json = _convertorContentFormatFile.ConvertXmlToJson<DocumentXML>(xml.Data);
                var filenameToSave = RootPath + Path.GetFileNameWithoutExtension(fi.Name) + "." + data.ConvertToFormat;

                await _saveStringService.SaveToFileAsync(filenameToSave, json, data.FileSaveAsFormat);

                _logger.LogInformation("Converting file: {sourcefile} to {convertfile} ", data.FileName, filenameToSave);
                return new ActionResult<FileFormatReturnDto>(new FileFormatReturnDto { FileName = filenameToSave, Endcoding = data.FileSaveAsFormat, ConvertedFormat = data.ConvertToFormat });
            default:
                return new ActionResult<FileFormatReturnDto>(ValidationProblem("Unknow format converting! Convert from xml to specify format is not set in request correct!"));
        }
    }
}

