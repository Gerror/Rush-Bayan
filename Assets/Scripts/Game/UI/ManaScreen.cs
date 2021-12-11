using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ManaScreen : MonoBehaviour
    {
        [SerializeField] private Text _manaLabel;
        
        public void SetManaValue(int value)
        {
            _manaLabel.text = value.ToString();
        }
    }
}
