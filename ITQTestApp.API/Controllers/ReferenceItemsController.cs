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
        private readonly ILogger<ReferenceItemsController> _logger;

        public ReferenceItemsController(
            IMediator mediatR,
            ILogger<ReferenceItemsController> logger)
        {
            _mediatR = mediatR;
            _logger = logger;
        }

        [HttpPut]
        [SwaggerRawReferenceItemsBody]
        public async Task<IActionResult> ReplaceItems(ReplaceReferenceItemsRequest request, CancellationToken cancellationToken)
        {
            var itemCount = request.Items?.Count ?? 0;
            _logger.LogDebug("Replace reference items requested. Count: {ItemCount}", itemCount);

            var command = new ReplaceReferenceItemsCommand(request.Items);

            await _mediatR.Send(command, cancellationToken);

            _logger.LogDebug("Replace reference items completed. Count: {ItemCount}", itemCount);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReferenceItemResponse>>> GetItems([FromQuery] GetReferenceItemsRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug(
                "Get reference items requested. Page: {Page}, PageSize: {PageSize}, Search: {Search}",
                request.Page,
                request.PageSize,
                request.Search);

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

            _logger.LogDebug(
                "Get reference items completed. Returned: {ReturnedCount}, Total: {TotalCount}",
                responseItems.Count,
                result.TotalCount);

            return Ok(new PagedResult<ReferenceItemResponse>(responseItems, result.TotalCount));
        }
    }
}
