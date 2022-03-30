using Microsoft.AspNetCore.Mvc;
using Nutino.HomeWork.Contracts.Dto;
using Nutino.HomeWork.Contracts.Dto.Convert;
using Nutino.HomeWork.Contracts.Dto.File;
using Nutino.HomeWork.Domain.Services;
using Nutino.HomeWork.Domain.Shared;

namespace Nutino.HomeWork.API.Controllers;

public interface IStreamProcessingController
{
    /// <summary> Download file form server to Computer folder</summary>
    /// <param name="fileName"></param>
    /// <param name="encodingFile"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    Task<ActionResult> DownloadFile(string fileName, FileEcodingType encodingFile = FileEcodingType.UTF8);

    /// <summary> Show specific content of url  </summary>
    /// <param name="url">Url must by definet as valid URI <example>http://google.com</example></param>
    /// <param name="encodingFile">Type of encoding show content format</param>
    /// <returns></returns>
    Task<ActionResult> ShowUrl(string url, FileEcodingType encodingFile = FileEcodingType.UTF8);

    /// <summary> Upload specific file on the server.</summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<ActionResult> UploadFile([FromForm] FileTransferDto data);
}

[ApiController]
[Route("api/[controller]")]
public class StreamProcessingController : StreamProcessingControllerBase, IStreamProcessingController
{
    private readonly ILogger<StreamProcessingController> _logger;
    private readonly ISaveStringService _saveStringService;
    private readonly ILoadStringService _loadStringService;

    public StreamProcessingController(
        ILogger<StreamProcessingController> logger,
        ISaveStringService saveStringService,
        ILoadStringService loadStringService,
        IWebHostEnvironment environment) : base(environment)
    {
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
        _saveStringService = saveStringService;
        _loadStringService = loadStringService;
    }

    /// <summary> Download file form server to Computer folder</summary>
    /// <param name="fileName"></param>
    /// <param name="encodingFile"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    [HttpGet("DownloadFile")]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DownloadFile(string fileName, FileEcodingType encodingFile = FileEcodingType.UTF8)
    {
        _logger.LogDebug("Starting donwloading file: {filename}", fileName);

        var fi = CheckFile(fileName);

        _logger.LogDebug("Upload file from path: {p}", fi.FullName);
        var data = await _loadStringService.LoadFromFileAsync(fi.FullName, encodingFile);

        var contentType = GetContentTypeFromExtensionFile(fi.Extension);
        var file = File(data.DataAsByte, contentType);

        var cd = new System.Net.Mime.ContentDisposition
        {
            FileName = fi.Name,
            // always prompt the user for downloading, set to true if you want 
            // the browser to try to show the file inline
            Inline = false,
        };

        Response.Headers.Append("Content-Disposition", cd.ToString());
        _logger.LogInformation("File: {filename} download successfully", fileName);
        return file;
    }

    /// <summary> Show specific content of url  </summary>
    /// <param name="url">Url must by definet as valid URI <example>http://google.com</example></param>
    /// <param name="encodingFile">Type of encoding show content format</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("ShowUrl")]
    public async Task<ActionResult> ShowUrl(string url, FileEcodingType encodingFile = FileEcodingType.UTF8)
    {
        _logger.LogDebug("Starting show data on url : {ulr}", url);

        var data = await _loadStringService.LoadFromUrlAsync(url, encodingFile);
        var file = File(data.DataAsByte, MineTextPLain);
       
        _logger.LogInformation("Url: {url} show successfully", url);
        return file;
    }

    /// <summary> Upload specific file on the server.</summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("UploadFile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UploadFile([FromForm] FileTransferDto data)
    {
        _logger.LogDebug("Starting upload file: {filename} with content: {cont}", data.File.FileName, data.File.ContentType);
        await _saveStringService.SaveToFileAsync(RootPath, data);

        _logger.LogInformation("Upload file was successfully. File: {filename} with content: {cont}", data.File.FileName, data.File.ContentType);
        return Ok("Upload file was successfully");
    }
}

