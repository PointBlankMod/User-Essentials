using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointBlank.API;
using PointBlank.API.Commands;
using PointBlank.API.Player;
using PointBlank.API.Tasks;
using PointBlank.API.Unturned.Chat;
using PointBlank.API.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace UserEssentials_Unturned.Commands
{
    public class Home : PointBlankCommand
    {
        #region Properties
        public override string[] DefaultCommands => new string[]
        {
            "Home"
        };

        public override string Help => Translate("Home_Help");

        public override string Usage => Commands[0];

        public override string DefaultPermission => "useressentials.commands.home";

        public override EAllowedServerState AllowedServerState => EAllowedServerState.RUNNING;

        public override EAllowedCaller AllowedCaller => EAllowedCaller.PLAYER;
        #endregion

        public override void Execute(PointBlankPlayer executor, string[] args)
        {
            UnturnedPlayer player = (UnturnedPlayer)executor;

            if(player.Stance.stance == EPlayerStance.DRIVING ||
               player.Stance.stance == EPlayerStance.SITTING)
            {
                UnturnedChat.SendMessage(executor, Translate("Home_Stance"), ConsoleColor.Red);
                return;
            }
            if (player.Metadata.ContainsKey("Home"))
            {
                UnturnedChat.SendMessage(executor, Translate("Home_Waiting"), ConsoleColor.Red);
                return;
            }
            if(!BarricadeManager.tryGetBed(player.SteamID, out Vector3 position, out byte angle))
            {
                UnturnedChat.SendMessage(executor, Translate("Home_NoBed"), ConsoleColor.Red);
                return;
            }
            int delay = Configure<int>("Home_Delay_Seconds");

            if (player.HasPermission("useressentials.bypass.homedelay"))
                delay = 0;
            if (delay > 0)
                UnturnedChat.SendMessage(executor, Translate("Home_Delay", delay), ConsoleColor.Green);

            player.Metadata.Add("Home", true);
            PointBlankTask.Create()
                .Delay(TimeSpan.FromSeconds(delay))
                .Action((task) =>
                {
                    PointBlankLogging.Log("Test!");
                    if (!UnturnedPlayer.IsInServer(player))
                        return;

                    player.Teleport(position);
                    player.Metadata.Remove("Home");
                    UnturnedChat.SendMessage(executor, Translate("Home_Success"), ConsoleColor.Green);
                }).Build(true);
        }
    }
}
