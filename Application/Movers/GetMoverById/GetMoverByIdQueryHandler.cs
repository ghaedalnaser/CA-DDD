using Domain.Movers;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movers.GetMoverById
{
    internal class GetMoverByIdQueryHandler : IRequestHandler<GetMoverByIDQuery, Result<GetMoverResponse>>
    {
        private readonly IMoverRepository _moverRepository;
        public GetMoverByIdQueryHandler(IMoverRepository moverRepository)
        {
            _moverRepository = moverRepository;
        }
        public async Task<Result<GetMoverResponse>> Handle(GetMoverByIDQuery request, CancellationToken cancellationToken)
        {
            
            var mover = await _moverRepository.GetByIdAsync(request.Id);
            if (mover == null)
                return Result.Failure<GetMoverResponse>(MoverError.NotFound);

            var response = new GetMoverResponse
            (
                Id : mover.Id,
                Energy: mover.Energy.Value,
                WeightLimit: mover.WeightLimit.Value,
                Status: mover.Status.ToString()
            );

            return Result.Success(response);
        }
    }
}
