using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance;
        private void Awake() => Instance = this;

        #endregion

        #region Gameplay Variables

        public static bool HasGameStarted;
        public static bool HasGamePaused;
        public static bool HasGameEnded;

        #endregion
        
        #region Change Camera Position Variables

        [Header("Start Gameplay")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Vector3 cameraDefaultPosition, cameraFocusedPositionEntry, cameraFocusedPositionExit;
        [HideInInspector] public int camPositionPreset;
        #endregion
        
        #region Fade In / Out Variables

        [Header("Fade In / Out")]
        [SerializeField] private Transform fadeBlack;
        [SerializeField] private ParticleSystem matrixCode;
        [SerializeField] private float fadeStartPosY;
        [SerializeField] private float fadeEndPosY;

        private Tween _fadeInMoveIn;
        private Tween _fadeOutMoveOut;

        #endregion

        private void Start()
        {
            //LevelManager.Instance.SwitchLevelScenes(1);
            //LevelManager.Instance.ChangeLevel();
            //ScoreManager.Instance.ResetScore();
        }

        #region Change Camera Position

        public void ChangeCameraPosition()
        {
            if (!HasGameStarted)
            {
                mainCamera.transform.DOMove(cameraFocusedPositionEntry, 1f);
                PlayerManager.Instance.PlayerSpawn();
            }
            else
            {
                mainCamera.transform.DOMove(cameraDefaultPosition, 1f);
            }
        }

        #endregion

        #region Fade In / Out

        public void FadeIn()
        {
            var main = matrixCode.main;
            main.loop = true;
            matrixCode.Play(true);

            _fadeInMoveIn.Kill();
            _fadeOutMoveOut.Kill();
            var fadeBlackPosition = fadeBlack.position;
            fadeBlackPosition = new Vector3(fadeBlackPosition.x, fadeStartPosY, fadeBlackPosition.z);
            fadeBlack.position = fadeBlackPosition;
            
            MoveIn();
        }

        private void MoveIn()
        {
            _fadeInMoveIn = fadeBlack.DOMoveY(0, 5);
        }
        
        public void FadeOut()
        {
            var main = matrixCode.main;
            main.loop = false;
            matrixCode.Play(true);
            
            _fadeInMoveIn.Kill();
            _fadeOutMoveOut.Kill();
            var fadeBlackPosition = fadeBlack.position;
            fadeBlackPosition = new Vector3(fadeBlackPosition.x, 0, fadeBlackPosition.z);
            fadeBlack.position = fadeBlackPosition;

            Invoke(nameof(MoveOut), 2f);
        }
        
        private void MoveOut()
        {
            _fadeOutMoveOut = fadeBlack.DOMoveY(fadeEndPosY, 3)
                .OnComplete(ChangeCameraPosition);
        }

        #endregion
        
        #region Save And Load

        public void SaveGame()
        {
            SaveLoadManager.SaveGame();
        }

        public void LoadGame()
        {
            SaveLoadManager.LoadGame();
        }

        #endregion
    }
}