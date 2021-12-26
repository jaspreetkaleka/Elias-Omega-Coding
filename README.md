# Elias Omega Coding
Elias omega coding is a universal code which encodes positive integers of any size into a stream of bits.

To code a number N:

1. Place a "0" at the end of the code.
2. If N = 1, stop; encoding is complete.
3. Prepend the binary representation of N to the beginning of the code. This will be at least two bits, the first bit of which is a 1.
4. Let N equal the number of bits just prepended, minus one.
5. Return to step 2 to prepend the encoding of the new N.

To decode an Elias omega-coded integer:

1. Start with a variable N, set to a value of 1.
2. If the next bit is a "0", stop. The decoded number is N.
3. If the next bit is a "1", then read it plus N more bits, and use that binary number as the new value of N. Go back to step 2.
