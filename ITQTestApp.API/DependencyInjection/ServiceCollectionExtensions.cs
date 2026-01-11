using ITQTestApp.API.Contracts.Responses;
using ITQTestApp.API.Serialization;
using ITQTestApp.API.Swagger.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ITQTestApp.API.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new ReferenceItemArrayJsonConverter());
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid value." : e.ErrorMessage)
                        .ToList();

                    var message = errors.Count == 0
                        ? "Request validation failed."
                        : string.Join(" ", errors);

                    var error = new ErrorResponse
                    {
                        Code = "validation_error",
                        Message = message
                    };

                    return new BadRequestObjectResult(error);
                };
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<RawReplaceReferenceItemsRequestBodyOperationFilter>();
            });

            return services;
        }
    }
}


