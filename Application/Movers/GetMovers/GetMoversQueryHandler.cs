using Application.Movers.GetMoverById;
using MediatR;
using Domain.Movers;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movers.GetMovers
{
    internal class GetMoversQueryHandler : IRequestHandler<GetMoversQuery, Result<List<GetMoverResponse>>>  
    {
        private readonly IMoverRepository _moverRepository;
        public GetMoversQueryHandler(IMoverRepository moverRepository)
        {
            _moverRepository = moverRepository;
        }
        public async Task<Result<List<GetMoverResponse>>> Handle(GetMoversQuery request, CancellationToken cancellationToken)
        {
            var movers = await _moverRepository.GetAllAsync(cancellationToken);
            var response = movers.Select(mover => new GetMoverResponse(
                    mover.Id, 
                    mover.Energy.Value, 
                    mover.WeightLimit.Value, 
                    mover.Status.ToString())).ToList();
            
            return Result.Success(response);
        }

    }
}
