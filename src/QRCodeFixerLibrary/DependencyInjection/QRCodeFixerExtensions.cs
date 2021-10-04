using QRCodeFixerLibrary;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class QRCodeFixerExtensions
    {
        public static IServiceCollection AddQRCodeFixer(this IServiceCollection services)
        {
            services
                .AddQRCodeDecoder()
                .AddQRCodeEncoder()
                .AddTransient<QRCodeFixer>();

            return services;
        }
    }
}