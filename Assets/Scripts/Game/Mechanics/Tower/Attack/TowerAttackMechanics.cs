using System.Collections;
using Game.Core;
using UnityEngine;
using Object = UnityEngine.Object;
using Game.Mechanics.Mob;
using UnityEditor.SceneManagement;
using Zenject;

namespace Game.Mechanics.Tower.Attack
{
    [RequireComponent(typeof(TowerLevels))]
    public class TowerAttackMechanics : TowerAction
    {
        [SerializeField] private ValueProvider _damage;
        [SerializeField] private ValueProvider _attackInterval;
        [SerializeField] private GameObject _bulletPrefab;

        private ITowerAttack _towerAttack;
        private TowerLevels _towerLevels;
        private PrefabFactory _prefabFactory;
        private Transform _tempObjectParent;
        public MobSpawnMechanics MobSpawnMechanics;

        [Inject]
        private void Contruct(PrefabFactory prefabFactory, Transform tempObjectParent)
        {
            _prefabFactory = prefabFactory;
            _tempObjectParent = tempObjectParent;
        }
        
        protected override void Init()
        {
            _towerAttack = GetComponent<ITowerAttack>();
            _towerLevels = GetComponent<TowerLevels>();
            _actionInterval = _attackInterval.GetValue(_towerLevels.Levels);
        }

        protected override void TakeAction()
        {
            if (MobSpawnMechanics.MobOrderedDictionary.Count > 0)
            {
                GameObject mob = _towerAttack.GetTargetMob();

                GameObject bulletGO = _prefabFactory.Spawn(_bulletPrefab, _tempObjectParent);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                bullet.Init(mob, (int) _damage.GetValue(_towerLevels.Levels));
            }
        }
    }
}