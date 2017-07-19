using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameServer.network;

namespace GameServer.Buffers
{
    class ManuFactureMaterial
    {
        public void Manu(User user)
        {
            PacketManager Write = new PacketManager();
            Write.OP(806);
            Write.Hex("00 00 00 06 04 46 00 12 68 8A 00 00 00 02 00 00 30 CA 00 00 00 3C FF FF FF FF 00 FF 00 00 00 00 22 B0 00 00 00 78 FF FF FF FF 00 FF 00 00 05 46 00 0A A2 94 00 00 00 02 00 00 30 CA 00 00 00 1B FF FF FF FF 00 FF 00 00 00 00 22 B0 00 00 00 36 FF FF FF FF 00 FF 00 00 05 47 00 0A A2 9E 00 00 00 02 00 00 30 CA 00 00 00 21 FF FF FF FF 00 FF 00 00 00 00 22 B0 00 00 00 42 FF FF FF FF 00 FF 00 00 05 48 00 0A A2 A8 00 00 00 02 00 00 30 CA 00 00 00 27 FF FF FF FF 00 FF 00 00 00 00 22 B0 00 00 00 4E FF FF FF FF 00 FF 00 00 05 49 00 0A A2 B2 00 00 00 02 00 00 30 CA 00 00 00 2D FF FF FF FF 00 FF 00 00 00 00 22 B0 00 00 00 5A FF FF FF FF 00 FF 00 00 05 4A 00 0A A2 BC 00 00 00 02 00 00 30 CA 00 00 00 33 FF FF FF FF 00 FF 00 00 00 00 22 B0 00 00 00 66 FF FF FF FF 00 FF 00 00");
            user.Send(Write.ack);
        }
    }
}
