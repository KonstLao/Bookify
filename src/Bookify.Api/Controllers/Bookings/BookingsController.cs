using Bookify.Aplication.Apartments;
using Bookify.Aplication.Bookings;
using Bookify.Aplication.Bookings.GetBookings;
using Bookify.Aplication.Bookings.ReserveBooking;
using Bookify.Aplication.Users;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Bogus.DataSets.Name;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bookify.Api.Controllers.Bookings
{
    /// <summary>
    /// Контроллер для работы с бронированиями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Возвращает список всех бронирований пользователя по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Список бронирований пользователя.</returns>
        /// <response code="200">Успешное получение списка бронирований.</response>
        /// <response code="400">Ошибка при получении списка бронирований.</response>
        /// <remarks>
		/// Пример запроса:
		///
		///     GET/ToDo
		///     {
		///         "id": "211B68D4-CE60-4F38-8E5E-054F632596EB"
		///     }
		/// 
		/// </remarks>
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUserBookings([DefaultValue("211B68D4-CE60-4F38-8E5E-054F632596EB")] Guid id)
        {
            var getAllBookingsQuery = new GetAllBookingsQuery(id);
            var result = await sender.Send(getAllBookingsQuery);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        /// <summary>
        /// Возвращает бронирование по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор бронирования.</param>
        /// <returns>Информация о бронировании.</returns>
        /// <response code="200">Успешное получение информации о бронировании.</response>
        /// <response code="400">Ошибка при получении информации о бронировании.</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /ToDo/by-id?id=88DD6FD7-8ED1-4633-9DF4-001B5F97A24A
        /// 
        /// </remarks>
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetBookingById([DefaultValue("88DD6FD7-8ED1-4633-9DF4-001B5F97A24A")] Guid id)
        {
            var getBookingQuery = new GetBookingQuery(id);
            var result = await sender.Send(getBookingQuery);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        /// <summary>
        /// Зарезервировать квартиру
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Идентификатор созданного бронирования.</returns>
        /// <response code="201">Успешное создание бронирования.</response>
        /// <response code="400">Ошибка при создании бронирования.</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET/ToDo
        ///     {
        ///         "apartmentId": "F0CFE5D3-EC49-45F8-94F2-34EB7A1C42F8",
        ///         "userId": "211B68D4-CE60-4F38-8E5E-054F632596EB",
        ///         "startDate": "2024-11-10",
        ///         "endDate": "2024-11-20"
        ///     }
        /// 
        /// </remarks> 
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPost("Reserve-Booking")]
        public async Task<IActionResult> ReserveBooking(
        ReserveBookingRequest request,
        CancellationToken cancellationToken)
        {
            var command = new ReserveBookingCommand(
                request.UserId,
                request.ApartmentId,
                request.StartDate,
                request.EndDate);

            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Изменение статуса букинга
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="newStatus">новый статус</param>
        /// <returns></returns>
        private async Task<IActionResult> ChangeBookingStatusAsync(
        ChangeBookingStatusRequest request,
        CancellationToken cancellationToken, BookingStatus newStatus)
        {
            var command = new ChangeBookingStatusCommand(
                request.Id, newStatus);

            var result = await sender.Send(command, cancellationToken);
            
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Сменить статус резервирования букинга на "подтвержден"
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Статус бронирования.</returns>
        /// <response code="200">Успешное изменение статуса бронирования на "Подтверждено".</response>
        /// <response code="400">Ошибка при изменении статуса бронирования на "Подтверждено".</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET/ToDo
        ///     {
        ///         "Id": "FF7A87FD-33E8-4769-9676-2986EAEC4D94"
        ///     }
        /// 
        /// </remarks> 
        [ProducesResponseType(typeof(BookingStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPost("Confirm-Booking")]
        public async Task<IActionResult> ConfirmBookigAsync(
        ChangeBookingStatusRequest request,
        CancellationToken cancellationToken)
        {
            return await ChangeBookingStatusAsync(request, cancellationToken, BookingStatus.Confirmed);
        }

        /// <summary>
        /// отмена бронирования со стороны клиента
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Статус бронирования.</returns>
        /// <response code="200">Успешное изменение статуса бронирования на "Отменено пользователем".</response>
        /// <response code="400">Ошибка при изменении статуса бронирования на "Отменено пользователем".</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET/ToDo
        ///     {
        ///         "Id": "FF7A87FD-33E8-4769-9676-2986EAEC4D94"
        ///     }
        /// 
        /// </remarks> 
        [ProducesResponseType(typeof(BookingStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPost("Cancell-Booking")]
        public async Task<IActionResult> CancellBookingAsync(
        ChangeBookingStatusRequest request,
        CancellationToken cancellationToken)
        {
            return await ChangeBookingStatusAsync(request, cancellationToken, BookingStatus.Cancelled);
        }

        /// <summary>
        /// отмена бронирования со стороны сервиса
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Статус бронирования.</returns>
        /// <response code="200">Успешное изменение статуса бронирования на "Отменено со стороны сервиса".</response>
        /// <response code="400">Ошибка при изменении статуса бронирования на "Отменено со стороны сервиса".</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET/ToDo
        ///     {
        ///         "Id": "FF7A87FD-33E8-4769-9676-2986EAEC4D94"
        ///     }
        /// 
        /// </remarks> 
        [ProducesResponseType(typeof(BookingStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPost("Reject-Booking")]
        public async Task<IActionResult> RejectBookingAsync(
        ChangeBookingStatusRequest request,
        CancellationToken cancellationToken)
        {
            return await ChangeBookingStatusAsync(request, cancellationToken, BookingStatus.Rejected);
        }

        /// <summary>
        /// завершение бронирования
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Статус бронирования.</returns>
        /// <response code="200">Успешное изменение статуса бронирования на "Завершено".</response>
        /// <response code="400">Ошибка при изменении статуса бронирования на "Завершено".</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET/ToDo
        ///     {
        ///         "Id": "FF7A87FD-33E8-4769-9676-2986EAEC4D94"
        ///     }
        /// 
        /// </remarks> 
        [ProducesResponseType(typeof(BookingStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPost("Complete-Booking")]
        public async Task<IActionResult> CompleteBookingAsync(
        ChangeBookingStatusRequest request,
        CancellationToken cancellationToken)
        {
            return await ChangeBookingStatusAsync(request, cancellationToken, BookingStatus.Completed);
        }

    }




}
