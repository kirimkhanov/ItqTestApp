using ITQTestApp.API.Swagger.Attributes;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ITQTestApp.API.Swagger.Filters
{
    public sealed class RawReplaceReferenceItemsRequestBodyOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasMarker = context.MethodInfo
                .GetCustomAttributes(typeof(SwaggerRawReferenceItemsBodyAttribute), inherit: true)
                .Any();

            if (!hasMarker) return;

            if (operation.RequestBody?.Content == null) return;

            if (!operation.RequestBody.Content.TryGetValue("application/json", out var mediaType))
                return;

            mediaType.Schema = new OpenApiSchema
            {
                Type = "array",
                Items = new OpenApiSchema
                {
                    Type = "object",
                    AdditionalPropertiesAllowed = true,
                    AdditionalProperties = new OpenApiSchema { Type = "string" },
                    MinProperties = 1,
                    MaxProperties = 1
                }
            };

            mediaType.Example = new OpenApiArray
        {
            new OpenApiObject { ["1"] = new OpenApiString("value1") },
            new OpenApiObject { ["5"] = new OpenApiString("value2") },
            new OpenApiObject { ["10"] = new OpenApiString("value32") }
        };
        }
    }
}
