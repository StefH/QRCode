using System;
using System.Drawing;
#if NET45_OR_GREATER
using System.Drawing.Imaging;
#endif
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using QRCodeDecoderLibrary;
using QRCodeEncoderLibrary;
using Stef.Validation;

namespace QRCodeFixerLibrary
{
    public class QRCodeFixer
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public QRCodeFixer(ILogger<QRCodeFixer> logger, IServiceProvider serviceProvider)
        {
            _logger = Guard.NotNull(logger, nameof(logger));
            _serviceProvider = Guard.NotNull(serviceProvider, nameof(serviceProvider));
        }

        public string FixAndSaveAsPng(string sourceFilename, string destinationFilename)
        {
            Guard.NotNullOrEmpty(sourceFilename, nameof(sourceFilename));
            Guard.NotNullOrEmpty(destinationFilename, nameof(destinationFilename));
            Guard.Condition(destinationFilename, f => destinationFilename.EndsWith(".png", StringComparison.OrdinalIgnoreCase), nameof(destinationFilename));

            var (encoder, data) = FixInternal(sourceFilename);
            encoder.SaveQRCodeToPngFile(destinationFilename);

            return data;
        }

#if NET45_OR_GREATER
        public string FixAndSave(string sourceFilename, string destinationFilename, ImageFormat imageFormat)
        {
            Guard.NotNullOrEmpty(sourceFilename, nameof(sourceFilename));
            Guard.NotNullOrEmpty(destinationFilename, nameof(destinationFilename));

            var (encoder, data) = FixInternal(sourceFilename);
            encoder.SaveQRCodeToFile(destinationFilename, imageFormat);

            return data;
        }

#endif
        private (QRCodeEncoder encoder, string data) FixInternal(string sourceFilename)
        {
            var decoder = (QRDecoder)_serviceProvider.GetService(typeof(QRDecoder));

            var sourceBitmap = new Bitmap(sourceFilename);

            byte[][] data = decoder.ImageDecoder(sourceBitmap);
            if (data == null)
            {
                throw new ApplicationException();
            }


            var encoder = new QRCodeEncoder
            {
                ErrorCorrection = decoder.ErrorCorrection // Use same error correction as source
            };

            encoder.Encode(data);

            return (encoder, QRDecoder.ByteArrayToString(data[0]));
        }
    }
}