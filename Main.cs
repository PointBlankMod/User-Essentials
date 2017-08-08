using PointBlank.API.Collections;
using PointBlank.API.Plugins;

namespace UserEssentials
{
    public class Main : Plugin
    {
        #region Overrides
        
        public override TranslationList Translations => new TranslationList()
        {
            #region Private Messaging
            
            { "CPM_help", "Private message another person." },
            { "CPM_usage", " <user> <message>" },
            { "CPM_sent", "Message sent to {0}." },
            { "CPM_goof", "{0} is not a player." },
            { "CReply_help", "Reply to the last person to private message you." },
            { "CReply_usage", " <message>" },
            { "CReply_sent", "Replied to {0}." },
            { "CReply_lonely", "There is nobody to reply to." },
            { "CReply_goof", "Recipient left the server." }
            
            #endregion
        };

        public override ConfigurationList Configurations => new ConfigurationList() { };

        public override string Version => "1.0.0.0";

        public override string VersionURL => "198.245.61.226/kr4ken/pointblank/ue/ue_version";

        public override string BuildURL => "198.245.61.226/kr4ken/pointblank/ue/UE.dll";

        public override void Load() { }

        public override void Unload() { }
        
        #endregion
    }
}