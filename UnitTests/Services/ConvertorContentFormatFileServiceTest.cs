using System;
using Nutino.HomeWork.Contracts.Interfaces;
using Nutino.HomeWork.Domain.XmlTemplates;
using UnitTests.Base;
using Xunit;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;
using Shouldly;

namespace UnitTests;

public class ConvertorContentFormatFileServiceTest : TestBed<TestServicesFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IConvertorContentFormatFile _convertXmlToDoc;

    [Theory]
    [InlineData("<Document><Title>NadpisTitle</Title><Text>TextNejakyTest</Text></Document>", "{\"Title\":\"NadpisTitle\",\"Text\":\"TextNejakyTest\"}")]
    [InlineData("<Document><Text> TextNejakyTest </Text></Document>", "{\"Title\":null,\"Text\":\" TextNejakyTest \"}")]
    [InlineData("<Document><Text>TextNejakyTest</Text><dalsiElement>HUAAA</dalsiElement></Document>", "{\"Title\":null,\"Text\":\"TextNejakyTest\"}")]
    public void ConvertXMLToSpecifigJsonStructure(string xml, string json)
    {
        var xmlTojson = _convertXmlToDoc.ConvertXmlToJson<DocumentXML>(xml);
        xmlTojson.ShouldBe(json);
    }


    [Theory]
    [InlineData("<nevalidni format>")]
    public void ConvertXmlToSpecifigJsonStructureException(string xml)
    {
        Should.Throw<Exception>(() =>
        {
            var xmlTojson = _convertXmlToDoc.ConvertXmlToJson<DocumentXML>(xml);
        });
    }

    public ConvertorContentFormatFileServiceTest(ITestOutputHelper testOutputHelper, TestServicesFixture fixture) : base(testOutputHelper, fixture)
    {
        _testOutputHelper = testOutputHelper ?? throw new NullReferenceException(nameof(testOutputHelper));
        _convertXmlToDoc = fixture.GetService<IConvertorContentFormatFile>(testOutputHelper) ?? throw new NullReferenceException(nameof(IConvertorContentFormatFile));
    }
}