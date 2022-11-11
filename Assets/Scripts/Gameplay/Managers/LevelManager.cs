using System;
using Scriptable_Objects;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [Header("Level Data")]
    [SerializeField] private LevelData levelData;

    public int gameLevel;


    [Header("Level Variables")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private TextMeshPro levelCounter;
    [SerializeField] private MeshRenderer baseEnv;
    [SerializeField] private MeshRenderer bgRGB;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeLevel();
        }
    }

    public void ChangeLevel()
    {
        var randomLevelColor = (LevelColor)Random.Range(0, 5);
        
        switch (randomLevelColor)
        {
            case LevelColor.Red:
                baseEnv.material = levelData.levelDataHolders[0].baseMaterial;
                bgRGB.material = levelData.levelDataHolders[0].bgRGBMaterial;
                directionalLight.color = levelData.levelDataHolders[0].color;
                levelCounter.colorGradientPreset = levelData.levelDataHolders[0].colorGradient;
                break;
            case LevelColor.Green:
                baseEnv.material = levelData.levelDataHolders[1].baseMaterial;
                bgRGB.material = levelData.levelDataHolders[1].bgRGBMaterial;
                directionalLight.color = levelData.levelDataHolders[1].color;
                levelCounter.colorGradientPreset = levelData.levelDataHolders[1].colorGradient;
                break;
            case LevelColor.Blue:
                baseEnv.material = levelData.levelDataHolders[2].baseMaterial;
                bgRGB.material = levelData.levelDataHolders[2].bgRGBMaterial;
                directionalLight.color = levelData.levelDataHolders[2].color;
                levelCounter.colorGradientPreset = levelData.levelDataHolders[2].colorGradient;
                break;
            case LevelColor.Gray:
                baseEnv.material = levelData.levelDataHolders[3].baseMaterial;
                bgRGB.material = levelData.levelDataHolders[3].bgRGBMaterial;
                directionalLight.color = levelData.levelDataHolders[3].color;
                levelCounter.colorGradientPreset = levelData.levelDataHolders[3].colorGradient;
                break;
            case LevelColor.Purple:
                baseEnv.material = levelData.levelDataHolders[4].baseMaterial;
                bgRGB.material = levelData.levelDataHolders[4].bgRGBMaterial;
                directionalLight.color = levelData.levelDataHolders[4].color;
                levelCounter.colorGradientPreset = levelData.levelDataHolders[4].colorGradient;
                break;
        }
    }
}