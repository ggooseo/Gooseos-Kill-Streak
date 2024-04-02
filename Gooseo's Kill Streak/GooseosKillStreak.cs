﻿using Exiled.API.Features;
using Exiled.Events.Handlers;
using System;
using EventHandler = gooseoskillstreak.Events.EventHandler;
using Player = Exiled.Events.Handlers.Player;

namespace gooseoskillstreak
{
    public class GooseosKillStreak : Plugin<Config>
    {
        public override string Prefix => "coineffect";
        public override string Name => "RandomCoinEffect";
        public override string Author => "Gooseo";
        public override Version Version { get; } = new(1, 0, 0);

        public static GooseosKillStreak Instance { get; private set; } = null!;

        private protected EventHandler eventHandler = null!;

        public override void OnEnabled()
        {
            Instance = this;
            eventHandler = new EventHandler();

            Player.Died += eventHandler.OnPlayerDied;

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