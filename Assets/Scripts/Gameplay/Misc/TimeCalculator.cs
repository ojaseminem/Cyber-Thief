using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.Misc
{
    public class TimeCalculator : MonoBehaviour
    {
        public static TimeCalculator Instance;
        private void Awake() => Instance = this;

        [HideInInspector]
        public float elapsedTime;
        private bool _timerGoing;
        private TimeSpan _timeSpan;

        public void BeginTimer()
        {
            _timerGoing = true;
            elapsedTime = 0f;

            StartCoroutine(UpdateTimer());
        }

        public void EndTimer()
        {
            _timerGoing = false;
        }
        
        private IEnumerator UpdateTimer()
        {
            while (_timerGoing)
            {
                elapsedTime += Time.deltaTime;
                
                yield return null;
            }
        }
    }
}