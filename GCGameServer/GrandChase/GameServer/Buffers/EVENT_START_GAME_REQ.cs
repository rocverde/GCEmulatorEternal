using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameServer.network;
using GameServer.Conexao;

namespace GameServer.Buffers
{
    class StartGame
    {        
        public void rungame(User user)
        {
            Sala sala = user.AtualSala;
            int playersInSala = sala.PlayersEmSala();
            PacketManager Write = new PacketManager();
            Write.OP(38);
            Write.Int(0);
            Write.Int(1379592610);
            Write.Hex("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            Write.Int(playersInSala);
            for (int a = 0; a < sala.MaxJogadores; a++)
            {
                if (sala.slotslen[a].ativo == true)
                {
                    Write.Int(sala.slotslen[a].user.pInfo.userid);
                }
            }
            Write.Int(149246);
            Write.Int(playersInSala);
            for (int b = 0; b < sala.MaxJogadores; b++)
            {
                if (sala.slotslen[b].ativo == true)
                {
                    Write.Int(sala.slotslen[b].user.pInfo.userid);
                    Write.Hex("00 00 01 04 00 00 00 6A");
                }
            }
            Write.Int(0);
            Write.Int(sala.ObterSessao().pInfo.userid);
            Write.Hex("00 00 00 00 00 00 00");
            Write.Byte((byte)sala.MatchMode);
            Write.Int(sala.GameMode);
            Write.Int(sala.ITMode);
            Write.Boolean(false);
            Write.Int(sala.Map);
            Write.Hex("00 00 00 00 FF FF FF FF 00 00 00 01 00 00 00");
            Write.Short((short)playersInSala);
            Write.Short((short)sala.slotsAbertos());
            for (int c = 0; c < sala.MaxJogadores; c++)
            {
                if (sala.slotslen[c].ativo == true)
                {
                    Write.Boolean(sala.slotslen[c].aberto);
                }
            }
            Write.Hex("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            Write.Short((short)playersInSala);
            for (int d = 0; d < sala.MaxJogadores; d++)
            {
                if (sala.slotslen[d].ativo == true)
                {
                    Write.Int(sala.slotslen[d].user.pInfo.userid);
                    Write.Byte((byte)sala.slotslen[d].user.PersonagemAtual);
                    Write.Int(1000);
                }
            }
            Write.Hex("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");

            for (int e = 0; e < sala.MaxJogadores; e++ )
            {
                if (sala.slotslen[e].ativo == true)
                {
                    sala.slotslen[e].user.Send(Write.ack);
                }
            }
            sala.jogando = true;
        }
    }
}
