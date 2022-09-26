using CommandQuery;
using Notino.HomeWork.Contracts.Dto.File;
using Notino.HomeWork.Contracts.Interfaces;

namespace Notino.HomeWork.API.CQS;

public class UploadFileCqs : ICommand
{
    public FileTransferDto Dto { get; init; }
}

public class UploadCommandHandler : StreamProcessingBase, ICommandHandler<UploadFileCqs>
{
    private readonly ISaveStringService _saveStringService;

    public UploadCommandHandler(ISaveStringService saveStringService, IWebHostEnvironment environment) : base(environment)
    {
        _saveStringService = saveStringService;
    }

    public async Task HandleAsync(UploadFileCqs query, CancellationToken cancellationToken)
    {
        await _saveStringService.SaveToFileAsync(RootPath, query.Dto, cancellationToken);
    }
}

