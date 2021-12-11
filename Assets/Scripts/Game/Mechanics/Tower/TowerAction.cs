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
        
        protected abstract void Init();
        protected abstract void TakeAction();

        protected void Start()
        {
            _animator = GetComponent<Animator>();

            Init();
            
            StartCoroutine(ActionCoroutine());
        }
    }
}