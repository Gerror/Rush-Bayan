using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class BaseLevelUpButton : MonoBehaviour
    {
        private Button _button;
        public event Action<int> LevelUpEvent;
        public int ButtonId;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(LevelUp);
        }

        private void LevelUp()
        {
            LevelUpEvent?.Invoke(ButtonId);
        }
    }
}