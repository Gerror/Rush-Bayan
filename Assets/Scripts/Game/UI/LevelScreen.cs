using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class LevelScreen : MonoBehaviour
    {
        [SerializeField] private Text _levelText;
        [SerializeField] private Image _image;

        public void SetLevel(int level)
        {
            _levelText.text = "L. " + level.ToString();
        }

        public void SetImageSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}