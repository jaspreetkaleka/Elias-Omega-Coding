using System;
using System.Diagnostics;

namespace EliasOmegaCoding
{
    class Program
    {
        static void Main()
        {
            var t = new DateTime(2011, 12, 31, 23, 59, 59, 999);

            // Encode values
            var e = new BufferEncoder();
            e.Append(7000);
            e.Append(t.Ticks);
            e.Append(1);
            var buffer = e.GetByteArray();

            Console.WriteLine("Buffer size {0}", buffer.Length);

            // Decode values and check
            var d = new BufferDecoder(buffer);
            var v = d.ReadValue(); Debug.Assert(v == 7000);
            v = d.ReadValue(); Debug.Assert(v == t.Ticks);
            v = d.ReadValue(); Debug.Assert(v == 1);

            Console.WriteLine("Success");
            Console.ReadLine();
        }
    }
}
