using MediatR;

namespace CryptocurrencyStatictics.Core.Common
{
    public interface IQuery<out T> : IRequest<T>
    {
    }
}
