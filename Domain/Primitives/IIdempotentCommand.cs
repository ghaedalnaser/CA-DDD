namespace Domain.Primitives;

public interface IIdempotentCommand
{
    string IdempotencyKey { get; }
}
