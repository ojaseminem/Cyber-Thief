using TMPro;
using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Data, LevelData")]
    public class LevelData : ScriptableObject
    {
        public LevelDataHolder[] levelDataHolders;
    }
    
    [System.Serializable]
    public class LevelDataHolder
    {
        public string name;
        public Color color;
        public Material baseMaterial;
        public Material bgRGBMaterial;
        public TMP_ColorGradient colorGradient;
    }
}