using System;
using System.Collections.Generic;
using System.Linq;
using PointBlank.API.Commands;
using PointBlank.API.Player;
using PointBlank.API.Unturned.Chat;
using PointBlank.API.Unturned.Player;
using SDG.Unturned;

namespace UserEssentials.TPA
{
    [PointBlankCommand("TPA", 1)]
    public class CTPA : PointBlankCommand
    {
        #region Properties
        
        public override String[] DefaultCommands => new[] { "tpa" };

        public override String Help => Main.Instance.Translations["CTPA_Help"];

        public override String Usage => Commands[0] + Main.Instance.Translations["CTPA_Usage"];

        public override String DefaultPermission => "";

        public override EAllowedServerState AllowedServerState => EAllowedServerState.RUNNING;

        public override EAllowedCaller AllowedCaller => EAllowedCaller.PLAYER;
        
        #endregion

        public override void Execute(PointBlankPlayer Executor, String[] args)
        {
            if (args.Length != 1)
            {
                UnturnedChat.SendMessage(Executor, Usage);
                return;
            }
            
            UnturnedPlayer PExecutor = (UnturnedPlayer) Executor;
            
            switch (args[0])
            {
                case "a":
                case "accept":
                    if (!PExecutor.Metadata.ContainsKey("TPA_Request"))
                    {
                        UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_Lonely"));
                        return;
                    }

                    if (PExecutor.Player.stance.stance == EPlayerStance.PRONE)
                    {
                        UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_Prone"));
                        return;
                    }

                    ulong Requester = (ulong)PExecutor.Metadata.LastOrDefault(request => request.Key == "TPA_Request").Value;

                    if (!UnturnedPlayer.TryGetPlayer(Requester.ToString(), out UnturnedPlayer PRequester))
                    {
                        UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_Gone"));

                        // how in the hell do you remove a dictionary entry by value
                        
                        return;
                    }
                    
                    break;
                case "d":
                case "deny":
                    break;
                case "c":
                case "cancel":
                    break;
                #region Request
                    
                default:
                    if (!UnturnedPlayer.TryGetPlayer(args[0], out UnturnedPlayer Target))
                    {
                        UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_Goof", args[0]));
                        return;
                    }

                    if (PExecutor == Target)
                    {
                        UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_Self"));
                        return;
                    }

                    if (Target.Metadata.Contains(new KeyValuePair<string, object>("TPA_Request", PExecutor.SteamID.m_SteamID)))
                    {
                        UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_TooMany", Target.CharacterName));
                        return;
                    }
                    
                    UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_Sent", Target.CharacterName));
                    UnturnedChat.SendMessage(Target, Main.Instance.Translate("CTPA_Request", PExecutor.CharacterName));
                    
                    Target.Metadata.Add("TPA_Request", PExecutor.SteamID.m_SteamID);
                    break;
                    
                    #endregion
            }
        }
    }
}