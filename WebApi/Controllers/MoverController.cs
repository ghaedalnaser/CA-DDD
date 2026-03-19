using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Movers.CreateMover;
using Application.Movers.GetMoverById;
using Application.Movers.GetMovers;
using Application.Movers.GetLeaderBoard;
using Domain.Movers.MoverValueObject;
using Domain.Items.ItemValueObjects;
using WebApi.Extensions;
namespace WebApi.Controllers
{

    [Route("api/mover")]
    public sealed class MoverController : ApiController
    {
        public MoverController(ISender sender) : base(sender)
        {
        }
        //Create a mover
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMover([FromBody] CreateMoverCommand command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        //Get all movers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMovers(GetMoversQuery command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        //Get a mover by id
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMover(Guid id, CancellationToken cancellationToken)
        {
            var command = new GetMoverByIDQuery(new MoverId(id));
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        //get leaderboard
        [HttpGet("leaderboard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLeaderboard(GetLeaderboardQuery command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);

        }


    }
}
