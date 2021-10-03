using System;
using System.IO;
#if NET45
using System.Drawing;
using System.Drawing.Imaging;
#endif
using Stef.Validation;

namespace QRCodeEncoderLibrary
{
    public class QRCodeEncoder : QREncoder
    {
        private static readonly byte[] PngFileSignature = new byte[] { 137, (byte)'P', (byte)'N', (byte)'G', (byte)'\r', (byte)'\n', 26, (byte)'\n' };

        private static readonly byte[] PngIendChunk = new byte[] { 0, 0, 0, 0, (byte)'I', (byte)'E', (byte)'N', (byte)'D', 0xae, 0x42, 0x60, 0x82 };

        /// <summary>
        /// Save QRCode image to PNG file
        /// </summary>
        /// <param name="filename">PNG file name</param>
        public void SaveQRCodeToPngFile(string filename)
        {
            Guard.NotNullOrEmpty(filename, nameof(filename));
            Guard.Condition(filename, f => filename.EndsWith(".png"), nameof(filename));

            if (QRCodeMatrix == null)
            {
                throw new InvalidOperationException("QRCode must be encoded first");
            }

            // file name to stream
            using (Stream OutputStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // save file
                SaveQRCodeToPngFile(OutputStream);
            }
        }

        /// <summary>
        /// Save QRCode image to PNG stream
        /// </summary>
        /// <param name="outputStream">PNG output stream</param>
        public void SaveQRCodeToPngFile(Stream outputStream)
        {
            Guard.NotNull(outputStream, nameof(outputStream));

            if (QRCodeMatrix == null)
            {
                throw new InvalidOperationException("QRCode must be encoded first");
            }

            // header
            byte[] header = BuildPngHeader();

            // barcode data
            byte[] inputBuf = QRCodeMatrixToPng();

            // compress barcode data
            byte[] outputBuf = PngImageData(inputBuf);

            // stream to binary writer
            var binaryWriter = new BinaryWriter(outputStream);

            // write signature
            binaryWriter.Write(PngFileSignature, 0, PngFileSignature.Length);

            // write header
            binaryWriter.Write(header, 0, header.Length);

            // write image data
            binaryWriter.Write(outputBuf, 0, outputBuf.Length);

            // write end of file
            binaryWriter.Write(PngIendChunk, 0, PngIendChunk.Length);

            // flush all buffers
            binaryWriter.Flush();
        }

#if NET45
        /// <summary>
        /// Save barcode Bitmap to file
        /// </summary>
        /// <param name="filename">File name</param>
        /// <param name="format">Image file format (i.e. PNG, BMP, JPEG)</param>
        public void SaveQRCodeToFile(string filename, ImageFormat format)
        {
            Guard.NotNullOrEmpty(filename, nameof(filename));

            // create Bitmap image of barcode
            var barcodeBitmap = CreateQRCodeBitmap();

            // save image to file
            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                barcodeBitmap.Save(fileStream, format);
            }
        }

        /// <summary>
        /// Save barcode Bitmap to stream
        /// </summary>
        /// <param name="outputStream">Output stream</param>
        /// <param name="Format">Image file format (i.e. PNG, BMP, JPEG)</param>
        public void SaveQRCodeToFile(Stream outputStream, ImageFormat Format)
        {
            Guard.NotNull(outputStream, nameof(outputStream));

            // create Bitmap image of barcode
            var barcodeBitmap = CreateQRCodeBitmap();

            // save image
            barcodeBitmap.Save(outputStream, Format);

            // flush stream
            outputStream.Flush();
        }

        /// <summary>
        /// Create Bitmap image of the Pdf417 barcode
        /// </summary>
        /// <returns>Barcode Bitmap</returns>
        public Bitmap CreateQRCodeBitmap()
        {
            return CreateQRCodeBitmap(Brushes.White, Brushes.Black);
        }

        /// <summary>
        /// Create Pdf417 barcode Bitmap image from boolean black and white matrix
        /// </summary>
        /// <param name="WhiteBrush">Background color (White brush)</param>
        /// <param name="BlackBrush">Bar color (Black brush)</param>
        /// <returns>Pdf417 barcode image</returns>
        public Bitmap CreateQRCodeBitmap(Brush WhiteBrush, Brush BlackBrush)
        {
            if (QRCodeMatrix == null)
            {
                throw new ApplicationException("QRCode must be encoded first");
            }

            // create picture object and make it white
            int imageDimension = QRCodeImageDimension;
            var image = new Bitmap(imageDimension, imageDimension);
            Graphics graphics = Graphics.FromImage(image);
            graphics.FillRectangle(WhiteBrush, 0, 0, imageDimension, imageDimension);

            // x and y image pointers
            int xOffset = QuietZone;
            int yOffset = QuietZone;

            // convert result matrix to output matrix
            for (int row = 0; row < QRCodeDimension; row++)
            {
                for (int Col = 0; Col < QRCodeDimension; Col++)
                {
                    // bar is black
                    if (QRCodeMatrix[row, Col])
                    {
                        graphics.FillRectangle(BlackBrush, xOffset, yOffset, ModuleSize, ModuleSize);
                    }
                    xOffset += ModuleSize;
                }
                xOffset = QuietZone;
                yOffset += ModuleSize;
            }

            return image;
        }
#endif
        internal byte[] BuildPngHeader()
        {
            // header
            byte[] header = new byte[25];

            // header length
            header[0] = 0;
            header[1] = 0;
            header[2] = 0;
            header[3] = 13;

            // header label
            header[4] = (byte)'I';
            header[5] = (byte)'H';
            header[6] = (byte)'D';
            header[7] = (byte)'R';

            // image width
            int ImageDimension = QRCodeImageDimension;
            header[8] = (byte)(ImageDimension >> 24);
            header[9] = (byte)(ImageDimension >> 16);
            header[10] = (byte)(ImageDimension >> 8);
            header[11] = (byte)ImageDimension;

            // image height
            header[12] = (byte)(ImageDimension >> 24);
            header[13] = (byte)(ImageDimension >> 16);
            header[14] = (byte)(ImageDimension >> 8);
            header[15] = (byte)ImageDimension;

            // bit depth (1)
            header[16] = 1;

            // color type (grey)
            header[17] = 0;

            // Compression (deflate)
            header[18] = 0;

            // filtering (up)
            header[19] = 0; // 2;

            // interlace (none)
            header[20] = 0;

            // crc
            uint crc = CRC32.Checksum(header, 4, 17);
            header[21] = (byte)(crc >> 24);
            header[22] = (byte)(crc >> 16);
            header[23] = (byte)(crc >> 8);
            header[24] = (byte)crc;

            return header;
        }

