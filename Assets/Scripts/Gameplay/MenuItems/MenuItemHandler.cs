using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.MenuItems
{
    public class MenuItemHandler : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private float endValue;
        
        public void Move()
        {
            transform.DOLocalMoveY(endValue, duration).SetEase(Ease.InOutBounce);
        }
    }
}