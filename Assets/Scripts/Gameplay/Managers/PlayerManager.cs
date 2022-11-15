using System;
using UnityEngine;

namespace Gameplay.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Singleton

        public static PlayerManager Instance;
        private void Awake() => Instance = this;

        #endregion

        [SerializeField] private Transform playerSpawnPosition;
        
        public void PlayerSpawn()
        {
            // todo - play spawn animation or play spawn sound or play some spawn fx or play spawn cinematic
            //todo - call game manager change camera position again
            //todo -                HasGameStarted = true;
        }
    }
}