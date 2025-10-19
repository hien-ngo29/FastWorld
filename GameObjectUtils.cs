using UnityEngine;

namespace FastWorld
{
    public static class GameObjectUtils
    {
        // Find a game object from name including inactive object
        public static GameObject GetGameObjectFromName(string objectName)
        {
            var gameObject = GameObject.Find(objectName);
            if (gameObject != null)
                return gameObject;

            var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (var obj in allObjects)
            {
                if (obj.name == objectName)
                {
                    return obj;
                }
            }
            return null;
        }
    }
}