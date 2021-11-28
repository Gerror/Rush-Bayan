using Game.Core;
using Game.Mechanics;
using UnityEngine;
using Zenject;

namespace Game.Controllers
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] private MainUiController _mainUiController;
        [SerializeField] private TowerOwner _player;
        [SerializeField] private TowerOwner _enemy;

        private GameManager _gameManager;
        
        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        private void Start()
        {
            _mainUiController.StartGameEvent += StartGame;

            _player.DeadEvent += EndGame;
            _player.GetDamagedEvent += GetDamaged;
            _enemy.DeadEvent += EndGame;
            _enemy.GetDamagedEvent += GetDamaged;
        }

        private void GetDamaged(int currentHp, bool isPlayer)
        {
            _mainUiController.SetHealth(currentHp, isPlayer);
        }

        private void StartGame()
        {
            _gameManager.StartGame();
        }

        private void EndGame()
        {
            _gameManager.EndGame();
        }
    }
}