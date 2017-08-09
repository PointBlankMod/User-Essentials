using System.Collections.Generic;
using PointBlank.API.Unturned.Player;

namespace UserEssentials.TPA
{
    public class Utils
    {
        public static void RemoveTPARequest(UnturnedPlayer Player, ulong Requester)
        {
            if (!Player.Metadata.ContainsKey("TPA_Requests")) return;
            
            List<ulong> Requests = (List<ulong>)Player.Metadata["TPA_Requests"];

            if (!Requests.Contains(Requester)) return;

            Requests.Remove(Requester);

            Player.Metadata.Remove("TPA_Requests");
            
            if (Requests.Count == 0) return;
            
            Player.Metadata.Add("TPA_Requests", Requests);
        }

        public static void AddTPARequest(UnturnedPlayer Player, ulong Requester)
        {
            if (!Player.Metadata.ContainsKey("TPA_Requests")) return;
            
            List<ulong> Requests = (List<ulong>)Player.Metadata["TPA_Requests"];

            if (Requests.Contains(Requester)) return;
            
            Requests.Add(Requester);

            Player.Metadata.Remove("TPA_Requests");
            Player.Metadata.Add("TPA_Requests", Requests);
        }
    }
}