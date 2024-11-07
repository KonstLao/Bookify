using Bookify.Aplication.Apartments;
using Bookify.Aplication.Users;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Bookify.Api.Controllers.Apartments
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Поиск доступных квартир в указанный период.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
		///
		///     GET/ToDo
		///     {
		///         "DateFrom": "2024-11-01"
        ///         "DateTo" : "2024-12-01"
		///     }
		/// 
        /// </remarks>
        /// <param name="dateFrom">Дата начала бронирования.</param>
        /// <param name="dateTo">Дата окончания бронирования.</param>
        /// <returns>Список доступных квартир.</returns>
        /// <response code="200">Успешный поиск квартир.</response>
        /// <response code="400">Ошибка при поиске квартир.</response>
        [ProducesResponseType(typeof(List<ApartmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpGet("search")]
        public async Task<IActionResult> SearchApartmentsByPeriod(
                    [DefaultValue("2024-11-01")] DateOnly dateFrom,
                    [DefaultValue("2024-12-01")] DateOnly dateTo)
        {
            var searchApartmentsQuery = new SearchApartmentsQuery(dateFrom, dateTo);
            var result = await sender.Send(searchApartmentsQuery);
            if (result.IsSuccess) {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        /// <summary>
        /// Поиск квартиры по указанному иденификатору.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET/ToDo
        ///     {
        ///         "Id": "F0CFE5D3-EC49-45F8-94F2-34EB7A1C42F8"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">Идентификатор квартиры/апартамента.</param>
        /// <returns>квартира по идентификатору.</returns>
        /// <response code="200">Успешный поиск квартиры.</response>
        /// <response code="400">Ошибка при поиске квартиры.</response>
        [ProducesResponseType(typeof(List<ApartmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpGet("search-by-id")]
        public async Task<IActionResult> SearchApartmentById([DefaultValue("F0CFE5D3-EC49-45F8-94F2-34EB7A1C42F8")] Guid id)
        {
            var searchApartmentsQuery = new SearchApartmentByIdQuery(id);
            var result = await sender.Send(searchApartmentsQuery);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }


        /// <summary>
        /// Создать новую квартиру.
        /// </summary>
        /// <param name="request">Запрос на создание квартиры.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Информация о созданной квартире.</returns>
        /// <response code="201">Успешное создание квартиры.</response>
        /// <response code="400">Ошибка при создании квартиры.</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /ToDo/Create-Apartment
        ///     {
        ///         "name": "Стильная квартира в центре города",
        ///         "description": "Современная квартира с прекрасным видом на город",
        ///         "country": "Россия",
        ///         "state": "Москва",
        ///         "zipCode": "123456",
        ///         "city": "Москва",
        ///         "street": "Ленинский проспект",
        ///         "priceAmount": 100,
        ///         "cleaningFeeAmount": 10,
        ///         "code": "USD",
        ///         "amenities": [1, 2, 3] 
        ///     }
        /// 
        /// </remarks> 
        [ProducesResponseType(typeof(ApartmentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPost("Create-Apartment")]
        public async Task<IActionResult> CreateAparment(ApartmentRequest request, CancellationToken cancellationToken)
        {
            var CreateApartmentCommand = new CreateApartmentCommand(
                request.Name,
                request.Description,
                request.Country,
                request.State,
                request.ZipCode,
                request.City,
                request.Street,
                request.PriceAmount,
                request.CleaningFeeAmount,
                request.Code,
                request.Amenities);
            var result = await sender.Send(CreateApartmentCommand, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction("SearchApartmentById", new { id = result.Value }, result.Value);
        }

        /// <summary>
        /// Удалить квартиру по указанному идентификатору.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE /ToDo/Delete-Apartment/BB955679-BEA6-4494-800B-5468FF691068
        ///     
        /// </remarks>
        /// <param name="id">Идентификатор квартиры/апартамента.</param>
        /// <returns>Успешное удаление квартиры</returns>
        /// <response code="204">true - Успешное удаление квартиры.</response>
        /// <response code="400">Ошибка при удалении квартиры.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpDelete("Delete-Apartment/{id}")]
        public async Task<IActionResult> DeleteApartment(Guid id)
        {
            var deleteApartmentCommand = new DeleteApartmentCommand(id);
            var result = await sender.Send(deleteApartmentCommand);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }
    }
}
