using Game.Core;
using Game.Core.Sounds;
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
        [SerializeField] private AudioClip _backgroundAudio;
        
        private GameManager _gameManager;
        private SoundManager _soundManager;
        
        [Inject]
        private void Construct(GameManager gameManager, SoundManager soundManager)
        {
            _gameManager = gameManager;
            _soundManager = soundManager;
        }
        
        private void Start()
        {
            _mainUiController.StartGameEvent += StartGame;

            _player.DeadEvent += EndGame;
            _player.GetDamagedEvent += GetDamaged;
            _enemy.DeadEvent += EndGame;
            _enemy.GetDamagedEvent += GetDamaged;
            
            _soundManager.CreateSoundObject().Play(_backgroundAudio, transform.position, true, 0.1f); 
        }

        private void GetDamaged(int currentHp, bool isPlayer)
        {
            _mainUiController.SetHealth(currentHp, isPlayer);
        }

        private void StartGame()
        {
            _gameManager.StartGame();
        }

        private void EndGame(bool deadPlayer)
        {
            _mainUiController.SetEndGameText(deadPlayer);
            _gameManager.EndGame();
        }
    }
}