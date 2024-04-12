using Exiled.API.Features;
using Exiled.API.Features.Pickups;
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

            if (!attacker.TryGetSessionVariable("Kills", out int kills))
            {
                attacker.SessionVariables.Add("Kills", 1);
                return;
            }

            kills++;
            attacker.SessionVariables["Kills"] = kills;

            if (kills % config.EveryKillAmountBroadcast == 0)
            {
                string message = config.KillStreakBroadcastMessage
                    .Replace("%attacker%", attacker.DisplayNickname)
                    .Replace("%victim%", victim.DisplayNickname)
                    .Replace("%kills%", kills.ToString());

                Map.Broadcast(config.KillstreakBroadcastDuration, message, default, true);
            }


            if (kills % config.EveryKillAmountBigReward == 0 && config.KillStreakLargeRewards)
            {
                foreach (ItemType itemType in config.BigRewardItems)
                {
                    if (attacker.IsInventoryFull)
                        Pickup.CreateAndSpawn(itemType, attacker.Position, attacker.Rotation);
                    else
                        attacker.AddItem(itemType);
                }

                string message = config.KillStreakRewardMessage
                    .Replace("%amount%", config.BigRewardItems.Count.ToString());

                attacker.Broadcast(config.KillstreakBroadcastDuration, message, default, true);
            } else if (kills % config.EveryKillAmountSmallReward == 0 && config.KillStreakSmallRewards)
            {
                foreach (ItemType itemType in config.SmallRewardItems)
                {
                    if (attacker.IsInventoryFull)
                        Pickup.CreateAndSpawn(itemType, attacker.Position, attacker.Rotation);
                    else
                        attacker.AddItem(itemType);
                }

                string message = config.KillStreakRewardMessage
                    .Replace("%amount%", config.SmallRewardItems.Count.ToString());

                attacker.Broadcast(config.KillstreakBroadcastDuration, message, default, true);
            }
        }
    }


}
