using System;
using DG.Tweening;
using Gameplay.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Device.Application;

namespace Gameplay.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance;
        private void Awake() => Instance = this;

        #endregion

        #region Menu Variables

        [SerializeField] private GameObject hudMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject gameOverMenu;

        #endregion

        #region Game UI Variables

        [HideInInspector]
        public int ditcoins;
        [HideInInspector]
        public int vethereum;
        
        [SerializeField] private TextMeshProUGUI ditcoinsText;
        [SerializeField] private TextMeshProUGUI vethereumText;

        public static Action IncrementDitcoins;
        public static Action IncrementVethereum;

        public static bool PlayerDead;
        
        #endregion
        
        #region Fade In / Out Variables

        [Header("Fade In / Out")]
        [SerializeField] private GameObject fade;
        [SerializeField] private GameObject fadeBlack;
        [SerializeField] private ParticleSystem matrixCode;

        #endregion

        private void OnEnable()
        {
            IncrementDitcoins += IncrementDitcoinCount;
            IncrementVethereum += IncrementVethereumCount;
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
            ditcoinsText.text = "0";
            vethereumText.text = "0";
            PlayerManager.Instance.PlayerSpawn();
            Time.timeScale = 1;
            TimeCalculator.Instance.BeginTimer();
            PlayerDead = false;
        }

        public void Replay()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void GoToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MenuScene");
        }
        
        public void PauseGame()
        {
            Time.timeScale = 0;
            hudMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            hudMenu.SetActive(true);
            pauseMenu.SetActive(false);
        }
        
        public void GameOver()
        {
            hudMenu.SetActive(false);
            if (SaveLoadManager.CurrentSaveData.highScore <= LevelManager.Instance.gameLevel)
                SaveLoadManager.CurrentSaveData.highScore = LevelManager.Instance.gameLevel;
            SaveLoadManager.CurrentSaveData.ditcoin += ditcoins;
            SaveLoadManager.CurrentSaveData.vethereum += vethereum;
            SaveLoadManager.CurrentSaveData.timePlayed += TimeCalculator.Instance.elapsedTime;
            SaveLoadManager.SaveGame();
            FadeIn();
        }

        private void ShowGameOverMenu()
        {
            gameOverMenu.SetActive(true);
        }

        private void IncrementDitcoinCount()
        {
            ditcoins++;
            ditcoinsText.text = ditcoins.ToString();
        }

        private void IncrementVethereumCount()
        {
            vethereum++;
            vethereumText.text = vethereum.ToString();
        }
        
        #region Fade In

        private void FadeIn()
        {
            fade.SetActive(true);
            fadeBlack.GetComponent<Animator>().enabled = true;
            matrixCode.Play(true);
            Invoke(nameof(ShowGameOverMenu), 2f);
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