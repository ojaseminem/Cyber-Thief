using System;
using System.Collections;
using Gameplay.MenuItems;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            SaveLoadManager.LoadGame();
            RefreshSettings();
            RefreshStats();
        }

        public void Play()
        {
            StartCoroutine(PlayRoutine());
            IEnumerator PlayRoutine()
            {
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
                yield return new WaitForSeconds(4f);
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
            timePlayedText.text = SaveLoadManager.CurrentSaveData.timePlayed;
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