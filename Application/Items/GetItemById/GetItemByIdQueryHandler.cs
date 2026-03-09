using Application.Items.GetItem;
using Domain.Items;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items.GetItemById
{
    public sealed class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, Result<GetItemResponse>>
    {
        private readonly IItemRepository _itemRepository;
       
        public GetItemByIdQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<Result<GetItemResponse>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            // Get item by id
            var item = await _itemRepository.GetByIdAsync(request.Id, cancellationToken);
            if (item is null)
            {
                return Result.Failure<GetItemResponse>(ItemError.NotFound);
            }
            //format response
            var response = new GetItemResponse(
                Id: item.Id,
                Name: item.Name,
                Weight: item.Weight.Value
                );
            return Result.Success(response);
        }
    }
}
