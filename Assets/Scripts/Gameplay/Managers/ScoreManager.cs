using System;
using TMPro;
using UnityEngine;

namespace Gameplay.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Singleton

        public static ScoreManager Instance;
        private void Awake() => Instance = this;

        #endregion

        public int bitcoinCount;
        public int ethereumCount;
        
        [SerializeField] private TextMeshProUGUI bitcoinText;
        [SerializeField] private TextMeshProUGUI ethereumText;

        public void ResetScore()
        {
            bitcoinCount = 0;
            ethereumCount = 0;
            bitcoinText.text = bitcoinCount.ToString();
            ethereumText.text = ethereumCount.ToString();
        }
        public void SetScore()
        {
            bitcoinText.text = bitcoinCount.ToString();
            ethereumText.text = ethereumCount.ToString();
        }
    }
}