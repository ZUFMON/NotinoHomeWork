using CommandQuery;
using Nutino.HomeWork.Domain.Shared;

namespace Nutino.HomeWork.API.CQS;

    public class ShowUrlCqs : IQuery<ShowUrlReturnCqs>
    {
        public string Url { get; init; }
        public FileEcodingType EncodingFile { get; init; } = FileEcodingType.UTF8;
    }

    public class ShowUrlReturnCqs
    {
        public IData Data { get; init; }
    }

    public class ShowUrlQueryHandler : StreamProcessingBase, IQueryHandler<ShowUrlCqs, ShowUrlReturnCqs>
    {
        private readonly ILoadStringService _loadStringService;

        public ShowUrlQueryHandler(ILoadStringService loadStringService, IWebHostEnvironment environment) : base(environment)
        {
            _loadStringService = loadStringService;
        }

        public async Task<ShowUrlReturnCqs> HandleAsync(ShowUrlCqs query, CancellationToken cancellationToken)
        {
            var data = await _loadStringService.LoadFromUrlAsync(query.Url, query.EncodingFile);
            return new ShowUrlReturnCqs
            {
                Data = data
            };
        }
    }



