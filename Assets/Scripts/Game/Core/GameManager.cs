using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
        public event Action StartGameEvent;
        public event Action EndGameEvent;

        public void StartGame()
        {
            StartGameEvent?.Invoke();
        }

        public void EndGame()
        {
            EndGameEvent?.Invoke();
        }
    }
}