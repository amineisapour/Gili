namespace GiliX.Mediator
{
    public interface IRequest : MediatR.IRequest<FluentResults.Result>
    {
    }

    public interface IRequest<TReturnValue> : MediatR.IRequest<FluentResults.Result<TReturnValue>>
    {
    }
}
