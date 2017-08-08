using System;
using System.Collections.Generic;
using System.Linq;
using PointBlank.API.Commands;
using PointBlank.API.Player;
using PointBlank.API.Unturned.Chat;
using PointBlank.API.Unturned.Player;
using SDG.Unturned;
using Main = UserEssentials.Main;

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

        public override void Execute(PointBlankPlayer Executor, String[] Arguments)
        {
            if (Arguments.Length > 2)
            {
                UnturnedChat.SendMessage(Executor, Usage);
                return;
            }
            
            UnturnedPlayer PExecutor = (UnturnedPlayer) Executor;
            ulong Requester;
            
            switch (Arguments[0])
            {
                #region Accept
                
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

                    if (Arguments.Length == 1)
                        Requester = ((List<ulong>) PExecutor.Metadata["TPA_Request"]).FirstOrDefault();
                    else
                    {
                        if(!UnturnedPlayer.TryGetPlayer(Arguments[1], out UnturnedPlayer PotentialRequester))
                        {
                            UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("Player_Goof", Arguments[1]));
                            return;
                        }

                        if (!((List<ulong>) PExecutor.Metadata["TPA_Request"]).Contains(PotentialRequester.SteamID.m_SteamID))
                        {
                            UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("CTPA_Goof", PotentialRequester.CharacterName));
                            return;
                        }

                        Requester = PotentialRequester.SteamID.m_SteamID;
                    }

                    if (!UnturnedPlayer.TryGetPlayer(Requester.ToString(), out UnturnedPlayer PRequester))
                    {
                        UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_Gone"));

                        RemoveTPARequest(PExecutor, Requester);
                        
                        return;
                    }
                    
                    PRequester.Teleport(PExecutor.Position);
                    
                    UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("CTPA_Accepted", PRequester.CharacterName));
                    UnturnedChat.SendMessage(PRequester, Main.Instance.Translate("CTPA_RAccepted", PExecutor.CharacterName));
                    break;
                    
                    #endregion
                #region Deny
                
                case "d":
                case "deny":
                    if (!PExecutor.Metadata.ContainsKey("TPA_Requests"))
                    {
                        UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("CTPA_Lonely"));
                        return;
                    }

                    if (Arguments.Length == 1)
                        Requester = ((List<ulong>) PExecutor.Metadata["TPA_Requests"]).FirstOrDefault();
                    else
                    {
                        if(!UnturnedPlayer.TryGetPlayer(Arguments[1], out UnturnedPlayer PotentialRequester))
                        {
                            UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("Player_Goof", Arguments[1]));
                            return;
                        }
                        
                        if (!((List<ulong>) PExecutor.Metadata["TPA_Request"]).Contains(PotentialRequester.SteamID.m_SteamID))
                        {
                            UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("CTPA_Goof", PotentialRequester.CharacterName));
                            return;
                        }

                        Requester = PotentialRequester.SteamID.m_SteamID;
                    }
                    
                    UnturnedPlayer pRequester = UnturnedPlayer.Get(Requester);
                    
                    UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("CTPA_Rekt", pRequester.CharacterName));
                    UnturnedChat.SendMessage(pRequester, Main.Instance.Translate("CTPA_Pathetic", PExecutor.CharacterName));
                    
                    RemoveTPARequest(PExecutor, Requester);
                    break;
                    #endregion
                #region Cancel
                case "c":
                case "cancel":
                    if (!PExecutor.Metadata.ContainsKey("TPA_Requests"))
                    {
                        UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("CTPA_Lonely"));
                        return;
                    }

                    if (Arguments.Length == 1)
                    {
                        bool removed = false;
                        
                        for (int i = 0; i < Provider.clients.Count; i++)
                        {
                            UnturnedPlayer Player = UnturnedPlayer.Get(Provider.clients[i]);

                            if (!Player.Metadata.ContainsKey("TPA_Requests"))
                                continue;
                            
                            if (!((List<ulong>) Player.Metadata["TPA_Requests"]).Contains(PExecutor.SteamID.m_SteamID))
                                continue;
                            
                            RemoveTPARequest(Player, PExecutor.SteamID.m_SteamID);
                            removed = true;
                        }

                        if (!removed)
                        {
                            UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("CTPA_None"));
                            return;
                        }
                        
                        UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("CTPA_Cancelled_All"));
                    }
                    else
                    {
                        if(!UnturnedPlayer.TryGetPlayer(Arguments[1], out UnturnedPlayer PotentialRequester))
                        {
                            UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("Player_Goof", Arguments[1]));
                            return;
                        }
                        
                        if (!PotentialRequester.Metadata.ContainsKey("TPA_Request") ||
                            !((List<ulong>) PotentialRequester.Metadata["TPA_Request"]).Contains(PExecutor.SteamID.m_SteamID))
                        {
                            UnturnedChat.SendMessage(PExecutor, Main.Instance.Translate("CTPA_YouGoof", PotentialRequester.CharacterName));
                            return;
                        }

                        RemoveTPARequest(PotentialRequester, PExecutor.SteamID.m_SteamID);
                        UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_Cancelled", PotentialRequester.CharacterName));
                    }
                    break;
                    
                #endregion
                #region Request
                    
                default:
                    if (Arguments.Length != 1)
                    {
                        UnturnedChat.SendMessage(Executor, Usage);
                        return;
                    }
                    
                    if (!UnturnedPlayer.TryGetPlayer(Arguments[0], out UnturnedPlayer Target))
                    {
                        UnturnedChat.SendMessage(Executor, Main.Instance.Translate("Player_Goof", Arguments[0]));
                        return;
                    }

                    if (PExecutor == Target)
                    {
                        UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_Self"));
                        return;
                    }

                    if (!Target.Metadata.ContainsKey("TPA_Requests") || ((List<ulong>)Target.Metadata["TPA_Requests"]).Contains(PExecutor.SteamID.m_SteamID))
                    {
                        UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_TooMany", Target.CharacterName));
                        return;
                    }
                    
                    UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CTPA_Sent", Target.CharacterName));
                    UnturnedChat.SendMessage(Target, Main.Instance.Translate("CTPA_Request", PExecutor.CharacterName));

                    if (Target.Metadata["TPA_Requests"] == null)
                    {
                        Target.Metadata.Add("TPA_Requests", new List<ulong> {PExecutor.SteamID.m_SteamID});
                        return;
                    }

                    AddTPARequest(Target, PExecutor.SteamID.m_SteamID);
                    break;
                    
                    #endregion
            }
        }
        
        public void RemoveTPARequest(UnturnedPlayer Player, ulong Requester)
        {
            if (!Player.Metadata.ContainsKey("TPA_Requests")) return;
            
            List<ulong> Requests = (List<ulong>)Player.Metadata["TPA_Requests"];

            if (!Requests.Contains(Requester)) return;

            Requests.Remove(Requester);

            Player.Metadata.Remove("TPA_Requests");
            
            if (Requests.Count == 0) return;
            
            Player.Metadata.Add("TPA_Requests", Requests);
        }

        public void AddTPARequest(UnturnedPlayer Player, ulong Requester)
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