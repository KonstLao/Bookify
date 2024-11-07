using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Bookify.Domain.Abstractions
{
	/// <summary>
	/// Интерфейс описывает контракт доменного события
	/// </summary>
	public interface IDomainEvent : INotification
	{
	}
}
