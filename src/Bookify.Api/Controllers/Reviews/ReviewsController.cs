using Bogus.DataSets;
using Bookify.Api.Controllers.Users;
using Bookify.Aplication.Bookings.GetBookings;
using Bookify.Aplication.Reviews;
using Bookify.Aplication.Users;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using System.ComponentModel;

namespace Bookify.Api.Controllers.Reviews
{
    /// <summary>
    /// Контроллер для работы с отзывами о квартирах.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Возвращает список всех отзывов для указанной квартиры.
        /// </summary>
        /// <param name="id">Идентификатор квартиры.</param>
        /// <returns>Список отзывов о квартире.</returns>
        /// <response code="200">Успешное получение списка отзывов.</response>
        /// <response code="400">Ошибка при получении списка отзывов.</response>
        /// <remarks>
		/// Пример запроса:
		///
		///     GET/ToDo
		///     {
		///         "id": "F0CFE5D3-EC49-45F8-94F2-34EB7A1C42F8"
		///     }
		/// 
		/// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(List<ReviewResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllReviewsByApartmentId([DefaultValue("F0CFE5D3-EC49-45F8-94F2-34EB7A1C42F8")] Guid id)
        {
            var getReviewsQuery = new GetReviewsQuery(id);
            var result = await sender.Send(getReviewsQuery);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        /// <summary>
        /// получение отзыва по идентификатору
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>информация об отзыве</returns>
        /// <response code="200">Успешное получение информации об отзыве.</response>
        /// <response code="400">Ошибка получения информации об отзыве".</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET/ToDo
        ///     {
        ///         "id": "6E52C51F-F756-433D-A665-0909515121BA"
        ///     }
        /// 
        /// </remarks>
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Получить отзыв по идентификатору", Description = "Метод получает отзыв по указанному идентификатору. Значение по умолчанию для идентификатора: '6E52C51F-F756-433D-A665-0909515121BA'.")]
        //[SwaggerParameter(Name = "id", In = ParameterLocation.Path, Description = "Идентификатор отзыва", Example = "6E52C51F-F756-433D-A665-0909515121BA")]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetReviewAsync([DefaultValue("6E52C51F-F756-433D-A665-0909515121BA")] Guid id)
        {

            var getReview = new GetReviewQuery(id);
            var result = await sender.Send(getReview);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Создание нового отзыва
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>идентификатор новго отзыв</returns>
        /// <response code="201">Успешное создание отзыва.</response>
        /// <response code="400">Ошибка при создании отзыва".</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET/ToDo
        ///     {
        ///         "bookingId": "F2C0F8B6-2AD2-4DAC-A2A4-1A0522CB19C8",
        ///         "rating": "5",
        ///         "comment": "Хорошие апартаменты!",
        ///     }
        /// 
        /// </remarks> 
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPost("Create-Review")]
        public async Task<IActionResult> CreateReviewAsync(CreateReviewRequest request, CancellationToken cancellationToken)
        {
            var createReview = new CreateReviewCommand(request.BookingId, request.Rating, request.Comment);
            var result = await sender.Send(createReview, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }


    }
}
