using Application.Items.CreateItem;
using Application.Items.DeleteItem;
using Application.Items.GetItem;
using Application.Items.GetItemById;
using Application.Items.UpdateItem;
using Domain.Items.ItemValueObjects;
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
        //Create an item
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command, CancellationToken cancellationToken)
        {

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        //Get all items
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetItems(GetItemsQuery command,CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        //Get an item by id
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetItem(Guid id, CancellationToken cancellationToken)
        {
            var command = new GetItemByIdQuery(new ItemId(id));
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        //update an item
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] UpdateItemRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateItemCommand(new ItemId(id), request.Name, request.Weight);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);

        }
        //delete an item
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteItem(Guid id, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new DeleteItemCommand(new ItemId(id)), cancellationToken);
            return result.IsSuccess ? Ok() : NotFound(result.Error);
        }
    }
}
