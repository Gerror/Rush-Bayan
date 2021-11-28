using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Game.Core;
using UnityEditor.SceneManagement;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Mechanics.Mob
{
    public class MobSpawnMechanics : MonoBehaviour
    {
        [Min(0.1f)] [SerializeField] private float _spawnInterval;
        [SerializeField] private GameObject _mobPrefab;
        [SerializeField] private GameObject _pointParent;
        [SerializeField] private ManaMechanics _manaMechanics;
        [Min(10)] [SerializeField] private int _mobReward;
        
        private static int _mobIdCounter = 0;
        
        private List<Vector3> _pointList;
        private OrderedDictionary _mobOrderedDictionary;
        private MobsHpUpdater _mobsHpUpdater;
        private GameManager _gameManager;
        private PrefabFactory _prefabFactory;

        public event Action MobEndMovementEvent;

        [Inject]
        private void Construct(GameManager gameManager, PrefabFactory prefabFactory)
        {
            _gameManager = gameManager;
            _prefabFactory = prefabFactory;
        }
        
        public List<Vector3> PointList
        {
            get => _pointList;
        }

        public OrderedDictionary MobOrderedDictionary
        {
            get => _mobOrderedDictionary;
        }

        private void Awake()
        {
            _pointList = new List<Vector3>();
            _mobOrderedDictionary = new OrderedDictionary();
            
            _gameManager.StartGameEvent += StartGame;
            _gameManager.EndGameEvent += EndGame;
            
            _mobsHpUpdater = GetComponent<MobsHpUpdater>();
            
            foreach (Transform pointTransform in _pointParent.transform)
            {
                _pointList.Add(pointTransform.position);
            }
        }

        private void StartGame()
        {
            StartCoroutine(IntervalSpawnMob());
        }

        private void EndGame()
        {
            foreach (Transform mob in transform)
            {
                Destroy(mob.gameObject);
            }

            _mobOrderedDictionary.Clear();
            _mobIdCounter = 0;
        }

        private IEnumerator IntervalSpawnMob()
        {
            SpawnMob();
            while (true)
            {
                yield return new WaitForSeconds(_spawnInterval);
                SpawnMob();
            }
        }

        private void SpawnMob()
        {

            GameObject mobGo = _prefabFactory.Spawn(_mobPrefab, _pointList[0], Quaternion.identity, gameObject.transform);
            mobGo.GetComponent<MobMovement>().MobSpawnMechanics = this;
            
            MobMechanics mobMechanics = mobGo.GetComponent<MobMechanics>();
            mobMechanics.DeadEvent += MobDead;
            mobMechanics.Id = _mobIdCounter;

            MobHP mobHp = mobGo.GetComponent<MobHP>();
            mobHp.Init(_mobsHpUpdater.CurrentStartMobHp);

            _mobOrderedDictionary.Add(mobMechanics.Id.ToString(), mobGo);

            _mobIdCounter++;
        }

        private void MobDead(int id, bool mobWasKilled)
        {
            GameObject mobGO = (GameObject)_mobOrderedDictionary[id.ToString()];
            Destroy(mobGO);
            _mobOrderedDictionary.Remove(id.ToString());

            if (mobWasKilled)
                _manaMechanics.ChangeMana(_mobReward);
            else
                MobEndMovementEvent?.Invoke();
        }
    }
}