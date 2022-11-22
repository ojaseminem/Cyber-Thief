using Gameplay.Managers;
using PathCreation;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerPathFollower : MonoBehaviour
    {
        [SerializeField] private PathCreator pathCreator;
        private const float Speed = 4;

        [HideInInspector]
        public float distanceTravelled;
        public bool moveOnPath;
        
        private void Update()
        {
            if (!moveOnPath) return;
            distanceTravelled += Speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            if (distanceTravelled >= 17) DonePath();
        }

        private void DonePath()
        {
            moveOnPath = false;
            AudioManager.Instance.PlaySound("Portal");
            LevelManager.Instance.SwitchLevelScenes(0);
        }
        
        public void ResetPath()
        {
            distanceTravelled = 0;
            moveOnPath = true;
        }
    }
}