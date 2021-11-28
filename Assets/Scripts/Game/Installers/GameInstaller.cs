using Zenject;
using Game.Core;
using Game.Core.Sounds;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public SoundManager SoundManager;
        public GameSettings GameSettings;
        public GameManager GameManager;

        public override void InstallBindings()
        {
            Container.Bind<PrefabFactory>().AsSingle();
            
            Container.Bind<SoundManager>().FromInstance(SoundManager).AsSingle();
            Container.Bind<GameSettings>().FromInstance(GameSettings).AsSingle();
            Container.Bind<GameManager>().FromInstance(GameManager).AsSingle();
        }
    }
}