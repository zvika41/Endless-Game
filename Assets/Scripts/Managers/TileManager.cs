using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class TileManager : MonoBehaviour
    {
        #region --- Serialize Fields ---

        [SerializeField] private GameObject[] tilePrefabs;

        //[SerializeField] private List<PoolObject> list;

        #endregion Serialize Fields


        #region --- Members ---

        //private Dictionary<string, Queue<GameObject>> _poolDictionary;
        
        private Transform _playerTransform;
        private Transform _transform;
        private List<GameObject> _activeTiles;
        private float _spawnPosition;
        private float _tileLength;
        private int _tileNumber;
        private bool _isGameStarted;

        #endregion Members


        #region --- Mono Methods ---

        private void Start()
        {
            RegisterToCallbacks();
            
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
            if(!_isGameStarted || _playerTransform == null || (!(_playerTransform.position.z - 35 > _spawnPosition - _tileNumber * _tileLength))) return;
        
            SpawnTile(Random.Range(1, tilePrefabs.Length));
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
            GameObject go = Instantiate(tilePrefabs[tileIndex], _transform.forward * _spawnPosition, _transform.rotation);
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
            Destroy(_activeTiles[0]);
            _activeTiles.RemoveAt(0);
        }

        #endregion Private Methods

    
        #region --- Event Handler ---

        private void RegisterToCallbacks()
        {
            Client.Instance.GameStarted += OnGameStarted;
            Client.Instance.RestartGame += OnRestartGame;
        }
    
        private void OnGameStarted()
        {
            //UnRegisterFromCallbacks();
            _isGameStarted = true;
            _playerTransform = Client.Instance.PlayerController.PlayerTransform;
            _activeTiles = new List<GameObject>();
            _tileLength = 30;
            _tileNumber = 5;

            for (int i = 0; i < tilePrefabs.Length; i++)
            {
                if (i == 0)
                {
                    SpawnTile(0);
                }
                else
                {
                    SpawnTile(Random.Range(1, tilePrefabs.Length));
                }
            }
        }
        
        private void OnRestartGame()
        {
            foreach (GameObject activeTile in _activeTiles)
            {
                
                Destroy(activeTile);
            }
            
            _activeTiles = null;
            _isGameStarted = false;
            _playerTransform = null;
            _tileLength = 0;
            _tileNumber = 0;
            _spawnPosition = 0;
        }
    
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.GameStarted -= OnGameStarted;
        }

        #endregion Event Handler
        
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