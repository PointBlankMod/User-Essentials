using System.Collections.Generic;
using System.Linq;
using PointBlank.API.Collections;
using PointBlank.API.Plugins;
using PointBlank.API.Unturned.Player;

namespace UserEssentials
{
    public class Main : PointBlankPlugin
    {
        #region Overrides
        
        public override TranslationList Translations => new TranslationList
        {
            #region Private Messaging
            
            { "CPM_Help", "Private message another person." },
            { "CPM_Usage", " <user> <message>" },
            { "CPM_Sent", "Message sent to {0}." },
            { "CReply_Help", "Reply to the last person to private message you." },
            { "CReply_Usage", " <message>" },
            { "CReply_Sent", "Replied to {0}." },
            { "CReply_Lonely", "There is nobody to reply to." },
            { "CReply_Goof", "Recipient left the server." },
            
            #endregion
            
            #region TPA
            
            { "CTPA_Help", "Request/cancel/accept/deny a teleport to another player." },
            { "CTPA_Usage", "<player/accept/cancel/deny> <player to accept/cancel/deny>" },
            { "CTPA_Goof", "{0} did not request to teleport to you." },
            { "CTPA_YouGoof", "You did not request to teleport to {0}" },
            { "CTPA_TooMany", "You have already requested a teleport to {0}." },
            { "CTPA_Sent", "Requested to teleport to {0}." },
            { "CTPA_Request", "{0} Requested to teleport to you." },
            { "CTPA_Self", "You may not teleport to yourself." },
            { "CTPA_Lonely", "You have no teleport requests." },
            { "CTPA_Prone", "You may not accept teleport requests while proned." },
            { "CTPA_Accepted", "You accepted {0}'s teleport request." },
            { "CTPA_RAccepted", "{0} accepted your teleport request." },
            { "CTPA_Pathetic", "{0} denied your teleport request." },
            { "CTPA_Rekt", "You denied {0}'s teleport request." },
            { "CTPA_None", "You've sent no teleport requests." },
            { "CTPA_Cancelled", "You cancelled your teleport request to {0}." },
            { "CTPA_Cancelled_All", "You cancelled all teleport requests." },
            
            #endregion
            
            { "Player_Goof", "{0} is not a player." }
        };

        public override ConfigurationList Configurations => new ConfigurationList();

        public override string Version => "1.0.0.0";

        public override string VersionURL => "198.245.61.226/kr4ken/pointblank/ue/ue_version";

        public override string BuildURL => "198.245.61.226/kr4ken/pointblank/ue/UE.dll";

        public override void Load() { }

        public override void Unload() { }
        
        #endregion
    }
}