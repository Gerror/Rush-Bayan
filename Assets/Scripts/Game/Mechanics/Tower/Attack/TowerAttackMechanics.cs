using Game.Core;
using UnityEngine;
using Game.Mechanics.Mob;
using Zenject;

namespace Game.Mechanics.Tower.Attack
{
    [RequireComponent(typeof(TowerLevels))]
    public class TowerAttackMechanics : TowerAction
    {
        [SerializeField] private ValueProvider _damage;
        [SerializeField] private ValueProvider _attackInterval;
        [SerializeField] private GameObject _bulletPrefab;

        private TargetMobProvider _targetMobProvider;
        private PrefabFactory _prefabFactory;
        private Transform _tempObjectParent;

        [Inject]
        private void Contruct(PrefabFactory prefabFactory, Transform tempObjectParent)
        {
            _prefabFactory = prefabFactory;
            _tempObjectParent = tempObjectParent;
        }
        
        protected override void Init()
        {
            _targetMobProvider = GetComponent<TargetMobProvider>();
            _actionInterval = _attackInterval.GetValue(_tower.TowerLevels.Levels);
        }

        protected override void TakeAction()
        {
            if (_tower.MobSpawnMechanics.MobOrderedDictionary.Count > 0)
            {
                GameObject mob = _targetMobProvider.GetTargetMob();
                
                GameObject bulletGO = _prefabFactory.Spawn(_bulletPrefab, transform.position,
                    _bulletPrefab.transform.rotation, _tempObjectParent);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                bullet.Init(mob, _damage, _tower);
            }
        }
    }
}