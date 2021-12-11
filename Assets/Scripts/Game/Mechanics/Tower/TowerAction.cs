using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Mechanics.Tower
{
    public abstract class TowerAction : MonoBehaviour
    {
        protected Animator _animator;
        protected float _actionInterval;
        protected Tower _tower;
        
        protected IEnumerator ActionCoroutine()
        {
            TakeActionWithTrigger();
            while (true)
            {
                yield return new WaitForSeconds(_actionInterval);
                TakeActionWithTrigger();
            }
        }

        protected void TakeActionWithTrigger()
        {
            _animator.SetTrigger("Action");
            TakeAction();
        }

        protected void PreInit()
        {
            Init();
            StartCoroutine(ActionCoroutine());
        }
        
        protected abstract void Init();
        protected abstract void TakeAction();

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _tower = GetComponent<Tower>();
            
            _tower.InitEvent += PreInit;
        }
    }
}