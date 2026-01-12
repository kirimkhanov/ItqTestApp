using ITQTestApp.API.Contracts.Requests;
using ITQTestApp.API.Contracts.Responses;
using ITQTestApp.API.Swagger.Attributes;
using ITQTestApp.Application.Commands;
using ITQTestApp.Application.Common.Pagination;
using ITQTestApp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITQTestApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceItemsController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public ReferenceItemsController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpPut]
        [SwaggerRawReferenceItemsBody]
        public async Task<IActionResult> ReplaceItems(ReplaceReferenceItemsRequest request, CancellationToken cancellationToken)
        {
            var command = new ReplaceReferenceItemsCommand(request.Items);

            await _mediatR.Send(command, cancellationToken);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReferenceItemResponse>>> GetItems([FromQuery] GetReferenceItemsRequest request, CancellationToken cancellationToken)
        {
            var query = new GetReferenceItemsQuery(request.Page, request.PageSize, request.Search);

            var result = await _mediatR.Send(query, cancellationToken);

            var responseItems = result.Items
                .Select(item => new ReferenceItemResponse
                {
                    RowNumber = item.RowNumber,
                    Code = item.Code,
                    Value = item.Value
                })
                .ToList();

            return Ok(new PagedResult<ReferenceItemResponse>(responseItems, result.TotalCount));
        }
    }
}
