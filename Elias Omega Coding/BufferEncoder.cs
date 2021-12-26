using System;
using System.Collections;
using System.IO;

namespace EliasOmegaCoding
{
    /// <summary>
    /// Encode values base on Elias gamma coding
    /// </summary>
    class BufferEncoder
    {
        readonly MemoryStream _ms;
        byte _c;
        int _p;

        public BufferEncoder()
        {
            _ms = new MemoryStream();
            _c = 0;
            _p = 0;
        }

        void WriteBit(bool bit)
        {
            if (bit) _c |= (byte)(1 << _p);
            _p += 1;
            if (_p < 8) return;
            _ms.WriteByte(_c);
            _c = 0;
            _p = 0;
        }

        /// <summary>
        /// Append a integer to the stream
        /// </summary>
        /// <param name="value">Integer to encode. Must be greater than one.</param>
        public void Append(long value)
        {
            if (value < 1) throw new ArgumentException("value must be greater than 1");
            if (_p < 0) throw new Exception("BufferEncoder is closed");
            var t = new BitArray(8192);
            var l = 0;
            while (value > 1)
            {
                var len = 0;
                for (var temp = value; temp > 0; temp >>= 1)  // calculate 1+floor(log2(num))
                    len++;
                for (var i = 0; i < len; i++)
                    t[l++] = ((value >> i) & 1) != 0;
                value = len - 1;
            }
            for (--l; l >= 0; --l) WriteBit(t[l]);
            WriteBit(false);
        }

        /// <summary>
        /// Get the result buffer. Close the stream.
        /// </summary>
        /// <returns>Encoded buffer</returns>
        public byte[] GetByteArray()
        {
            if (_p != 0) _ms.WriteByte(_c);

            var r = _ms.ToArray();
            _p = -1;
            _ms.Close();

            return r;
        }
    }
}
