using System.Collections.Generic;
using Game.Controllers;
using UnityEngine;
using Game.Core;
using Game.Mechanics;
using Game.UI;
using Zenject;

namespace Game.Generators
{
    public class BaseLevelBarGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _levelUiPrefab;
        [SerializeField] private TowerOwner _towerOwner;
        [SerializeField] private BaseLevelUpController baseLevelUpController;

        private GameSettings _gameSettings;
        private GameManager _gameManager;
        private PrefabFactory _prefabFactory;
        
        [Inject]
        private void Construct(GameSettings gameSettings, PrefabFactory prefabFactory, GameManager gameManager)
        {
            _gameSettings = gameSettings;
            _prefabFactory = prefabFactory;
            _gameManager = gameManager;
        }
        
        private void Awake()
        {
            _gameManager.StartGameEvent += StartGame;
            _gameManager.EndGameEvent += EndGame;
        }

        private void StartGame()
        {
            List<BaseLevelScreen> levelScreens = new List<BaseLevelScreen>();
            for (int i = 0; i < _gameSettings.NumberOfTypeOfTower; i++)
            {
                GameObject go = _prefabFactory.Spawn(_levelUiPrefab, transform);
                go.transform.localScale = Vector3.one;
                
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

        private void EndGame()
        {
            foreach (Transform levelScreenTransform in transform)
            {
                Destroy(levelScreenTransform.gameObject);
            }
            baseLevelUpController.LevelScreens.Clear();
        }
    }
}