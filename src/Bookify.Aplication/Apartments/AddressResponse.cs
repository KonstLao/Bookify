using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Apartments
{
    /// <summary>
    /// Представляет адрес
    /// </summary>
    public sealed class AddressResponse
    {
        /// <summary>
        /// Страна
        /// </summary>
        public string? Country { get; init; }

        /// <summary>
        /// Область/Регион
        /// </summary>
        public string? State { get; init; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string? ZipCode { get; init; }

        /// <summary>
        /// Город
        /// </summary>
        public string? City { get; init; }

        /// <summary>
        /// Улица
        /// </summary>
        public string? Street { get; init; }
    }

}
