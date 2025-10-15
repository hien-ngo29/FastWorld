using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace FastWorld
{
    internal class FastWorld : Mod
    {
        public FastWorld() : base("FastWorld") { }

        public override string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public override void Initialize()
        {
            Log("Initializing");

            On.HeroController.Start += HeroAwake;

            Log("Initialized");
        }

        private void HeroAwake(On.HeroController.orig_Start orig, HeroController self)
        {
            orig(self);
            if (self.gameObject.GetComponent<KnightSpeedScaler>() == null)
            {
                KnightSpeedScaler speedScaler = self.gameObject.AddComponent<KnightSpeedScaler>();
            }
        }
    }
}