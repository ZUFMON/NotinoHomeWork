using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nutino.HomeWork.Contracts.Interfaces;
using Nutino.HomeWork.Domain.Services;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace UnitTests.Base
{
    public class TestServicesFixture : TestBedFixture
    {
        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
            => services
                .AddSingleton<ISaveStringService, SaveStringService>()
                .AddSingleton<ILoadStringService, LoadStringService>()
                .AddScoped<IConvertorContentFormatFile, ConvertorContentFormatFile>();

        [Obsolete]
        protected override IEnumerable<string> GetConfigurationFiles()
        {
            yield return "appsettings.json";
        }


        protected override ValueTask DisposeAsyncCore()
            => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "appsettings.json", IsOptional = false };
        }
    }
}
