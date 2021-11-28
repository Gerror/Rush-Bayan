using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MobHpLabel : MonoBehaviour
    {
        [SerializeField] private Text _hpLabel;

        public void SetHp(int hp)
        {
            if (hp >= 1000)
                _hpLabel.text = Math.Round(((float) hp) / 1000f, 1).ToString() + "k";
            else
                _hpLabel.text = hp.ToString();
        }
    }
}