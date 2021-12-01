using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    [CreateAssetMenu]
    public class TowerConfig : ScriptableObject
    {
        public GameObject Prefab;
        public Sprite Sprite;
        [SerializeField] List<GameObject> _mergeViews;

        public GameObject GetMergeView(int mergeLevel)
        {
            return _mergeViews[mergeLevel];
        }
    }
}