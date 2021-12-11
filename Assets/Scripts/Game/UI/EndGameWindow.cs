using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private Text _endGameText;
        
        public event Action BackToMainMenuEvent;
        public event Action RestartEvent;

        public void SetEndGameText(string text)
        {
            _endGameText.text = text;
        }
        
        public void BackToMainMenu()
        {
            BackToMainMenuEvent?.Invoke();
        }
        
        public void Restart()
        {
            RestartEvent?.Invoke();
        }
    }
}