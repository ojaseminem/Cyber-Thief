using DG.Tweening;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyWebcamHandler : MonoBehaviour
    {
        private void Start()
        {
            transform.DOLocalMoveY(.45f, .25f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}