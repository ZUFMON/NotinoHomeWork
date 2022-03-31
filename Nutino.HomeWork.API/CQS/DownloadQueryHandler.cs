using CommandQuery;
using Nutino.HomeWork.Domain.Shared;

namespace Nutino.HomeWork.API.CQS;

public class DownoadFileCqs : IQuery<DownoadFileReturnCqs>
{
    public string FileName { get; init; }
    public FileEcodingType EncodingFile { get; init; } = FileEcodingType.UTF8;
}

public class DownoadFileReturnCqs
{
    public string Filename { get; init; }
    public IData Data { get; init; }
    public string ContentType { get; init; }
}

public class DownloadQueryHandler : StreamProcessingBase, IQueryHandler<DownoadFileCqs, DownoadFileReturnCqs>
{
    private readonly ILoadStringService _loadStringService;
    private readonly ILogger<ShowUrlQueryHandler> _logger;

    public DownloadQueryHandler(ILogger<ShowUrlQueryHandler> logger,
        ILoadStringService loadStringService,
        IWebHostEnvironment environment) : base(environment)
    {
        _loadStringService = loadStringService;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<DownoadFileReturnCqs> HandleAsync(DownoadFileCqs query, CancellationToken cancellationToken)
    {
        var fi = CheckFile(query.FileName);

        _logger.LogDebug("Upload file from path: {p}", fi.FullName);
        var data = await _loadStringService.LoadFromFileAsync(fi.FullName, query.EncodingFile);

        var contentType = GetContentTypeFromExtensionFile(fi.Extension);

        return new DownoadFileReturnCqs
        {
            Filename = fi.Name,
            Data = data,
            ContentType = contentType
        };
    }
}

