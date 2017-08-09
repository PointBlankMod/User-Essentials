using PointBlank.API.Commands;
using PointBlank.API.Player;
using PointBlank.API.Unturned.Player;
using PointBlank.API.Unturned.Chat;

namespace UserEssentials.PrivateMessaging
{
    public class CReply : PointBlankCommand
    {
        #region Properties
        
        public override string[] DefaultCommands => new[] { "reply", "r" };

        public override string Help => Main.Instance.Translations["CReply_Help"];

        public override string Usage => Commands[0] + Main.Instance.Translations["CReply_Usage"];

        public override string DefaultPermission => "privatemessaging.send";

        public override EAllowedServerState AllowedServerState => EAllowedServerState.RUNNING;

        public override EAllowedCaller AllowedCaller => EAllowedCaller.PLAYER;
        
        public override int MinimumParams => 1;
        
        #endregion

        public override void Execute(PointBlankPlayer Executor, string[] Arguments)
        {
            if(!Executor.Metadata.ContainsKey("LastPM"))
            {
                UnturnedChat.SendMessage(Executor, Main.Instance.Translations["CReply_Lonely"]);
                return;
            }
            
            UnturnedPlayer Player = (UnturnedPlayer)Executor.Metadata["LastPM"];
            
            if (UnturnedPlayer.IsServer(Player) || !UnturnedPlayer.IsInServer(Player))
            {
                Executor.Metadata.Remove("LastPM");
                UnturnedChat.SendMessage(Executor, Main.Instance.Translations["CReply_Goof"]);
                return;
            }
            
            UnturnedChat.SendMessage(Player, Arguments[0]);
            UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CReply_Sent", Player.CharacterName));
            Player.Metadata.Add("LastPM", Executor);
        }
    }
}