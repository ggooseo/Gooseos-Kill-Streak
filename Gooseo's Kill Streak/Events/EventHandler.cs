using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;

namespace gooseoskillstreak.Events
{
    public class EventHandler
    {
        public void OnPlayerDied(DiedEventArgs ev)
        {
            if (ev.Attacker == ev.Player || ev.Attacker.IsHost)
                return;

            if (!ev.Player.SessionVariables.ContainsKey("KillStreak"))
            {
                ev.Player.SessionVariables.Add("KillStreak", 1);
            }
            else
            {
                ev.Player.SessionVariables[ev.Player.RawUserId] = 
                switch (killstreak[ev.Attacker.Id])
                {
                    case 5:
                        ev.Killer.Broadcast(5, $"<color=red>Du hast eine Killstreak von <color=yellow>{killstreak[ev.Killer.Id]}</color> Kills!</color>" +
                            $"\n<i><color=#6d04f6>Du bist im Blutrausch!</color></i>", Broadcast.BroadcastFlags.Normal);
                        break;
                    case 10:
                        ev.Killer.Broadcast(5, $"<color=red>Du hast eine Killstreak von <color=yellow>{killstreak[ev.Killer.Id]}</color> Kills!</color>" +
                            $"\n<i><color=#6d04f6>DOMINATING!</color></i>", Broadcast.BroadcastFlags.Normal);
                        break;
                }
            }
        }
    }
}
