using Bookify.Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Abstractions.Messaging
{
    /// <summary>
    /// Интерфейс обработчика запроса
    /// </summary>
    /// <typeparam name="TQuery"> Тип запроса </typeparam>
    /// <typeparam name="TResponse"> Тип ответа </typeparam>
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }

}
