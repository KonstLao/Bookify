using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Apartments
{
	public record Address(
		string Country,
		string State,
		string ZipCode,
		string City,
		string Street);

	//public record Address
	//{
	//	public string Country { get; init; }
	//	public string State { get; init; }
	//	public string ZipCode { get; init; }
	//	public string City { get; init; }
	//	public string Street { get; init; }
	//}
}
