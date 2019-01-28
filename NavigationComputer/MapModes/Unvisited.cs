﻿using System.Collections.Generic;
using BattleTech;
using Harmony;

namespace NavigationComputer.MapModes
{
    public class Unvisited : IMapMode
    {
        private readonly float _dimLevel;

        public Unvisited(float dimLevel = 10f)
        {
            _dimLevel = dimLevel;
        }

        public string Name { get; } = "Unvisited Systems";

        public void Apply(SimGameState simGame)
        {
            var visitedSystems = Traverse.Create(simGame).Field("VisitedStarSystems").GetValue<List<string>>();

            foreach (var system in visitedSystems)
                MapModesUI.DimSystem(system, _dimLevel);
        }

        public void Unapply(SimGameState simGame)
        {
        }
    }
}
