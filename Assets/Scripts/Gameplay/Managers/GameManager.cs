using System;
using DG.Tweening;
using Gameplay.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Device.Application;
using Random = UnityEngine.Random;

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
        [SerializeField] private GameObject tutorialMenu;

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
        public static bool TutorialCompleted;
        
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
            SaveLoadManager.LoadGame();
            TutorialCompleted = SaveLoadManager.CurrentSaveData.tutorialCompleted;

            if (!TutorialCompleted)
            {
                StartTutorial();
            }
            
            var rand = Random.Range(0, 3);
            switch (rand)
            {
                case 0:
                    AudioManager.Instance.PlaySound("BG");
                    break;
                case 1:
                    AudioManager.Instance.PlaySound("BG_2");
                    break;
                case 2:
                    AudioManager.Instance.PlaySound("BG_3");
                    break;
            }
        }

        private void StartTutorial()
        {
            Time.timeScale = 0f;
            tutorialMenu.SetActive(true);
        }

        public void TutorialCompletedSuccessfully()
        {
            Time.timeScale = 1f;
            tutorialMenu.SetActive(false);
            TutorialCompleted = true;
            SaveLoadManager.CurrentSaveData.tutorialCompleted = TutorialCompleted;
            SaveLoadManager.SaveGame();
        }
        
        public void Replay()
        {
            AudioManager.Instance.PlaySound("Click");
            SceneManager.LoadScene("GameScene");
        }
        
        public void GoToMenu()
        {
            Time.timeScale = 1;
            AudioManager.Instance.PlaySound("Click");
            SceneManager.LoadScene("MenuScene");
        }
        
        public void PauseGame()
        {
            Time.timeScale = 0;
            AudioManager.Instance.PlaySound("Pause");
            hudMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            AudioManager.Instance.PlaySound("Click");
            hudMenu.SetActive(true);
            pauseMenu.SetActive(false);
        }
        
        public void GameOver()
        {
            hudMenu.SetActive(false);
            var curr = SaveLoadManager.CurrentSaveData;
            curr.ditcoin += ditcoins;
            curr.vethereum += vethereum;
            curr.timePlayed += TimeCalculator.Instance.elapsedTime;
            if (curr.highScore <= LevelManager.Instance.gameLevel) curr.highScore = LevelManager.Instance.gameLevel;
            SaveLoadManager.SaveGame();

            var rand = Random.Range(0, 2);
            switch (rand)
            {
                case 0:
                    AudioManager.Instance.PlaySound("GameOver");
                    break;
                case 1:
                    AudioManager.Instance.PlaySound("GameOver_2");
                    break;
            }

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
    }
}