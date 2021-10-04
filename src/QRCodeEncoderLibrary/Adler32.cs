namespace QRCodeEncoderLibrary
{
    internal static class Adler32
    {
        const uint Adler32Base = 65521;

        /////////////////////////////////////////////////////////////////////
        // Accumulate Adler Checksum
        /////////////////////////////////////////////////////////////////////
        internal static uint Checksum(byte[] buffer, int position, int length)
        {
            // split current Adler chksum into two 
            uint low = 1; // AdlerValue & 0xFFFF;
            uint high = 0; // AdlerValue >> 16;

            while (length > 0)
            {
                // We can defer the modulo operation:
                // Under worst case the starting value of the two halves is 65520 = (AdlerBase - 1)
                // each new byte is maximum 255
                // The low half grows AdlerLow(n) = AdlerBase - 1 + n * 255
                // The high half grows AdlerHigh(n) = (n + 1)*(AdlerBase - 1) + n * (n + 1) * 255 / 2
                // The maximum n before overflow of 32 bit unsigned integer is 5552
                // it is the solution of the following quadratic equation
                // 255 * n * n + (2 * (AdlerBase - 1) + 255) * n + 2 * (AdlerBase - 1 - UInt32.MaxValue) = 0
                int n = length < 5552 ? length : 5552;
                length -= n;
                while (--n >= 0)
                {
                    low += (uint)buffer[position++];
                    high += low;
                }
                low %= Adler32Base;
                high %= Adler32Base;
            }

            return (high << 16) | low;
        }
    }
}