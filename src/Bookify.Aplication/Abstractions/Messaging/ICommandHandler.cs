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
    /// Интерфейс, описывающий контракт обработчика команды
    /// </summary>
    /// <typeparam name="TCommand"> Тип команды </typeparam>
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }

    /// <summary>
    /// Интерфейс, описывающий контракт обработчика команды с ответом
    /// </summary>
    /// <typeparam name="TCommand"> Тип команды </typeparam>
    /// <typeparam name="TResponse"> Тип ответа </typeparam>
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {
    }

}
