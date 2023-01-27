using MediatR;

namespace CryptocurrencyStatictics.Core.Common
{
    public interface ICommand<out T> : IRequest<T>
    {
    }
}
