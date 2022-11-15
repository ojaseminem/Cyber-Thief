using System;
using System.Collections;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Levels
{
    public class LevelHandler : MonoBehaviour
    {
        public int levelType;

        [Header("Game Level Scene Variables")]
        
        
        [Header("Level Switcher Scene Variables")]
        [SerializeField] private ParticleSystem matrixFx;
        [SerializeField] private PlayerPathFollower playerPathFollower;
        
        private void OnEnable()
        {
            switch (levelType)
            {
                case 0:
                    
                    break;
                case 1:
                    matrixFx.Play(true);
                    playerPathFollower.ResetPath();
                    break;
            }
        }

        private void OnDisable()
        {
            switch (levelType)
            {
                case 0:
                    
                    break;
                case 1:
                    matrixFx.Stop(true);
                    playerPathFollower.moveOnPath = false;
                    break;
            }
        }

        private IEnumerator ChangeLevel()
        {
            yield return new WaitForSeconds(4f);
            LevelManager.Instance.SwitchLevelScenes(0);
        }
    }
}