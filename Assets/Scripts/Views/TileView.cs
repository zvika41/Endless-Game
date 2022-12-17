using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Views
{
    public class TileView : MonoBehaviour
    {
        #region --- Serialize Fields ---

        [SerializeField] private List<Object> tilePrefabs;

        //[SerializeField] private List<PoolObject> list;

        #endregion Serialize Fields


        #region --- Members ---
        
        //private Dictionary<string, Queue<GameObject>> _poolDictionary;

        private Action _coinCollected;
        private Transform _playerTransform;
        private Transform _transform;
        private List<Object> _activeTiles;
        private float _spawnPosition;
        private float _tileLength;
        private int _numberOfTiles;
        private bool _isGameStarted;

        #endregion Members


        #region --- Mono Methods ---

        private void Start()
        {
            // _poolDictionary = new Dictionary<string,  Queue<GameObject>>();
            //
            // foreach (PoolObject poolObject in list)
            // {
            //     Queue<GameObject> objectPool = new Queue<GameObject>();
            //
            //     for (int i = 0; i < poolObject.size; i++)
            //     {
            //         GameObject obj = Instantiate(poolObject.prefab);
            //         obj.SetActive(false);
            //         objectPool.Enqueue(obj);
            //     }
            //
            //     _poolDictionary.Add(poolObject.tag, objectPool);
            // }

        }

        private void Update()
        {
            if(!_isGameStarted || _playerTransform == null || (!(_playerTransform.position.z - 35 > _spawnPosition - _numberOfTiles * _tileLength))) return;

            SpawnTile(Random.Range(1, tilePrefabs.Count));
            DeleteTile();
        }

        #endregion Mono Methods


        #region --- Private Methods ---
        
        private void SpawnTile(int tileIndex)
        {
            // if(!_poolDictionary.ContainsKey(objectTag)) return;
            //
            // GameObject objectToSpawn = _poolDictionary[objectTag].Dequeue();
            //
            // objectToSpawn.SetActive(true);
            // _transform  = transform;
            // objectToSpawn.transform.position = _transform.forward * 35;
            // objectToSpawn.transform.rotation = _transform.rotation;
            //
            // _poolDictionary[objectTag].Enqueue(objectToSpawn);
            
            _transform = transform;
            Object go = Instantiate(tilePrefabs[tileIndex], _transform.forward * _spawnPosition, _transform.rotation);
            _activeTiles.Add(go);
            _spawnPosition += _tileLength;
        }

        // private IEnumerator SpawnTarget()
        // {
        //     while (Client.Instance.GameController.IsGameStarted && _playerTransform.position.z - 35 > _spawnPosition - _tileNumber * _tileLength)
        //     {
        //         yield return new WaitForSeconds(0.3f);
        //         string objectTag = _activeTiles[Random.Range(1, _activeTiles.Count)];
        //         SpawnTile(objectTag);
        //     }
        // }
    
        private void DeleteTile()
        {
            Destroy(_activeTiles[0].GameObject());
            _activeTiles.RemoveAt(0);
        }

        #endregion Private Methods


        #region --- Public Methods ---

        public void SetupView(float tileLength, int numberOfTiles)
        {
            _playerTransform = Client.Instance.PlayerController.PlayerTransform;
            _activeTiles = new List<Object>();
            _tileLength = tileLength;
            _numberOfTiles = numberOfTiles;

            for (int i = 0; i < tilePrefabs.Count; i++)
            {
                SpawnTile(i);
            }
            
            _isGameStarted = true;
        }
        
        public void RestartGame()
        {
            foreach (Object tile in _activeTiles)
            {
                Destroy(tile.GameObject());
            }
            
            tilePrefabs.Clear();
            _activeTiles.Clear();
            _activeTiles = null;
            _isGameStarted = false;
            _playerTransform = null;

            Destroy(gameObject);
        }

        #endregion Public Methods
        
        
        
        //
        // #region --- Internal Classes ---
        //
        // [System.Serializable]
        // public class PoolObject
        // {
        //     public string tag;
        //     public GameObject prefab;
        //     public int size;
        // }
        //
        // #endregion Internal Classes
    }
}