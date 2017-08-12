﻿using System.Collections.Generic;
using System.Linq;
using PointBlank.API.Collections;
using PointBlank.API.Plugins;
using PointBlank.API.Unturned.Player;

namespace UserEssentials
{
    public class UserEssentials : PointBlankPlugin
    {
        #region Overrides
        
        public override TranslationList Translations => new TranslationList
        {
            { "PlayerNotFound", "Specified player has not been found!" },

            #region Private Messaging
            { "PrivateMessage_Help", "Private message another person." },
            { "PrivateMessage_Usage", " <user> <message>" },
            { "PrivateMessage_Sent", "Message sent to {0}." },
            { "Reply_Help", "Reply to the last person to private message you." },
            { "Reply_Usage", " <message>" },
            { "Reply_Sent", "Replied to {0}." },
            { "Reply_NoPlayer", "There is nobody to reply to." },
            { "Reply_Left", "Recipient left the server." },
            #endregion
            
            #region TPA
            { "TPA_Help", "Request/cancel/accept/deny a teleport to another player." },
            { "TPA_Usage", " <player/accept/cancel/deny> <accept/cancel/deny>" },
            
            #endregion
        };

        public override ConfigurationList Configurations => new ConfigurationList();

        public override string Version => "1.0.0.0";

        public override string VersionURL => "";

        public override string BuildURL => "";

        public override void Load() { }

        public override void Unload() { }
        #endregion
    }
}