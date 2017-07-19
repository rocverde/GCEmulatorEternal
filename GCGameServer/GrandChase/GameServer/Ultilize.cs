using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using GameServer.Conexao;
using GameServer.network;

namespace GameServer
{
    public static class Ultilize
    {
        public static string IpHex = "01 00 00 7F";
        //public static ArrayList ListaDePlayers = new ArrayList();
        //public static ArrayList ListaDeSalas = new ArrayList();
        public static List<Sala> ListaDeSalas;
        public static List<User> LobbyListaDePlayers;
        public static Dictionary<short, Sala> SalaCountID { get; set; }
        
        public static void init()
        {
            ListaDeSalas = new List<Sala>();
            LobbyListaDePlayers = new List<User>();
            SalaCountID = new Dictionary<short, Sala>();
        }
        public static int StrToInt(string temp)
        {
            int result = Convert.ToInt32(temp);
            return result;
        }

        public static byte[] StringFromHex(string hex)
        {
            hex = hex.Replace(" ", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }
    }
}
