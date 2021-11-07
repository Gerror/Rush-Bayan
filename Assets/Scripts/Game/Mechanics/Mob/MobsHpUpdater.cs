using System.Collections;
using UnityEngine;
using Game.Core;

namespace Game.Mechanics.Mob
{
    public class MobsHpUpdater : MonoBehaviour
    {
        [Min(300)] [SerializeField] private int _startMobHp;
        private MobSpawnMechanics _mobSpawnMechanics;

        public int StartMobHp
        {
            get => _startMobHp;
        }

        private void Start()
        {
            _mobSpawnMechanics = GetComponent<MobSpawnMechanics>();
            StartCoroutine(UpdateMobHp());
        }

        private IEnumerator UpdateMobHp()
        {
            while (true)
            {
                yield return new WaitForSeconds(GameSettings.MobUpdateInterval);
                _startMobHp += 100;
            }
        }
    }
}