using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Life;
using ModKit.Helper;
using ModKit.Internal;
using ModKit.Interfaces;
using Life.Network;
using System.Diagnostics;
using Mirror;
using Life.DB;

namespace SConnexion
{
    public class Connexion581 : ModKit.ModKit
    {
        private readonly MyEvents _events;

        public Connexion581(IGameAPI api) : base(api)
        {
            PluginInformations = new PluginInformations(AssemblyHelper.GetName(), "4.0.0", "Shape581");
            _events = new MyEvents(api);
        }

        public override void OnPluginInit()
        {
            base.OnPluginInit();
            _events.Init(Nova.server);
            ModKit.Internal.Logger.LogSuccess($"{PluginInformations.SourceName} v{PluginInformations.Version}", "initialisé");
        }
    }
}

public class MyEvents : ModKit.Helper.Events
{
    public MyEvents(IGameAPI api) : base(api)
    {
    }

    public override void OnPlayerSpawnCharacter(Player player)
    {
        Nova.server.SendMessageToAdmins($"<color=#008000>[Connexion581] Un joueur vient de se connecter :");
        Nova.server.SendMessageToAdmins($"<color=#008000>- Pseudo steam : <color=#00FF00>{player.account.username}<color=#008000>.");
        Nova.server.SendMessageToAdmins($"<color=#008000>- Nom et prénom RP : <color=#00FF00>{player.character.Firstname} {player.character.Lastname}<color=#008000>.");
        Nova.server.SendMessageToAdmins($"<color=#008000>- ID : <color=#00FF00>{player.character.Id}<color=#008000>.");

        Logger.LogSuccess("Connexion581", $"[Connexion] Un joueur vient de se connecter :");
        Logger.LogSuccess("Connexion581", $"- Pseudo steam : {player.account.username}.");
        Logger.LogSuccess("Connexion581", $"- Nom et prénom RP : {player.character.Firstname} {player.character.Lastname}.");
        Logger.LogSuccess("Connexion581", $"- ID : {player.character.Id}.");
    }

    private static void PlaySoundForAdmins(Player player, bool isAdmin)
    {
        bool isConect = Mirror.NetworkConnection.LocalConnectionId == 0;


        if (isConect == true)
        {
            if (isAdmin == true)
            {
                player.setup.TargetPlayClaironById(0.2f, Nova.server.config.roleplayConfig.ticketAlertSound);

            }
        } 
    }


    public override void OnPlayerDisconnect(Mirror.NetworkConnection conn)
    {
        Nova.server.SendMessageToAdmins($"<color=#800000>[Connexion581] Le joueur <color=#00FF00>{conn.identity.name} <color=#008000>vient de se déconecter.");
    }

    public override void OnPlayerConnect(Player player)
    {
        Nova.server.SendMessageToAdmins("<color=#008000>[Connexion581] Un joueur est en train de se connecter :");
        Nova.server.SendMessageToAdmins($"<color=#008000>- Pseudo steam : <color=#00FF00>{player.account.username}<color=#008000>.");

        Logger.LogWarning("Connexion581", "Un joueur est en train de se connecter :");
        Logger.LogWarning("Connexion581", $"- Pseudo steam : {player.account.username}.");
        Logger.LogWarning("Connexion581", $"- Steam ID : {player.account.steamId}");

        bool isAdmin = player.IsAdmin;

        if (isAdmin == true)
        {
            Logger.LogWarning("Connexion581", $"Un membres du staff vient de ce connecter (Pseudo steam : {player.account.username}).");
        }
    }

}