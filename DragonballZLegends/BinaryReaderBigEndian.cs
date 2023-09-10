using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonballZLegends
{
    internal class BinaryReaderBigEndian : BinaryReader
    {
        //
        // Summary:
        //     Initializes a new instance of the System.IO.BinaryReader class based on the specified
        //     stream and using UTF-8 encoding.
        //
        // Parameters:
        //   input:
        //     The input stream.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The stream does not support reading, is null, or is already closed.
        public BinaryReaderBigEndian(Stream input) : base(input) { }
        //
        // Summary:
        //     Initializes a new instance of the System.IO.BinaryReader class based on the specified
        //     stream and character encoding.
        //
        // Parameters:
        //   input:
        //     The input stream.
        //
        //   encoding:
        //     The character encoding to use.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The stream does not support reading, is null, or is already closed.
        //
        //   T:System.ArgumentNullException:
        //     encoding is null.
        public BinaryReaderBigEndian(Stream input, Encoding encoding) : base(input, encoding) { }
        //
        // Summary:
        //     Initializes a new instance of the System.IO.BinaryReader class based on the specified
        //     stream and character encoding, and optionally leaves the stream open.
        //
        // Parameters:
        //   input:
        //     The input stream.
        //
        //   encoding:
        //     The character encoding to use.
        //
        //   leaveOpen:
        //     true to leave the stream open after the System.IO.BinaryReader object is disposed;
        //     otherwise, false.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The stream does not support reading, is null, or is already closed.
        //
        //   T:System.ArgumentNullException:
        //     encoding or input is null.
        public BinaryReaderBigEndian(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen) { }
        //
        // Summary:
        //     Reads a 2-byte signed integer from the current stream and advances the current
        //     position of the stream by two bytes.
        //
        // Returns:
        //     A 2-byte signed integer read from the current stream.
        //
        // Exceptions:
        //   T:System.IO.EndOfStreamException:
        //     The end of the stream is reached.
        //
        //   T:System.ObjectDisposedException:
        //     The stream is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred.
        public override short ReadInt16() => base.ReadInt16().FlipEndian();
        //
        // Summary:
        //     Reads a 4-byte signed integer from the current stream and advances the current
        //     position of the stream by four bytes.
        //
        // Returns:
        //     A 4-byte signed integer read from the current stream.
        //
        // Exceptions:
        //   T:System.IO.EndOfStreamException:
        //     The end of the stream is reached.
        //
        //   T:System.ObjectDisposedException:
        //     The stream is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred.
        public override int ReadInt32() => base.ReadInt32().FlipEndian();
        //
        // Summary:
        //     Reads an 8-byte signed integer from the current stream and advances the current
        //     position of the stream by eight bytes.
        //
        // Returns:
        //     An 8-byte signed integer read from the current stream.
        //
        // Exceptions:
        //   T:System.IO.EndOfStreamException:
        //     The end of the stream is reached.
        //
        //   T:System.ObjectDisposedException:
        //     The stream is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred.
        public override long ReadInt64() => base.ReadInt64().FlipEndian();

        //
        // Summary:
        //     Reads a 2-byte unsigned integer from the current stream using little-endian encoding
        //     and advances the position of the stream by two bytes.
        //
        // Returns:
        //     A 2-byte unsigned integer read from this stream.
        //
        // Exceptions:
        //   T:System.IO.EndOfStreamException:
        //     The end of the stream is reached.
        //
        //   T:System.ObjectDisposedException:
        //     The stream is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred.
        public override ushort ReadUInt16() => base.ReadUInt16().FlipEndian();
        //
        // Summary:
        //     Reads a 4-byte unsigned integer from the current stream and advances the position
        //     of the stream by four bytes.
        //
        // Returns:
        //     A 4-byte unsigned integer read from this stream.
        //
        // Exceptions:
        //   T:System.IO.EndOfStreamException:
        //     The end of the stream is reached.
        //
        //   T:System.ObjectDisposedException:
        //     The stream is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred.
        public override uint ReadUInt32() => base.ReadUInt32().FlipEndian();

        //
        // Summary:
        //     Reads an 8-byte unsigned integer from the current stream and advances the position
        //     of the stream by eight bytes.
        //
        // Returns:
        //     An 8-byte unsigned integer read from this stream.
        //
        // Exceptions:
        //   T:System.IO.EndOfStreamException:
        //     The end of the stream is reached.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred.
        //
        //   T:System.ObjectDisposedException:
        //     The stream is closed.
        public override ulong ReadUInt64() => base.ReadUInt64().FlipEndian();
    }
}
