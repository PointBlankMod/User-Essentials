using PointBlank.API.Commands;
using PointBlank.API.Unturned.Player;
using PointBlank.API.Unturned.Chat;
using PointBlank.API.Player;    

namespace UserEssentials.Commands
{
    public class PrivateMessage : PointBlankCommand
    {
        #region Properties
        public override string[] DefaultCommands => new string[]
        {
            "PM",
            "Tell",
            "Whisper"
        };

        public override string Help => Translate("PrivateMessage_Help");

        public override string Usage => Commands[0] + Translate("PrivateMessage_Usage");

        public override string DefaultPermission => "useressentials.commands.privatemessage";

        public override EAllowedServerState AllowedServerState => EAllowedServerState.RUNNING;
        
        public override int MinimumParams => 2;
        #endregion

        public override void Execute(PointBlankPlayer Executor, string[] Arguments)
        {
            if (!UnturnedPlayer.TryGetPlayer(Arguments[0], out UnturnedPlayer Player))
            {
                UnturnedChat.SendMessage(Executor, Translate("PlayerNotFound"));
                return;
            }
            
            UnturnedChat.SendMessage(Player, UnturnedPlayer.GetName(Executor) + ": " + Arguments[1]);
            UnturnedChat.SendMessage(Executor, Translate("PrivateMessage_Sent", Player));
            
            if (!UnturnedPlayer.IsServer(Executor))
                Player.Metadata.Add("LastPM", Executor.Get<UnturnedPlayer>());
        }
    }
}