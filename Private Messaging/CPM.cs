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

        public override string Help => Main.Instance.Translations["CPM_Help"];

        public override string Usage => Commands[0] + Main.Instance.Translations["CPM_Usage"];

        public override string DefaultPermission => "privatemessaging.send";

        public override EAllowedServerState AllowedServerState => EAllowedServerState.RUNNING;
        
        #endregion

        public override void Execute(PointBlankPlayer Executor, string[] Arguments)
        {
            if (!UnturnedPlayer.TryGetPlayer(Arguments[0], out UnturnedPlayer Player))
            {
                UnturnedChat.SendMessage(Executor, Main.Instance.Translate("Player_Goof", Arguments[0]));
                return;
            }
            
            UnturnedChat.SendMessage(Player, Arguments[1]);
            UnturnedChat.SendMessage(Executor, Main.Instance.Translate("CPM_sent", Player.CharacterName));
            
            if (!UnturnedPlayer.IsServer(Executor))
                Player.Metadata.Add("LastPM", Executor);
        }
    }
}