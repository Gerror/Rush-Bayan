using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Mechanics.Mob
{
    public class MobMechanics : MonoBehaviour
    {
        private bool idIsSet = false;
        [SerializeField] private int id;

        private MobHP _mobHp;
        private MobMovement _mobMovement;

        public event Action<int, bool> DeadEvent;

        public int Id
        {
            get => id;
            set
            {
                if (!idIsSet)
                {
                    id = value;
                    idIsSet = true;
                }
            }
        }

        private void Start()
        {
            _mobHp = GetComponent<MobHP>();
            _mobMovement = GetComponent<MobMovement>();
            
            _mobHp.DeadEvent += MobDead;
            _mobMovement.EndMovementEvent += MobEndMovement;
        }

        private void MobDead()
        {
            DeadEvent?.Invoke(id, true);
        }

        private void MobEndMovement()
        {
            DeadEvent?.Invoke(id, false);
        }
    }
}
