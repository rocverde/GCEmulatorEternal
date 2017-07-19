using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameServer.network;

namespace GameServer.Buffers
{
    class UserList
    {
        public void userlist(User user)
        {
            PacketManager Write = new PacketManager();
            Write.OP(19);
            Write.Int(Ultilize.LobbyListaDePlayers.Count);
            for (int a = 0; a < Ultilize.LobbyListaDePlayers.Count; a++)
            {
                User temp = Ultilize.LobbyListaDePlayers[a];
                Write.Int(temp.pInfo.userid);
                Write.UStr(temp.pInfo.usuario);
                Write.Byte(11);
                Write.Byte(0);
            }
            int buffersize = Write.ack.Length - 7;
            byte[] getComprimir = new byte[0];
            Array.Resize(ref getComprimir, Write.ack.Length - 7);
            Array.Copy(Write.ack, 7, getComprimir, 0, Write.ack.Length - 7);

            byte[] temp1 = GCNet.CoreLib.ZLib.CompressData(getComprimir);

            PacketManager Write2 = new PacketManager();
            Write2.OP(19);
            Write2.Hex("00 00 00 00 00 00 00 00 00 00 00 01");
            Write2.Int(temp1.Length + 4);
            Write2.Byte(1);
            Write2.Bytes(BitConverter.GetBytes(buffersize));
            Write2.Hex(BitConverter.ToString(temp1).Replace("-", " "));

            user.Send(Write2.ack);
        }
    }
}
