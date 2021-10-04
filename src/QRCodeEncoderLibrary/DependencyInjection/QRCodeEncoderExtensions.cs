using QRCodeEncoderLibrary;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class QRCodeEncoderExtensions
    {
        public static IServiceCollection AddQRCodeEncoder(this IServiceCollection services)
        {
            return services.AddTransient<QRCodeEncoder>();
        }
    }
}