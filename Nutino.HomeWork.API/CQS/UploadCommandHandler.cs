using CommandQuery;
using Nutino.HomeWork.Contracts.Dto.File;

namespace Nutino.HomeWork.API.CQS;

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

