using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class LevelUpButton : MonoBehaviour
    {
        public event Action<int> LevelUpEvent;
        public int ButtonId;
        
        public void LevelUp()
        {
            LevelUpEvent?.Invoke(ButtonId);
        }
    }
}