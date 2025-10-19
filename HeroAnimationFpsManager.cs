using System.Collections.Generic;

namespace FastWorld
{
    public class HeroAnimationFpsManager
    {
        private AnimationFpsChanger movementAnimation;
        private AnimationFpsChanger spellAnimation;

        public HeroAnimationFpsManager()
        {
            movementAnimation = new("Knight");
            movementAnimation.ReloadFps();

            spellAnimation = new ("Q Pillar")
        }
    }
}