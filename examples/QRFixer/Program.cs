using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QRCodeDecoderLibrary;
using QRCodeEncoderLibrary;
using QRCodeFixerLibrary;

Console.WindowWidth = 200;

var services = new ServiceCollection();
services.AddTransient<QRDecoder>();
services.AddTransient<QRCodeEncoder>();
services.AddTransient<QRCodeFixer>();

services.AddLogging(configure =>
{
    configure.SetMinimumLevel(LogLevel.Debug);
    configure.AddSimpleConsole(options =>
    {
        options.TimestampFormat = "[yyyy-MM-dd HH:mm:ss.fff] ";
        options.SingleLine = true;
    });
});

var serviceProvider = services.BuildServiceProvider();

var fixer = serviceProvider.GetRequiredService<QRCodeFixer>();
var logger = serviceProvider.GetRequiredService<ILogger<QRCodeFixer>>();

for (int i = 0; i < 4; i++)
{
    var source = $"source-damaged-{i}.png";
    logger.LogWarning("Source filename '{0}'", source);
    fixer.FixAndSaveAsPng(source, $"source-fixed-{i}.png");
}

logger.LogWarning("Source filename '{0}'", "source-damaged-skewing_and_noise.png");
fixer.FixAndSaveAsPng("source-damaged-skewing_and_noise.png", "source-fixed-skewing_and_noise.png");
