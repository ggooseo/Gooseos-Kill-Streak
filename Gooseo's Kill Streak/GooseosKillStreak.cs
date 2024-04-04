using Exiled.API.Features;
using Exiled.Events.Handlers;
using System;
using EventHandler = gooseoskillstreak.Events.EventHandler;

namespace gooseoskillstreak
{
    public class GooseosKillStreak : Plugin<Config>
    {
        public override string Prefix => "gsk";
        public override string Name => "Gooseos Kill Streak";
        public override string Author => "Gooseo";
        public override Version Version { get; } = new(1, 0, 0);
        public override Version RequiredExiledVersion => new(8,8,0);

        public static GooseosKillStreak Instance { get; private set; } = null!;

        private protected EventHandler eventHandler = null!;

        public override void OnEnabled()
        {
            Instance = this;
            eventHandler = new();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            eventHandler = null!;
            Instance = null!;

            base.OnDisabled();
        }
    }
}