        internal static byte[] PngImageData(byte[] inputBuf)
        {
            // output buffer is:
            // Png IDAT length 4 bytes
            // Png chunk type IDAT 4 bytes
            // Png chunk data made of:
            //		header 2 bytes
            //		compressed data DataLen bytes
            //		adler32 input buffer checksum 4 bytes
            // Png CRC 4 bytes
            // Total output buffer length is 18 + DataLen

            // compress image
            byte[] outputBuf = ZLibCompression.Compress(inputBuf);

            // png chunk data length
            int pngDataLen = outputBuf.Length - 12;
            outputBuf[0] = (byte)(pngDataLen >> 24);
            outputBuf[1] = (byte)(pngDataLen >> 16);
            outputBuf[2] = (byte)(pngDataLen >> 8);
            outputBuf[3] = (byte)pngDataLen;

            // add IDAT
            outputBuf[4] = (byte)'I';
            outputBuf[5] = (byte)'D';
            outputBuf[6] = (byte)'A';
            outputBuf[7] = (byte)'T';

            // adler32 checksum
            uint readAdler32 = Adler32.Checksum(inputBuf, 0, inputBuf.Length);

            // ZLib checksum is Adler32 write it big endian order, high byte first
            int AdlerPtr = outputBuf.Length - 8;
            outputBuf[AdlerPtr++] = (byte)(readAdler32 >> 24);
            outputBuf[AdlerPtr++] = (byte)(readAdler32 >> 16);
            outputBuf[AdlerPtr++] = (byte)(readAdler32 >> 8);
            outputBuf[AdlerPtr] = (byte)readAdler32;

            // crc
            uint crc = CRC32.Checksum(outputBuf, 4, outputBuf.Length - 8);
            int crcPtr = outputBuf.Length - 4;
            outputBuf[crcPtr++] = (byte)(crc >> 24);
            outputBuf[crcPtr++] = (byte)(crc >> 16);
            outputBuf[crcPtr++] = (byte)(crc >> 8);
            outputBuf[crcPtr++] = (byte)crc;

            // successful exit
            return outputBuf;
        }

        // convert barcode matrix to PNG image format
        internal byte[] QRCodeMatrixToPng()
        {
            // image width and height
            int imageDimension = this.QRCodeImageDimension;

            // width in bytes including filter leading byte
            int pngWidth = (imageDimension + 7) / 8 + 1;

            // PNG image array
            // array is all zeros in other words it is black image
            int pngLength = pngWidth * imageDimension;
            byte[] pngImage = new byte[pngLength];

            // first row is a quiet zone and it is all white (filter is 0 none)
            int pngPtr;
            for (pngPtr = 1; pngPtr < pngWidth; pngPtr++) pngImage[pngPtr] = 255;

            // additional quiet zone rows are the same as first line (filter is 2 up)
            int pngEnd = QuietZone * pngWidth;
            for (; pngPtr < pngEnd; pngPtr += pngWidth) pngImage[pngPtr] = 2;

            // convert result matrix to output matrix
            for (int matrixRow = 0; matrixRow < QRCodeDimension; matrixRow++)
            {
                // make next row all white (filter is 0 none)
                pngEnd = pngPtr + pngWidth;
                for (int PngCol = pngPtr + 1; PngCol < pngEnd; PngCol++)
                {
                    pngImage[PngCol] = 255;
                }

                // add black to next row
                for (int MatrixCol = 0; MatrixCol < QRCodeDimension; MatrixCol++)
                {
                    // bar is white
                    if (!QRCodeMatrix[matrixRow, MatrixCol])
                    {
                        continue;
                    }

                    int PixelCol = ModuleSize * MatrixCol + QuietZone;
                    int PixelEnd = PixelCol + ModuleSize;
                    for (; PixelCol < PixelEnd; PixelCol++)
                    {
                        pngImage[pngPtr + 1 + PixelCol / 8] &= (byte)~(1 << (7 - (PixelCol & 7)));
                    }
                }

                // additional rows are the same as the one above (filter is 2 up)
                pngEnd = pngPtr + ModuleSize * pngWidth;
                for (pngPtr += pngWidth; pngPtr < pngEnd; pngPtr += pngWidth)
                {
                    pngImage[pngPtr] = 2;
                }
            }

            // bottom quiet zone and it is all white (filter is 0 none)
            pngEnd = pngPtr + pngWidth;
            for (pngPtr++; pngPtr < pngEnd; pngPtr++)
            {
                pngImage[pngPtr] = 255;
            }

            // additional quiet zone rows are the same as first line (filter is 2 up)
            for (; pngPtr < pngLength; pngPtr += pngWidth)
            {
                pngImage[pngPtr] = 2;
            }

            return pngImage;
        }
    }
}