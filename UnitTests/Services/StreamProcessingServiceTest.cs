using System;
using System.Threading.Tasks;
using Notino.HomeWork.Contracts.Interfaces;
using Notino.HomeWork.Domain.Shared;
using Shouldly;
using UnitTests.Base;
using Xunit;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace UnitTests;

public class StreamProcessingServiceTest : TestBed<TestServicesFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;

    [Theory]
    [InlineData("http://www.google.com", "Apache-2.0")]
    public async Task LoadHttpRequest(string url, string except)
    {
        var google = await _loadStringService.LoadFromUrlAsync(url);
        google.Data.ShouldContain(except);
    }

    [Theory]
    [InlineData("--==X==--")]
    public void LoadHttpRequestException(string url)
    {
        Should.Throw<Exception>(async () =>
        {
            var google = await _loadStringService.LoadFromUrlAsync(url);
        });
    }

    [Theory]
    [InlineData("SaveAndLoadFileTestUtf8.txt","TExt 123455667789  ěščřřžžýáíé ?:?: 1°", FileEcodingType.UTF8)]
    [InlineData("SaveAndLoadFileTestUtf16.txt", "TExt 123455667789  ěščřřžžýáíé ?:?: 1°", FileEcodingType.UTF16)]
    public async Task SaveAndLoadFile(string filenamePath ,string data, FileEcodingType encodingFile)
    {
        await _saveStringService.SaveToFileAsync(filenamePath, data, encodingFile);
        var readDataFromFile = await _loadStringService.LoadFromFileAsync(filenamePath, encodingFile);

        readDataFromFile.Data.ShouldBe(data);
        readDataFromFile.FileEncoding.ShouldBe(encodingFile);
    }

    [Theory]
    [InlineData("c:\\XXX\\password.key")]
    public void SaveAndLoadFileException(string filenamePath)
    {
        Should.Throw<Exception>(
            async () =>
            {
                await _saveStringService.SaveToFileAsync(filenamePath, "xxx", FileEcodingType.UTF16);
            });
    }


    private readonly ISaveStringService _saveStringService;
    private readonly ILoadStringService _loadStringService;

    public StreamProcessingServiceTest(ITestOutputHelper testOutputHelper, TestServicesFixture fixture) : base(testOutputHelper, fixture)
    {
        _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        _saveStringService = fixture.GetService<ISaveStringService>(testOutputHelper) ?? throw new ArgumentNullException(nameof(ISaveStringService));
        _loadStringService = fixture.GetService<ILoadStringService>(testOutputHelper) ?? throw new ArgumentNullException(nameof(ILoadStringService));
    }
}