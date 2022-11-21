using System;
using Gameplay.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Levels
{
    public class RamTileHandler : MonoBehaviour
    {
        [SerializeField] private GameObject[] coinSlots;
        [SerializeField] private PickUp[] coins;
        private void OnEnable()
        {
            DisableAll();
            coinSlots[RandomCoinSlotNumber()].SetActive(true);
            foreach (var pickUp in coins)
            {
                pickUp.ResetVisibility();
            }
        }

        private void DisableAll()
        {
            foreach (var coinSlot in coinSlots)
            {
                coinSlot.SetActive(false);
            }
        }
        
        private int RandomCoinSlotNumber()
        {
            var rand = Random.Range(0, coinSlots.Length);
            return rand;
        }
        
        private void OnDisable()
        {
            foreach (var coinSlot in coinSlots)
            {
                coinSlot.gameObject.SetActive(false);
            }
        }
    }
}