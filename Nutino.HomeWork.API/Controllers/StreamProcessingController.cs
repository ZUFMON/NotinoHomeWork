using CommandQuery;
using Microsoft.AspNetCore.Mvc;
using Nutino.HomeWork.API.CQS;
using Nutino.HomeWork.Contracts.Dto.File;
using Nutino.HomeWork.Domain.Shared;

namespace Nutino.HomeWork.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StreamProcessingController : ControllerBase
{
    private readonly ILogger<StreamProcessingController> _logger;
    private readonly IQueryHandler<DownoadFileCqs, DownoadFileReturnCqs> _downloadFileQueryHandler;
    private readonly IQueryHandler<ShowUrlCqs, ShowUrlReturnCqs> _shoQueryHandler;
    private readonly ICommandHandler<UploadFileCqs> _uploadFileCommandHandler;

    public StreamProcessingController(ILogger<StreamProcessingController> logger,
        IQueryHandler<DownoadFileCqs, DownoadFileReturnCqs> downloadFileQueryHandler,
        IQueryHandler<ShowUrlCqs, ShowUrlReturnCqs> shoQueryHandler,
        ICommandHandler<UploadFileCqs> uploadFileCommandHandler)
    {
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
        _downloadFileQueryHandler = downloadFileQueryHandler;
        _shoQueryHandler = shoQueryHandler;
        _uploadFileCommandHandler = uploadFileCommandHandler;
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

        var downloadFile = await _downloadFileQueryHandler.HandleAsync(new DownoadFileCqs { FileName = fileName, EncodingFile = encodingFile }, default);

        var file = File(downloadFile.Data.DataAsByte, downloadFile.ContentType);
        var cd = new System.Net.Mime.ContentDisposition
        {
            FileName = downloadFile.Filename,
            // always prompt the user for downloading, set to true if you want 
            // the browser to try to show the file inline
            Inline = false
        };

        Response.Headers.Append("Content-Disposition", cd.ToString());
        _logger.LogInformation("File: {filename} download successfully", fileName);
        return file;
    }

    /// <summary> Show content of url on web adress </summary>
    /// <param name="url">Url must by definet as valid URI <example>http://google.com</example></param>
    /// <param name="encodingFile">Type of encoding show content format</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("ShowUrl")]
    public async Task<ActionResult> ShowUrl(string url, FileEcodingType encodingFile = FileEcodingType.UTF8)
    {
        _logger.LogDebug("Starting show data on url : {ulr}", url);

        var data = await _shoQueryHandler.HandleAsync(new ShowUrlCqs { Url = url, EncodingFile = encodingFile }, default);
        var file = File(data.Data.DataAsByte, StreamProcessingBase.MineTextPLain);

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
        await _uploadFileCommandHandler.HandleAsync(new UploadFileCqs { Dto = data }, default);

        _logger.LogInformation("Upload file was successfully. File: {filename} with content: {cont}", data.File.FileName, data.File.ContentType);
        return Ok("Upload file was successfully");
    }
}

