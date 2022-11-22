using System;
using System.Collections;
using Gameplay.MenuItems;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Gameplay.Managers
{
    public class MenuManager : MonoBehaviour
    {
        //Menu Variables
        [SerializeField] private TextMeshProUGUI ditcoinsText;
        [SerializeField] private TextMeshProUGUI vethereumText;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI timePlayedText;
        [SerializeField] private MenuItemHandler[] menuItems;

        [SerializeField] private Slider volumeSlider;

        //Character Variables
        [SerializeField] private Transform character;
        [SerializeField] private Transform endPoint;
        [SerializeField] private Animator animCharacter;
        [SerializeField] private float speed;
        private bool _charCanMove;
        
        private static readonly int RunTrigger = Animator.StringToHash("RunTrigger");

        private void Start()
        {
            RefreshSettings();
            RefreshStats();
            
            var rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    AudioManager.Instance.PlaySound("BG_1");
                    break;
                case 1:
                    AudioManager.Instance.PlaySound("BG_2");
                    break;
                case 2:
                    AudioManager.Instance.PlaySound("BG_3");
                    break;
                case 3:
                    AudioManager.Instance.PlaySound("BG_4");
                    break;
            }
        }

        public void Play()
        {
            StartCoroutine(PlayRoutine());
            IEnumerator PlayRoutine()
            {
                AudioManager.Instance.PlaySound("Click");
                MoveMenuItems();
                void MoveMenuItems()
                {
                    foreach (var menuItemHandler in menuItems)
                    {
                        menuItemHandler.Move();
                    }
                }

                yield return new WaitForSeconds(2f);
                _charCanMove = true;
                animCharacter.SetTrigger(RunTrigger);
                yield return new WaitForSeconds(3f);
                AudioManager.Instance.PauseSound("BG_1");
                AudioManager.Instance.PauseSound("BG_2");
                AudioManager.Instance.PauseSound("BG_3");
                AudioManager.Instance.PauseSound("BG_4");
                AudioManager.Instance.PlaySound("Portal");
                yield return new WaitForSeconds(.5f);
                SceneManager.LoadScene("GameScene");
            }
        }
        
        private void Update()
        {
            if (_charCanMove)
            {
                character.position = Vector3.MoveTowards(character.position, endPoint.position,
                    speed * Time.deltaTime);
            }
        }

        public void RefreshStats()
        {
            SaveLoadManager.LoadGame();
            ditcoinsText.text = SaveLoadManager.CurrentSaveData.ditcoin.ToString();
            vethereumText.text = SaveLoadManager.CurrentSaveData.vethereum.ToString();
            highScoreText.text = SaveLoadManager.CurrentSaveData.highScore.ToString();
            var elapsedTime = SaveLoadManager.CurrentSaveData.timePlayed;
            var timeSpan = TimeSpan.FromSeconds(elapsedTime);
            timePlayedText.text = timeSpan.ToString("mm' : 'ss' . 'ff");
        }
        
        public void RefreshSettings()
        {
            SaveLoadManager.LoadGame();
            volumeSlider.value = SaveLoadManager.CurrentSaveData.volume;
        }

        public void UpdateVolume()
        {
            AudioListener.volume = volumeSlider.value;
            SaveLoadManager.CurrentSaveData.volume = volumeSlider.value;
            SaveLoadManager.SaveGame();
        }
    }
}