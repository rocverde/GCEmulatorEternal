using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameServer.network;

namespace GameServer.Buffers
{
    class Chat
    {
        public void chat(User user,Readers Ler)
        {
            Ler.Byte();
            Ler.Int();
            string ServerName = Ler.UString();
            Ler.Int();
            Ler.Int();
            Ler.Int();
            string texto = Ler.UString();

            if (user.Status == "LOBBY")
            {
                EnviarLobby(user,texto);
            }
            if (user.Status == "SALA")
            {
                EnviarSala(user, texto);
            }
            if (user.Status == "JOGANDO")
            {
                EnviarSala(user, texto);
            }
        }

        private void EnviarLobby(User user, string texto)
        {
            PacketManager Write = new PacketManager();
            Write.OP(7);
            Write.Byte(1);
            Write.Int(user.pInfo.userid);
            Write.UStr(user.pInfo.nickname);
            Write.Int(0);
            Write.Int(0);
            Write.Int(-1);
            Write.UStr(texto);            
            Write.Int(0);
            Write.Int(0);
            for (int gg = 0; gg < Ultilize.LobbyListaDePlayers.Count; gg++)
            {                   
                Ultilize.LobbyListaDePlayers[gg].Send(Write.ack);
                //Console.WriteLine("ID: "+Ultilize.LobbyListaDePlayers[gg].pInfo.usuario);
            }
        }

        private void EnviarSala(User user, string texto)
        {
            PacketManager Write = new PacketManager();
            Write.OP(7);
            Write.Byte(1);
            Write.Int(user.pInfo.userid);
            Write.UStr(user.pInfo.nickname);
            Write.Int(0);
            Write.Int(0);
            Write.Int(-1);
            Write.UStr(texto);
            Write.Int(0);
            Write.Int(0);
            for (int gg = 0; gg < user.AtualSala.MaxJogadores; gg++)
            {
                user.AtualSala.slotslen[gg].user.Send(Write.ack);
            }
        }

    }
}
