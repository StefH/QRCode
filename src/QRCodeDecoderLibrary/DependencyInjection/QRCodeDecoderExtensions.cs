using QRCodeDecoderLibrary;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class QRCodeDecoderExtensions
    {
        public static IServiceCollection AddQRCodeDecoder(this IServiceCollection services)
        {
            return services.AddTransient<QRDecoder>();
        }
    }
}