using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nutino.HomeWork.API.Controllers;

namespace UnitTests.Base
{
    public class TestControllerFixture: TestServicesFixture
    {
        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        {
            base.AddServices(services, configuration);
            services
                .AddSingleton<IConvertXmlToDocumentJsonController, ConvertXmlToDocumentJsonController>()
                .AddSingleton<IStreamProcessingController, StreamProcessingController>();
        }
    }
}
