using System;
using UnityEngine;

namespace Game.UI
{
    public class AboutMeWindow : MonoBehaviour
    {
        public event Action ExitEvent;

        public void Exit()
        {
            ExitEvent?.Invoke();
        }
    }
}