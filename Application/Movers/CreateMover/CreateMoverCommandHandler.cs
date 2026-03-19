using Domain.Movers;
using Domain.Movers.MoverValueObject;
using Domain.Primitives;
using MediatR;
using Domain.Items.ItemValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movers.CreateMover
{
    internal class CreateMoverCommandHandler : IRequestHandler<CreateMoverCommand, Result<Guid>>
    {
        private readonly IMoverRepository _moverRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateMoverCommandHandler(IMoverRepository moverRepository, IUnitOfWork unitOfWork)
        {
            _moverRepository = moverRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateMoverCommand request, CancellationToken cancellationToken)
        {
            //validate the request  
            if (request.Energy <= 0)
                return Result.Failure<Guid>(MoverError.InvalidEnergy);
            if (request.WeightLimit <= 0)
                return Result.Failure<Guid>(MoverError.InvalidWeightLimit);
        
            //create the mover
            var mover = Mover.Create(
                    new MoverId(Guid.NewGuid()),
                    new Energy(request.Energy),
                    new Weight(request.WeightLimit));
            //save the mover
            await _moverRepository.AddAsync(mover);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(mover.Id);

        }
    }

 }
