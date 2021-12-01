using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Helpers
{
    public class TempObjectCleaner : MonoBehaviour
    {
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _gameManager.EndGameEvent += CleanTempObjects;
        }

        private void CleanTempObjects()
        {
            foreach (Transform tempObj in transform)
            {
                Destroy(tempObj.gameObject);
            }
        }
    }
}