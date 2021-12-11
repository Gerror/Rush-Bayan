using Game.Core;
using Game.Mechanics;
using Game.Mechanics.Tower;
using UnityEngine;
using Zenject;

namespace Game.Controllers
{
    public class TowerMergeController : MonoBehaviour
    {
        [SerializeField] private LayerMask _towerLayerMask;
        [SerializeField] private GameObject _folowingByFingerPrefab;
        
        private Camera _mainCamera;
        private Tower _target;
        private GameObject _folowingByFingerObj;
        private PrefabFactory _prefabFactory;
        private Transform _tempObjectParent;

        [Inject]
        private void Construct(PrefabFactory prefabFactory, Transform tempObjectParent)
        {
            _prefabFactory = prefabFactory;
            _tempObjectParent = tempObjectParent;
        }

        void Start()
        {
            _mainCamera = Camera.main;
        }
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Tower playerTower = GetPlayerTower();
                if (playerTower != null)
                {
                    SpriteRenderer towerSpriteRenderer = playerTower.gameObject.GetComponent<SpriteRenderer>();

                    _folowingByFingerObj = _prefabFactory.Spawn(_folowingByFingerPrefab, _tempObjectParent);
                    _folowingByFingerObj.GetComponent<SpriteRenderer>().sprite = towerSpriteRenderer.sprite;

                    _target = playerTower;
                }
            }

            if (Input.GetMouseButtonUp(0) && _folowingByFingerObj != null)
            {
                Tower playerTower = GetPlayerTower();
                if (playerTower != null)
                {
                    if (playerTower.TowerIndex == _target.TowerIndex && playerTower != _target)
                    {
                        playerTower.Merge(_target);
                    }
                }

                _target = null;
                Destroy(_folowingByFingerObj);
            }
        }

        private Tower GetPlayerTower()
        {
            Vector3 clickPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            clickPoint.z = 0f;

            var col = Physics2D.OverlapPoint(clickPoint, _towerLayerMask);
            
            if (col)
            {
                Tower tower = col.GetComponent<Tower>();
                if (tower.TowerOwner.IsPlayer)
                {
                    return tower;
                }
            }

            return null;
        }
    }
}