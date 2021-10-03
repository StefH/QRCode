using System;
using System.Drawing;
#if NET45_OR_GREATER
using System.Drawing.Imaging;
#endif
using System.IO;
using QRCodeDecoderLibrary;
using QRCodeEncoderLibrary;
using Stef.Validation;


namespace QRCodeFixer
{
    public static class QRFixer
    {
        public static void FixAndSaveAsPng(string sourceFilename, string destinationFilename, TextWriter textWriter = null)
        {
            Guard.NotNullOrEmpty(sourceFilename, nameof(sourceFilename));
            Guard.NotNullOrEmpty(destinationFilename, nameof(destinationFilename));
            Guard.Condition(destinationFilename, f => destinationFilename.EndsWith(".png", StringComparison.OrdinalIgnoreCase), nameof(destinationFilename));

            var encoder = FixInternal(sourceFilename, textWriter);
            encoder.SaveQRCodeToPngFile(destinationFilename);
        }

#if NET45_OR_GREATER
        public static void FixAndSave(string sourceFilename, string destinationFilename, ImageFormat imageFormat, TextWriter textWriter = null)
        {
            Guard.NotNullOrEmpty(sourceFilename, nameof(sourceFilename));
            Guard.NotNullOrEmpty(destinationFilename, nameof(destinationFilename));

            var encoder = FixInternal(sourceFilename, textWriter);
            encoder.SaveQRCodeToFile(destinationFilename, imageFormat);
        }

#endif
        private static QRCodeEncoder FixInternal(string sourceFilename, TextWriter textWriter)
        {
#if DEBUG
            textWriter ??= Console.Out;
            QRCodeTrace.Open(textWriter);
#endif
            var decoder = new QRDecoder();

            var sourceBitmap = new Bitmap(sourceFilename);

            byte[][] data = decoder.ImageDecoder(sourceBitmap);
            if (data == null)
            {
                throw new ApplicationException();
            }

            string text = QRDecoder.ByteArrayToStr(data[0]);

            var encoder = new QRCodeEncoder
            {
                ErrorCorrection = decoder.ErrorCorrection // Use same error correction as source
            };

            encoder.Encode(data);

            return encoder;
        }
    }
}