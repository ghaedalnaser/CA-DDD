using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Primitives;
using Domain.Items;
using MediatR;
using Domain.Items.ItemValueObjects;

namespace Application.Items.GetItem
{
    internal sealed class GetItemQueryHandler : IRequestHandler<GetItemQuery,Result<List<GetItemResponse>>>
    {
        private readonly IItemRepository _itemRepository;
        public GetItemQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<Result<List<GetItemResponse>>> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var items = await _itemRepository.GetAllAsync(cancellationToken);

            //format response
            var response = items.Select(item => new GetItemResponse(
                Id: item.Id,
                Name: item.Name,
                Weight: item.Weight.Value
                )).ToList();

            return Result.Success(response);
        }
    }
}
