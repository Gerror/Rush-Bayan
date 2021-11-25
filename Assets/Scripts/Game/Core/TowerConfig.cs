using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    [CreateAssetMenu]
    public class TowerConfig : ScriptableObject
    {
        public GameObject Prefab;
        public Sprite Sprite;
    }
}