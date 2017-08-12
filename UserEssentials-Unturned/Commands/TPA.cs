using System;
using System.Collections.Generic;
using System.Linq;
using PointBlank.API.Commands;
using PointBlank.API.Player;
using PointBlank.API.Unturned.Chat;
using PointBlank.API.Unturned.Player;
using SDG.Unturned;

namespace UserEssentials.Commands
{
    public class TPA : PointBlankCommand
    {
        #region Properties
        public override String[] DefaultCommands => new string[]
        {
            "TPA"
        };

        public override String Help => Translate("TPA_Help");

        public override String Usage => Commands[0] + Translate("TPA_Usage");

        public override String DefaultPermission => "useressentials.commands.tpa";

        public override EAllowedServerState AllowedServerState => EAllowedServerState.RUNNING;

        public override EAllowedCaller AllowedCaller => EAllowedCaller.PLAYER;

        public override int MinimumParams => 1;
        #endregion

        public override void Execute(PointBlankPlayer Executor, String[] Arguments)
        {
            switch (Arguments[0].ToLower())
            {
                case "a":
                case "accept":

                    break;
            }
        }
    }
}