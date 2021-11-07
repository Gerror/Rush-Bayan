using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Mechanics.Tower.Attack
{
    public interface ITowerAttack
    {
        public GameObject GetTargetMob();
    }
}