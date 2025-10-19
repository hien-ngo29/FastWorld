using System.Collections.Generic;
using UnityEngine;

namespace FastWorld
{
    public class HeroAnimationFpsManager
    {
        private AnimationFpsChanger movementAnimation;
        private AnimationGroupFpsChanger spellAnimationGroupFspChanger;
        private AnimationGroupFpsChanger effectsAnimationGroupFspChanger;

        public HeroAnimationFpsManager()
        {
            spellAnimationGroupFspChanger = new("Spells");
            effectsAnimationGroupFspChanger = new("Effects");
            movementAnimation = new("Knight");

            ReloadFps();
        }

        public void ReloadFps()
        {
            movementAnimation.ReloadFps();
            spellAnimationGroupFspChanger.ReloadFps();
            effectsAnimationGroupFspChanger.ReloadFps();
        }
    }
}