using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SevenZip;

namespace System.IO.Compression
{
   public class LZMA
    {
        public void Compress(Stream inStream, Stream outStream)
        {
            Int32 dictionary = 1 << 23;

            Int32 posStateBits = 2;
            Int32 litContextBits = 3; // for normal files
            // UInt32 litContextBits = 0; // for 32-bit data
            Int32 litPosBits = 0;
            // UInt32 litPosBits = 2; // for 32-bit data
            Int32 algorithm = 2;
            Int32 numFastBytes = 128;


            CoderPropID[] propIDs = 
				{
					CoderPropID.DictionarySize,
					CoderPropID.PosStateBits,
					CoderPropID.LitContextBits,
					CoderPropID.LitPosBits,
					CoderPropID.Algorithm,
					CoderPropID.NumFastBytes,
					CoderPropID.MatchFinder,
					CoderPropID.EndMarker
				};
            object[] properties = 
				{
					(Int32)(dictionary),
					(Int32)(posStateBits),
					(Int32)(litContextBits),
					(Int32)(litPosBits),
					(Int32)(algorithm),
					(Int32)(numFastBytes),
					"bt4",
					false
				};

            SevenZip.Compression.LZMA.Encoder encoder = new SevenZip.Compression.LZMA.Encoder();
            encoder.SetCoderProperties(propIDs, properties);
            encoder.WriteCoderProperties(outStream);
            Int64 fileSize = inStream.Length;
            for (int i = 0; i < 8; i++)
                outStream.WriteByte((Byte)(fileSize >> (8 * i)));

            encoder.Code(inStream, outStream, -1, -1, null);
        }

        public void Compress(string inFile, string outFile)
        {
            FileStream inStream = new FileStream(inFile, FileMode.Open, FileAccess.Read);
            FileStream outStream = new FileStream(outFile, FileMode.Create, FileAccess.Write);

            Compress(inStream, outStream);

            inStream.Close();
            outStream.Close();
        }

        public void Decompress(Stream inStream, Stream outStream)
        {
            byte[] properties = new byte[5];
            inStream.Read(properties, 0, 5);
            SevenZip.Compression.LZMA.Decoder decoder = new SevenZip.Compression.LZMA.Decoder();
            decoder.SetDecoderProperties(properties);
            long outSize = 0;
            for (int i = 0; i < 8; i++)
            {
                int v = inStream.ReadByte();
                outSize |= ((long)(byte)v) << (8 * i);
            }
            long compressedSize = inStream.Length - inStream.Position;
            decoder.Code(inStream, outStream, compressedSize, outSize, null);
        }

        public void Decompress(string compressedFile, string outFile)
        {
            FileStream inStream = new FileStream(compressedFile, FileMode.Open, FileAccess.Read);
            FileStream outStream = new FileStream(outFile, FileMode.Create, FileAccess.Write);

            Decompress(inStream, outStream);

            inStream.Close();
            outStream.Close();
        }
    }

    public class LzmaStream : Stream
    {
        private CompressionMode modo;
        private Stream stream;
        private ICoder coder;
        private bool iniciadoCoder;
        private long streamSize;

        public LzmaStream(Stream stream, CompressionMode mode)
        {
            modo = mode;

            this.stream = stream;

            if (modo == CompressionMode.Decompress)
            {
                coder = new SevenZip.Compression.LZMA.Decoder();
            }
            else
            {
                coder = new SevenZip.Compression.LZMA.Encoder();
            }
            iniciadoCoder = false;
        }

 

        public override bool CanRead
        {
            get
            {
                if ((this.stream != null) && (this.modo == CompressionMode.Decompress))
                {
                    return this.stream.CanRead;
                }
                return false;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                if ((this.stream != null) && (this.modo == CompressionMode.Compress))
                {
                    return this.stream.CanWrite;
                }
                return false;
            }
        }

        public override void Flush()
        {
            if (this.stream == null)
            {
                return;
            }

        }

        public override long Length
        {
            get { throw new Exception("Not supported"); }
        }

        public override long Position
        {
            get
            {
                throw new Exception("Not supported");
            }
            set
            {
                throw new Exception("Not supported");
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (CanRead)
            {
                SevenZip.Compression.LZMA.Decoder decoder = (SevenZip.Compression.LZMA.Decoder)coder;
                long compressedSize = 0;
                if (iniciadoCoder == false)
                {
                    byte[] properties = new byte[5];
                    stream.Read(properties, 0, 5);
                    decoder.SetDecoderProperties(properties);
                    streamSize = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        int v = stream.ReadByte();
                        streamSize |= ((long)(byte)v) << (8 * i);
                    }
                    compressedSize = stream.Length - stream.Position;

                    iniciadoCoder = true;
                }
                MemoryStream resultado = new MemoryStream(count);

                decoder.Code(stream, resultado, compressedSize, streamSize, null);

                byte[] res = resultado.ToArray();
                resultado.Close();

                int contadorMemoryStream = 0;
                for (int contador = offset; contador < offset + count && contadorMemoryStream<res.Length; contador++)
                {
                    buffer[contador] = res[contadorMemoryStream];

                    contadorMemoryStream++;
                }

                return res.Length;
            }
            else return 0;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new Exception("Not supported");
        }

        public override void SetLength(long value)
        {
            throw new Exception("Not supported");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (CanWrite)
            {
                SevenZip.Compression.LZMA.Encoder encoder = (SevenZip.Compression.LZMA.Encoder)coder;

                if (iniciadoCoder == false)
                {
                    Int32 dictionary = 1 << 23;

                    Int32 posStateBits = 2;
                    Int32 litContextBits = 3; // for normal files
                    // UInt32 litContextBits = 0; // for 32-bit data
                    Int32 litPosBits = 0;
                    // UInt32 litPosBits = 2; // for 32-bit data
                    Int32 algorithm = 2;
                    Int32 numFastBytes = 128;


                    CoderPropID[] propIDs = 
				{
					CoderPropID.DictionarySize,
					CoderPropID.PosStateBits,
					CoderPropID.LitContextBits,
					CoderPropID.LitPosBits,
					CoderPropID.Algorithm,
					CoderPropID.NumFastBytes,
					CoderPropID.MatchFinder,
					CoderPropID.EndMarker
				};
                    object[] properties = 
				{
					(Int32)(dictionary),
					(Int32)(posStateBits),
					(Int32)(litContextBits),
					(Int32)(litPosBits),
					(Int32)(algorithm),
					(Int32)(numFastBytes),
					"bt4",
					false
				};

                 
                    encoder.SetCoderProperties(propIDs, properties);
                    encoder.WriteCoderProperties(stream);
                    Int64 fileSize = buffer.Length;
                    for (int i = 0; i < 8; i++)
                        stream.WriteByte((Byte)(fileSize >> (8 * i)));

                    iniciadoCoder = true;
                }

                MemoryStream flujoEntrada=new MemoryStream(buffer,offset,count);
                encoder.Code(flujoEntrada, stream, -1, -1, null);
            }
        }
    }
}
