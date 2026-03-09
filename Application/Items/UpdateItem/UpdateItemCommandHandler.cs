using MediatR;
using Domain.Primitives;
using Domain.Items.ItemValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Items;

namespace Application.Items.UpdateItem
{
    public sealed class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand , Result>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateItemCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _itemRepository = itemRepository;
        }
        public async Task<Result> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            //get the existing item from the repository
            var existingItem = await _itemRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingItem == null)
            {
                return Result.Failure(ItemError.NotFound);
            }

            // Validate the request
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result.Failure<Guid>(ItemError.InvalidName);

            if (request.Weight <= 0)
                return Result.Failure<Guid>(ItemError.InvalidWeight);

            // Update the existing item with new values
            existingItem.Update(request.Name, new Weight(request.Weight));
            await _itemRepository.UpdateAsync(existingItem, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
