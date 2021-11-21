using System.Collections;
using System.Collections.Generic;
using Game.Mechanics.Tower;
using UnityEngine;

namespace Game.Mechanics.Input
{
    [RequireComponent(typeof(TowerSpawnMechanics))]
    [RequireComponent(typeof(ManaMechanics))]
    public abstract class InputMechanics : MonoBehaviour
    {
        [SerializeField] protected TowerSpawnMechanics _towerSpawnMechanics;
        [SerializeField] protected ManaMechanics _manaMechanics;

        public abstract void StartInputMechanics();
    }
}