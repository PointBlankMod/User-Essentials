using PointBlank.API.Collections;
using PointBlank.API.Plugins;

namespace UserEssentials
{
    public class Main : Plugin
    {
        #region Overrides
        
        public override TranslationList Translations => new TranslationList
        {
            #region Private Messaging
            
            { "CPM_Help", "Private message another person." },
            { "CPM_Usage", " <user> <message>" },
            { "CPM_Sent", "Message sent to {0}." },
            { "CPM_Goof", "{0} is not a player." },
            { "CReply_Help", "Reply to the last person to private message you." },
            { "CReply_Usage", " <message>" },
            { "CReply_Sent", "Replied to {0}." },
            { "CReply_Lonely", "There is nobody to reply to." },
            { "CReply_Goof", "Recipient left the server." },
            
            #endregion
            
            #region TPA
            
            { "CTPA_Help", "Request/cancel/accept/deny a teleport to another player." },
            { "CTPA_Usage", "<player/accept/cancel/deny>" },
            { "CTPA_Goof", "{0} is not a player." },
            { "CTPA_TooMany", "You have already requested a teleport to {0}." },
            { "CTPA_Sent", "Requested to teleport to {0}." },
            { "CTPA_Request", "{0} Requested to teleport to you." },
            { "CTPA_Self", "You may not teleport to yourself." },
            { "CTPA_Lonely", "You have no teleport requests." },
            { "CTPA_Prone", "You may not accept teleport requests while proned" }
            
            #endregion
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