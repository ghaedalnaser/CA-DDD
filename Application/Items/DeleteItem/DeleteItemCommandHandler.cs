using Domain.Items;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items.DeleteItem
{
    internal class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Result>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteItemCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var existingItem = await _itemRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingItem == null)
            {
                return Result.Failure(ItemError.NotFound);
            }
            await _itemRepository.DeleteAsync(existingItem, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
