using System;
using System.Collections.Generic;
using System.Linq;
using PointBlank.API.Commands;
using PointBlank.API.Player;
using PointBlank.API.Implements;
using PointBlank.API.Unturned.Chat;
using PointBlank.API.Unturned.Player;

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
            UnturnedPlayer player = (UnturnedPlayer)Executor;
            UnturnedPlayer target;

            if (!player.Metadata.ContainsKey("TPA"))
                player.Metadata.Add("TPA", new Queue<UnturnedPlayer>());
            Queue<UnturnedPlayer> queue = (Queue<UnturnedPlayer>)player.Metadata["TPA"];

            switch (Arguments[0].ToLower())
            {
                #region Accept
                case "a":
                case "accept":
                    if(queue.Count < 1)
                    {
                        UnturnedChat.SendMessage(Executor, Translate("TPA_NoRequest"), ConsoleColor.Red);
                        return;
                    }
                    target = queue.Dequeue();

                    if (!UnturnedPlayer.IsInServer(target))
                    {
                        UnturnedChat.SendMessage(Executor, Translate("TPA_Left"), ConsoleColor.Red);
                        return;
                    }
                    player.Teleport(target.Position);
                    UnturnedChat.SendMessage(Executor, Translate("TPA_Teleport", target), ConsoleColor.Green);
                    UnturnedChat.SendMessage(target, Translate("TPA_Accept", player), ConsoleColor.Green);
                    break;
                #endregion

                #region Deny
                case "d":
                case "deny":
                    if (queue.Count < 1)
                    {
                        UnturnedChat.SendMessage(Executor, Translate("TPA_NoRequest"), ConsoleColor.Red);
                        return;
                    }
                    target = queue.Dequeue();

                    UnturnedChat.SendMessage(Executor, Translate("TPA_Denied"), ConsoleColor.Green);
                    if (UnturnedPlayer.IsInServer(target))
                        UnturnedChat.SendMessage(target, Translate("TPA_Deny", player), ConsoleColor.Red);
                    break;
                #endregion

                #region Current
                case "c":
                case "current":
                    if (queue.Count < 1)
                    {
                        UnturnedChat.SendMessage(Executor, Translate("TPA_NoRequest"), ConsoleColor.Red);
                        return;
                    }
                    do
                    {
                        target = queue.Peek();
                        if (!UnturnedPlayer.IsInServer(target))
                            target = queue.Dequeue();
                    } while (!UnturnedPlayer.IsInServer(target) || queue.Count < 1);

                    if (queue.Count > 0)
                        UnturnedChat.SendMessage(Executor, Translate("TPA_Current", target), ConsoleColor.Green);
                    else
                        UnturnedChat.SendMessage(Executor, Translate("TPA_NoRequest"), ConsoleColor.Red);
                    break;
                #endregion

                #region Request
                default:
                    if(!UnturnedPlayer.TryGetPlayer(Arguments[0], out target))
                    {
                        UnturnedChat.SendMessage(Executor, Translate("PlayerNotFound"), ConsoleColor.Red);
                        return;
                    }

                    if (!target.Metadata.ContainsKey("TPA"))
                        target.Metadata.Add("TPA", new Queue<UnturnedPlayer>());
                    queue = (Queue<UnturnedPlayer>)target.Metadata["TPA"];

                    queue.Enqueue(player);
                    UnturnedChat.SendMessage(Executor, Translate("TPA_Request", target), ConsoleColor.Green);
                    UnturnedChat.SendMessage(target, Translate("TPA_Requested", player), ConsoleColor.Green);
                    break;
                #endregion
            }
        }
    }
}