using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KCP;


using Assets.scripts.Utils;
namespace NetWork
{
    public class ProtobufDecoder
    {
        public static void Decode(ByteBuf In, ByteBuf Out)
        {
            In.MarkReaderIndex();
            int preIndex = In.ReaderIndex();
            int length = readint32(In);
            if (length == -1)
            {
                return;
            }

            if (preIndex != In.ReaderIndex())
            {
                if (length < 0)
                {

                }
                else
                {
                    if (In.ReadableBytes() < length)
                    {
                        In.ResetReaderIndex();
                    }
                    else
                    {
                        Out.WriteBytes(In, length);
                    }
                }
            }
        }
        private static int readint32(ByteBuf buffer)
        {
            if (buffer.ReadableBytes() >= 4)
            {
                int var1 = buffer.ReadByte();
                int var2 = buffer.ReadByte();
                int var3 = buffer.ReadByte();
                int var4 = buffer.ReadByte();

                int addr = var1 & 0xFF;
                addr |= ((var2 << 8) & 0xFF00);
                addr |= ((var3 << 16) & 0xFF0000);
                addr |= (int)((var4 << 24) & 0xFF000000);
                return addr;
            }
            else { return -1; }
        }
    }
}
