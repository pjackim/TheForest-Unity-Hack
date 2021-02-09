using UnityEngine;

namespace ForestCheat
{
    class ParadoxFramework
    {
        public static GameObject cheatObj = null;
        static void LoadCheat()
        {
            cheatObj = new GameObject();
            cheatObj.AddComponent<ForestHack>();
            Object.DontDestroyOnLoad(cheatObj);
        }
    }
}

