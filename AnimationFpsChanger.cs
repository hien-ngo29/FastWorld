using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace FastWorld
{
    public class AnimationFpsChanger
    {
        private GameObject gameObject;

        public Dictionary<string, float> defaultAnimationClipFps;

        public AnimationFpsChanger(string objectName)
        {
            gameObject = GameObjectUtils.GetGameObjectFromName(objectName);
            if (defaultAnimationClipFps == null)
                defaultAnimationClipFps = GetCurrentAnimationClipFps();
        }

        public AnimationFpsChanger(GameObject gameObject)
        {
            this.gameObject = gameObject;
            if (defaultAnimationClipFps == null)
                defaultAnimationClipFps = GetCurrentAnimationClipFps();
        }

        public void ReloadFps()
        {
            var animator = gameObject.GetComponent<tk2dSpriteAnimator>();
            if (animator != null) 
            {
                foreach (var animationClipFps in defaultAnimationClipFps)
                {
                    animator.GetClipByName(animationClipFps.Key).fps = animationClipFps.Value * FastWorld.KnightSlowness;
                }
            }
        }

        public Dictionary<string, float> GetCurrentAnimationClipFps()
        {
            Dictionary<string, float> clipFps = new();
            var animator = gameObject.GetComponent<tk2dSpriteAnimator>();

            if (animator == null)
                return null;

            foreach (var clip in animator.Library.clips)
            {
                if (!clipFps.ContainsKey(clip.name))
                {
                    clipFps.Add(clip.name, clip.fps);
                }
            }
            return clipFps;
        }
    }
}