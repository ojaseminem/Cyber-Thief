using Gameplay.Levels;
using Gameplay.Managers;
using UnityEngine;

namespace Gameplay.Misc
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] private GameObject pickupEffect;
        [SerializeField] private int btcOrEth;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Pickup();
            }
        }

        private void Pickup()
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            if (btcOrEth == 0)
            {
                GameManager.IncrementDitcoins?.Invoke();
                /*if(PlayerController.instance.canCollectCollectables)
                {
                    PlayerManager.instance.PlusCoin(+1, "+");
                    AudioManager.instance.PlaySound("SFX_CoinCollected");
                }*/
            }
            else if(btcOrEth == 1)
            {
                /*if(PlayerController.instance.canCollectCollectables)
                {
                    PlayerManager.instance.PlusDiamond(+1, "+");
                    AudioManager.instance.PlaySound("SFX_DiamondCollected");
                }*/
            }

            TurnOffVisibility();
            var pickedUpEffect = Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(pickedUpEffect, 2f);
        }

        private void TurnOffVisibility()
        {
            transform.GetComponent<ParticleSystem>().Stop(true);
        }

        public void ResetVisibility()
        {
            transform.GetComponent<BoxCollider>().enabled = true;
            transform.GetComponent<ParticleSystem>().Play(true);
        }
    }
}
