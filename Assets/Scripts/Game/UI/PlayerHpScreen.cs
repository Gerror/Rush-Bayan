using Game.Core;
using UnityEngine;

namespace Game.UI
{
    public class PlayerHpScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _heartPrefab;
        [SerializeField] private GameObject _damagedHeartPrefab;
        [SerializeField] private bool _reverse = false;
        
        
        public void SetHealth(int health)
        {
            foreach (var child in transform)
            {
                Destroy((GameObject) child);
            }

            if (!_reverse)
            {
                ViewPrefabs(GameSettings.MaxPlayerHealth - health, _damagedHeartPrefab);
                ViewPrefabs(health, _heartPrefab);
            }
            else
            {
                ViewPrefabs(GameSettings.MaxPlayerHealth - health, _heartPrefab);
                ViewPrefabs(health, _damagedHeartPrefab);
            }
        }

        private void ViewPrefabs(int count, GameObject prefab)
        {
            for (int i = 0; i < count; i++)
            {
                Instantiate(prefab, transform);
            }
        }
    }
}