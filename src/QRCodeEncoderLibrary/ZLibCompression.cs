using System.IO;
using System.IO.Compression;

namespace QRCodeEncoderLibrary
{
    internal static class ZLibCompression
    {
        internal static byte[] Compress(byte[] inputBuffer)
        {
            int inputLength = inputBuffer.Length;

            // create output memory stream to receive the compressed buffer
            var outputStream = new MemoryStream();

            // deflate compression object
            var deflateStream = new DeflateStream(outputStream, CompressionMode.Compress, true);

            // load input buffer into the compression class
            deflateStream.Write(inputBuffer, 0, inputLength);

            // compress, flush and close
#if NETSTANDARD1_3
            deflateStream.Flush();
			deflateStream.Dispose();
#else
            deflateStream.Close();
#endif

            // compressed file length
            int outputLength = (int)outputStream.Length;

            // create empty output buffer
            byte[] outputBuffer = new byte[outputLength + 18];

            // Header is made out of 16 bits [iiiicccclldxxxxx]
            // iiii is compression information. It is WindowBit - 8 in this case 7. iiii = 0111
            // cccc is compression method. Deflate (8 dec) or Store (0 dec)
            // The first byte is 0x78 for deflate and 0x70 for store
            // ll is compression level 2
            // d is preset dictionary. The preset dictionary is not supported by this program. d is always 0
            // xxx is 5 bit check sum (31 - header % 31)
            // write two bytes in most significant byte first
            outputBuffer[8] = 0x78;
            outputBuffer[9] = 0x9c;

            // copy the compressed result
            outputStream.Seek(0, SeekOrigin.Begin);
            outputStream.Read(outputBuffer, 10, outputLength);

#if NETSTANDARD1_3
			outputStream.Flush();
            outputStream.Dispose();
#else
            outputStream.Close();
#endif

            return outputBuffer;
        }
    }
}