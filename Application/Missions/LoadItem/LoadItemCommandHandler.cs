using MediatR;
using Domain.Primitives;
using Domain.Items;
using Domain.Items.ItemValueObjects;
using Domain.Mission;
using Domain.Mission.MissionValueObject;
using Domain.Movers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Missions.LoadItem
{
    internal class LoadItemCommandHandler : IRequestHandler<LoadItemCommand, Result>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMissionRepository _missionRepository;
        private readonly IMoverRepository _moverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LoadItemCommandHandler(IItemRepository itemRepository, IMissionRepository missionRepository, IMoverRepository moverRepository, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _missionRepository = missionRepository;
            _moverRepository = moverRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Result> Handle(LoadItemCommand command, CancellationToken cancellationToken)
        {
            // Get mission
            var mission = await _missionRepository.GetByIdAsync(command.MissionId, cancellationToken);
            if (mission is null)
                return Result.Failure(MissionError.NotFound);
                
            // Get item
            var item = await _itemRepository.GetByIdAsync(command.ItemId, cancellationToken);
            if (item is null)
                return Result.Failure(ItemError.NotFound);
                
            // Get mover (check if mission has assigned mover)
            if (mission.MoverId == null)
                return Result.Failure(MissionError.MoverNotAssigned);
                
            var mover = await _moverRepository.GetByIdAsync(mission.MoverId, cancellationToken);
            if (mover is null)
                return Result.Failure(MoverError.NotFound);

            // Load item to mission
            var result = mission.LoadItem(item, mover);
            if (result.IsFailure)
                return Result.Failure(result.Error);

            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch (ConcurrencyException)
            {
                return Result.Failure(ItemError.Conflict);
            }
        }
    }
}
