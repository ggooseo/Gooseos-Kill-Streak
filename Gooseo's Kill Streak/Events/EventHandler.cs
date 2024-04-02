using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;

namespace gooseoskillstreak.Events
{
    public class EventHandler
    {
        public void OnPlayerLeft(LeftEventArgs ev)
        {
            var playersSessionVars = ev.Player.SessionVariables;

            if (playersSessionVars.ContainsKey("Kills"))
                playersSessionVars.Remove("Kills");
            
        }
        public void OnPlayerDied(DiedEventArgs ev)
        {
            if (ev.Attacker.IsHost || ev.Player.IsHost)
                return;

            var attackerSessionVars = ev.Attacker.SessionVariables;

            if ((ev.Attacker == ev.Player || ev.DamageHandler.IsSuicide) && attackerSessionVars.ContainsKey("Kills")) {
                attackerSessionVars.Remove("Kills");
                return;
            }

            if (!attackerSessionVars.ContainsKey("Kills"))
            {
                attackerSessionVars.Add("Kills", 1);
                return;
            }

            int kills = attackerSessionVars.TryGetValue("Kills", out object killsObj) ? (int)killsObj + 1 : 1;
            attackerSessionVars["Kills"] = kills;

            if (kills % 5 == 0)
            {
                Map.Broadcast(2, $"<color=red>{ev.Attacker.DisplayNickname}</color> is on a <color=yellow>{kills}</color> kill streak!", default, true);
            }
        }
    }
}
