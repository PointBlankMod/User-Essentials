using PointBlank.API.Commands;
using PointBlank.API.Player;
using PointBlank.API.Unturned.Player;
using PointBlank.API.Unturned.Chat;

namespace UserEssentials.Commands
{
    public class Reply : PointBlankCommand
    {
        #region Properties
        public override string[] DefaultCommands => new string[]
        {
            "R",
            "Reply"
        };

        public override string Help => Translate("Reply_Help");

        public override string Usage => Commands[0] + Translate("Reply_Usage");

        public override string DefaultPermission => "useressentials.commands.reply";

        public override EAllowedServerState AllowedServerState => EAllowedServerState.RUNNING;

        public override EAllowedCaller AllowedCaller => EAllowedCaller.PLAYER;
        
        public override int MinimumParams => 1;
        #endregion

        public override void Execute(PointBlankPlayer Executor, string[] Arguments)
        {
            if(!Executor.Metadata.ContainsKey("LastPM"))
            {
                UnturnedChat.SendMessage(Executor, Translate("Reply_NoPlayer"));
                return;
            }
            UnturnedPlayer Player = (UnturnedPlayer)Executor.Metadata["LastPM"];
            
            if (UnturnedPlayer.IsServer(Player) || !UnturnedPlayer.IsInServer(Player))
            {
                Executor.Metadata.Remove("LastPM");
                UnturnedChat.SendMessage(Executor, Translate("Reply_Left"));
                return;
            }
            
            UnturnedChat.SendMessage(Player, Executor.Get<UnturnedPlayer>() + ": " + Arguments[0]);
            UnturnedChat.SendMessage(Executor, Translate("Reply_Sent", Player));
            if (Player.Metadata.ContainsKey("LastPM"))
                Player.Metadata["LastPM"] = Executor.Get<UnturnedPlayer>();
            else
                Player.Metadata.Add("LastPM", Executor.Get<UnturnedPlayer>());
        }
    }
}