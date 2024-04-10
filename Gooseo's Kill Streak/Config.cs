using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace gooseoskillstreak
{
    public class Config : IConfig
    {
        public bool KillStreakSmallRewards { get; set; } = true;
        public bool KillStreakLargeRewards { get; set; } = true;

        public ushort EveryKillAmountSmallReward { get; set; } = 5;
        public ushort EveryKillAmountBigReward { get; set; } = 10;

        public List<ItemType> SmallRewardItems { get; set; } = new() { ItemType.Adrenaline, ItemType.Painkillers };
        public List<ItemType> BigRewardItems { get; set; } = new() { ItemType.SCP500, ItemType.Coin, ItemType.Ammo556x45, ItemType.Ammo12gauge, ItemType.Ammo44cal, ItemType.Ammo762x39, ItemType.Ammo9x19 };

        public ushort EveryKillAmountBroadcast { get; set; } = 5;
        public ushort KillstreakBroadcastDuration { get; set; } = 3;

        public string KillStreakBroadcastMessage { get; set; } = "<color=orange>%attacker%</color> just killed <color=orange>%victim%</color> and is now on a <color=red>%kills%</color> kill streak!";
        public string KillStreakRewardMessage { get; set; } = "You have been <color=yellow>rewarded</color> with <color=orange>%amount%</color> items!";

        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
