using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameServer.network;

namespace GameServer.Buffers
{
    class LeaveChannel
    {
        public void SairDoCanal(User user)
        {
            Ultilize.LobbyListaDePlayers.Remove(user);
        }
    }
}
