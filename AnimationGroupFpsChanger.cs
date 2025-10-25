using System.Collections.Generic;
using UnityEngine;

namespace FastWorld
{
    public class AnimationGroupFpsChanger
    {
        private GameObject gameObject;
        private List<AnimationFpsChanger> animationGroup;

        public AnimationGroupFpsChanger(string objectName)
        {
            gameObject = GameObjectUtils.GetGameObjectFromName(objectName);

            animationGroup = new List<AnimationFpsChanger>();
            foreach (Transform child in GameObject.Find(objectName).transform)
            {
                if (child.gameObject.GetComponent<tk2dSpriteAnimator>() != null)
                    animationGroup.Add(new AnimationFpsChanger(child.gameObject));
            }
        }

        public AnimationGroupFpsChanger(GameObject gameObject)
        {
            this.gameObject = gameObject;

            animationGroup = new List<AnimationFpsChanger>();
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.GetComponent<tk2dSpriteAnimator>() != null)
                    animationGroup.Add(new AnimationFpsChanger(child.gameObject));
            }
        }

        public void ReloadFps()
        {
            foreach (var animation in animationGroup)
            {
                animation.ReloadFps();
            }
        }
    }
}