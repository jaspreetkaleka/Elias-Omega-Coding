using System;

namespace EliasOmegaCoding
{
    /// <summary>
    /// Decode stream of values encoded with Elias gamma coding
    /// </summary>
    class BufferDecoder
    {
        readonly byte[] _buffer;
        readonly int _len;
        private int _pos;

        public BufferDecoder(byte[] buffer, int pos, int length)
        {
            if (pos < 0 || pos > buffer.Length) throw new ArgumentException("pos");
            if (length < 0 || pos + length > buffer.Length) throw new ArgumentException("length");

            _buffer = buffer;
            _len = length * 8;
            _pos = pos * 8;
        }

        public BufferDecoder(byte[] buffer) : this(buffer, 0, buffer.Length) { }

        bool GetBit()
        {
            if (_pos >= _len) throw new Exception("Buffer overrun");
            var r = (_buffer[_pos / 8] & (1 << (_pos % 8))) != 0;
            ++_pos;
            return r;
        }

        /// <summary>
        /// Read the next value from the stream
        /// </summary>
        /// <returns>Decoded value</returns>
        public long ReadValue()
        {
            var num = 1L;
            while (GetBit())
            {
                var len = num;
                num = 1;
                for (var i = 0; i < len; ++i)
                {
                    num <<= 1;
                    if (GetBit()) num |= 1;
                }
            }
            return num;
        }
    }
}
