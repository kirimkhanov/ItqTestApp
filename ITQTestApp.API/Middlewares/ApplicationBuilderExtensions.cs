namespace ITQTestApp.API.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
