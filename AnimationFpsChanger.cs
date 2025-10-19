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
            gameObject = GameObject.Find(objectName);
            defaultAnimationClipFps = GetCurrentAnimationClipFps();
        }

        public AnimationFpsChanger(GameObject gameObject)
        {
            this.gameObject = gameObject;
            defaultAnimationClipFps = GetCurrentAnimationClipFps();
        }

        public void ReloadFps()
        {
            var animator = gameObject.GetComponent<tk2dSpriteAnimator>();
            if (animator != null)
            {
                foreach (var animationClipFps in defaultAnimationClipFps)
                {
                    animator.GetClipByName(animationClipFps.Key).fps = animationClipFps.Value / Time.timeScale;
                }
            }
        }

        public Dictionary<string, float> GetCurrentAnimationClipFps()
        {
            Dictionary<string, float> clipFps = new();
            var animator = gameObject.GetComponent<tk2dSpriteAnimator>();

            foreach (var clip in animator.Library.clips)
            {
                if (!clipFps.ContainsKey(clip.name))
                {
                    Modding.Logger.Log("Found: " + clip.name);
                    clipFps.Add(clip.name, clip.fps);
                }
            }
            return clipFps;
        }
    }
}