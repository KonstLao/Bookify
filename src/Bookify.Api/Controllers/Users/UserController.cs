using Bookify.Aplication.Reviews;
using Bookify.Aplication.Users;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Bookify.Api.Controllers.Users
{
    /// <summary>
    /// Контроллер предоставляет методы для работы с пользователями
    /// </summary>
    /// <param name="sender"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISender sender) : ControllerBase
    {

		/// <summary>
		/// Позволяет вернуть информацию о пользователе по идентификатору
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		///
		///     GET/ToDo
		///     {
		///         "id": "211B68D4-CE60-4F38-8E5E-054F632596EB"
		///     }
		/// 
		/// </remarks>
		/// <param name="id">Идентификатор пользователя</param>
		/// <returns></returns>
		[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
		[HttpGet("by-id")]
        public async Task<IActionResult> GetUserById([DefaultValue("211B68D4-CE60-4F38-8E5E-054F632596EB")] Guid id)
        {
            var getUserQuery = new GetUserQuery(id);
            var result = await sender.Send(getUserQuery);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>идентификатор новго пользователя</returns>
        /// <response code="201">Успешное создание пользователя.</response>
        /// <response code="400">Ошибка при создании пользователя".</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET/ToDo
        ///     {
        ///         "firstName": "Константин",
        ///         "lastName": "Костин",
        ///         "email": "Kostya777@mail.ru",
        ///         "userName": "Kostya777"
        ///     }
        /// 
        /// </remarks> 
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPost("Create-User")]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var createUser = new CreateUserCommand(request.FirstName, request.LastName, request.Email, request.UserName);
            var result = await sender.Send(createUser, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
