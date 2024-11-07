using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Aplication.Abstractions.Data
{
    /// <summary>
    /// Фабрика подключений к базе данных
    /// </summary>
    public interface ISqlConnectionFactory
    {
        /// <summary>
        /// Создать подключение к базе данных
        /// </summary>
        /// <returns> Подключение к базе данных </returns>
        IDbConnection CreateConnection();
    }

}
