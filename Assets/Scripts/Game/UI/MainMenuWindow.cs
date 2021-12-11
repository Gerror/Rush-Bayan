using System;
using UnityEngine;

namespace Game.UI
{
    public class MainMenuWindow : MonoBehaviour
    {
        public event Action ExitGameEvent;
        public event Action AboutMeEvent;
        public event Action StartGameEvent;

        public void StartGame()
        {
            StartGameEvent?.Invoke();
        }

        public void AboutMe()
        {
            AboutMeEvent?.Invoke();
        }

        public void ExitGame()
        {
            ExitGameEvent?.Invoke();
        }
    }
}