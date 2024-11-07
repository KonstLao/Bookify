using Bookify.Domain.Abstractions;

using MediatR;

/// <summary>
/// Интерфейс запроса
/// </summary>
/// <typeparam name="TResponse"> Тип ответа </typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}
