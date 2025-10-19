using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UObject = UnityEngine.Object;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FastWorld
{
    internal class FastWorld : Mod
    {
        // 1 / timescale
        public static float KnightSlowness = 1f;

        public FastWorld() : base("FastWorld") { }

        public override string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public override void Initialize()
        {
            Log("Initializing");

            string sanicSaveFile = Path.Combine(Application.persistentDataPath, "Sanic.GlobalSettings.json");

            if (File.Exists(sanicSaveFile))
            {
                string json = File.ReadAllText(sanicSaveFile);
                JObject obj = JObject.Parse(json);
                float speedMultiplier = (float)obj["SpeedMultiplier"];
                KnightSlowness = 1 / speedMultiplier;
            }

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