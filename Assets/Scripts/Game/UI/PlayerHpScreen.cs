using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class PlayerHpScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _heartPrefab;
        [SerializeField] private GameObject _damagedHeartPrefab;
        [SerializeField] private bool _reverse = false;
        
        private GameSettings _gameSettings;
        
        [Inject]
        private void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        
        public void SetHealth(int health)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            if (!_reverse)
            {
                ViewPrefabs(_gameSettings.MaxPlayerHealth - health, _damagedHeartPrefab);
                ViewPrefabs(health, _heartPrefab);
            }
            else
            {
                ViewPrefabs(health, _heartPrefab);
                ViewPrefabs(_gameSettings.MaxPlayerHealth - health, _damagedHeartPrefab);
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