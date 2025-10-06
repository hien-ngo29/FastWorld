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

            

            Log("Initialized");
        }
    }
}