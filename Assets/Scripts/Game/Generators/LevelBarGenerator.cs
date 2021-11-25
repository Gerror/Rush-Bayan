using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.UI;

namespace Game.Mechanics
{
    public class LevelBarGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _levelUiPrefab;
        [SerializeField] private TowerOwner towerOwner;
        private List<LevelUpButton> _levelUpButtons;

        private void Awake()
        {
            _levelUpButtons = new List<LevelUpButton>();
            for (int i = 0; i < GameSettings.NumberOfTypeOfTower; i++)
            {
                GameObject go = Object.Instantiate(_levelUiPrefab, transform);
                
                LevelUpButton levelUpButton = go.GetComponent<LevelUpButton>();
                if (levelUpButton)
                {
                    levelUpButton.ButtonId = i;
                    _levelUpButtons.Add(levelUpButton);
                }

                LevelScreen levelScreen = go.GetComponentInChildren<LevelScreen>();
                if (levelScreen)
                {
                    levelScreen.SetImageSprite(towerOwner.TowerConfigs[i].Sprite);
                }
            }
        }
    }
}