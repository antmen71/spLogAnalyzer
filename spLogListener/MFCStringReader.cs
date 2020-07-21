using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace spLogListener
{
    public class MFCStringReader : System.IO.BinaryReader
    {
        public MFCStringReader(Stream s)
            : base(s)
        {
        }

        public MFCStringReader(Stream s, Encoding e)
            : base(s, e)
        {
        }

        public string ReadCString()
        {
            string str = "";
            int nConvert = 1; // if we get ANSI, convert

            UInt32 nNewLen = ReadStringLength();
            if (nNewLen == unchecked((UInt32)(-1)))
            {
                nConvert = 1 - nConvert;
                nNewLen = ReadStringLength();
                if (nNewLen == unchecked((UInt32)(-1)))
                    return str;
            }

            // set length of string to new length
            UInt32 nByteLen = nNewLen;
            nByteLen += (UInt32)(nByteLen * (1 - nConvert)); // bytes to read

            // read in the characters
            if (nNewLen != 0)
            {
                // read new data
                byte[] byteBuf = ReadBytes((int)nByteLen);

                // convert the data if as necessary
                StringBuilder sb = new StringBuilder();
                if (nConvert != 0)
                {
                    for (int i = 0; i < nNewLen; i++)
                        sb.Append((char)byteBuf[i]);
                }
                else
                {
                    for (int i = 0; i < nNewLen; i++)
                        sb.Append((char)(byteBuf[i * 2] + byteBuf[i * 2 + 1] * 256));
                }

                str = sb.ToString();
            }

            return str;
        }

        private UInt32 ReadStringLength()
        {
            UInt32 nNewLen;

            // attempt BYTE length first
            byte bLen = ReadByte();

            if (bLen < 0xff)
                return bLen;

            // attempt WORD length
            UInt16 wLen = ReadUInt16();
            if (wLen == 0xfffe)
            {
                // UNICODE string prefix (length will follow)
                return unchecked((UInt32)(-1));
            }
            else if (wLen == 0xffff)
            {
                // read DWORD of length
                nNewLen = ReadUInt32();
                return nNewLen;
            }
            else
                return wLen;
        }
    }

}
