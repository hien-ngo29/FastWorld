using System.Collections.Generic;
using UnityEngine;

namespace FastWorld
{
    public class HeroAnimationFpsManager
    {
        private AnimationFpsChanger movementAnimation;
        private List<AnimationFpsChanger> spellAnimations;

        public HeroAnimationFpsManager()
        {
            spellAnimations = new List<AnimationFpsChanger>();
            foreach (Transform child in GameObject.Find("Spells").transform)
            {
                Modding.Logger.Log("GameObject: " + child.gameObject.name);
                if (child.gameObject.GetComponent<tk2dSpriteAnimator>() != null)
                    spellAnimations.Add(new AnimationFpsChanger(child.gameObject));
            }

            movementAnimation = new("Knight");

            ReloadFps();
        }

        public void ReloadFps()
        {
            movementAnimation.ReloadFps();
            foreach (var animation in spellAnimations)
            {
                animation.ReloadFps();
            }
        }
    }
}