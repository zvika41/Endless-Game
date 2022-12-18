using System;
using Models;
using Singleton;
using UnityEngine;
using Views;

namespace Controllers
{
    public class PlayerController
    {
        #region --- Events ---
        public event Action PlayerLoadCompleted;
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
            Client.Instance.GameSettingsDone += OnGameSettingsDone;
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
            _playerView.SetupView(model.Speed, model.LandDistance, model.JumpForce, model.CurrentLane, model.Gravity, model.MaxSpeed, OnPlayerLoadCompleted, OnCoinCollected, OnGameOver);
        }

        #endregion Private Method
        

        #region --- Event Handler ---

        private void RegisterToCallbacks()
        {
            Client.Instance.GameLoadStart += OnGameLoadStart;
            Client.Instance.GameStarted += OnGameStarted;
        }
        
        private void OnGameSettingsDone()
        {
            Client.Instance.GameSettingsDone -= OnGameSettingsDone;
            RegisterToCallbacks();
           _model = new PlayerModel(OnDataSetDone);
        }

        private void OnGameLoadStart()
        {
            _model.InitData();
        }
    
        private void OnDataSetDone()
        {
            SetupView(_model);
            _playerView.HandlePlayerState(false);
        }

        private void OnPlayerLoadCompleted()
        {
            PlayerLoadCompleted?.Invoke();
        }
        
        private void OnGameStarted()
        {
            _playerView.HandlePlayerState(true);
        }
        
        private void OnCoinCollected()
        {
            CoinCollected?.Invoke();
        }
        
        private void OnGameOver()
        {
            _playerView.Destroy();
            Client.Instance.BroadcastGameEndedEvent();
        }
        
        private void UnRegisterFromCallbacks()
        {
            Client.Instance.GameLoadStart -= OnGameLoadStart;
            Client.Instance.GameStarted -= OnGameStarted;
        }

        #endregion Event Handler
    }
}
