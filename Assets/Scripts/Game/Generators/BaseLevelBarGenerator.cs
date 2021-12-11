using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Mechanics;
using Game.UI;

namespace Game.Generators
{
    public class BaseLevelBarGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _levelUiPrefab;
        [SerializeField] private TowerOwner _towerOwner;
        [SerializeField] private BaseLevelUpController baseLevelUpController;

        private void Awake()
        {
            List<BaseLevelScreen> levelScreens = new List<BaseLevelScreen>();
            for (int i = 0; i < GameSettings.NumberOfTypeOfTower; i++)
            {
                GameObject go = Object.Instantiate(_levelUiPrefab, transform);

                BaseLevelScreen baseLevelScreen = go.GetComponentInChildren<BaseLevelScreen>();
                if (baseLevelScreen)
                {
                    baseLevelScreen.SetImageSprite(_towerOwner.TowerConfigs[i].Sprite);
                    levelScreens.Add(baseLevelScreen);
                }
                
                BaseLevelUpButton baseLevelUpButton = go.GetComponent<BaseLevelUpButton>();
                if (baseLevelUpButton)
                {
                    baseLevelUpButton.ButtonId = i;
                }
            }

            baseLevelUpController.LevelScreens = levelScreens;
        }
    }
}