using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameServer.network;
using GameServer.Conexao;

namespace GameServer.Buffers
{
    class joinRoom
    {
        public Sala obterRoom(short salaid)
        {
            return Ultilize.SalaCountID[salaid];            
        }

        public void enterRoom(User user, Readers Ler, CharsInfo charsInfo)
        {
            Ler.Byte();
            Ler.Byte();
            Ler.Byte();
            short salaindex = (short)Ler.UShort();
            string senha = Ler.UString();
            Sala sala = obterRoom(salaindex);
            int slotPos = -1;
            for (int i = 0; i < 3; i++)
            {
                if (slotPos == -1 && sala.slotslen[i].aberto== true)
                    slotPos = i;
            }

            if (sala.slotsAbertos() == 0)
            {
                SalaCheia(user);
            }
            if (sala.jogando == true)
            {
                SalaCheia(user);
            }

            sala.slotslen[slotPos].ativo = true;
            sala.slotslen[slotPos].user = user;
            sala.slotslen[slotPos].aberto = false;
            sala.slotslen[slotPos].AFK = false;
            sala.slotslen[slotPos].Mortes = 0;
            sala.slotslen[slotPos].Lider = false;

            user.AtualSala = sala;
            
            PacketManager Write = new PacketManager();
            Write.OP(22);
            Write.UStr(user.pInfo.usuario);
            Write.Int(user.pInfo.userid);
            Write.UStr(user.pInfo.nickname);
            Write.Int(slotPos);
            Write.Byte((byte)user.PersonagemAtual);
            Write.Hex("00 FF 00 FF 00 FF 00 00 00 00");
            Write.Byte((byte)(slotPos/3));
            Write.Hex("01 00 00 00 0D 00 00 00 00 10 F4 00 00 00 00 00 4E 00 00 00 07 00 00 00 00 00 00 00 00 00 00 00 00 00 00 08 00 00 00 00 00 00 00 00 00 00 00 00 00 00 09 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 10 00 00 00 00 00 00 00 00 00 00 00 00 00 00 11 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 00 00 00 00 00 00 00 00 00 00 00 00 00 13 00 00 00 00 00 00 00 00 00 00 00 00 00 00 14 00 00 00 00 00 00 00 00 00 00 00 00 00 00 15 00 00 00 00 00 00 00 00 00 00 00 00 00 00 16 00 00 00 00 00 00 00 00 00 00 00 00 00 00 17 00 00 00 00 00 00 00 00 00 00 00 00 00 00 18 00 00 00 00 00 00 00 00 00 00 00 00 00 00 19 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 24 00 00 00 00 00 00 00 00 00 00 00 00 00 00 27 00 00 00 00 00 00 00 00 00 00 00 00 00 00 28 00 00 00 00 00 00 00 00 00 00 00 00 00 00 29 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 30 00 00 00 00 00 00 00 00 00 00 00 00 00 00 31 00 00 00 00 00 00 00 00 00 00 00 00 00 00 32 00 00 00 00 00 00 00 00 00 00 00 00 00 00 33 00 00 00 00 00 00 00 00 00 00 00 00 00 00 34 00 00 00 00 00 00 00 00 00 00 00 00 00 00 35 00 00 00 00 00 00 00 00 00 00 00 00 00 00 36 00 00 00 00 00 00 00 00 00 00 00 00 00 00 37 00 00 00 00 00 00 00 00 00 00 00 00 00 00 38 00 00 00 00 00 00 00 00 00 00 00 00 00 00 39 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 40 00 00 00 00 00 00 00 00 00 00 00 00 00 00 43 00 00 00 00 00 00 00 00 00 00 00 00 00 00 44 00 00 00 00 00 00 00 00 00 00 00 00 00 00 45 00 00 00 00 00 00 00 00 00 00 00 00 00 00 46 00 00 00 00 00 00 00 00 00 00 00 00 00 00 47 00 00 00 00 00 00 00 00 00 00 00 00 00 00 48 00 00 00 00 00 00 00 00 00 00 00 00 00 00 49 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 50 00 00 00 00 00 00 00 00 00 00 00 00 00 00 51 00 00 00 00 00 00 00 00 00 00 00 00 00 00 52 00 00 00 00 00 00 00 00 00 00 00 00 00 00 53 00 00 00 00 00 00 00 00 00 00 00 00 00 00 54 00 00 00 00 00 00 00 00 00 00 00 00 00 00 55 00 00 00 00 00 00 00 00 00 00 00 00 00 00 56 00 00 00 00 00 00 00 00 00 00 00 00 00 00 57 00 00 00 00 00 00 00 00 00 00 00 00 00 00 58 00 00 00 00 00 00 00 00 00 00 00 00 00 00 59 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5F 00 00 00 00 00 00 00 00 00 00 00");
            if (sala.ObterSessao() == user)
            {
                Write.Byte(1);
            }
            else
            {
                Write.Byte(0);
            }
            Write.Hex("01 00 00 00 00 00 00 00 00 00 00 00 00");
            Write.Int(charsInfo.getLength());
            for (int countchar = 0; countchar < charsInfo.getLength(); countchar++)
            {
                Write.Byte((byte)charsInfo.personagems[countchar].personagemid);
                Write.Byte((byte)charsInfo.personagems[countchar].personagemid);
                Write.Int(0);
                Write.Byte((byte)charsInfo.personagems[countchar].classe);
                Write.Byte((byte)charsInfo.personagems[countchar].classe);
                Write.Int(0);
                Write.Int(charsInfo.personagems[countchar].experiencia);
                Write.Int(charsInfo.personagems[countchar].vitoria);
                Write.Int(charsInfo.personagems[countchar].derrota);
                Write.Int(charsInfo.personagems[countchar].vitoria);
                Write.Int(charsInfo.personagems[countchar].derrota);
                Write.Int(0);
                Write.Int(charsInfo.personagems[countchar].experiencia);
                Write.Int(charsInfo.personagems[countchar].nivel);
                Write.Int(charsInfo.personagems[countchar].equipamentos.Length);
                for (int equipscount = 0; equipscount < charsInfo.personagems[countchar].equipamentos.Length; equipscount++)
                {
                    Write.Int(charsInfo.personagems[countchar].equipamentos[equipscount].itemid);
                    Write.Int(0);
                    Write.Int(charsInfo.personagems[countchar].equipamentos[equipscount].itemuid);
                    Write.Int(0);
                    Write.Int(0);
                    Write.Int(0);
                    Write.Int(0);
                    Write.Short(0);
                    Write.Byte(0);
                }
                Write.Int(0);
                Write.Int(0);
                Write.Int(charsInfo.personagems[countchar].splef);
                Write.Hex("00 00 00 A0 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 07 AF 00 00 00 00 00 00 07 AF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 2C 00 00 01 2C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 07");
            }
            Write.Hex("00 00 00 04 13 00 A8 C0 01 EC A8 C0 9B BA FE A9");
            Write.Hex(Ultilize.IpHex);
            Write.Hex("00 00 00 01 7E F6 00 00 00 00 00 00 00 00 00 00 00 02 00 00 00 00 00 00 E5 6A 00 00 00 01 2C BD 52 5A 00 00 00 00 01 00 00 E5 88 00 00 00 01 2C BD 52 5B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 56 86 32 00 56 87 6E D4 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            for (int a = 0; a < sala.MaxJogadores; a++ )
            {
                if (user.AtualSala.slotslen[a].ativo == true && user.AtualSala.slotslen[a].user != user)
                {
                    user.AtualSala.slotslen[a].user.Send(Write.ack);
                    user.AtualSala.slotslen[a].user.Status = "SALA";
                }
            }
            JoinRoomInfo(user,sala);            
        }

        public void JoinRoomInfo(User user,Sala sala)
        {
            PacketManager Write = new PacketManager();
            Write.OP(1554);
            Write.Byte(0);
            Write.Short(sala.SalaID);
            Write.UStr(sala.SalaNome);
            Write.Byte(0);
            if (sala.SalaSenha.Length > 0)
            {
                Write.Byte(1);
            }
            else
            {
                Write.Byte(0);
            }
            Write.UStr(sala.SalaSenha);
            Write.Short((short)sala.PlayersEmSala());
            Write.Short((short)sala.slotsAbertos());
            Write.Hex("00 0B");
            Write.Byte((byte)sala.MatchMode);
            Write.Int(sala.GameMode);
            Write.Int(sala.ITMode);
            Write.Boolean(sala.RndMap);
            Write.Int(sala.Map);
            Write.Int(12);
            for (int a = 0; a < sala.MaxJogadores; a++ )
            {
                Write.Boolean(sala.slotslen[a].aberto);
            }
            Write.Hex("FF FF FF FF 00 00 00 00 00 00 00 00 01");
            Write.Hex(Ultilize.IpHex);
            Write.Short((short)9600);
            Write.Hex(Ultilize.IpHex);
            Write.Short((short)9700);
            Write.Hex("01 00 01 00 00 01 2C 00 00 00 14 00 02 4B 52 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 06 01 00 00 00 00");

            user.Send(Write.ack);
            RoomInfoDivide(user);
            Ultilize.LobbyListaDePlayers.Remove(user);
            return;
        }


        public void SalaCheia(User user)
        {
            PacketManager Write = new PacketManager();
            Write.OP(1468);
            Write.Int(6);
            Write.Hex("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 01 30 00 00 00 F9 00 00 09 0D 00 00 00 00 00 00 00 00 F2 04 00 00 00 00 00 00 13 49 F4 FC 09 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 09 13 F2 04 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            user.Send(Write.ack);
            return;
        }


        public void RoomInfoDivide(User user)
        {
            int temp = -1;
            for (int a = 0; a < user.AtualSala.MaxJogadores; a++)
            {
                if (user.AtualSala.slotslen[a].ativo == false)
                    continue;

                temp++;
                PacketManager Write = new PacketManager();
                Write.OP(1468);
                Write.Int(0);
                Write.Int(user.AtualSala.PlayersEmSala());
                Write.Int(temp);
                Write.UStr(user.AtualSala.slotslen[a].user.pInfo.usuario);
                Write.Int(user.AtualSala.slotslen[a].user.pInfo.userid);
                Write.UStr(user.AtualSala.slotslen[a].user.pInfo.nickname);
                Write.Int(a);
                Write.Byte((byte)user.AtualSala.slotslen[a].user.PersonagemAtual);
                Write.Hex("00 FF 00 FF 00 FF 00 00 00 00");
                Write.Byte((byte)user.AtualSala.obterTime(user.AtualSala.slotslen[a].user));
                Write.Hex("01 00 00 00 0D 00 00 00 00 10 F4 00 00 00 00 00 4E 00 00 00 07 00 00 00 00 00 00 00 00 00 00 00 00 00 00 08 00 00 00 00 00 00 00 00 00 00 00 00 00 00 09 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 10 00 00 00 00 00 00 00 00 00 00 00 00 00 00 11 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 00 00 00 00 00 00 00 00 00 00 00 00 00 13 00 00 00 00 00 00 00 00 00 00 00 00 00 00 14 00 00 00 00 00 00 00 00 00 00 00 00 00 00 15 00 00 00 00 00 00 00 00 00 00 00 00 00 00 16 00 00 00 00 00 00 00 00 00 00 00 00 00 00 17 00 00 00 00 00 00 00 00 00 00 00 00 00 00 18 00 00 00 00 00 00 00 00 00 00 00 00 00 00 19 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 24 00 00 00 00 00 00 00 00 00 00 00 00 00 00 27 00 00 00 00 00 00 00 00 00 00 00 00 00 00 28 00 00 00 00 00 00 00 00 00 00 00 00 00 00 29 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 30 00 00 00 00 00 00 00 00 00 00 00 00 00 00 31 00 00 00 00 00 00 00 00 00 00 00 00 00 00 32 00 00 00 00 00 00 00 00 00 00 00 00 00 00 33 00 00 00 00 00 00 00 00 00 00 00 00 00 00 34 00 00 00 00 00 00 00 00 00 00 00 00 00 00 35 00 00 00 00 00 00 00 00 00 00 00 00 00 00 36 00 00 00 00 00 00 00 00 00 00 00 00 00 00 37 00 00 00 00 00 00 00 00 00 00 00 00 00 00 38 00 00 00 00 00 00 00 00 00 00 00 00 00 00 39 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 40 00 00 00 00 00 00 00 00 00 00 00 00 00 00 43 00 00 00 00 00 00 00 00 00 00 00 00 00 00 44 00 00 00 00 00 00 00 00 00 00 00 00 00 00 45 00 00 00 00 00 00 00 00 00 00 00 00 00 00 46 00 00 00 00 00 00 00 00 00 00 00 00 00 00 47 00 00 00 00 00 00 00 00 00 00 00 00 00 00 48 00 00 00 00 00 00 00 00 00 00 00 00 00 00 49 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 50 00 00 00 00 00 00 00 00 00 00 00 00 00 00 51 00 00 00 00 00 00 00 00 00 00 00 00 00 00 52 00 00 00 00 00 00 00 00 00 00 00 00 00 00 53 00 00 00 00 00 00 00 00 00 00 00 00 00 00 54 00 00 00 00 00 00 00 00 00 00 00 00 00 00 55 00 00 00 00 00 00 00 00 00 00 00 00 00 00 56 00 00 00 00 00 00 00 00 00 00 00 00 00 00 57 00 00 00 00 00 00 00 00 00 00 00 00 00 00 58 00 00 00 00 00 00 00 00 00 00 00 00 00 00 59 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5F 00 00 00 00 00 00 00 00 00 00 00");
                if (user.AtualSala.ObterSessao() == user.AtualSala.slotslen[a].user)
                {
                    Write.Byte(1);
                }
                else
                {
                    Write.Byte(0);
                }
                Write.Hex("01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                Write.Byte((byte)user.AtualSala.slotslen[a].user.pInfo.charsinfo.getLength());
                for (int countchar = 0; countchar < user.AtualSala.slotslen[a].user.pInfo.charsinfo.getLength(); countchar++)
                {
                    Write.Byte((byte)user.AtualSala.slotslen[a].user.pInfo.charsinfo.personagems[countchar].personagemid);
                    Write.Int(0);
                    Write.Byte((byte)user.AtualSala.slotslen[a].user.pInfo.charsinfo.personagems[countchar].classe);
                    Write.Int(0);
                    Write.Byte(0);
                    Write.Int(user.AtualSala.slotslen[a].user.pInfo.charsinfo.personagems[countchar].experiencia);
                    Write.Int(user.AtualSala.slotslen[a].user.pInfo.charsinfo.personagems[countchar].nivel);
                    Write.Int(user.AtualSala.slotslen[a].user.pInfo.charsinfo.personagems[countchar].vitoria);
                    Write.Int(user.AtualSala.slotslen[a].user.pInfo.charsinfo.personagems[countchar].derrota);

                    Write.Int(user.AtualSala.slotslen[a].user.pInfo.charsinfo.personagems[countchar].equipamentos.Length);
                    for (int equipscount = 0; equipscount < user.AtualSala.slotslen[a].user.pInfo.charsinfo.personagems[countchar].equipamentos.Length; equipscount++)
                    {
                        Write.Int(user.AtualSala.slotslen[a].user.pInfo.charsinfo.personagems[countchar].equipamentos[equipscount].itemid);
                        Write.Int(1);
                        Write.Int(user.AtualSala.slotslen[a].user.pInfo.charsinfo.personagems[countchar].equipamentos[equipscount].itemuid);
                        Write.Int(0);
                        Write.Int(0);
                        Write.Int(0);
                        Write.Int(0);
                        Write.Short(0);
                        Write.Byte(0);
                    }
                    Write.Hex("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 01 00");
                    Write.Int(0);
                    Write.Int(255);
                    Write.Int(160);
                    Write.Hex("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 02 00 FF 00 00 00 00 00 00");
                    Write.Int(0);
                    Write.Hex("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 2C 00 00 01 2C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 07");
                }
                Write.Hex("00 00 00 04 13 00 A8 C0 01 EC A8 C0 9B BA FE A9");
                Write.Hex(Ultilize.IpHex);
                Write.Hex("00 00 00 01 7E F5 00 00 00");
                Write.Byte(user.AtualSala.slotslen[a].Status);
                Write.Hex("00 00 00 00 00 00 00 02 00 00 00 00 00 00 E5 6A 00 00 00 01 2C BD 52 5A 00 00 00 00 01 00 00 E5 88 00 00 00 01 2C BD 52 5B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 56 86 32 00 56 87 6E D4 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                user.Send(Write.ack);
            }
        }
    }
}
