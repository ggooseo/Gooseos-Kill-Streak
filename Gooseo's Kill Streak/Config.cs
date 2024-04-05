using Exiled.API.Interfaces;

namespace gooseoskillstreak
{
    public class Config : IConfig
    {
        public ushort BroadcastEveryAmountKill { get; set; } = 5;
        public ushort BroadcastDuration { get; set; } = 3;
        public string KillStreakMessage { get; set; } = "<color=yellow>%attacker%</color> just killed <color=blue>%victim%</color> and is now on a <color=red>%kills%</color> kill streak!";

        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
