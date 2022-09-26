using System;
using System.Threading;
using System.Threading.Tasks;
using CommandQuery;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Notino.HomeWork.API.Controllers;
using Notino.HomeWork.API.CQS;
using Notino.HomeWork.Contracts.Dto.Convert;
using Notino.HomeWork.Domain.Shared;
using Shouldly;
using Xunit;
using Xunit.Abstractions;


namespace UnitTests.Integration;

public class ConvertXmlToDocumentControlerTest
{
    private readonly ITestOutputHelper _testOutputHelper;


    [Fact]
    public async Task ConvertXmlToDocumentControllerTest()
    {
        ConvertXmlToSpecifigCqs dto = new ConvertXmlToSpecifigCqs(
        new ConvertXmlToSpecifigDto
        {
            ConvertToFormat = ConvertToFormat.Json,
            Encoding = FileEcodingType.UTF8,
            FileName = "TestData\\ConvertXmlToDocumentControllerTest.xml",
            FileSaveAsFormat = FileEcodingType.UTF8
        });


        var returnFile = new FileFormatReturnDto
        {
            FileName = "convertFile.xml",
            ConvertedFormat = ConvertToFormat.Json,
            Encoding = FileEcodingType.UTF8
        };


        var queryCommandMock = new Mock<IQueryHandler<ConvertXmlToSpecifigCqs, FileFormatReturnCqs>>();
        queryCommandMock.Setup(x => x.HandleAsync(It.IsAny<ConvertXmlToSpecifigCqs>(), default))
            .ReturnsAsync(new FileFormatReturnCqs(returnFile));

        var sut = new ConvertXmlToDocumentJsonController(queryCommandMock.Object);
        var result = await sut.ConvertXmlToAsync(dto.Dto, CancellationToken.None);

        var resutlCode = result.Result as OkObjectResult;
        resutlCode.ShouldNotBeNull();
        resutlCode.StatusCode.ShouldBe(200);
    }

    [Fact]
    public void ConvertXmlToDocumentControllerErrorREsultTest()
    {
        var queryCommandMock = new Mock<IQueryHandler<ConvertXmlToSpecifigCqs, FileFormatReturnCqs>>();
        queryCommandMock.Setup(x => x.HandleAsync(It.IsAny<ConvertXmlToSpecifigCqs>(), default))
            .ReturnsAsync(() => throw new Exception("error"));

        var sut = new ConvertXmlToDocumentJsonController(queryCommandMock.Object);

        Should.Throw<Exception>(async () => await sut.ConvertXmlToAsync(null, CancellationToken.None));

    }
}

