using System;
using MediatR;
using Domain.Primitives;

namespace Application.Item.CreateItem
{
    public sealed record CreateItemCommand(string Name, decimal Weigth) : IRequest<Result<Guid>>;
}
