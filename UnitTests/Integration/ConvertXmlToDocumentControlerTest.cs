using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Nutino.HomeWork.API.Controllers;
using Nutino.HomeWork.Contracts.Dto.Convert;
using Nutino.HomeWork.Contracts.Interfaces;
using Nutino.HomeWork.Domain.Shared;
using Shouldly;
using UnitTests.Base;
using Xunit;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace UnitTests.Integration
{
    public class ConvertXmlToDocumentControlerTest:TestBed<TestServicesFixture>
    {
        private readonly ITestOutputHelper _testOutputHelper;


        [Fact]
        public async Task ConvertXmlToDocumentControllerTest()
        {
            ConvertXmlToSpecifigDto dto = new ConvertXmlToSpecifigDto()
            {
                ConvertToFormat = ConvertToFormat.Json,
                Endcoding = FileEcodingType.UTF8,
                FileName = "TestData\\ConvertXmlToDocumentControllerTest.xml",
                FileSaveAsFormat = FileEcodingType.UTF8
            };

            var returnDto = new FileFormatReturnDto()
            {
                Endcoding = FileEcodingType.UTF8,
                FileName = "FileName.txt",
                ConvertedFormat = ConvertToFormat.Json
            };

            var ConvertXmlControlerMock = new Mock<IConvertXmlToDocumentJsonController>();

            var convertXml = new Mock<IConvertorContentFormatFile>();
            var logger = new Mock<ILogger<ConvertXmlToDocumentJsonController>>();
            var loadString = new Mock<ILoadStringService>();
            var saveString = new Mock<ISaveStringService>();
            var enviroment = new Mock<IWebHostEnvironment>();


            var returnsResult = ConvertXmlControlerMock.Setup(_ => _.ConvertXmlToAsync(dto)).ReturnsAsync(returnDto);

            
            var sut = new ConvertXmlToDocumentJsonController(logger.Object, convertXml.Object, loadString.Object, saveString.Object, enviroment.Object);

            var result =  await sut.ConvertXmlToAsync(dto);

            var resutlCode = result.Result as OkObjectResult;
            resutlCode.ShouldNotBeNull();
            resutlCode.StatusCode.ShouldBe(200);
        }


        public ConvertXmlToDocumentControlerTest(ITestOutputHelper testOutputHelper, TestServicesFixture fixture) : base(testOutputHelper, fixture)
        {
            _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
            
        }
    }
}
