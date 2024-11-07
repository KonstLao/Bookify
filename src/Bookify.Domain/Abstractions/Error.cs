using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Abstractions
{
    public record Error(string Code, string Name)
    {
        public static Error None = new(string.Empty, string.Empty);
        public static Error NullValue = new("Error.NullValue", "Null value was provided");
        /// <summary>
        /// Dates overlap or join with each other
        /// </summary>
        public static Error DateJoinWith = new("Error.DateJoinWith", "Dates overlap or join with each other");
    }

}
