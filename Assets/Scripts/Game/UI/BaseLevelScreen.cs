using System;
using Game.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class BaseLevelScreen : MonoBehaviour
    {
        [SerializeField] private Text _levelText;
        [SerializeField] private Text _manaCostText;
        [SerializeField] private Image _image;
        

        public void SetLevel(int level)
        {
            if (level != GameSettings.MaxLevels[(int) GameSettings.LevelType.BaseLevel] - 1)
                _levelText.text = "L. " + (level + 1).ToString();
            else
                _levelText.text = "L. MAX";
        }

        public void SetManaCost(string manaCostStr)
        {
            if (_manaCostText)
                _manaCostText.text = manaCostStr;
        }

        public void SetImageSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}