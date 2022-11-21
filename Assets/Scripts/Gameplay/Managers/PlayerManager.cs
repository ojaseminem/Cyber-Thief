using System;
using Gameplay.Player;
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
        [SerializeField] private PlayerController playerController;
        
        public void PlayerSpawn()
        {
            playerController.transform.position = playerSpawnPosition.position;
            playerController.gameObject.SetActive(true);
        }

        public void PlayerCompletedLevel()
        {
            playerController.gameObject.SetActive(false);
        }
    }
}