﻿using System.Collections.Generic;
using System.Linq;
using PointBlank.API.Collections;
using PointBlank.API.Plugins;
using PointBlank.API.Unturned.Player;

namespace UserEssentials
{
    public class UserEssentials : PointBlankPlugin
    {
        #region Properties
        public override TranslationList DefaultTranslations => new TranslationList
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
            { "TPA_Help", "Request/accept/deny a teleport to another player." },
            { "TPA_Usage", " <player/accept/deny/current> <accept/deny>" },
            { "TPA_NoRequest", "You have no TPA requests!" },
            { "TPA_Left", "The player has left the server!" },
            { "TPA_Teleport", "Successfully teleported to {0}!" },
            { "TPA_Accept", "{0} has accepted your TPA request!" },
            { "TPA_Deny", "Request from {0} has been denied!" },
            { "TPA_Denied", "Denied TPA request!" },
            { "TPA_Current", "Current: {0}!" },
            { "TPA_Request", "Request sent to {0}!" },
            { "TPA_Requested", "TPA request from {0}!" },
            #endregion

            #region Home
            { "Home_Help", "Teleports the user to their bed" },
            { "Home_Stance", "Can't teleport to home while sitting/driving!" },
            { "Home_NoBed", "No bed has been found!" },
            { "Home_Delay", "Please wait {0} seconds." },
            { "Home_Waiting", "Already waiting to teleport!" },
            { "Home_Success", "Successfully teleported home!" },
            #endregion
        };

        public override ConfigurationList DefaultConfigurations => new ConfigurationList()
        {
            { "Home_Delay_Seconds", 5 },
        };

        public override string Version => "1.0.0.0";

        public override string VersionURL => "http://198.245.61.226/kr4ken/pointblank/useressentials/Version.txt";

        public override string BuildURL => "http://198.245.61.226/kr4ken/pointblank/useressentials/UserEssentials.dll";
        #endregion

        public override void Load() { }

        public override void Unload() { }
    }
}