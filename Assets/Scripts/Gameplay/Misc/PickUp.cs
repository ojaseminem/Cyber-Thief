using Gameplay.Levels;
using Gameplay.Managers;
using UnityEngine;

namespace Gameplay.Misc
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] private GameObject pickupEffect;
        [SerializeField] private int dtcOrVeth;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Pickup();
            }
        }

        private void Pickup()
        {
            TurnOffVisibility();

            if (dtcOrVeth == 0)
            {
                GameManager.IncrementDitcoins?.Invoke();
                AudioManager.Instance.PlaySound("Ditcoin");
            }
            else if(dtcOrVeth == 1)
            {
                GameManager.IncrementVethereum?.Invoke();
                AudioManager.Instance.PlaySound("Vethereum");
            }

            var pickedUpEffect = Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(pickedUpEffect, 2f);
        }

        private void TurnOffVisibility()
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            transform.GetComponent<ParticleSystem>().Stop(true);
        }

        public void ResetVisibility()
        {
            transform.GetComponent<BoxCollider>().enabled = true;
            transform.GetComponent<ParticleSystem>().Play(true);
        }
    }
}
