using System;
using Models;
using UnityEngine;
using Views;

namespace Controllers
{
    public class PlayerController
    {
        #region --- Events ---
        public event Action CoinCollected;

        #endregion Events
        
        
        #region --- Members ---

        private PlayerModel _model;
        private PlayerView _playerView;

        #endregion Members
        
        
        #region --- Properties ---

        public Transform PlayerTransform { get; set; }

        #endregion Properties
    

        #region --- Constructor/Destructor ---

        public PlayerController()
        {
            _model = new PlayerModel(OnDataSetDone);
            RegisterToCallbacks();
        }

        ~PlayerController()
        {
            UnRegisterFromCallbacks();
            _model = null;
        }

        #endregion Constructor/Destructor


        #region --- Private Methods ---

        private void SetupView(PlayerModel model)
        {
            _playerView = Client.Instance.GetLoadedBundle(_model.PrefabName).GetComponent<PlayerView>();
            _playerView.SetupView(model.Speed, model.LandDistance, model.JumpForce, model.CurrentLane, model.Gravity, model.MaxSpeed, OnCoinCollected, OnGameOver);
        }

        #endregion Private Method
        

        #region --- Event Handler ---

        private void RegisterToCallbacks()
        {
            Client.Instance.GameStarted += OnGameStarted;
        }
    
        private void OnGameStarted()
        {
            _model.InitData();
        }
        
        private void OnDataSetDone()
        {
            SetupView(_model);
        }
        
        private void OnCoinCollected()
        {
            CoinCollected?.Invoke();
        }
        
        private void OnGameOver()
        {
            Client.Instance.BroadcastGameEndedEvent();
            _playerView.Destroy();
        }
        
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.GameStarted -= OnGameStarted;
        }

        #endregion Event Handler
    }
}
