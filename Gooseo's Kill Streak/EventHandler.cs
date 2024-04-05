using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerHandler = Exiled.Events.Handlers.Player;

namespace gooseoskillstreak.Events
{
    public class EventHandler
    {
        private protected Config config = GooseosKillStreak.Instance.Config;

        public EventHandler()
        {
            PlayerHandler.Died += OnPlayerDied;
            PlayerHandler.Left += OnPlayerLeft;
        }

        ~EventHandler()
        {
            PlayerHandler.Died -= OnPlayerDied;
            PlayerHandler.Left -= OnPlayerLeft;
        }

        public void OnPlayerLeft(LeftEventArgs ev)
        {
            var playersSessionVars = ev.Player.SessionVariables;

            if (playersSessionVars.ContainsKey("Kills"))
                playersSessionVars.Remove("Kills");
            
        }
        public void OnPlayerDied(DiedEventArgs ev)
        {
            Player attacker = ev?.Attacker;
            Player victim = ev?.Player;

            if (victim != null && victim.SessionVariables?.ContainsKey("Kills") == true)
            {
                victim.SessionVariables.Remove("Kills");
            }

            if (attacker == null)
                return;

            if (!attacker.SessionVariables.TryGetValue("Kills", out var killsObj) || !(killsObj is int kills))
            {
                attacker.SessionVariables["Kills"] = 1;
                return;
            }

            kills++;
            attacker.SessionVariables["Kills"] = kills;

            if (kills % config.BroadcastEveryAmountKill == 0)
            {
                string message = config.KillStreakMessage
                    .Replace("%attacker%", attacker.DisplayNickname)
                    .Replace("%victim%", victim.DisplayNickname)
                    .Replace("%kills%", kills.ToString());

                Map.Broadcast(config.BroadcastDuration, message, default, true);
            }
        }
    }
}
