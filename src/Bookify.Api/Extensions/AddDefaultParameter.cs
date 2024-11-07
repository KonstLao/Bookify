using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bookify.Api.Extensions
{
    public class AddDefaultParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Добавляем значения по умолчанию для всех параметров
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "name",
                In = ParameterLocation.Query, 
                Schema = new OpenApiSchema { Type = "string" },
                Required = false,
                Example = new OpenApiString("Стильная квартира в центре города")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "description",
                In = ParameterLocation.Query, 
                Schema = new OpenApiSchema { Type = "string" },
                Required = false,
                Example = new OpenApiString("Современная квартира с прекрасным видом на город")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "country",
                In = ParameterLocation.Query, 
                Schema = new OpenApiSchema { Type = "string" },
                Required = false,
                Example = new OpenApiString("Россия")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "state",
                In = ParameterLocation.Query, 
                Schema = new OpenApiSchema { Type = "string" },
                Required = false,
                Example = new OpenApiString("Москва")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "zipCode",
                In = ParameterLocation.Query, 
                Schema = new OpenApiSchema { Type = "string" },
                Required = false,
                Example = new OpenApiString("123456")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "city",
                In = ParameterLocation.Query, 
                Schema = new OpenApiSchema { Type = "string" },
                Required = false,
                Example = new OpenApiString("Москва")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "street",
                In = ParameterLocation.Query, 
                Schema = new OpenApiSchema { Type = "string" },
                Required = false,
                Example = new OpenApiString("Ленинский проспект")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "priceAmount",
                In = ParameterLocation.Query, 
                Schema = new OpenApiSchema { Type = "number", Format = "double" },
                Required = false,
                Example = new OpenApiString("100")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "cleaningFeeAmount",
                In = ParameterLocation.Query, 
                Schema = new OpenApiSchema { Type = "number", Format = "double" },
                Required = false,
                Example = new OpenApiString("10")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "code",
                In = ParameterLocation.Query, 
                Schema = new OpenApiSchema { Type = "string" },
                Required = false,
                Example = new OpenApiString("USD")
            });

            //// Добавляем значение по умолчанию для amenities (в данном случае - пустой список)
            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "amenities",
            //    In = ParameterLocation.Query,
            //    Schema = new OpenApiSchema
            //    {
            //        Type = "array",
            //        Items = new OpenApiSchema { Type = "integer" }
            //    },
            //    Required = false,
            //    Example = new OpenApiArray(new List<OpenApiObject>() { }) 
            //});
        }
    }
    }
