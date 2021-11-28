using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
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
        
        private static int mobIdCounter = 0;
        
        private List<Vector3> _pointList;
        private OrderedDictionary _mobOrderedDictionary;

        private MobsHpUpdater _mobsHpUpdater;
        
        public List<Vector3> PointList
        {
            get => _pointList;
        }

        public OrderedDictionary MobOrderedDictionary
        {
            get => _mobOrderedDictionary;
        }

        private void Start()
        {
            _pointList = new List<Vector3>();
            _mobOrderedDictionary = new OrderedDictionary();
            _mobsHpUpdater = GetComponent<MobsHpUpdater>();
            
            foreach (Transform pointTransform in _pointParent.transform)
            {
                _pointList.Add(pointTransform.position);
            }

            StartCoroutine(IntervalSpawnMob());
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
            GameObject mobGO = Object.Instantiate(_mobPrefab, _pointList[0], Quaternion.identity);
            mobGO.transform.SetParent(gameObject.transform);
            mobGO.GetComponent<MobMovement>().MobSpawnMechanics = this;
            
            MobMechanics mobMechanics = mobGO.GetComponent<MobMechanics>();
            mobMechanics.DeadEvent += MobDead;
            mobMechanics.Id = mobIdCounter;

            MobHP mobHp = mobGO.GetComponent<MobHP>();
            mobHp.Init(_mobsHpUpdater.StartMobHp);

            _mobOrderedDictionary.Add(mobMechanics.Id.ToString(), mobGO);

            mobIdCounter++;
        }

        private void MobDead(int id, bool mobEndMovement)
        {
            GameObject mobGO = (GameObject)_mobOrderedDictionary[id.ToString()];
            Destroy(mobGO);
            _mobOrderedDictionary.Remove(id.ToString());
            
            if (mobEndMovement)
                _manaMechanics.ChangeMana(_mobReward);
        }
    }
}