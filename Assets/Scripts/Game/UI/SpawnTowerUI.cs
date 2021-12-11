using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SpawnTowerUI : MonoBehaviour
    {
        [SerializeField] private Text _spawnPriceLabel;
        
        public event Action SpawnTowerEvent;

        public void SpawnTower()
        {
            SpawnTowerEvent?.Invoke();
        }

        public void SetPrice(int price)
        {
            _spawnPriceLabel.text = price.ToString();
        }
    }
}