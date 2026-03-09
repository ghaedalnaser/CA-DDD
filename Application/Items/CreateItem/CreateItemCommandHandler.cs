using Domain.Items;
using Domain.Items.ItemValueObjects;
using Domain.Primitives;
using MediatR;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items.CreateItem
{
    internal sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Result<Guid>>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateItemCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            // Validate the request
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result.Failure<Guid>(ItemError.InvalidName);

            if (request.Weigth <= 0)
                return Result.Failure<Guid>(ItemError.InvalidWeight);

            // Create the item
            var item = Item.Create(
                new ItemId(Guid.NewGuid()),
                request.Name,
                new Weight(request.Weigth)
            );

            await _itemRepository.AddAsync(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(item.Id);
        }
    }
}
