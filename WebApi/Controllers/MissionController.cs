using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Missions.CreateMission;
using Application.Missions.GetMissionById;
using Application.Missions.GetMissions;
using Application.Missions.StartMission;
using Application.Missions.CompleteMission;
using Application.Missions.CancelMission;
using Application.Missions.FailMission;
using Application.Missions.LoadItem;
using Application.Missions.AssignToMover;
using Domain.Mission.MissionValueObject;
using Domain.Items.ItemValueObjects;
using Domain.Movers.MoverValueObject;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("api/mission")]
    public sealed class MissionController : ApiController
    {
        public MissionController(ISender sender) : base(sender)
        {
        }

        //Create a mission
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMission(CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new CreateMissionCommand(), cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
            
        }

        //Load item to mission
        [HttpPost("{missionId:guid}/load-item/{itemId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoadItem(Guid missionId, Guid itemId, CancellationToken cancellationToken)
        {
            var command = new LoadItemCommand(new ItemId(itemId), new MissionId(missionId));
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        //Assign mission to mover
        [HttpPost("{missionId:guid}/assign-to-mover/{moverId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignToMover(Guid missionId, Guid moverId, CancellationToken cancellationToken)
        {
            var command = new AssignToMoverCommand(new MissionId(missionId), new MoverId(moverId));
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        //Get all missions
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMissions(GetMissionsQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        //Get a mission by id
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMission(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetMissionByIdQuery(new MissionId(id));
            var result = await Sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        //Start a mission
        [HttpPost("{id:guid}/start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> StartMission(Guid id, CancellationToken cancellationToken)
        {
            var command = new StartMissionCommand(new MissionId(id));
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        //Complete a mission
        [HttpPost("{id:guid}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompleteMission(Guid id, CancellationToken cancellationToken)
        {
            var command = new CompleteMissionCommand(new MissionId(id));
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        //Cancel a mission
        [HttpPost("{id:guid}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelMission(Guid id, CancellationToken cancellationToken)
        {
            var command = new CancelMissionCommand(new MissionId(id));
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        //Fail a mission
        [HttpPost("{id:guid}/fail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FailMission(Guid id, CancellationToken cancellationToken)
        {
            var command = new FailMissionCommand(new MissionId(id));
            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
