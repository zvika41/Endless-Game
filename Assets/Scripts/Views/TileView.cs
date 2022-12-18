using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Views
{
    public class TileView : MonoBehaviour
    {
        #region --- Serialize Fields ---

        [SerializeField] private List<GameObject> tilePrefabs;

        #endregion Serialize Fields


        #region --- Members ---
        
        private Action _onSetupViewCompleted;
        private Transform _playerTransform;
        private Transform _transform;
        private List<GameObject> _activeTiles;
        private float _spawnPosition;
        private float _tileLength;
        private int _numberOfTiles;

        #endregion Members


        #region --- Mono Methods ---

        private void Update()
        {
            if(!Client.Instance.IsGameStarted || _playerTransform == null || (!(_playerTransform.position.z - 35 > _spawnPosition - _numberOfTiles * _tileLength))) return;
            
            SpawnTile(Random.Range(0, tilePrefabs.Count));
            DeleteTile();
        }

        #endregion Mono Methods


        #region --- Public Methods ---

        public void SetupView(float tileLength, int numberOfTiles, Action onSetupViewCompleted)
        {
            _tileLength = tileLength;
            _numberOfTiles = numberOfTiles;
            _onSetupViewCompleted = onSetupViewCompleted;
            _activeTiles = new List<GameObject>();
            _playerTransform = Client.Instance.PlayerController.PlayerTransform;

            OnSetupViewCompleted();
        }

        public void SpawnTiles()
        {
            for (int i = 0; i < tilePrefabs.Count; i++)
            {
                SpawnTile(i);
            }
        }
        
        public void Destroy()
        {
            foreach (GameObject tile in _activeTiles)
            {
                Destroy(tile.GameObject());
            }
            
            tilePrefabs.Clear();
            _activeTiles.Clear();
            _activeTiles = null;
            _playerTransform = null;

            Destroy(gameObject);
        }

        #endregion Public Methods
        
        
        #region --- Private Methods ---
        
        private void SpawnTile(int tileIndex)
        {
            _transform = transform;
            GameObject go = Instantiate(tilePrefabs[tileIndex], _transform.forward * _spawnPosition, _transform.rotation);
            _activeTiles.Add(go);
            _spawnPosition += _tileLength;
        }

        private void DeleteTile()
        {
            Destroy(_activeTiles[0]);
            _activeTiles.RemoveAt(0);
        }

        #endregion Private Methods

        
        #region --- Event Handler ---

        private void OnSetupViewCompleted()
        {
            _onSetupViewCompleted?.Invoke();
        }

        #endregion Event Handler
    }
}