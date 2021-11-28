using System;
using System.Collections;
using Game.Core;
using UnityEngine;
using Object = UnityEngine.Object;
using Game.Mechanics.Mob;
using Zenject;

namespace Game.Mechanics.Tower.Attack
{
    [RequireComponent(typeof(TowerLevels))]
    public class TowerAttackMechanics : MonoBehaviour
    {
        [SerializeField] private ValueProvider _damage;
        [SerializeField] private ValueProvider _attackInterval;
        [SerializeField] private GameObject _bulletPrefab;

        private ITowerAttack _towerAttack;
        private TowerLevels _towerLevels;
        public MobSpawnMechanics MobSpawnMechanics;

        private IEnumerator Attack()
        {
            if (MobSpawnMechanics.MobOrderedDictionary.Count > 0)
            {
                AttackMob(_towerAttack.GetTargetMob());
            }

            while (true)
            {
                yield return new WaitForSeconds(_attackInterval.GetValue(_towerLevels.Levels));
                if (MobSpawnMechanics.MobOrderedDictionary.Count > 0)
                {
                    AttackMob(_towerAttack.GetTargetMob());
                }
            }
        }

        private void AttackMob(GameObject mob)
        {
            if (!mob)
                return;

            GameObject bulletGO = Object.Instantiate(_bulletPrefab, transform);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.Init(mob, (int) _damage.GetValue(_towerLevels.Levels));
        }

        private void Start()
        {
            _towerAttack = GetComponent<ITowerAttack>();
            _towerLevels = GetComponent<TowerLevels>();
            StartCoroutine(Attack());
        }
    }
}