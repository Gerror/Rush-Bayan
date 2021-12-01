using UnityEngine;

namespace Game.Mechanics
{
    public class FolowingByFinger : MonoBehaviour
    {
        private Camera _mainCamera;
        
        void Start()
        {
            _mainCamera = Camera.main;
        }
        
        void Update()
        {
            Vector3 position = _mainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            position.z = 0;

            transform.position = position;
        }
    }
}