using CommandQuery;
using Microsoft.AspNetCore.Mvc;
using Nutino.HomeWork.API.CQS;
using Nutino.HomeWork.Contracts.Dto.Convert;

namespace Nutino.HomeWork.API.Controllers;

/// <summary> Controler for Converting source raw to specifig format </summary>
[ApiController]
[Route("api/[controller]")]
public class ConvertXmlToDocumentJsonController : ControllerBase
{
    private readonly IQueryHandler<ConvertXmlToSpecifigCqs, FileFormatReturnCqs> _queryHandler;

    public ConvertXmlToDocumentJsonController(IQueryHandler<ConvertXmlToSpecifigCqs, FileFormatReturnCqs> queryHandler)
    {
        _queryHandler = queryHandler;
    }

    [HttpPost("XmlToDocumentJson")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FileFormatReturnDto>> ConvertXmlToAsync(ConvertXmlToSpecifigDto data)
    {
        var input = new ConvertXmlToSpecifigCqs(data);
        var resultData = await _queryHandler.HandleAsync(input, default);
        return Ok(resultData.Dto);
    }
}

