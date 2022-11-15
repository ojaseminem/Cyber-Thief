using UnityEngine;

namespace Gameplay.Misc
{
    [System.Serializable]
    
    public class SaveData
    {
        public string userName = "Player Zero";
        public int maxGameLevel;

        public int bitCoin;
        public int ethereumCoin;

        public float volume;
    }
}