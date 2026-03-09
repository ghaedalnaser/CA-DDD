using Application.Items.CreateItem;
using Application.Items.GetItem;
using Domain.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{
    [Route("api/item")]
    public sealed class ItemController : ApiController
    {
        public ItemController(ISender sender) : base(sender)
        {

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command, CancellationToken cancellationToken)
        {

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetItems([FromQuery] Guid? id, CancellationToken cancellationToken)
        {
             var result = await Sender.Send(new GetItemQuery(id), cancellationToken);
            
            if (!result.IsSuccess)
            {
                return id.HasValue ? NotFound(result.Error) : BadRequest(result.Error);
            }

            // If ID was provided, return single item; otherwise return list
            if (id.HasValue)
            {
                var item = result.Value.FirstOrDefault();
                return item != null ? Ok(item) : NotFound();
            }

            return Ok(result.Value);
        }
    }
}
