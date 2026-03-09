using System;
using MediatR;
using Domain.Primitives;

namespace Application.Items.CreateItem
{
    public sealed record CreateItemCommand(string Name, decimal Weight) : IRequest<Result<Guid>>;
}
