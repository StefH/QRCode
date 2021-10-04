using System;
using System.Drawing;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QRCodeDecoderLibrary;
using QRCodeEncoderLibrary;
using Stef.Validation;
#if NET45_OR_GREATER
using System.Drawing.Imaging;
#endif

namespace QRCodeFixerLibrary
{
    public class QRCodeFixer
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public QRCodeFixer(ILogger<QRCodeFixer> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public string FixAndSaveAsPng(string sourceFilename, string destinationFilename)
        {
            Guard.NotNullOrEmpty(sourceFilename, nameof(sourceFilename));
            Guard.Condition(sourceFilename, f => File.Exists(f), nameof(sourceFilename));
            Guard.NotNullOrEmpty(destinationFilename, nameof(destinationFilename));
            Guard.Condition(destinationFilename, f => destinationFilename.EndsWith(".png", StringComparison.OrdinalIgnoreCase), nameof(destinationFilename));

            _logger.LogDebug("Trying to fix the QR Code in '{sourceFilename}' and save it as '{destinationFilename}'", sourceFilename, destinationFilename);

            var (encoder, data) = DecodeAndEncodeInternal(sourceFilename);
            encoder.SaveQRCodeToPngFile(destinationFilename);

            return data;
        }

#if NET45_OR_GREATER
        public string FixAndSave(string sourceFilename, string destinationFilename, ImageFormat imageFormat)
        {
            Guard.NotNullOrEmpty(sourceFilename, nameof(sourceFilename));
            Guard.Condition(sourceFilename, f => File.Exists(f), nameof(sourceFilename));
            Guard.NotNullOrEmpty(destinationFilename, nameof(destinationFilename));

            _logger.LogDebug("Trying to fix the QR Code in '{sourceFilename}' and save it as '{destinationFilename}'", sourceFilename, destinationFilename);

            var (encoder, data) = DecodeAndEncodeInternal(sourceFilename);
            encoder.SaveQRCodeToFile(destinationFilename, imageFormat);

            return data;
        }

#endif
        private (QRCodeEncoder encoder, string data) DecodeAndEncodeInternal(string sourceFilename)
        {
            var sourceBitmap = new Bitmap(sourceFilename);

            var decoder = _serviceProvider.GetRequiredService<QRDecoder>();
            byte[][] data = decoder.ImageDecoder(sourceBitmap);
            if (data == null)
            {
                throw new ApplicationException();
            }

            var encoder = _serviceProvider.GetRequiredService<QRCodeEncoder>();
            encoder.ErrorCorrection = decoder.ErrorCorrection; // Use same error correction as source

            encoder.Encode(data);

            return (encoder, QRDecoder.ByteArrayToString(data[0]));
        }
    }
}