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
    /// Интерфейс команды
    /// </summary>
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    /// <summary>
    /// Интерфейс команды с ответом типа <typeparamref name="TReponse"/>
    /// </summary>
    /// <typeparam name="TReponse"> Тип ответа </typeparam>
    public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand
    {
    }

    /// <summary>
    /// Интерфейс базовой команды
    /// </summary>
    public interface IBaseCommand
    {
    }
}
