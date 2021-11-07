using UnityEngine;
using Game.Mechanics.Mob;
using Game.Core;

namespace Game.Mechanics
{
    public class TowerOwnerMechanics : MonoBehaviour
    {
        [SerializeField] private MobSpawnMechanics _mobSpawnMechanics;
        [SerializeField] private GameObject[] _towerPrefabs = new GameObject[GameSettings.NumberOfTypeOfTower];

        public MobSpawnMechanics MobSpawnMechanics
        {
            get => _mobSpawnMechanics;
        }
        
        public GameObject[] TowerPrefab
        {
            get => _towerPrefabs;
        }
    }
}
