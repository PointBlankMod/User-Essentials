using PointBlank.API.Commands;
using PointBlank.API.Player;
using PointBlank.API.Unturned.Player;
using PointBlank.API.Unturned.Chat;

namespace UserEssentials.PrivateMessaging
{
    [PointBlankCommand("Reply", 1)]
    public class CReply : PointBlankCommand
    {
        #region Properties
        
        public override string[] DefaultCommands => new[] { "reply", "r" };

        public override string Help => Main.Instance.Translations["CReply_help"];

        public override string Usage => Commands[0] + Main.Instance.Translations["CReply_usage"];

        public override string DefaultPermission => "privatemessaging.send";

        public override EAllowedServerState AllowedServerState => EAllowedServerState.RUNNING;

        public override EAllowedCaller AllowedCaller => EAllowedCaller.PLAYER;
        
        #endregion

        public override void Execute(PointBlankPlayer executor, string[] args)
        {
            if(!executor.Metadata.ContainsKey("LastPM"))
            {
                UnturnedChat.SendMessage(executor, Main.Instance.Translations["CReply_lonely"]);
                return;
            }
            
            UnturnedPlayer Player = (UnturnedPlayer)executor.Metadata["LastPM"];
            
            if (UnturnedPlayer.IsServer(Player) || !UnturnedPlayer.IsInServer(Player))
            {
                executor.Metadata.Remove("LastPM");
                UnturnedChat.SendMessage(executor, Main.Instance.Translations["CReply_goof"]);
                return;
            }
            
            UnturnedChat.SendMessage(Player, args[0]);
            UnturnedChat.SendMessage(executor, Main.Instance.Translate("CReply_sent", Player.CharacterName));
            Player.Metadata.Add("LastPM", executor);
        }
    }
}