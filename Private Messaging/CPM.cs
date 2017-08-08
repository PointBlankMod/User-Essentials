using PointBlank.API.Commands;
using PointBlank.API.Unturned.Player;
using PointBlank.API.Unturned.Chat;
using PointBlank.API.Player;    

namespace UserEssentials.PrivateMessaging
{
    [PointBlankCommand("PM", 2)]
    public class CPM : PointBlankCommand
    {
        #region Properties
        
        public override string[] DefaultCommands => new [] { "pm", "tell", "whisper" };

        public override string Help => Main.Instance.Translations["CPM_help"];

        public override string Usage => Commands[0] + Main.Instance.Translations["CPM_usage"];

        public override string DefaultPermission => "privatemessaging.send";

        public override EAllowedServerState AllowedServerState => EAllowedServerState.RUNNING;
        
        #endregion

        public override void Execute(PointBlankPlayer executor, string[] args)
        {
            if (!UnturnedPlayer.TryGetPlayer(args[0], out UnturnedPlayer Player))
            {
                UnturnedChat.SendMessage(executor, Main.Instance.Translate("CPM_goof", args[0]));
                return;
            }
            
            UnturnedChat.SendMessage(Player, args[1]);
            UnturnedChat.SendMessage(executor, Main.Instance.Translate("CPM_sent", Player.CharacterName));
            
            if (!UnturnedPlayer.IsServer(executor))
                Player.Metadata.Add("LastPM", executor);
        }
    }
}