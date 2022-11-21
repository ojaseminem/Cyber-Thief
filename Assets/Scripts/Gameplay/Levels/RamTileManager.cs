using UnityEngine;

namespace Gameplay.Levels
{
    public class RamTileManager : MonoBehaviour
    {
        [SerializeField] private RamTileHandler[] ramTiles;
        public void RandomRamTile()
        {
            DisableAll();
            ramTiles[RandomRamTileNumber()].gameObject.SetActive(true);
        }

        private int RandomRamTileNumber()
        {
            var rand = Random.Range(0, ramTiles.Length);
            return rand;
        }
        
        private void DisableAll()
        {
            foreach (var ramTile in ramTiles)
            {
                ramTile.gameObject.SetActive(false);
            }
        }
    }
}
