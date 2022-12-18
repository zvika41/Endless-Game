using System;
using Models;
using Singleton;
using Views;

namespace Controllers
{
    public class TileController
    {
        #region --- Events ---
        public event Action TileLoadCompleted;

        #endregion Events
        
        
        #region --- Members ---

        private TileModel _model;
        private TileView _tileView;

        #endregion Members
        
        
        #region --- Constructor/Destructor ---

        public TileController()
        {
            _model = new TileModel(OnDataSetDone);
            RegisterToCallbacks();
        }

        ~TileController()
        {
            _model = null;
            UnRegisterFromCallbacks();
        }

        #endregion Constructor/Destructor
        
        
        #region --- Private Methods ---

        private void SetupView(TileModel model)
        {
            _tileView = Client.Instance.GetLoadedBundle(model.PrefabName).GetComponent<TileView>();
            _tileView.SetupView(_model.TileLength, _model.NumberOfTiles, OnTileLoadCompleted);
        }

        #endregion Private Methods
        
        
        #region --- Event Handler ---
        
        private void RegisterToCallbacks()
        {
            Client.Instance.PlayerController.PlayerLoadCompleted += OnPlayerLoadCompleted;
            Client.Instance.GameStarted += OnGameStarted;
            Client.Instance.RestartGame += OnRestartGame;
        }
        
        private void OnPlayerLoadCompleted()
        {
            _model.InitData();
        }

        private void OnTileLoadCompleted()
        {
            TileLoadCompleted?.Invoke();
        }
        
        private void OnGameStarted()
        {
            _tileView.SpawnTiles();
        }
        
        private void OnDataSetDone()
        {
            SetupView(_model);
        }

        private void OnRestartGame()
        {
            _tileView.Destroy();
        }

        private void UnRegisterFromCallbacks()
        {
            Client.Instance.PlayerController.PlayerLoadCompleted -= OnPlayerLoadCompleted;
            Client.Instance.GameStarted -= OnGameStarted;
            Client.Instance.RestartGame -= OnRestartGame;
        }
        
        #endregion Event Handler
    }
}
