using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Abstractions
{
	public abstract class Entity
	{

		private readonly List<IDomainEvent> _domainEvents = new();


		public void ClearDomainEvents()
		{
			_domainEvents.Clear();
		}

		protected void RaiseDomainEvent(IDomainEvent domainEvent)
		{
			_domainEvents.Add(domainEvent);
		}

		public IReadOnlyList<IDomainEvent> GetDomainEvents()
		{
			return _domainEvents.ToList();
		}


		/// <summary>
		/// Конструктор по умолчанию (для ORM)
		/// </summary>
		protected Entity()
		{

		}

		/// <summary>
		/// Конструктор с идентификатором
		/// </summary>
		/// <param name="id"> Идентификатор </param>
		protected Entity(Guid id)
		{
			Id = id;
		}

		/// <summary>
		/// Идентификатор сущности
		/// </summary>
		public Guid Id { get; init; }
	}
}
