using Xunit;

namespace UnitTests;

public class ProcessingConvertXML
{
    [Fact]
    public void ConvertXMLToSpecifigJsonStructure()
    {
        // nacteni dat ze source nebo cloud
        // nacteni string do Formatu XML
        // prevod z XML do Json
    }


    [Theory]
    [InlineData("fileXml.xml", "fileXml.json")]
    public void ConvertFileToJSonFromXml(string sourceFilename, string destinationFilename)
    {
        // nacteni souboru
        // konvertovani
        // ulozeni
    }


    [Theory]
    [InlineData("/TestData/source.xml", "/Temp/converted.json")]
    public void LoadAndSaveFileNameFromDisk(string sourceFilename, string destination)
    {
        // nacteni dat ze source nebo cloud
        // ulozeni souboru

    }

    [Theory]
    [InlineData("www.trochta.net/url.txt")]
    public void LoadHttpRequest(string url)
    {
        // mock url
        // nacteni z HTTP
    }

    [Theory]
    [InlineData("source.xml", "myAddress@email.xxx")]
    public void SendEmail(string sourceFilename, string emailAdress)
    {
        // nacteni souboru
        // odeslani emailu
    }
}
