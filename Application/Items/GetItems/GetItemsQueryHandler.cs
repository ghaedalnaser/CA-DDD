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
    internal sealed class GetItemsQueryHandler : IRequestHandler<GetItemsQuery,Result<List<GetItemResponse>>>
    {
        private readonly IItemRepository _itemRepository;
        public GetItemsQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<Result<List<GetItemResponse>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
          
                // Get all items
                var items = await _itemRepository.GetAllAsync(cancellationToken);

                //format response
                var response = items.Select(item => new GetItemResponse(
                     item.Id,
                     item.Name,
                     item.Weight.Value,
                     item.Status.ToString()
                    )).ToList();

                return Result.Success(response);
            
        }
    }
}
